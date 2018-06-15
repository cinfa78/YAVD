using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newVector3Value", menuName ="YAVD/Vector3 Value")]
public class SVector3Value : ScriptableObject {
    public Vector3 Value;
    public void SetX(float newX)
    {
        Value = new Vector3(newX, Value.y, Value.z);        
    }
    public void SetY(float newY)
    {
        Value = new Vector3(Value.x, newY, Value.z);
    }
    public void SetZ(float newZ)
    {
        Value = new Vector3(Value.x, Value.y, newZ);
    }
}
