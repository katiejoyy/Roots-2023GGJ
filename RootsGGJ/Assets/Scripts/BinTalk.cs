using UnityEngine;

public class BinTalk : Interactable
{    
    public override void Interact()
    {
        GetComponent<InfoPoint>().BeginConversation();
    }

}
