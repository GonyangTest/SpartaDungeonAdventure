using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    public PlayerController Controller;
    public PlayerRaycaster Raycaster;


    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
} 