using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The aim of the input manager is to abstract away the necessity for having to
 * put input polling into every class that needs to respond to user input and then
 * orchestrate between them.
 * 
 * Concerns:
 *  - We may want to be able to map different user input to different actions to make it easier to play
 *  
 *  With the above in mind, the structure is that the objects which require input, inform the input manager
 *  that they are taking focus (and relinquishing focus). The default will always be the player. The input
 *  manager will store commands and associate them to button mappings. A command can be called on the focus
 *  and the focus can then handle that input however it likes.
 *  
 *  19/02 23:00 - Unity actually has an input manager for mapping the buttons. But it doesn't handle the assigment
 *  of in game commands (obviously). So this is still useful. Essentially the model becomes
 *  
 *  User Input -> Input Name -> Command -> In Game Implementation based on Focus
 *  
 *  To use the Input Manager, you must implement the Actor abstract base class and override the functions required.
 *  To handle multiple input focus', we add as many input managers as there are players in the game, and give it a player extension
 *  equal to their player number i.e. "_1", "_2". Then we must have a matching input axis configured in the project.
 *  Then we set the default actor to the character they control. When we want to have menu input, we'll need to pass 
 *  the UI Actor into the takeInputFocus function and everything should work as normal.
 * 
 *  Needs to handle:
 *  - 4-Directional Movement
 *  - Initiate Interaction
 *  - UI Confirm
 *  - UI Cancel
 *  - UI Directions
 *  
 *  Reminder:
 *  - Add logging/debug options (Log Button Press, Button history?, Print Mappings, Print Focus, Focus History?)
 */
public class InputManager : MonoBehaviour
{
	/*
	* Take in input
	* If mapped command
	* call mapped command on UI focus
	*/

	readonly string HORIZONTAL_AXIS_INPUT_STRING = "Horizontal";
	readonly string VERTICAL_AXIS_INPUT_STRING = "Vertical";
	readonly string ACTION_CONFIRM_INPUT_STRING = "Submit";
	readonly string ACTION_CANCEL_INPUT_STRING = "Cancel";
	readonly string ACTION_MENU_INPUT_STRING = "Jump";
	readonly string SECRET_DEBUG_ACTION_STRING = "Fire1";
	//readonly string SHOOT_HORIZONTAL_AXIS_INPUT_STRING = "ShootHorizontal";
	//readonly string SHOOT_VERTICAL_AXIS_INPUT_STRING = "ShootVertical";

	readonly int COMMAND_STREAM_EVENT_MAX = 20;

	//I don't really like this impl but it's quick
	public string playerInputExtension;

	public float holdDampeningPeriod = 0.05f;

	public GameObject defaultActorSource;
	private Stack<List<Actor>> currentInputFocus;
	private Queue<Command> commandStream;
	private Hashtable holdingMap;

    public float sensitivity = 0.4f;

	void Awake()
	{
		commandStream = new Queue<Command>();
		currentInputFocus = new Stack<List<Actor>>();

        if(defaultActorSource != null)
        {
    		Actor defaultActor = defaultActorSource.GetComponent<Actor>();
	    	currentInputFocus.Push(new List<Actor>() { defaultActor });
        }

		holdingMap = new Hashtable();
	}

	void Update()
	{
		List<Command> currCommands = HandleInput();
		foreach (Command command in currCommands)
		{
			if (command != null)
			{
				AddToCommandStream(command);
			}
		}

        if(currentInputFocus.Count <= 0)
        {
            return;
        }

		foreach (Actor actor in currentInputFocus.Peek())
		{
			actor.OnCommandQueueUpdated(commandStream, currCommands.Count);
		}
	}

	public string PlayerInputExtension
	{
		get { return playerInputExtension; }
	}

	public int GetCommandStreamEventMax()
	{
		return COMMAND_STREAM_EVENT_MAX;
	}

	private void AddToCommandStream(Command currCommand)
	{
		if (commandStream.Count == COMMAND_STREAM_EVENT_MAX)
		{
			commandStream.Dequeue();
		}
		commandStream.Enqueue(currCommand);
	}

	private List<Command> HandleInput()
	{
		List<Command> commandsThisFrame = new List<Command>();

		ReadPrimaryAxes(ref commandsThisFrame);
		ReadSecondaryAxes(ref commandsThisFrame);
		ReadButtons(ref commandsThisFrame);

		return commandsThisFrame;
	}

	private void ReadPrimaryAxes(ref List<Command> commandsThisFrame)
	{
		ReadAxes(HORIZONTAL_AXIS_INPUT_STRING, VERTICAL_AXIS_INPUT_STRING, ref commandsThisFrame);
	}

	private void ReadSecondaryAxes(ref List<Command> commandsThisFrame)
	{
		//ReadAxes(SHOOT_HORIZONTAL_AXIS_INPUT_STRING, SHOOT_VERTICAL_AXIS_INPUT_STRING, ref commandsThisFrame, HorizontalVerticalActionInitiateCommand.SECONDARY_AXIS);
	}

