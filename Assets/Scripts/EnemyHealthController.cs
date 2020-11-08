using UnityEngine;
using System.Collections;

public class EnemyHealthController : MonoBehaviour
{
    public int currentHealth = 5;
    public EnemyMovement theEC;

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
        if (theEC != null)
        {
            theEC.GetShot();
        }
        if (currentHealth < 0)
        {
            AudioManager.instance.PlaySFX(2);
            GameManager.instance.currentEnemies.Remove(this.gameObject.GetComponent<EnemyHealthController>());
            Destroy(gameObject);
        }
    }
}
