using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelActionStopCommand : Command
{
	public override void Execute(Actor actor)
	{
		actor.OnCancelStop();
	}
}
