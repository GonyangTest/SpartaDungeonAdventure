using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public List<ItemData> Items;

    public void Start()
    {
        GameManager.Instance.PlayerInventory = this;
    }

    public void AddItem(ItemData itemData)
    {
        Items.Add(itemData);
    }

    public void RemoveItem(ItemData itemData)
    {
        Items.Remove(itemData);
    }

    public void UseItem(ItemData itemData)
    {
        ItemManager.Instance.UseItem(itemData);
        RemoveItem(itemData);
    }

    public List<ItemData> GetItems()
    {
        return Items;
    }
} 