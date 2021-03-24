using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;
     [SerializeField] protected float timeToLive;
    [SerializeField] public ParticleSystem onAppear;
    [SerializeField] public ParticleSystem onHit;
    public float damage;

    void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    public virtual void OnEnable()
    {
        if (onAppear)
        {
            Destroy(Instantiate(onAppear.gameObject, transform.position, Quaternion.identity) as GameObject,onAppear.startLifetime);
        }
    }
    public void OnDestroy()
    {
        if (onHit)
        {
            Destroy(Instantiate(onHit.gameObject, transform.position, Quaternion.identity) as GameObject, onHit.startLifetime);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var destructable = other.transform.GetComponent<Destructable>();
        if(destructable == null)
        {
            return;
        }

        destructable.TakeDamage(damage);
        Destroy(gameObject);
    }
}
