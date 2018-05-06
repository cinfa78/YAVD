using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBool", menuName = "YAVD/Bool Value")]
public class SBoolValue : ScriptableObject
{
    public bool Value;
    public SBoolValue(bool v)
    {
        Value = v;
    }

    public override string ToString()
    {
        return Value.ToString(); 
    }
}
