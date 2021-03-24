using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    Transform playerTransform;
    [SerializeField] bool followPlayer;
    void Start()
    {
    }

    public override void OnEnable()
    {
        base.OnEnable();
        playerTransform = GameManager.Instance.LocalPlayer.transform.Find("EnemyAimingPosition");
        gameObject.transform.LookAt(playerTransform);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
