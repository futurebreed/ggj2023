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
    public InputSupport inputSupport = new InputSupport();

    public float MaxMagnitudeForLightDrag = 0.25f;
    public float MaxMagnitudeForMediumDrag = 0.50f;
    public float MaxMagnitudeForStrongDrag = 1.00f;

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
                        inputSupport.state = DragState.Stationary;
                        inputSupport.acceleration = Vector2.zero;
                        inputSupport.velocity = Vector2.zero;
                        break;
                    }
                case TouchPhase.Moved:
                    {
                        Vector2 newVelocity = (touch.position - inputSupport.position) / Time.deltaTime;
                        inputSupport.acceleration = (newVelocity - inputSupport.velocity) / Time.deltaTime;
                        inputSupport.velocity = newVelocity;

                        updateDragStrength();
                        break;
                    }
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    {
                        inputSupport.state = DragState.None;
                        inputSupport.acceleration = Vector2.zero;
                        inputSupport.velocity = Vector2.zero;
                        break;
                    }
                default:
                    {
                        // no op
                        break;
                    }
            }

            inputSupport.position = touch.position;

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

        switch (inputSupport.state)
        {
            case DragState.None:
                {
                    if (leftMouseButtonDown)
                    {
                        inputSupport.state = DragState.Stationary;
                        inputSupport.acceleration = Vector2.zero;
                        inputSupport.velocity = Vector2.zero;
                        inputSupport.position = Input.mousePosition;
                        inputSupport.mouseDown = true;
                    }
                    else if (inputSupport.mouseDown)
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
                    else if (inputSupport.mouseDown)
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
                    else if (inputSupport.mouseDown)
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
        Vector2 newVelocity = (mousePosOnScreen - inputSupport.position) / Time.deltaTime;
        inputSupport.acceleration = (mousePosOnScreen - inputSupport.position) / Time.deltaTime;
        inputSupport.velocity = newVelocity;
        inputSupport.position = mousePosOnScreen;

        updateDragStrength();

        Debug.Log($"New input state: a {inputSupport.acceleration} v {inputSupport.velocity} s {inputSupport.speed} p {inputSupport.position}");
    }

    private void resetMouseDownState()
    {
        inputSupport.state = DragState.None;
        inputSupport.acceleration = Vector2.zero;
        inputSupport.velocity = Vector2.zero;
        inputSupport.position = Input.mousePosition;
        inputSupport.mouseDown = false;
    }

    private void updateDragStrength()
    {
        if (inputSupport.acceleration. <= MaxMagnitudeForLightDrag)
        {
            inputSupport.state = DragState.Light;
        }
        else if (inputSupport.acceleration.magnitude <= MaxMagnitudeForMediumDrag)
        {
            inputSupport.state = DragState.Medium;
        }
        else
        {
            inputSupport.state = DragState.Strong;
        }
    }
}

public class InputSupport
{
    public Vector2 position { get; set; } = Vector2.zero;
    public Vector2 velocity { get; set; } = Vector2.zero;
    public float speed => velocity.magnitude;
    public Vector2 acceleration { get; set; } = Vector2.zero;
    public DragState state { get; set; } = DragState.None;

    public bool mouseDown { get; set; } = false;
}