using UnityEngine;

public class FirewoodConversationController : MonoBehaviour, ConversationController
{
	public PlayerInventory _inventory;

	Conversation _preCollectConversation;
	Conversation _postCollectConversation;
	public TextAsset preCollectConversationText;
	public TextAsset postCollectConversationText;

    void Start ()
	{
        _preCollectConversation = CreateConversation(preCollectConversationText);
        _postCollectConversation = CreateConversation(postCollectConversationText);
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
		if(!_inventory.Has(ITEMS.FIREWOOD))
		{
			_preCollectConversation.beginConversation();
			return;
		}

		_postCollectConversation.beginConversation();
	}

	public void CompletedConversation() 
	{
		_inventory.Collect(ITEMS.FIREWOOD);
		Debug.Log("Collected the firewood!");
	}
}
