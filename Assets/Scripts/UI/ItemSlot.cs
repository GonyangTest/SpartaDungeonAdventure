using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ItemSlot : MonoBehaviour
{
    public ItemData ItemData;
    [SerializeField] private Image Icon;
    private Button _button;
    public UIChestInventory ChestInventory;
    public UIPlayerInventory PlayerInventory;
    public int Index;

    public void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void Set()
    {
        Icon.gameObject.SetActive(true);
        Icon.sprite = ItemData.Icon;
        // QuantityText.text = Quantity > 1 ? Quantity.ToString() : "";
    }

    public void Clear()
    {
        ItemData = null;
        Icon.gameObject.SetActive(false);
        // QuantityText.text = "";
    }

    public void OnClickButton()
    {
        if(ChestInventory != null)
        {
            ChestInventory.SelectItem(Index);
        }
        else if(PlayerInventory != null)
        {
            PlayerInventory.SelectItem(Index);
        }
    }
}
