using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile:IPooledObject {
    void OnHit();
    void OnDeflect();
}
