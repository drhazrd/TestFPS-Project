using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed, lifeTime;
    public int bulletDamage;
    public Rigidbody theRB;
    public GameObject impactEffect;
    public bool damageEnemy, damagePlayer;
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
        if (other.gameObject.tag == "Enemy" && damageEnemy)
        {
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<EnemyHealthController>().DealDamage(bulletDamage);
            Instantiate(impactEffect, transform.position + (transform.forward*(-moveSpeed* Time.deltaTime)), transform.rotation);
        }
        if (other.gameObject.tag == "Player" && damagePlayer)
        {
            Debug.Log(other.name + " was Hit by " + gameObject.name);
            PlayerHealthManager.instance.DamagePlayer(bulletDamage);
        }
        Destroy(gameObject);
    }
}
