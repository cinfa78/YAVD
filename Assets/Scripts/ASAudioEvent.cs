﻿using UnityEngine;

public abstract class ASAudioEvent : ScriptableObject {
    public abstract void Play(AudioSource source);
    public abstract void Play(AudioSource source, int clipNumber = 0);
}
