using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleCalculator : MonoBehaviour
{
  
    // Start is called before the first frame update
    public int x1, y1, z1;
    public int x2, y2, z2;

    public float angle;
    public float signedAngleX;
    public float signedAngleY;
    public float signedAngleZ;

    // Update is called once per frame
    void Update()
    {
        angle = Vector3.Angle(new Vector3(x1, y1, z1), new Vector3(x2, y2, z2));
        signedAngleX = Vector3.SignedAngle(new Vector3(x1, y1, z1), new Vector3(x2, y2, z2), Vector3.right);
        signedAngleY = Vector3.SignedAngle(new Vector3(x1, y1, z1), new Vector3(x2, y2, z2), Vector3.up);
        signedAngleZ = Vector3.SignedAngle(new Vector3(x1, y1, z1), new Vector3(x2, y2, z2), Vector3.forward);
        Debug.Log(angle);
    }
}
