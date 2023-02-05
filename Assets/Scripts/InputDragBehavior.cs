using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum InputMovementState
{
    None,
    Moving,
    DragStationary,
    DragLight,
    DragMedium,
    DragStrong
}

public class InputDragBehavior : MonoBehaviour
{
    public InputState inputState = new InputState();

    [SerializeField]
    public float MaxMagnitudeForLightDrag = 15.0f; // Don't forget this gets overridden in the editor so if you tweak these values here, they may not reflect that when you test

    [SerializeField]
    public float MaxMagnitudeForMediumDrag = 20.0f; // Don't forget this gets overridden in the editor so if you tweak these values here, they may not reflect that when you test

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
                        inputState.state = InputMovementState.DragStationary;
                        inputState.acceleration = Vector3.zero;
                        inputState.velocity = Vector3.zero;
                        break;
                    }
                case TouchPhase.Moved:
                    {
                        Vector3 newVelocity = (new Vector3(touch.position.x, touch.position.y, 0.0f) - inputState.position) / Time.deltaTime;
                        inputState.acceleration = (newVelocity - inputState.velocity) / Time.deltaTime;
                        inputState.velocity = newVelocity;

                        if ((inputState.state != InputMovementState.None) && (inputState.state != InputMovementState.DragStationary))
                        {
                            updateDragStrength();
                        }

                        break;
                    }
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    {
                        inputState.state = InputMovementState.None;
                        inputState.acceleration = Vector3.zero;
                        inputState.velocity = Vector3.zero;
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
            case InputMovementState.None:
                {
                    if (leftMouseButtonDown)
                    {
                        inputState.state = InputMovementState.DragStationary;
                        inputState.acceleration = Vector3.zero;
                        inputState.velocity = Vector3.zero;
                        inputState.position = Input.mousePosition;
                        inputState.mouseDown = true;
                    }
                    else if (inputState.mouseDown)
                    {
                        resetMouseDownState();
                    }
                    else {
                        updateMouseMovementState(false);
                    }

                    break;
                }
            case InputMovementState.Moving:
                {
                    if (leftMouseButtonDown)
                    {
                        updateMouseMovementState(true);
                    }
                    else if (inputState.mouseDown)
                    {
                        resetMouseDownState();
                    }
                    else
                    {
                        updateMouseMovementState(false);
                    }

                    break;
                }
            case InputMovementState.DragStationary:
                {
                    if (leftMouseButtonDown)
                    {
                        updateMouseMovementState(true);
                    }
                    else
                    {
                        resetMouseDownState();
                    }

                    break;
                }
            case InputMovementState.DragLight:
            case InputMovementState.DragMedium:
            case InputMovementState.DragStrong:
                {
                    if (leftMouseButtonDown)
                    {
                        updateMouseMovementState(true);
                    }
                    else
                    {
                        resetMouseDownState();
                    }

                    break;
                }
        }

        return hasAnyMouseInputUpdates;
    }

    private void updateMouseMovementState(bool isDragging)
    {
        Vector3 mousePosOnScreen = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
        Vector3 newVelocity = (mousePosOnScreen - inputState.position) / Time.deltaTime;
        inputState.acceleration = (mousePosOnScreen - inputState.position) / Time.deltaTime;
        inputState.velocity = newVelocity;
        inputState.position = mousePosOnScreen;

        if (isDragging)
        {
            if (inputState.velocity.magnitude > 0.0f)
            {
                updateDragStrength();
            }
            else
            {
                inputState.state = InputMovementState.DragStationary;
            }
        }
        else
        {
            if (inputState.velocity.magnitude > 0.0f)
            {
                inputState.state = InputMovementState.Moving;
            }
            else
            {
                inputState.state = InputMovementState.None;
            }
        }
    }

    private void resetMouseDownState()
    {
        inputState.state = InputMovementState.None;
        inputState.acceleration = Vector3.zero;
        inputState.velocity = Vector3.zero;
        inputState.position = Input.mousePosition;
        inputState.mouseDown = false;
    }

    private void updateDragStrength()
    {
        float sqrtMagnitude = Mathf.Sqrt(inputState.acceleration.magnitude);
        if (sqrtMagnitude <= MaxMagnitudeForLightDrag)
        {
            inputState.state = InputMovementState.DragLight;
        }
        else if (sqrtMagnitude <= MaxMagnitudeForMediumDrag)
        {
            inputState.state = InputMovementState.DragMedium;
        }
        else
        {
            inputState.state = InputMovementState.DragStrong;
        }

        Debug.Log($"State: {inputState.state} Val: {sqrtMagnitude}");
    }
}

public class InputState
{
    public Vector3 position { get; set; } = Vector3.zero;
    public Vector3 velocity { get; set; } = Vector3.zero;
    public float speed => velocity.magnitude;
    public Vector3 acceleration { get; set; } = Vector3.zero;
    public InputMovementState state { get; set; } = InputMovementState.None;

    public bool mouseDown { get; set; } = false;
}