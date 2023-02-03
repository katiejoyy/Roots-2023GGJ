using System;
using UnityEngine;

public class ConfirmActionInitiateCommand : Command
{
	public override void Execute(Actor actor)
	{
		actor.OnConfirm();
	}

	public override Texture GetImage()
	{
		if (Representation == null)
		{
			Representation = Resources.Load<Texture>("Sprites/Commands/ConfirmButton");
		}
		return Representation;
	}
}