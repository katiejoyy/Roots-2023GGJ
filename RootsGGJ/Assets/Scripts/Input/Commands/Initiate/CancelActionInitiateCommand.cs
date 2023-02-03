using System;
using UnityEngine;

public class CancelActionInitiateCommand : Command
{
	public override void Execute(Actor actor)
	{
		actor.OnCancel();
	}

	public override Texture GetImage()
	{
		if (Representation == null)
		{
			Representation = Resources.Load<Texture>("Sprites/Commands/CancelButton");
		}
		return Representation;
	}
}