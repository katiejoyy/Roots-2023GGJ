using System;
using UnityEngine;

public class DebugActionInitiateCommand : Command
{
	public override void Execute(Actor actor)
	{
		actor.OnDebugInput();
	}

	public override Texture GetImage()
	{
		if (Representation == null)
		{
			Representation = Resources.Load<Texture>("Sprites/Commands/DebugButton");
		}
		return Representation;
	}
}