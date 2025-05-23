using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIPlayerInventory : MonoBehaviour
{
   [Header("Slot")]
   private ItemSlot[] ItemSlots;
   public Transform _slotPanel;

   [Header("Selected Item")]
   public TextMeshProUGUI SelectedItemName;
   public TextMeshProUGUI SelectedItemDescription;
   
   public GameObject InventoryWindow;

   private ItemData _selectedItem;
   private int _selectedItemIndex;

   public GameObject UseButton;
   public GameObject EquipButton;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        InventoryWindow.SetActive(false);

        ItemSlots = new ItemSlot[_slotPanel.childCount];

        for(int i = 0; i < _slotPanel.childCount; i++)
        {
            ItemSlots[i] = _slotPanel.GetChild(i).GetComponent<ItemSlot>();
            ItemSlots[i].Index = i;   
            ItemSlots[i].PlayerInventory = this;
        }

        ClearSelectedWindow();
    }

    private void OnEnable()
    {
        ClearSelectedWindow();
    }

    private void ClearSelectedWindow()
    {
        SelectedItemName.text = "";
        SelectedItemDescription.text = "";

        UseButton.SetActive(false);
        EquipButton.SetActive(false);
    }    

    void UpdateUI()
    {
        for(int i = 0; i < ItemSlots.Length; i++)
        {
            if(ItemSlots[i].ItemData != null)
            {
                ItemSlots[i].Set();
            }
            else
            {
                ItemSlots[i].Clear();
            }
        }
    }
    private void SetItem()
    {
        List<ItemData> items = GameManager.Instance.PlayerInventory.GetItems();
        for(int i = 0; i < items.Count; i++)
        {
            ItemSlots[i].ItemData = items[i];
        }
    }


    public void SelectItem(int index)
    {
        if(ItemSlots[index].ItemData == null) return;

        _selectedItem = ItemSlots[index].ItemData;
        _selectedItemIndex = index;

        SelectedItemName.text = _selectedItem.ItemName;
        SelectedItemDescription.text = _selectedItem.Description;

        UseButton.SetActive(_selectedItem.ItemType == ItemType.Consumable);
        EquipButton.SetActive(_selectedItem.ItemType == ItemType.Equipable);
    }

    public void OnUseButton()
    {
        if(_selectedItem == null) return;
        GameManager.Instance.PlayerInventory.UseItem(_selectedItem);
        RemoveSelectedItem();
        ClearSelectedWindow();
    }

    public void RemoveSelectedItem()
    {

        ItemSlots[_selectedItemIndex].ItemData = null;
        _selectedItemIndex = -1;
        UpdateUI();
    }

    public void Toggle()
    {
        if(InventoryWindow.activeSelf)
        {
            CloseInventory();
        }
        else
        {
            OpenInventory();
        }
    }

    public void OpenInventory()
    {
        SetItem();
        UpdateUI();
        InventoryWindow.SetActive(true);
        CursorToggle();
    }

    public void CloseInventory()
    {
        InventoryWindow.SetActive(false);
        CursorToggle();
    }

    void CursorToggle()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
    