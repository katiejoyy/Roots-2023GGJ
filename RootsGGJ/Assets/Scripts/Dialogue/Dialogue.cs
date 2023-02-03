using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : Actor
{
    DialogueNode currentNode;
    public DialogueManager dialogueManager;
    int currentSelectedOption;

    void Update()
    {
        //We could add animation for the dialogue text in here.
    }

    public void setDialogueManager(DialogueManager manager)
    {
        dialogueManager = manager;
    }

    public void setNode(DialogueNode node)
    {
        currentNode = node;
        currentSelectedOption = 0;
        setNameText(node.Speaker);
        setDialogText(node.DialogueText);
        if(node.NextNodes.Count>1)
        {
            setOptions(node.OptionDialogueText);
        }
    }

    private void setDialogText(string dialogueText)
    {
        Transform dialogue = transform.Find("DialogueText");
        dialogue.GetComponent<Text>().text = dialogueText;
		setTextColour(dialogue.GetComponent<Text>());
    }

    private void setNameText(string speaker)
    {
        bool hasSpeaker = !string.IsNullOrWhiteSpace(speaker);
        transform.Find("NamePanel").gameObject.SetActive(hasSpeaker);

        if(!hasSpeaker)
        {
            return;
        }

        Transform nameText = transform.Find("NamePanel/Name");
        nameText.GetComponent<Text>().text = speaker;
		setTextColour(nameText.GetComponent<Text>());
    }

    private void setOptions(List<string> optionDialogueText)
    {
        //At the moment, we're assuming the dialogue UI has beenc created with the
        //right number of option placeholders for us to change
        for(int i=0; i<optionDialogueText.Count; ++i)
        {
            Transform optionText = transform.Find("DialoguePanel/OptionText_" + (i + 1));
            optionText.GetComponent<Text>().text = optionDialogueText[i];
        }

        changeSelectedOption(0);
    }

    public void changeSelectedOption(int optionIndex)
    {
        if(optionIndex!=currentSelectedOption)
        {
            Transform prevOptionText = transform.Find("DialoguePanel/OptionText_" + (currentSelectedOption+ 1));
            prevOptionText.GetComponent<Text>().color = Color.black;
        }

        currentSelectedOption = optionIndex;
        //Highlight text
        Transform optionText = transform.Find("DialoguePanel/OptionText_" + (optionIndex + 1));
        optionText.GetComponent<Text>().color = Color.white;
    }

    public void selectOption()
    {
        dialogueManager.moveToNextNode(currentNode.NextNodes[currentSelectedOption]);
    }

    public override void OnVerticalInput(float verticalAxisAmount)
    {
        if(verticalAxisAmount<0)
        {
            changeSelectedOption((currentSelectedOption + 1) % currentNode.OptionDialogueText.Count);
        }
        else
        {
            int nextOption = currentSelectedOption - 1;
            if(nextOption<0)
            {
                nextOption = currentNode.OptionDialogueText.Count - 1;
            }
            changeSelectedOption(nextOption);
        }
    }

    public override void OnConfirm()
    {
        selectOption();
    }


	private void setTextColour(Text text)
	{
		if(currentNode.NodeTags.Contains("blue"))
		{
			text.color = new Color(0.282f, 0.443f, 0.886f);
			return;
		}
		else if(currentNode.NodeTags.Contains("green"))
		{
			text.color = new Color(0.164f, 0.356f, 0.188f);
			return;
		}
		else if(currentNode.NodeTags.Contains("pink"))
		{
			text.color = new Color(0.937f, 0.384f, 0.968f);
			return;
		}
		else if(currentNode.NodeTags.Contains("yellow"))
		{
			text.color = new Color(0.619f, 0.619f, 0f);
			return;
		}
			
		text.color = Color.black;
	}
}