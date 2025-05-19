using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Item/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite icon;
    public ItemType itemType;
    public List<ItemEffect> effects;
}

public enum ItemType
{
    Equipable,
    Consumable
} 