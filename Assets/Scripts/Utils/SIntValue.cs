using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInt", menuName = "YAVD/Int Value")]
public class SIntValue : ScriptableObject
{

    public int Value;
    public SIntValue(int v)
    {
        Value = v;
    }
    public static SIntValue operator +(SIntValue f1, SIntValue f2)
    {
        return new SIntValue(f1.Value + f2.Value);
    }
    public override string ToString()
    {
        return Value.ToString(); 
    }
}
