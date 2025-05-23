using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.ExceptionServices;

public class Chest : MonoBehaviour, IInteractable
{
    public string Name => "����";
    private string _interactionPrompt = "E�� ���� ����";
    public string InteractionPrompt { get => _interactionPrompt; set => _interactionPrompt = value; }
    public string RewardPrompt = "";
    public bool isOpen = false;
    public List<ItemData> Items;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        for(int i = 0; i < Items.Count; i++)
        {
            RewardPrompt += $"{Items[i].name}\n";
        }
    }

    public void AddItem(ItemData item)
    {
        Items.Add(item);
    }
    public void RemoveItem(ItemData item)
    {
        Items.Remove(item);
    }
    
    public void Interact()
    {
        if (isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    public void Open()
    {
        if(!isOpen)
        {
            _animator.SetTrigger("open");
            isOpen = true;
            InteractionPrompt = "E�� ���� �ݱ�";
            UIManager.Instance.OpenChestInventory();
        }
    }

    public void Close()
    {
        if(isOpen)
        {
            _animator.SetTrigger("close");
            isOpen = false;
            InteractionPrompt = "E�� ���� ����";
            UIManager.Instance.CloseChestInventory();
        }
    }
}
