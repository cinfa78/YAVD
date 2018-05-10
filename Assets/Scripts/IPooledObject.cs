﻿using UnityEngine;

public interface IPooledObject {
    void OnSpawn();
    void OnDespawn();
}
