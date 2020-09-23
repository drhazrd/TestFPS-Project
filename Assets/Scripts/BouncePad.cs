using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounceAmt;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerController>().Bounce(bounceAmt);
            AudioManager.instance.PlaySFX(0);
        }
    }
}
