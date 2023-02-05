using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCell : GridCell
{

    private bool _isColliding = false;
    public bool IsColliding => _isColliding;

    public Mesh[] rockMeshes;

    public Material rockMaterial;

    // Start is called before the first frame update
    void Start()
    {
        // lets sample a rock mesh randomly
        int randomIndex = Random.Range(0, rockMeshes.Length);
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = rockMeshes[randomIndex];

        // set the rock material on the renderer
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = rockMaterial;

        // update the collider
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = meshFilter.mesh;

        // offset the transform of the rock to help it face the camera
        transform.position += new Vector3(0f, 0f, -0.6f);
        transform.Rotate(new Vector3(90f, 0f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        // note: this can probably be inverted to only be true for root collisions
        if (collision.gameObject.tag != "Dirt")
        {
            _isColliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag != "Dirt")
        {
            _isColliding = false;
        }
    }

}
