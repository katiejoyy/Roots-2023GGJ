using UnityEngine;

public class CrowConversationController : MonoBehaviour, ConversationController
{

	Conversation _preCollectConversation;
	public TextAsset preCollectConversationText;

    void Start ()
	{
        _preCollectConversation = CreateConversation(preCollectConversationText);
		_preCollectConversation.onConversationComplete += CompletedConversation;
	}

	private static Conversation CreateConversation(TextAsset conversationText)
	{
		Conversation conversation = new Conversation
		{
			dialogueManager = FindObjectOfType<DialogueManager>().gameObject
		};
		conversation.LoadConversation(conversationText);

		return conversation;
	}

	public void BeginConversation()
	{
		_preCollectConversation.beginConversation();
	}

	public void CompletedConversation() 
	{
	}
}
