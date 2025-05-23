using UnityEngine;
using System.Collections;

public abstract class ItemEffect : ScriptableObject
{
    public string effectName;
    public string description;
    public float effectValue;
    public float duration;

    public virtual void ApplyEffect() { }
    public virtual void RemoveEffect() { }
    public virtual IEnumerator Effect()
    {
        ApplyEffect();
        yield return new WaitForSeconds(duration);
        RemoveEffect();
    }
} 