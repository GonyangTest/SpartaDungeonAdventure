using UnityEngine;
using System.Collections;


[CreateAssetMenu(menuName = "Item/SpeedUpEffect")]
public class ItemSpeedUp : ItemEffect
{
    public override void ApplyEffect()
    {
        PlayerController player = GameManager.Instance.Controller;
        player.MoveSpeed += effectValue;
    }

    public override void RemoveEffect()
    {
        PlayerController player = GameManager.Instance.Controller;
        player.MoveSpeed -= effectValue;
    }
} 