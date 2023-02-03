using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* I'm not sure if this is the best way to do this? But seemed like an obvious
 * way to prototype.
 * 
 * The aim of this class is the manage the traversing of a dialogue tree. The aim isn't
 * to manage the input or outcomes of that dialogue. Just create the next dialogue in the tree,
 * receive a response from that dialogue with the next dialogue node until the end.
 * 
 */
public class DialogueManager : MonoBehaviour
{
    public GameObject inputManager;

    public const string END_NODE = "END";

    //We could do something nice like parse the dialogue into a tree structure and adjust the nextNodes accordingly
    //but for now let's just process it when it runs
    List<DialogueNode> currentDialogue;
    DialogueNode currentDialogueNode;
    DialogueNode previousDialogueNode;

    //This is a little bit of a cheat way too. We just keep the different option layouts
    //in prefabs and instantiate the thing we need. We could write something which automatically
    //resizes everything but it's a bit much right now.
    public GameObject dialogueUIPrefab;
    public GameObject twoOptDialogueUIPrefab;
    public GameObject threeOptDialogueUIPrefab;
    public GameObject dialogueUI;

    //We keep a reference to the game object which is speaking in the conversation
    //so we can feedback if we need to.
	public Conversation currentActor;

    private List<DialogueNode> routeTaken;

	void Awake ()
    {
        //We need a current dialogue tree, current node, call with the next node
        routeTaken = new List<DialogueNode>();
	}
	
	void Update ()
    {
		
	}

    public void moveToNextNode(string nextNodeName)
    {
        //this is a short cut for official end
        if(nextNodeName.Equals(END_NODE))
        {
            endDialogue();
            return;
        }

        foreach(DialogueNode node in currentDialogue)
        {
            if(node.NodeName.Equals(nextNodeName))
            {
                previousDialogueNode = currentDialogueNode;
                currentDialogueNode = node;
                routeTaken.Add(currentDialogueNode);
                showNodeInUI(currentDialogueNode);
                return;
            }
        }

        Debug.LogError("Could not find next node: " + nextNodeName + ". Ending dialogue early.");
        endDialogue();
    }

	public void beginDialogue(List<DialogueNode> conversation, Conversation actor)
    {
        if(conversation.Count==0)
        {
            Debug.LogError("Oh no! Your conversation contains no dialogue!");
            return;
        }

        currentActor = actor;
        routeTaken.Clear();

        currentDialogue = conversation;
        currentDialogueNode = currentDialogue[0];
        routeTaken.Add(currentDialogueNode);
        previousDialogueNode = currentDialogueNode;

        createDialogueUI();
        showNodeInUI(currentDialogueNode);
    }

    private void createDialogueUI()
	{
		dialogueUI.SetActive(true);
        dialogueUI.GetComponent<Dialogue>().setDialogueManager(this);
        inputManager.GetComponent<InputManager>().TakeInputFocus(dialogueUI.GetComponent<Dialogue>());
    }

    private void showNodeInUI(DialogueNode node)
    {
        if(currentDialogueNode.OptionDialogueText.Count!=previousDialogueNode.OptionDialogueText.Count)
        {
            inputManager.GetComponent<InputManager>().RelinquishInputFocus(dialogueUI.GetComponent<Dialogue>());
            GameObject.Destroy(dialogueUI);
            createDialogueUI();
        }

        if (dialogueUI != null)
        {
            dialogueUI.GetComponent<Dialogue>().setNode(node);
        }
    }

    private void endDialogue()
    {
        currentDialogueNode.Speaker = "";
        currentDialogueNode.NodeName = "";
        currentDialogueNode.DialogueText = "";
        currentDialogueNode.NextNodes.Clear();
        currentDialogueNode.OptionDialogueText.Clear();

        previousDialogueNode = currentDialogueNode;

        currentDialogue = null;

        //Then hide the dialogueUI? Delete it?
        inputManager.GetComponent<InputManager>().RelinquishInputFocus(dialogueUI.GetComponent<Dialogue>());
		dialogueUI.SetActive(false);

		currentActor.receiveConversationPath(routeTaken);
        //Can pass back the route taken to the actor here if we want. Then we can drive
        //Story, events, stats from dialogue!
    }
}
