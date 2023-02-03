using System;
using System.Collections.Generic;

[Serializable]
class DialogueNodeCollection
{
    public DialogueNode[] nodes;
    public DialogueNodeCollection()
    {

    }

    public List<DialogueNode> getNodes()
    {
        return new List<DialogueNode>(nodes);
    }
    
    public override String ToString()
    {
        string objectString = "";

        foreach(DialogueNode node in nodes)
        {
            objectString = objectString + node.ToString();
        }
        return objectString;
    }
}