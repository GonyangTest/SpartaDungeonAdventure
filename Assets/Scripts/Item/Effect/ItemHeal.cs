using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Item/HealEffect")]
public class ItemHealth : ItemEffect
{
    
    public override void ApplyEffect()
    {
        PlayerController player = GameManager.Instance.Controller;
        player.Heal(effectValue);
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