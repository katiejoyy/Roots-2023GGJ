using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmActionStopCommand : Command
{
	public override void Execute(Actor actor)
	{
		actor.OnConfirmStop();
	}
}
