using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDaisy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around its local X axis at 15 degree per second
        transform.Rotate(Vector3.up * Time.deltaTime * 35);
    }
}
