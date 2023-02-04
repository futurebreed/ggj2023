using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DragState
{
    None,
    Stationary,
    Light,
    Medium,
    Strong
}

public class InputDragBehavior : MonoBehaviour
{
    public InputState inputState = new InputState();

    [Range(0.0f, 1.0f)] public float MaxMagnitudeForLightDrag = 0.25f;
    [Range(0.0f, 1.0f)] public float MaxMagnitudeForMediumDrag = 0.50f;
    [Range(0.0f, 1.0f)] public float MaxMagnitudeForStrongDrag = 1.00f;

    private void Update()
    {
        // If a device happens to support multiple input types at once, we'll just take the first one we see.
        // This could contribute to some odd behavior if a player is fluidly switching from touch to mouse (like with a touch-screen laptop).
        // We could refactor for more strict input device tracking and logic to determine which one is primary.

        if (Input.touchSupported && updateTouchState())
        {
            return;
        }

        // mousePresent only works for Windows and Android. Mac will always be true. Consoles and iOS will always be false.
        if (Input.mousePresent && updateMouseState())
        {
            return;
        }

        // TODO if we ever want to support other input devices like keyboard, controller, etc.
    }

    private bool updateTouchState()
    {
        bool hasAnyTouchInputUpdates = false;

        foreach (var touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                case TouchPhase.Stationary:
                    {
                        inputState.state = DragState.Stationary;
                        inputState.acceleration = Vector2.zero;
                        inputState.velocity = Vector2.zero;
                        break;
                    }
                case TouchPhase.Moved:
                    {
                        Vector2 newVelocity = (touch.position - inputState.position) / Time.deltaTime;
                        inputState.acceleration = (newVelocity - inputState.velocity) / Time.deltaTime;
                        inputState.velocity = newVelocity;

                        updateDragStrength();
                        break;
                    }
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    {
                        inputState.state = DragState.None;
                        inputState.acceleration = Vector2.zero;
                        inputState.velocity = Vector2.zero;
                        break;
                    }
                default:
                    {
                        // no op
                        break;
                    }
            }

            inputState.position = touch.position;

            hasAnyTouchInputUpdates = true;
        }

        return hasAnyTouchInputUpdates;
    }

    private bool updateMouseState()
    {
        bool hasAnyMouseInputUpdates = false;

        bool leftMouseButtonDown = Input.GetMouseButton(0);
        // bool rightMouseButtonDown = Input.GetMouseButton(1);
        // bool middleMouseButtonDown = Input.GetMouseButton(2);

        switch (inputState.state)
        {
            case DragState.None:
                {
                    if (leftMouseButtonDown)
                    {
                        inputState.state = DragState.Stationary;
                        inputState.acceleration = Vector2.zero;
                        inputState.velocity = Vector2.zero;
                        inputState.position = Input.mousePosition;
                        inputState.mouseDown = true;
                    }
                    else if (inputState.mouseDown)
                    {
                        resetMouseDownState();
                    }

                    break;
                }
            case DragState.Stationary:
                {
                    if (leftMouseButtonDown)
                    {
                        updateMouseMovementState();
                    }
                    else if (inputState.mouseDown)
                    {
                        resetMouseDownState();
                    }

                    break;
                }
            case DragState.Light:
            case DragState.Medium:
            case DragState.Strong:
                {
                    if (leftMouseButtonDown)
                    {
                        updateMouseMovementState();
                    }
                    else if (inputState.mouseDown)
                    {
                        resetMouseDownState();
                    }

                    break;
                }
        }

        return hasAnyMouseInputUpdates;
    }

    private void updateMouseMovementState()
    {
        Vector2 mousePosOnScreen = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 newVelocity = (mousePosOnScreen - inputState.position) / Time.deltaTime;
        inputState.acceleration = (mousePosOnScreen - inputState.position) / Time.deltaTime;
        inputState.velocity = newVelocity;
        inputState.position = mousePosOnScreen;

        updateDragStrength();
    }

    private void resetMouseDownState()
    {
        inputState.state = DragState.None;
        inputState.acceleration = Vector2.zero;
        inputState.velocity = Vector2.zero;
        inputState.position = Input.mousePosition;
        inputState.mouseDown = false;
    }

    private void updateDragStrength()
    {
        if (inputState.acceleration.magnitude <= MaxMagnitudeForLightDrag)
        {
            inputState.state = DragState.Light;
        }
        else if (inputState.acceleration.magnitude <= MaxMagnitudeForMediumDrag)
        {
            inputState.state = DragState.Medium;
        }
        else
        {
            inputState.state = DragState.Strong;
        }

        Debug.Log($"New input state: a {inputState.acceleration} v {inputState.velocity} s {inputState.speed} p {inputState.position}");
    }
}

public class InputState
{
    public Vector2 position { get; set; } = Vector2.zero;
    public Vector2 velocity { get; set; } = Vector2.zero;
    public float speed => velocity.magnitude;
    public Vector2 acceleration { get; set; } = Vector2.zero;
    public DragState state { get; set; } = DragState.None;

    public bool mouseDown { get; set; } = false;
}