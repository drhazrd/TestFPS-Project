﻿using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Text healthText, ammoText;
    public Slider healthSlider;
    public Slider armorSlider;
    public GameObject sprintUI;
    public Image hitUI,blackoutUI;
    public float hitAlpha = .25f, hitFadeSpeed = 2f, blackoutFadeSpeed = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hitUI.color.a != 0)
        {
            hitUI.color = new Color(hitUI.color.r, hitUI.color.g, hitUI.color.b, Mathf.MoveTowards(hitUI.color.a, 0f, hitFadeSpeed * Time.deltaTime));

        }
        if (!GameManager.instance.levelEnding)
        {
            blackoutUI.color = new Color(blackoutUI.color.r, blackoutUI.color.g, blackoutUI.color.b, Mathf.MoveTowards(blackoutUI.color.a, 0f, blackoutFadeSpeed * Time.deltaTime));
        }
        else
        {
            blackoutUI.color = new Color(blackoutUI.color.r, blackoutUI.color.g, blackoutUI.color.b, Mathf.MoveTowards(blackoutUI.color.a, 1f, blackoutFadeSpeed * Time.deltaTime));
        }
    }
    public void ShowDamage()
    {
        hitUI.color = new Color(hitUI.color.r, hitUI.color.g, hitUI.color.b, .25f);
    }
}
