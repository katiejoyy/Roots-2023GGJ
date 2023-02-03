using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalVerticalActionInitiateCommand : Command
{
	public const int PRIMARY_AXIS = 0;
	public const int SECONDARY_AXIS = 1;

	private float horizontalAxisInput;
	private float verticalAxisInput;
	private int axis;

	public HorizontalVerticalActionInitiateCommand(float horizontalAxis, float verticalAxis, int axis = 0)
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
		if (axis == PRIMARY_AXIS)
		{
			actor.OnFullPrimaryAxisInput(horizontalAxisInput, verticalAxisInput);
		}
		else if (axis == SECONDARY_AXIS)
		{
			actor.OnFullSecondaryAxisInput(horizontalAxisInput, verticalAxisInput);
		}
	}

	public override Texture GetImage()
	{
		if (Representation == null)
		{
			if (horizontalAxisInput > 0)
			{
				if (verticalAxisInput > 0)
				{
					Representation = Resources.Load<Texture>("Sprites/Commands/NEArrow");
				}
				else if (verticalAxisInput < 0)
				{
					Representation = Resources.Load<Texture>("Sprites/Commands/SEArrow");
				}
				else
				{
					Representation = Resources.Load<Texture>("Sprites/Commands/RightArrow");
				}
			}
			else if (horizontalAxisInput < 0)
			{
				if (verticalAxisInput > 0)
				{
					Representation = Resources.Load<Texture>("Sprites/Commands/NWArrow");
				}
				else if (verticalAxisInput < 0)
				{
					Representation = Resources.Load<Texture>("Sprites/Commands/SWArrow");
				}
				else
				{
					Representation = Resources.Load<Texture>("Sprites/Commands/LeftArrow");
				}
			}
			else
			{
				if (verticalAxisInput > 0)
				{
					Representation = Resources.Load<Texture>("Sprites/Commands/UpArrow");
				}
				else if (verticalAxisInput < 0)
				{
					Representation = Resources.Load<Texture>("Sprites/Commands/DownArrow");
				}
			}

		}
		return Representation;
	}
}
