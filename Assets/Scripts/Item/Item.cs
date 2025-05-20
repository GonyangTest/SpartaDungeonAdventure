using UnityEngine;
using System.Collections.Generic;

public class Item : MonoBehaviour
{
    public ItemData itemData;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Item Trigger Enter");
        if(other.gameObject.CompareTag("Player"))
        {
            UseItem();
            Destroy(gameObject);
        }
    }

    public void UseItem()
    {
        foreach(ItemEffect effect in itemData.effects)
        {
            PlayerManager.Instance.Controller.StartItemEffectCoroutine(effect.Effect());
        }
    }
}

