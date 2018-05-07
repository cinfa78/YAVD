using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SMonsterBrain : ScriptableObject {

    public abstract void Think(Monster monster);

    public abstract float Damage(Monster monster, float damage, Vector3 from);
}
