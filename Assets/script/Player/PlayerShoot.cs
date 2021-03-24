using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Shooter assaultRifle;

    void Update()
    {
        if (GameManager.Instance.inputController.Fire1)
        {
            assaultRifle.Fire();
        }
    }
}
