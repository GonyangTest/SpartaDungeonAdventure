using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chest : MonoBehaviour, IInteractable
{
    public string Name => "상자";
    public string InteractionPrompt => "E를 눌러 열기";
    public bool isOpen = false;

    [SerializeField] private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void Interact()
    {
        Debug.Log("Interacted with chest");
        if (isOpen)
        {
            _animator.SetTrigger("close");
            isOpen = false;
        }
        else
        {
            _animator.SetTrigger("open");
            isOpen = true;
        }
    }
}
