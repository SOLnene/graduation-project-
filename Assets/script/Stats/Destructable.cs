using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Destructable : MonoBehaviour
{
    public float maxHitPoints;
    public Slider Hpbar;

    public Stat damage;
    public Stat armor;
    public Stat speed;

    public Projectile projectile;

    public event System.Action OnDeath;
    public event System.Action OnDamagerReceived;

    public float damageTaken;

    private void Start()
    {
        damageTaken = 0;
        Hpbar.maxValue = maxHitPoints;
    }

    public float HitPointRemaining
    {
        get
        {
            return maxHitPoints - damageTaken;
        }
    }

    public bool IsAlive
    {
        get
        {
            return HitPointRemaining > 0;
        }
    }

    public virtual void Die()
    {
        if (!IsAlive)
        {
            return;
        }

        if(OnDeath != null)
        {
            OnDeath();
        }
    }

    public virtual void TakeDamage(float amount)
    {
        

        if(OnDamagerReceived != null)
        {
            OnDamagerReceived();
        }

        if(HitPointRemaining <= 0)
        {
            Die();
        }
    }

    public void Reset()
    {
        damageTaken = 0;        
    }
}
