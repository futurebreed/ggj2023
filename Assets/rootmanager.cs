using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class rootmanager : MonoBehaviour
{
    [SerializeField]
    public GameObject smallRoot;

    [SerializeField]
    public GameObject largeRoot;

    [SerializeField]
    public SoundHandler soundHandler;

    [SerializeField]
    public GridGenerator gridGenerator;

    public int rootDepth;
    public Vector3 rootPosition;
    public HashSet<Vector3> newroots = new HashSet<Vector3>();
    public int latency;

    // Start is called before the first frame update
    void Start()
    {
        GridCell startCell = gridGenerator.getStart();
        rootPosition = new Vector3(startCell.GridX, startCell.GridY, -1);
        this.transform.position = rootPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Only process root growth if the mouse is actually dragging
        if ((InputDragBehavior.inputState.state != InputMovementState.None) && (InputDragBehavior.inputState.state != InputMovementState.Moving))
        {
            Tuple<int, int> positionOnGrid = InputDragBehavior.inputState.positionToGridCellSpace(GameObject.Find("GridGenerator").GetComponent<GridGenerator>().gameObject);

            int gridX = positionOnGrid.Item1; // Cell index
            int gridY = positionOnGrid.Item2; // Cell index
            if (gridX == -1 || gridY == -1)
            {
                // Click was not on grid
                return;
            }

            Debug.Log($"Dragging on Grid position: ({gridX}, {gridY})");

            Vector3 cursorPosition = new Vector3(gridX, gridY, -1);
            if (gridX < rootPosition.x + 2 && gridX > rootPosition.x - 2 && gridY < rootPosition.y + 2 && gridY > rootPosition.y - 2)
            {
                if (!newroots.Contains(cursorPosition))
                {
                    if (InputDragBehavior.inputState.state != InputMovementState.DragStationary)
                    {
                        rootPosition = cursorPosition;
                        soundHandler.Growth();
                    }
                    else
                    {
                        soundHandler.StopGrowth();
                        return;
                    }

                    if (newroots.Count < rootDepth)
                    {
                        GameObject newRoot = Instantiate(smallRoot, transform);
                        newRoot.transform.position = cursorPosition;
                        newroots.Add(cursorPosition);
                    }
                    else
                    {
                        for (int i = 0; i < transform.childCount; i++)
                        {
                            transform.GetChild(i).localScale *= 2;
                        }
                        GameObject newRoot = Instantiate(smallRoot, transform);
                        newRoot.transform.position = cursorPosition;
                        newroots.Add(cursorPosition);
                        float center = GameObject.Find("GridGenerator").GetComponent<GridGenerator>().gridWidth / 2.0f;
                        rootPosition = new Vector3(center, GameObject.Find("GridGenerator").GetComponent<GridGenerator>().gridHeight, -1);
                        newroots = new HashSet<Vector3>();
                    }
                }
            }

            if (InputDragBehavior.inputState.state != InputMovementState.DragLight &&
                InputDragBehavior.inputState.state != InputMovementState.DragMedium &&
                InputDragBehavior.inputState.state != InputMovementState.DragStrong)
            {
                soundHandler.StopGrowth();
            }
        }

        /*int xloc = (int)input.inputState.position.x;
        int yloc = (int)input.inputState.position.y;
        Tuple<int, int> mosPos = new(xloc, yloc);
        //if (xloc < rootPosition.x + 2 && xloc > rootPosition.x - 2 && yloc < rootPosition.y + 1 && yloc > rootPosition.y - 1)
        if (true)
        {
            if(!newroots.Contains(mosPos))
            {
                if (newroots.Count >= rootDepth)
                {

                }
                else
                {
                    newroots.Add(mosPos);
                    
                    
                }
            }
        }
        */
    }

    public void generateRoots(int iters_left, GameObject seed)
    {
        if (iters_left>0)
        {
            int subdivisions = 5;
            for (int i = 0; i < subdivisions; i++)
            {
                //GameObject newNode = Instantiate(rootNode, seed.transform);

                //RelativeJoint2D newJoint = newNode.AddComponent<RelativeJoint2D>();
                //newJoint.maxForce = seed.GetComponent<RelativeJoint2D>().maxForce / 2;
                //newJoint.maxTorque = seed.GetComponent<RelativeJoint2D>().maxTorque / 2;
                //newJoint.breakForce = newJoint.maxForce;
                //newJoint.breakTorque = newJoint.breakTorque;
                //generateRoots(iters_left - 1, newNode);
            }
        }
    }
}
