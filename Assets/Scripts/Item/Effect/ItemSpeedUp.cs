using UnityEngine;
using System.Collections;


[CreateAssetMenu(menuName = "Item/SpeedUpEffect")]
public class ItemSpeedUp : ItemEffect
{
    public override void ApplyEffect()
    {
        PlayerController player = PlayerManager.Instance.Controller;
        player.MoveSpeed += effectValue;
    }

    public override void RemoveEffect()
    {
        PlayerController player = PlayerManager.Instance.Controller;
        player.MoveSpeed -= effectValue;
    }
} 