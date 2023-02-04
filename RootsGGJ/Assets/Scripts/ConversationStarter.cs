using UnityEngine;

public class ConversationStarter : Interactable
{    
    public override void Interact()
    {
        GetComponent<ConversationController>().BeginConversation();
    }

}
