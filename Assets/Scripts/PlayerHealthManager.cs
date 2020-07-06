using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    public static PlayerHealthManager instance;

    // Start is called before the first frame update
    public int maxHealth;
    public int currentHealth;
    public Text healthText;
    public float invincibleLength;
    public float invincibleCounter;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = "Health: " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
        }
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = "Health: " + currentHealth.ToString();
    }
    public void DamagePlayer(int damageAmt) {
        if (invincibleCounter <= 0) {
            currentHealth -= damageAmt;

            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
                currentHealth = 0;
                GameManager.instance.PlayerDeath();
            }

            invincibleCounter = invincibleLength;
        }
    }
}
