using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtCell : GridCell
{

    public Material[] dirtMaterials;

    // Start is called before the first frame update
    void Start()
    {
        // get a reference to the mesh renderer
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        // this should be ordered from bottom to top, with index 0 being the darkest dirt
        int dirtLayersCount = dirtMaterials.Length;

        // calculate this cell's % of the grid height
        float gridHeightPercent = (float)_gridY / (float)_gridHeight;
        Debug.Log(gridHeightPercent);

        // compute the index of the dirt layer relative to the grid height
        // this will be a value between 0 and dirtLayersCount - 1
        int dirtLayer = (int)(gridHeightPercent * dirtLayersCount);

        // set the dirt material
        meshRenderer.material = dirtMaterials[dirtLayer];
    }
}
