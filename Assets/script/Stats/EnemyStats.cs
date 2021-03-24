using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyStats : Destructable
{
    public float attackInterval;
    Transform cam;

    public System.Action OnEnenyStatChange;
    private void Start()
    {
        cam = GameObject.Find("Main Camera").transform;
        Hpbar = gameObject.GetComponentInChildren<Slider>();
    }
    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }

    private void LateUpdate()
    {
        Hpbar.transform.LookAt(transform.position + cam.forward);
        Hpbar.transform.rotation = cam.transform.rotation;
    }

    private void OnEnable()
    {
        Reset();
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        damageTaken += amount - armor.GetEnemyValue();
        damageTaken = Mathf.Clamp(damageTaken, 0, int.MaxValue);
        print("reaming:" + HitPointRemaining);

    }
    public void enemyIntensify()
    {
        
    }


}
