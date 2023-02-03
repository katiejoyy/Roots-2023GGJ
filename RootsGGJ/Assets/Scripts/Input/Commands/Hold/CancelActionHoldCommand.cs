using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelActionHoldCommand : Command
{
	public override void Execute(Actor actor)
	{
		actor.OnCancelHeld();
	}
}
