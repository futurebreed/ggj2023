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
    InputDragBehavior input;

    public int rootDepth;
    public Vector3 rootPosition;
    public HashSet<Tuple<int,int>> newroots = new HashSet<Tuple<int, int>>();
    public int latency;
    private int waitCounter;

    // Start is called before the first frame update
    void Start()
    {
        float center = GameObject.Find("GridGenerator").GetComponent<GridGenerator>().gridWidth / 2.0f;
        rootPosition = new Vector3(center, GameObject.Find("GridGenerator").GetComponent<GridGenerator>().gridHeight, -1);
        waitCounter = latency;
    }

    // Update is called once per frame
    void Update()
    {
        // Only process root growth if the mouse is actually dragging
        if ((input.inputState.state != InputMovementState.None) && (input.inputState.state != InputMovementState.Moving))
        {
            waitCounter--;
            if (waitCounter == 0)
            {
                Tuple<int, int> positionOnGrid = input.inputState.positionToGridCellSpace(GameObject.Find("GridGenerator").GetComponent<GridGenerator>().gameObject);

                int gridX = positionOnGrid.Item1; // Cell index
                int gridY = positionOnGrid.Item2; // Cell index
                if (gridX == -1 || gridY == -1)
                {
                    // Click was not on grid
                    return;
                }

                Debug.Log($"Dragging on Grid position: ({gridX}, {gridY})");

                if ((input.inputState.position.x < rootPosition.x) && (input.inputState.position.x >= 0))
                {
                    rootPosition += Vector3.left;
                }
                else if (input.inputState.position.x < GameObject.Find("GridGenerator").GetComponent<GridGenerator>().gridWidth)
                {
                    rootPosition += Vector3.right;
                }

                rootPosition += Vector3.down;
                GameObject newRoot = Instantiate(smallRoot, this.transform);
                newRoot.transform.position = rootPosition;
                waitCounter = latency;
            }
        }

        
        /*int xloc = (int)input.inputState.position.x;
        int yloc = (int)input.inputState.position.y;
        Tuple<int, int> mosPos = new(xloc, yloc);
        //if (xloc < rootPosition.x + 1 && xloc > rootPosition.x - 1 && yloc < rootPosition.y + 1 && yloc > rootPosition.y - 1)
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
