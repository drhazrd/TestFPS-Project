using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public bool collected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"&& !collected)
        {
            collected = true;
            Destroy(gameObject);
        }
    }
}
