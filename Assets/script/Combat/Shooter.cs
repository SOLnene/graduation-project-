using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] float rateOfFire;
    [SerializeField] Projectile projectile;
    
    [SerializeField]public Transform hand;

    public PlayerStats playerStats;

    private WeaponReloader reloader;

    private Transform muzzle;

    float nextFireAllowed;

    public bool canFire;
    void Awake()
    {
        muzzle = transform.Find("Muzzle");
        reloader = GetComponent<WeaponReloader>();
        transform.SetParent(hand);
        playerStats = gameObject.transform.GetComponentInParent<PlayerStats>();
        projectile.damage = playerStats.damage.GetPlayerValue();
    }
       
    public void Reload()
    {
        if(reloader == null)
        {
            return;
        }
        reloader.Reload();
    }

    
    public virtual void Fire()
    {
        
        canFire = false;

        if (Time.time < nextFireAllowed)
        {
            
            return;
        }

        if (reloader != null)
        {
            if (reloader.IsReloading)
            {
                return;
            }
            if(reloader.RoundsReamingInClip == 0)
            {
                return;
            }

            reloader.TakeFromClip(1);        }

        nextFireAllowed = Time.time + rateOfFire;
        Instantiate(projectile, muzzle.position, muzzle.rotation);
        print("damage:"+projectile.damage);

        print("firing" + Time.time);
        canFire = true;
    }
}
