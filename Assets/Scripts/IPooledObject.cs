﻿using UnityEngine;

public interface IPooledObject {
    void onSpawn();
    void onDespawn();
}
