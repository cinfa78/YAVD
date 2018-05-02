using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASEnemyBrain : ScriptableObject {
    public abstract void Initialize(GameObject GO);
    public abstract void Think(GameObject GO);
}
