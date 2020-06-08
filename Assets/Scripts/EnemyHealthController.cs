using UnityEngine;
using System.Collections;

public class EnemyHealthController : MonoBehaviour
{
    public int currentHealth = 5; 

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DealDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }
}
