using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation
{
    public GameObject dialogueManager;
    public TextAsset conversationFile;
    DialogueNodeCollection conversation;

	public delegate void ConversationCompleteEvent();
	public event ConversationCompleteEvent onConversationComplete;

	public void LoadConversation(TextAsset convo)
	{
		conversationFile =  convo;
		conversation = new DialogueNodeCollection();

		if (conversationFile!=null)
		{
			conversation = JsonUtility.FromJson<DialogueNodeCollection>(conversationFile.text);
			Debug.Log("The read in conversation: " + conversationFile.text);
			Debug.Log("Conversation object: " + conversation.ToString());
		}
	}

    public List<DialogueNode> getConversation()
    {
        return conversation.getNodes();
    }

    public void receiveConversationPath(List<DialogueNode> route)
    {
		onConversationComplete?.Invoke();
    }

	public void beginConversation()
	{
		dialogueManager.GetComponent<DialogueManager>().beginDialogue(getConversation(), this);
	}
}
