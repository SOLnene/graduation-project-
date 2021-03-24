using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Item item;
    public PlayerStats playerStats;
    private void OnTriggerEnter(Collider other)
    {

        if(other.tag!= "Player")
        {
            return;
        }
        playerStats = other.GetComponent<PlayerStats>();
        Pickup(item);
    }

    public virtual void OnPickUp(Item item)
    {
        GameManager.Instance.Inventory.Add(item);
        playerStats.OnItemGet(item);
        
        
    }

    void Pickup(Item item)
    {
        print("picking" + item.name);
        OnPickUp(item);
        Destroy(gameObject);
;    }
}
