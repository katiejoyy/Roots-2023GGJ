using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
	private Texture representation;

	public Texture Representation
	{
		get { return representation; }
		set { representation = value; }
	}

	public abstract void Execute(Actor actor);
	public virtual Texture GetImage() { return null; }
	/*Can also have undo here*/
}
