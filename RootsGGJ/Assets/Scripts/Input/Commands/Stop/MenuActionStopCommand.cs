using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActionStopCommand : Command
{
	public override void Execute(Actor actor)
	{
		actor.OnDebugInputStop();
	}
}
