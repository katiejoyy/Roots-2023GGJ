using System;
using UnityEngine;

public class MenuActionInitiateCommand : Command
{
	public override void Execute(Actor actor)
	{
		actor.OnMenu();
	}

	public override Texture GetImage()
	{
		if (Representation == null)
		{
			Representation = Resources.Load("Sprites/Commands/MenuButton") as Texture;
		}
		return Representation;
	}
}