using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIChestInventory : MonoBehaviour
{
   [Header("Slot")]
   private ItemSlot[] ItemSlots;
   public Transform _slotPanel;

   [Header("Selected Item")]
   public TextMeshProUGUI SelectedItemName;
   public TextMeshProUGUI SelectedItemDescription;
   
   public GameObject InventoryWindow;

   public GameObject GetButton;

   private Chest _chest;

   private ItemData _selectedItem;
   private int _selectedItemIndex;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        InventoryWindow.SetActive(false);

        ItemSlots = new ItemSlot[_slotPanel.childCount];

        for(int i = 0; i < _slotPanel.childCount; i++)
        {
            ItemSlots[i] = _slotPanel.GetChild(i).GetComponent<ItemSlot>();
            ItemSlots[i].Index = i;   
            ItemSlots[i].ChestInventory = this;
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

        GetButton.SetActive(false);
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
        for(int i = 0; i < _chest.Items.Count; i++)
        {
            ItemSlots[i].ItemData = _chest.Items[i];
        }
    }


    public void SelectItem(int index)
    {
        if(ItemSlots[index].ItemData == null) return;

        _selectedItem = ItemSlots[index].ItemData;
        _selectedItemIndex = index;

        SelectedItemName.text = _selectedItem.ItemName;
        SelectedItemDescription.text = _selectedItem.Description;
        GetButton.SetActive(true);
    }

    public void OnGetButton()
    {
        if(_selectedItem == null) return;
        GameManager.Instance.PlayerInventory.AddItem(_selectedItem);
        RemoveSelectedItem();
        ClearSelectedWindow();
    }

    public void RemoveSelectedItem()
    {

        ItemSlots[_selectedItemIndex].ItemData = null;
        _selectedItemIndex = -1;
        _chest.RemoveItem(_selectedItem);
        UpdateUI();
    }

    public void OpenInventory(Chest chest)
    {
        _chest = chest;
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
    