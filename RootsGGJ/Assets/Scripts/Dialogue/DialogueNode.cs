using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueNode
{
    //The name of the speaker in the dialogue
    public string speaker;
    //The name of the dialog node
    public string nodeName;
    //The actual text to display
    public string dialogueText;
    //An ordered list of the adjacent nodes
    public List<string> nextNodes;
    //An ordered list of text to display to represent the next node
    //Note: the order must be the same as nextNodes. It can be empty of there is only one next Node
    public List<string> optionDialogueText;
    //Some extra metadate for the node
    public List<string> nodeTags;

    public DialogueNode()
    {
        Speaker = "";
        NodeName = "";
        DialogueText = "";
        NextNodes = new List<string>();
        OptionDialogueText = new List<string>();
        NodeTags = new List<string>();
    }

    public DialogueNode(string speaker,
                        string name, 
                        string text,
                        List<string> adjacentNodes, 
                        List<string> options, 
                        List<string> tags)
    {
        Speaker = speaker;
        NodeName = name;
        DialogueText = text;
        NextNodes = adjacentNodes;
        OptionDialogueText = options;
        NodeTags = tags;
    }

    public string Speaker
    {
        get
        {
            return speaker;
        }

        set
        {
            speaker = value;
        }
    }

    public string DialogueText
    {
        get
        {
            return dialogueText;
        }

        set
        {
            dialogueText = value;
        }
    }

    public List<string> NextNodes
    {
        get
        {
            return nextNodes;
        }

        set
        {
            nextNodes = value;
        }
    }

    public List<string> OptionDialogueText
    {
        get
        {
            return optionDialogueText;
        }

        set
        {
            optionDialogueText = value;
        }
    }

    public string NodeName
    {
        get
        {
            return nodeName;
        }

        set
        {
            nodeName = value;
        }
    }

    public List<string> NodeTags
    {
        get
        {
            return nodeTags;
        }

        set
        {
            nodeTags = value;
        }
    }
}
