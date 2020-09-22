using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System;

public class GunController : MonoBehaviour
{
    //GameObject Variables

    public Transform firePoint;
    public GameObject bullet, muzzleFireEffect;
    public PlayerController playerCtrl;

    //Shot Tracking Variables
    public float gunHitDistance;
    Transform localGunTransform;

    //Firing Variables
    public bool isFiring, canAutoFire;
    public float fireRate,zoomValue;
    [HideInInspector]
    public float fireCounter;

    //Ammo Variables
    public int currentAmmo, currentAmmoHolder, maxAmmo, reloadAmount, pickupAmt;
    public float reloadTimer;
    public Text ammoText;
    public bool isAmmoPouch;

    //Weapon Held Variables
    public bool isActive, isHeld;
    public string gunName;


    // Use this for initialization
    void Start()
    {
        gunName = this.gameObject.name;
        localGunTransform = firePoint.transform;        
        currentAmmo = reloadAmount;
        currentAmmoHolder = maxAmmo;
        isAmmoPouch = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("JoyFire1") > 0.1f || Input.GetButtonDown("JoyFire2"))
        {
            isFiring = true;
        }
        else
        {
            isFiring = false;
        }

        if (fireCounter > 0)
        {
            fireCounter -= Time.deltaTime;
        }
        muzzleFireEffect.SetActive(false);
        
        //Single Shots
        if (Input.GetMouseButton(0) && fireCounter<=0 || Input.GetAxis("JoyFire1") > 0.1f && fireCounter <= 0 || Input.GetButtonDown("JoyFire2") && fireCounter <= 0)
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
            FireShot();
        }

        //Automatic Shots
        if (Input.GetMouseButtonDown(0) && canAutoFire || Input.GetAxis("JoyFire1") > 0.1f && canAutoFire || Input.GetAxis("JoyFire2") > 0.1f && canAutoFire)
        {
            if (fireCounter <= 0)
            {
                FireShot();
            }
        }

        //Weapon Zoom
        if(Input.GetMouseButtonDown(1))
        {
            CameraController.instance.ZoomIn(zoomValue);
        }
        if (Input.GetMouseButtonUp(1))
        {
            CameraController.instance.ZoomOut();
        }

        //Text and UI info
        if (!isAmmoPouch)
        {
            ammoText.text = currentAmmo.ToString() + " / " + currentAmmoHolder.ToString();
        }
        else if (isAmmoPouch)
        {
            ammoText.text = currentAmmo.ToString();
        }
    }
    public void FireShot()
    {
        if (currentAmmo == 0 && currentAmmoHolder == 0)
        {
            Debug.Log("At leaset this works!");
            return;
        }
        else if (currentAmmo <= 0)
        {
            Debug.Log("Reloading in " + reloadTimer.ToString() + " seconds");
            StartCoroutine(Reload(reloadTimer));
            isFiring = false;
        }
        else if (currentAmmo > 0)
        {
            currentAmmo--;
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            fireCounter = fireRate;
            muzzleFireEffect.SetActive(true);
            isFiring = true;
            Debug.Log("Fire!");
        }
    }
    public void GetAmmo()
    {
        if (currentAmmoHolder >= maxAmmo)
        {
            currentAmmoHolder = maxAmmo;
        }
        else
        {
            currentAmmoHolder += pickupAmt;
        }
    }

    public IEnumerator Reload(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);
        if (currentAmmo == 0)
        {
            currentAmmo = reloadAmount;
            currentAmmoHolder -= reloadAmount;
        }
        else
        {
            yield return null;
        }
    }
}
