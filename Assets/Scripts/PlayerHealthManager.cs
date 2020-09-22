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
    public int maxArmor;
    public int currentArmor;
    //public Text healthText;
    //public Text armorText;
    public float invincibleLength;
    public float invincibleCounter;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentArmor = maxArmor;
        UIController.instance.armorSlider.maxValue = maxArmor;
        UIController.instance.armorSlider.value = currentArmor;

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
        UIController.instance.armorSlider.value = currentArmor;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = "Health: " + currentHealth + "/" + maxHealth;
    }
    public void DamagePlayer(int damageAmt) {
        if (invincibleCounter <= 0) {
            if (currentArmor <= 0)
            {
                currentArmor = 0;
                currentHealth -= damageAmt;

                UIController.instance.ShowDamage();
                AudioManager.instance.PlaySFX(7);


                if (currentHealth <= 0)
                {
                    gameObject.SetActive(false);
                    currentHealth = 0;
                    GameManager.instance.PlayerDied();
                    AudioManager.instance.PlaySFX(6);

                }
            }
            else
            {
                currentArmor -= damageAmt;
            }
            invincibleCounter = invincibleLength;
        }
    }
    public void HealPlayer(int healedAmt)
    {
        AudioManager.instance.PlaySFX(5);
        currentHealth += healedAmt;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    public void ArmorRepair(int repairedAmt)
    {
        currentHealth +=repairedAmt;
         if (currentArmor > maxArmor)
        {
            currentArmor = maxArmor;
        }

    }
}
