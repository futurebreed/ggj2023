using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridCell", menuName = "MazeGrid/GridCell", order = 0)
public class GridCellScriptableObject : ScriptableObject
{

    // todo: we'll add more properties to this scriptable object as we refne
    // the game

    // for prototyping lets only toggle visibility
    public bool isVisible = true;

}
