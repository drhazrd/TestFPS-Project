﻿using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    public ParticleSystem fireEffect;
    public PlayerController playerCam;
    public float gunHitDistance;
    public Transform localGunTransform;
    // Use this for initialization
    void Start()
    {
        fireEffect = GetComponentInChildren<ParticleSystem>();
        localGunTransform = playerCam.camTrans;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(localGunTransform.position, localGunTransform.forward, out hit, gunHitDistance))
            {
                if (Vector3.Distance(localGunTransform.position, hit.point) > 2f)
                {
                    firePoint.LookAt(hit.point);
                }
            }
            else
            {
                firePoint.LookAt(localGunTransform.position + (localGunTransform.forward * 30f ));
            }

            Instantiate(bullet, firePoint.position, firePoint.rotation);
            fireEffect.Play();
        }
    }
}