using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject shell;
    public float rangeToTarget, timeBetweenShots = .5f, shotAccuracy = 1f, rotateSpeed;
    private float shotCounter;
    public Transform turretBody, firepoint;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = timeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToTarget)
        {
            turretBody.LookAt(PlayerController.instance.transform.position + new Vector3(0,shotAccuracy,0));
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                Instantiate(shell, firepoint.position, firepoint.rotation);
                shotCounter = timeBetweenShots;
            }
        }
        else
        {
            shotCounter = timeBetweenShots;
            turretBody.rotation = Quaternion.Lerp(turretBody.rotation, Quaternion.Euler(0f, turretBody.rotation.eulerAngles.y + 10f, 0), rotateSpeed* Time.deltaTime);
        }
    }
}
