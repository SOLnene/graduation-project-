using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : Interactable
{
    

    public override void OnInteract()
    {
        base.OnInteract();
        GameManager.Instance.Spawner.InitNewGame();
        print("goto next level");
    }
}
