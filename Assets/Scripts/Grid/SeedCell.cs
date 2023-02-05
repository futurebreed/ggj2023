using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedCell : GridCell
{
    private void Start()
    {
        // offset the transform of the seed to help it face the camera
        transform.position += new Vector3(0f, -0.5f, -0.6f);
        transform.Rotate(new Vector3(0f, -90f));
        transform.localScale *= 0.7f;
    }
}
