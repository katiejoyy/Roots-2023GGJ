using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalVerticalActionHoldCommand : Command
{
	private float horizontalAxisInput;
	private float verticalAxisInput;
	private int axis;

	public HorizontalVerticalActionHoldCommand(float horizontalAxis, float verticalAxis, int axis = 0)
	{
		horizontalAxisInput = horizontalAxis;
		verticalAxisInput = verticalAxis;
		this.axis = axis;
	}

	public float GetHorizontalAxis()
	{
		return horizontalAxisInput;
	}

	public float GetVerticalAxis()
	{
		return verticalAxisInput;
	}

	public override void Execute(Actor actor)
	{
		if (axis == HorizontalVerticalActionInitiateCommand.PRIMARY_AXIS)
		{
			actor.OnFullPrimaryAxisInputHeld(horizontalAxisInput, verticalAxisInput);
		}
		else if (axis == HorizontalVerticalActionInitiateCommand.SECONDARY_AXIS)
		{
			actor.OnFullSecondaryAxisInputHeld(horizontalAxisInput, verticalAxisInput);
		}
	}
}
