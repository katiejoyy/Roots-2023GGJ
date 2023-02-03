using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmActionHoldCommand : Command
{
	public override void Execute(Actor actor)
	{
		actor.OnConfirmHeld();
	}
}