	private void ReadAxes(string horizontalInputString, string verticalInputString, ref List<Command> commandsThisFrame, int axisIndex = 0)
    {
        float horizontalAxisInput = Input.GetAxisRaw(horizontalInputString + playerInputExtension);
        float verticalAxisInput = Input.GetAxisRaw(verticalInputString + playerInputExtension);

        if (Math.Abs(horizontalAxisInput) < sensitivity)
        {
            horizontalAxisInput = 0;
        }

        if (Math.Abs(verticalAxisInput) < sensitivity)
        {
            verticalAxisInput = 0;
        }

        bool directionChanged = false;

		if(holdingMap == null)
		{
			return;
		}

        KeyValuePair<float, float> horizontalHoldValue = (holdingMap.Contains(horizontalInputString) ? (KeyValuePair<float, float>)holdingMap[horizontalInputString] : new KeyValuePair<float, float>());
        KeyValuePair<float, float> verticalHoldValue = (holdingMap.Contains(horizontalInputString) ? (KeyValuePair<float, float>)holdingMap[verticalInputString] : new KeyValuePair<float, float>());

        directionChanged = DirectionHasChanged(horizontalAxisInput, horizontalHoldValue, horizontalInputString) ||
                           DirectionHasChanged(verticalAxisInput, verticalHoldValue, verticalInputString);

        if (directionChanged)
        {
            commandsThisFrame.Add(new HorizontalVerticalActionInitiateCommand(horizontalAxisInput, verticalAxisInput, axisIndex));
            holdingMap[horizontalInputString] = new KeyValuePair<float, float>(Time.time, horizontalAxisInput);
            holdingMap[verticalInputString] = new KeyValuePair<float, float>(Time.time, verticalAxisInput);
        }
        else
        {
            commandsThisFrame.Add(new HorizontalVerticalActionHoldCommand(horizontalAxisInput, verticalAxisInput, axisIndex));
            holdingMap[horizontalInputString] = new KeyValuePair<float, float>(horizontalHoldValue.Key, horizontalAxisInput);
            holdingMap[verticalInputString] = new KeyValuePair<float, float>(verticalHoldValue.Key, verticalAxisInput);
        }

        if (NoAxisInput(horizontalAxisInput, verticalAxisInput) && WasHoldingOnAxes(horizontalInputString, verticalInputString))
        {
            holdingMap.Remove(horizontalInputString);
            holdingMap.Remove(verticalInputString);
            commandsThisFrame.Add(new HorizontalVerticalActionStopCommand());
        }
    }

    private bool WasHoldingOnAxes(string horizontalInputString, string verticalInputString)
    {
        return holdingMap.Contains(horizontalInputString) || holdingMap.Contains(verticalInputString);
    }

    private static bool NoAxisInput(float horizontalAxisInput, float verticalAxisInput)
    {
        return horizontalAxisInput == 0 && verticalAxisInput == 0;
    }

    private bool DirectionHasChanged(float axisInput, KeyValuePair<float, float> holdValue, string axisInputString)
	{
		bool centralLastFrame = (!holdingMap.Contains(axisInputString) || holdValue.Value == 0);
		if ((centralLastFrame || holdValue.Value > 0) && axisInput < 0)
		{
			return true;
		}
		else if ((centralLastFrame || holdValue.Value < 0) && axisInput > 0)
		{
			return true;
		}
		else if (centralLastFrame && axisInput != 0)
		{
			return true;
		}
		else if (!centralLastFrame && axisInput == 0)
		{
			return true;
		}

		return false;
	}

	private void ReadButtons(ref List<Command> commandsThisFrame)
	{
		if (CheckInitialButtonPress(ref commandsThisFrame))
		{
			return;
		}

		if (CheckButtonReleased(ref commandsThisFrame))
		{
			return;
		}

		if (CheckButtonsHeld(ref commandsThisFrame))
		{
			return;
		}
	}

	private bool CheckInitialButtonPress(ref List<Command> commandsThisFrame)
	{
		bool handled = false;

		//Looks like there's a small bug where the User can press a button
		//fast enough to have GetButtonDown return true 2 frames in a row.
		//So we just update the time in the map when that happens
		if (Input.GetButtonDown(ACTION_CONFIRM_INPUT_STRING + playerInputExtension))
		{
			commandsThisFrame.Add(new ConfirmActionInitiateCommand());
			holdingMap[ACTION_CONFIRM_INPUT_STRING] = Time.time;
			handled = true;
		}
		else if (Input.GetButtonDown(ACTION_CANCEL_INPUT_STRING + playerInputExtension))
		{
			commandsThisFrame.Add(new CancelActionInitiateCommand());
			holdingMap[ACTION_CANCEL_INPUT_STRING] = Time.time;
			handled = true;
		}
		else if (Input.GetButtonDown(ACTION_MENU_INPUT_STRING + playerInputExtension))
		{
			commandsThisFrame.Add(new MenuActionInitiateCommand());
			holdingMap[ACTION_MENU_INPUT_STRING] = Time.time;
			handled = true;
		}
		else if (Input.GetButtonDown(SECRET_DEBUG_ACTION_STRING + playerInputExtension))
		{
			commandsThisFrame.Add(new DebugActionInitiateCommand());
			holdingMap[SECRET_DEBUG_ACTION_STRING] = Time.time;
			handled = true;
		}

		return handled;
	}

