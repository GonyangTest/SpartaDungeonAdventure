using UnityEngine;

public abstract class ItemEffect : ScriptableObject
{
    public string effectName;
    public string description;
    public float effectValue;
    public float duration;

    public abstract void ApplyEffect(PlayerController player);
} 