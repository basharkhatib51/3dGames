using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action<int> playerSeenHandler;
    public static EventManager instance;
    public List<BotManager> registeredObjects = new List<BotManager>();
    // Start is called before the first frame update
    void Awake()
    {
        instance = this; 
    }


    public void OnPLayerSeenHandler(int id) {
        playerSeenHandler?.Invoke(id);
    }
}
