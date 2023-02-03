using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalVerticalActionStopCommand : Command
{
	private int axis;

	public HorizontalVerticalActionStopCommand(int axis = 0)
	{
		this.axis = axis;
	}

	public override void Execute(Actor actor)
	{
		if (axis == HorizontalVerticalActionInitiateCommand.PRIMARY_AXIS)
		{
			actor.OnFullPrimaryAxisInputStop();
		}
		else if (axis == HorizontalVerticalActionInitiateCommand.SECONDARY_AXIS)
		{
			actor.OnFullSecondaryAxisInputStop();
		}
	}
}
