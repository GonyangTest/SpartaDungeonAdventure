using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector] public PlayerController Controller;
    [HideInInspector] public PlayerRaycaster Raycaster;
    [HideInInspector] public PlayerInventory PlayerInventory;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
} 