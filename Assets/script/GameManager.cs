using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager 
{
    public event Action<Player> OnLocalPlayerJoined;
    private GameObject gameObject;

    private static GameManager m_Instance;
    public static GameManager Instance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = new GameManager();
                m_Instance.gameObject = new GameObject("_GameManager");
                m_Instance.gameObject.AddComponent<InputController>();
                m_Instance.gameObject.AddComponent<Timer>();
                m_Instance.gameObject.AddComponent<Spawner>();
                m_Instance.gameObject.AddComponent<Inventory>();
                m_Instance.gameObject.AddComponent<ItemManager>();
            }
            return m_Instance;
        }
    }

    private InputController m_InputController;
    public InputController inputController
    {
        get
        {
            if(m_InputController == null)
            {
                m_InputController = gameObject.GetComponent<InputController>();

            }
            return m_InputController;
        }

    }

    private Timer m_Timer;
    public Timer Timer
    {
        get
        {
            if (m_Timer == null)
            {
                m_Timer = gameObject.GetComponent<Timer>();
            }
            return m_Timer;
        }
    }

    private Spawner m_Spawner;
    public Spawner Spawner
    {
        get
        {
            if (m_Spawner == null)
            {
                m_Spawner = gameObject.GetComponent<Spawner>();
            }
            return m_Spawner;
        }
    }

    private Inventory m_Inventory;
    public Inventory Inventory
    {
        get
        {
            if(m_Inventory == null)
            {
                m_Inventory = gameObject.GetComponent<Inventory>();
            }
            return m_Inventory;
        }

    }

    private ItemManager m_ItemManager;
    public ItemManager ItemManager
    {
        get
        {
            if(m_ItemManager == null)
            {
                m_ItemManager = gameObject.GetComponent<ItemManager>();
            }
            return m_ItemManager;
        }
    }

    private Player m_LocalPlayer;
    public Player LocalPlayer
    {
        get
        {
            return m_LocalPlayer;
        }
        set
        {
            m_LocalPlayer = value; 
            if(OnLocalPlayerJoined != null)
            {
                OnLocalPlayerJoined(m_LocalPlayer);
            }
        }
    }
}
