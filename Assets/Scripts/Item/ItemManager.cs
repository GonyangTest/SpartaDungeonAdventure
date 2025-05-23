using UnityEngine;
using System.Collections.Generic;

public class ItemManager : Singleton<ItemManager>
{
    public void UseItem(ItemData itemData)
    {
        foreach(ItemEffect effect in itemData.Effects)
        {
            GameManager.Instance.Controller.StartItemEffectCoroutine(effect.Effect());
        }
    }
}

