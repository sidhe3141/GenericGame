using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace sidhe3141utils.Xna.Action_and_Input
{
    /// <summary>
    /// The buttons on a mouse.
    /// </summary>
    public enum MouseButtons
    {
        left,
        right,
        center,
        X1,
        X2
    }

    /// <summary>
    /// The sticks on a gamepad.
    /// </summary>
    public enum Sticks
    {
        left,
        right,
        mouse
    }

    /// <summary>
    /// Types of possible inputs to be tracked.
    /// Author: James Yakura.
    /// </summary>
    public enum InputType
    {
        /// <summary>
        /// A key.
        /// </summary>
        Key,
        /// <summary>
        /// A mouse button.
        /// </summary>
        MouseButton,
        /// <summary>
        /// A gamepad button or stick's pressed or released state.
        /// </summary>
        GamePadButton,
        /// <summary>
        /// A gamepad stick's deflection values or a mouse cursor's position.
        /// </summary>
        GamePadStick,
        /// <summary>
        /// A location, determined by a smartphone's GPS, relative to a circle of a given radius around a point. Not currently supported.
        /// </summary>
        GPS,
        /// <summary>
        /// Scrolling up with the wheel.
        /// </summary>
        ScrollWheelUp,
        /// <summary>
        /// Scrolling down with the wheel.
        /// </summary>
        ScrollWheelDown

    }

    /// <summary>
    /// Types of input changes that can register as an event.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// Has the input been pressed/within range this tick?
        /// </summary>
        Down,
        /// <summary>
        /// Has the input been pressed/within range last tick and ceased being so this tick?
        /// </summary>
        Typed,
        /// <summary>
        /// Has the input been released/out of range last tick and ceased being so this tick?
        /// </summary>
        Entered,
    }

    /// <summary>
    /// Handles most common types of user input.
    /// Author: James Yakura.
    /// </summary>
    public class Input
    {
        InputType type;
        EventType ev;
        Keys key;
        Buttons gpbutton;
        Sticks stick;
        MouseButtons mbutton;
        bool trueLastPass;
        Rectangle bounds;
        PlayerIndex player;
        int lastWheelValue;

        /// <summary>
        /// Creates a new input watcher associated with a key.
        /// </summary>
        /// <param name="Event">The event type to watch for.</param>
        /// <param name="key">The keyboard key to watch.</param>
        public Input(EventType Event, Keys key)
        {
            this.type = InputType.Key;
            this.ev = Event;
            this.key = key;
            trueLastPass = false;
        }
        /// <summary>
        /// Creates a new input watcher associated with a gamepad button.
        /// </summary>
        /// <param name="Event">The event type to watch for.</param>
        /// <param name="button">The gamepad button or stick depress to watch for.</param>
        public Input(EventType Event, Buttons button, PlayerIndex player)
        {
            type = InputType.GamePadButton;
            ev = Event;
            this.gpbutton = button;
            trueLastPass = false;
            this.player = player;
        }
        /// <summary>
        /// Creates a new input watcher associated with stick movement.
        /// </summary>
        /// <param name="Event">The event type to watch for.</param>
        /// <param name="stick">The stick to watch.</param>
        /// <param name="bounds">The rectangle that the end of the stick's Vector2 is in if a neutral stick is at the origin.</param>
        /// <param name="player">The gamepad to check. Leave null for mouse input.</param>
        public Input(EventType Event, Sticks stick, Rectangle bounds, PlayerIndex player)
        {
            type = InputType.GamePadStick;
            ev = Event;
            this.stick = stick;
            this.bounds=bounds;
            this.player = player;
            trueLastPass=false;
        }
        /// <summary>
        /// Creates a new input watcher associated with a mouse button.
        /// </summary>
        /// <param name="Event">The event type to watch for.</param>
        /// <param name="button">The mouse button to watch.</param>
        /// <param name="bounds">The edges of the area to watch.</param>
        public Input(EventType Event, MouseButtons button)
        {
            type = InputType.MouseButton;
            ev = Event;
            mbutton = button;
            trueLastPass=false;
        }
        /// <summary>
        /// Creates a new input watcher associated with the scroll wheel.
        /// </summary>
        /// <param name="Event">The event type to watch for.</param>
        /// <param name="direction">True if the direction watched is up, false if it is down.</param>
        public Input(EventType Event, bool direction)
        {
            if (direction)
            {
                type = InputType.ScrollWheelUp;
            }
            else
            {
                type = InputType.ScrollWheelDown;
            }
            ev = Event;
            lastWheelValue = Mouse.GetState().ScrollWheelValue;
            trueLastPass=false;
        }

        /// <summary>
        /// Gets whether the basic event is true.
        /// </summary>
        public bool Value
        {
            get
            {
                switch (type)
                {
                    case InputType.Key:
                        return Keyboard.GetState().IsKeyDown(key);
                    case InputType.GamePadButton:
                        return GamePad.GetState(player).IsButtonDown(gpbutton);
                    case InputType.GamePadStick:
                        return StickInBounds;
                    case InputType.MouseButton:
                        switch (mbutton)
                        {
                            case MouseButtons.left:
                                return Mouse.GetState().LeftButton == ButtonState.Pressed;
                            case MouseButtons.center:
                                return Mouse.GetState().MiddleButton == ButtonState.Pressed;
                            case MouseButtons.right:
                                return Mouse.GetState().RightButton == ButtonState.Pressed;
                            case MouseButtons.X1:
                                return Mouse.GetState().XButton1 == ButtonState.Pressed;
                            case MouseButtons.X2:
                                return Mouse.GetState().XButton2 == ButtonState.Pressed;
                            default:
                                return false;
                        }
                    case InputType.ScrollWheelDown:
                        bool result = Mouse.GetState().ScrollWheelValue < lastWheelValue;
                        lastWheelValue = Mouse.GetState().ScrollWheelValue;
                        return result;
                    case InputType.ScrollWheelUp:
                        bool result2 = Mouse.GetState().ScrollWheelValue > lastWheelValue;
                        lastWheelValue = Mouse.GetState().ScrollWheelValue;
                        return result2;
                    default:
                        return false;
                }
                
            }
        }
        /// <summary>
        /// Gets whether the stick is within bounds.
        /// </summary>
        public bool StickInBounds
        {
            get
            {
                Point target;
                switch(stick){
                    case Sticks.left:
                        target = new Point((int)GamePad.GetState(player).ThumbSticks.Left.X,(int)GamePad.GetState(player).ThumbSticks.Left.Y);
                    break;
                    case Sticks.right:
                    target = new Point((int)GamePad.GetState(player).ThumbSticks.Right.X, (int)GamePad.GetState(player).ThumbSticks.Right.Y);
                    break;
                    case Sticks.mouse:
                    target = new Point(Mouse.GetState().X, Mouse.GetState().Y);
                    break;
                    default:
                    target = new Point();
                    break;
                    }
                return bounds.Contains(target);
            }
        }
        /// <summary>
        /// Gets whether the event watched for is true.
        /// </summary>
        public bool PostProcessValue
        {
            get
            {
                switch (ev)
                {
                    case EventType.Down:
                        return Value;
                    case EventType.Entered:
                        if (Value & !trueLastPass)
                        {
                            trueLastPass = true;
                            return true;
                        }
                        else
                        {
                            trueLastPass = Value;
                            return false;
                        }
                    case EventType.Typed:
                        if (!Value & trueLastPass)
                        {
                            trueLastPass = false;
                            return true;
                        }
                        else
                        {
                            trueLastPass = Value;
                            return false;
                        }
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// Gets and sets the bounds of the input.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                return bounds;
            }
            set
            {
                bounds = value;
            }
        }
    }
}
