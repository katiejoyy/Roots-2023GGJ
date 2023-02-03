using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugActionHoldCommand : Command
{
	public override void Execute(Actor actor)
	{
		actor.OnDebugInputHeld();
	}
}
