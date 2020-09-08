using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPickup : MonoBehaviour
{
    public int repairAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthManager.instance.ArmorRepair(repairAmount);
            Destroy(gameObject);
        }
    }
}
