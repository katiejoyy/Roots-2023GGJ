using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActionHoldCommand : Command
{
	public override void Execute(Actor actor)
	{
		actor.OnMenuHeld();
	}
}
