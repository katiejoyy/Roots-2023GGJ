using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The Actor Interface is part of the Input Manager system. It
 * is implemented by anything that wishes to respond to user input.
 * The input manager will call these functions directly, if this
 * Actor has requested focus from the Input Manager.
 */
public abstract class Actor : MonoBehaviour
{
	public virtual void OnHorizontalInput(float horizontalAxisInput) { }
	public virtual void OnVerticalInput(float verticalAxisAmount) { }
	public virtual void OnSecondaryAxisHorizontalInput(float horizontalAxisInput) { }
	public virtual void OnSecondaryAxisVerticalInput(float verticalAxisAmount) { }

	public virtual void OnCancel() { }
	public virtual void OnMenu() { }
	public virtual void OnDebugInput() { }
	public virtual void OnConfirm() { }

	public virtual void OnCancelStop() { }
	public virtual void OnMenuStop() { }
	public virtual void OnDebugInputStop() { }
	public virtual void OnConfirmStop() { }

	public virtual void OnCancelHeld() { }
	public virtual void OnMenuHeld() { }
	public virtual void OnDebugInputHeld() { }
	public virtual void OnConfirmHeld() { }

	public virtual void OnFullPrimaryAxisInput(float horizontalAxisInput, float verticalAxisInput) { }
	public virtual void OnFullSecondaryAxisInput(float horizontalAxisInput, float verticalAxisInput) { }

	public virtual void OnFullPrimaryAxisInputStop() { }
	public virtual void OnFullSecondaryAxisInputStop() { }

	public virtual void OnFullPrimaryAxisInputHeld(float horizontalAxisInput, float verticalAxisInput) { }
	public virtual void OnFullSecondaryAxisInputHeld(float horizontalAxisInput, float verticalAxisInput) { }

	public virtual void OnCommandQueueUpdated(Queue<Command> commandQueue, int countThisFrame)
	{
		for (int i = commandQueue.Count - countThisFrame; i < commandQueue.Count; ++i)
		{
			Command command = commandQueue.ToArray()[i];
			if (command != null)
			{
				command.Execute(this);
			}
		}
	}
}