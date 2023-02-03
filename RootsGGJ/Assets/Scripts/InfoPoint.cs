using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPoint : MonoBehaviour
{
	Conversation conversation;
	public TextAsset conversation_text;

    void Start ()
	{
        conversation = new Conversation
		{
			dialogueManager = FindObjectOfType<DialogueManager>().gameObject
		};
		conversation.LoadConversation(conversation_text);
	}

	public void BeginConversation()
	{
		conversation.beginConversation();
	}
}
