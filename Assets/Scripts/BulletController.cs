using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed, lifeTime;
    public int bulletDamage;
    public Rigidbody theRB;
    public GameObject impactEffect;
    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        theRB.velocity = transform.forward * moveSpeed;
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<EnemyHealthController>().DealDamage(bulletDamage);
        }

        Destroy(gameObject);
        Instantiate(impactEffect, transform.position + (transform.forward*(-moveSpeed* Time.deltaTime)), transform.rotation);
    }
}
