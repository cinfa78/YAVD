using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewFloat", menuName ="YAVD/Float Value")]
public class SFloatValue : ScriptableObject {

    public float Value;
    public SFloatValue(float v)
    {
        Value = v;
    }
    public static SFloatValue operator +(SFloatValue f1, SFloatValue f2)
    {
        return new SFloatValue(f1.Value+f2.Value);
    }
    public override string ToString()
    {
        return Value.ToString();
    }
}
