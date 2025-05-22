using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector] public PlayerController Controller;
    [HideInInspector] public PlayerRaycaster Raycaster;


    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
} 