	private bool CheckButtonReleased(ref List<Command> commandsThisFrame)
	{
		bool handled = false;

		if (Input.GetButtonUp(ACTION_CONFIRM_INPUT_STRING + playerInputExtension))
		{
			commandsThisFrame.Add(new ConfirmActionStopCommand());
			if (holdingMap.Contains(ACTION_CONFIRM_INPUT_STRING))
			{
				holdingMap.Remove(ACTION_CONFIRM_INPUT_STRING);
			}
			handled = true;
		}
		else if (Input.GetButtonUp(ACTION_CANCEL_INPUT_STRING + playerInputExtension))
		{
			commandsThisFrame.Add(new CancelActionStopCommand());
			if (holdingMap.Contains(ACTION_CANCEL_INPUT_STRING))
			{
				holdingMap.Remove(ACTION_CANCEL_INPUT_STRING);
			}
			handled = true;
		}
		else if (Input.GetButtonUp(ACTION_MENU_INPUT_STRING + playerInputExtension))
		{
			commandsThisFrame.Add(new MenuActionStopCommand());
			if (holdingMap.Contains(ACTION_MENU_INPUT_STRING))
			{
				holdingMap.Remove(ACTION_MENU_INPUT_STRING);
			}
			handled = true;
		}
		else if (Input.GetButtonUp(SECRET_DEBUG_ACTION_STRING + playerInputExtension))
		{
			commandsThisFrame.Add(new DebugActionStopCommand());
			if (holdingMap.Contains(SECRET_DEBUG_ACTION_STRING))
			{
				holdingMap.Remove(SECRET_DEBUG_ACTION_STRING);
			}
			handled = true;
		}

		return handled;
	}

	private bool CheckButtonsHeld(ref List<Command> commandsThisFrame)
	{
		bool handled = false;

		if (holdingMap.Contains(ACTION_CONFIRM_INPUT_STRING))
		{
			if (Input.GetButton(ACTION_CONFIRM_INPUT_STRING + playerInputExtension) && Time.time >= (float)holdingMap[ACTION_CONFIRM_INPUT_STRING] + holdDampeningPeriod)
			{
				commandsThisFrame.Add(new ConfirmActionHoldCommand());
				holdingMap.Remove(ACTION_CONFIRM_INPUT_STRING);
				handled = true;
			}
		}
		else if (holdingMap.Contains(ACTION_CANCEL_INPUT_STRING))
		{
			if (Input.GetButton(ACTION_CANCEL_INPUT_STRING + playerInputExtension) && Time.time >= (float)holdingMap[ACTION_CANCEL_INPUT_STRING] + holdDampeningPeriod)
			{
				commandsThisFrame.Add(new CancelActionHoldCommand());
				holdingMap.Remove(ACTION_CANCEL_INPUT_STRING);
				handled = true;
			}
		}
		else if (holdingMap.Contains(ACTION_MENU_INPUT_STRING))
		{
			if (Input.GetButton(ACTION_MENU_INPUT_STRING + playerInputExtension) && Time.time >= (float)holdingMap[ACTION_MENU_INPUT_STRING] + holdDampeningPeriod)
			{
				commandsThisFrame.Add(new MenuActionHoldCommand());
				holdingMap.Remove(ACTION_MENU_INPUT_STRING);
				handled = true;
			}
		}
		else if (holdingMap.Contains(SECRET_DEBUG_ACTION_STRING))
		{
			if (Input.GetButton(SECRET_DEBUG_ACTION_STRING + playerInputExtension) && Time.time >= (float)holdingMap[SECRET_DEBUG_ACTION_STRING] + holdDampeningPeriod)
			{
				commandsThisFrame.Add(new DebugActionHoldCommand());
				holdingMap.Remove(SECRET_DEBUG_ACTION_STRING);
				handled = true;
			}
		}

		return handled;
	}

	public void TakeInputFocus(Actor actor)
	{
		currentInputFocus.Push(new List<Actor>() { actor });
	}

	public void RelinquishInputFocus(Actor actor)
	{
		if (currentInputFocus.Peek().Contains(actor))
		{
			currentInputFocus.Pop();
		}

		if (currentInputFocus.Count == 0)
		{
            if(defaultActorSource)
            {
			    Actor defaultActor = defaultActorSource.GetComponent<Actor>();
			    currentInputFocus.Push(new List<Actor>() { defaultActor });
            }
		}
	}

	public void JoinInputFocus(Actor actor)
	{
		currentInputFocus.Peek().Add(actor);
	}

	public void LeaveInputFocus(Actor actor)
	{
		if (currentInputFocus.Peek().Contains(actor))
		{
			currentInputFocus.Peek().Remove(actor);
		}

		if (currentInputFocus.Peek().Count == 0)
		{
			currentInputFocus.Pop();

			if (currentInputFocus.Count == 0)
			{
				Actor defaultActor = defaultActorSource.GetComponent<Actor>();
				currentInputFocus.Push(new List<Actor>() { defaultActor });
			}
		}
	}
}
