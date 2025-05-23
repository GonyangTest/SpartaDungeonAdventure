using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Item/ItemData")]
public class ItemData : ScriptableObject
{
    public string ItemName;
    public string Description;
    public Sprite Icon;
    public ItemType ItemType;
    public List<ItemEffect> Effects;
}

public enum ItemType
{
    Equipable,
    Consumable
}

