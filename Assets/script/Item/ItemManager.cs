using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item[] defaultItem;


    public delegate void OnItemGet(Item item);
    public OnItemGet onItemGet;

    private void Start()
    {
        
        
    }
}
