using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class rootmanager : MonoBehaviour
{
    public GameObject rootNode;
    public int rootDepth;

    // Start is called before the first frame update
    void Start()
    {
        generateRoots(rootDepth,this.GameObject());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateRoots(int iters_left, GameObject seed)
    {
        if (iters_left>0)
        {
            int subdivisions = Random.Range(0, 4);
            for (int i = 0; i < subdivisions; i++)
            {
                Transform newPosition=seed.GetComponent<Transform>();
                GameObject newNode = Instantiate(rootNode, newPosition);
                RelativeJoint2D newJoint = newNode.AddComponent<RelativeJoint2D>();
                newJoint.maxForce = seed.GetComponent<RelativeJoint2D>().maxForce / 2;
                newJoint.maxTorque = seed.GetComponent<RelativeJoint2D>().maxTorque / 2;
                newJoint.breakForce = newJoint.maxForce;
                newJoint.breakTorque = newJoint.breakTorque;
                generateRoots(iters_left - 1, newNode);
            }
        }
    }
}
