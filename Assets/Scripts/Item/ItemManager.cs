using UnityEngine;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
    private Dictionary<string, Item> items = new Dictionary<string, Item>();

    public void CreateItem(Item item) { }
    public void RemoveItem(Item item) { }
} 