using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeProperties : MonoBehaviour
{
    // Start is called before the first frame update
    private string no;
    private float x;
    private float y;
    private float z;

    public NodeProperties(string no, int x,int y, int z)
    {
        this.no = no;
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public string No { get { return no; } set { no = value; } }

    public float X { get { return x; } set { x = value; } }

    public float Y { get { return y; } set { y = value; } }

    public float Z { get { return z; } set { z = value; } }
}
