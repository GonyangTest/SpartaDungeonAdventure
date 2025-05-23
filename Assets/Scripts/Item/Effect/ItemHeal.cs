using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Item/Effect/HealEffect")]
public class ItemHealth : ItemEffect
{
    public float Interval;
    
    public override void ApplyEffect()
    {
        PlayerController player = GameManager.Instance.Controller;
        player.Heal((int)effectValue);
    }
    
    public override IEnumerator Effect()
    {
        int count = (int)(duration / Interval);
        while(count > 0)    
        {
            ApplyEffect();
            yield return new WaitForSeconds(Interval);
            count--;
        }
    }
} 