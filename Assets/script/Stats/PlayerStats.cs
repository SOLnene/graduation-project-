using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Destructable
{
    // Start is called before the first frame update
    void Start()
    {
        //GameManager.Instance.ItemManager.onItemGet += OnItemGet;
    }

    public void OnItemGet(Item item)
    {
        //if (item != null) { }
        armor.AddModifier(item.ammorModifier);
        damage.AddModifier(item.damageModifier);
        speed.AddModifier(item.speedModifier);
        projectile.damage = damage.GetPlayerValue();
        print("armor"+armor.GetPlayerValue() + "damage"+damage.GetPlayerValue() +"speed"+ speed.GetPlayerValue());
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        damageTaken += amount - armor.GetPlayerValue();
        damageTaken = Mathf.Clamp(damageTaken, 0, int.MaxValue);
        print("playerReaming:" + HitPointRemaining);
    }

    public override void Die()
    {
        base.Die();
        print("u die");
    }
}
