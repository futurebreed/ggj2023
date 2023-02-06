using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCell : GridCell
{
    private void Start()
    {
        this.gameObject.tag = "Exit";
    }
}
