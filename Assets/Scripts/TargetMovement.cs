using UnityEngine;
using System.Collections;

public class TargetMovement : MonoBehaviour
{
    public bool shouldMove, shouldRotate;
    public float moveSpeed, rotateSpeed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            transform.position += new Vector3(moveSpeed, 0f, 0f) * Time.deltaTime;
        }
        if (shouldRotate)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, rotateSpeed * Time.deltaTime, 0f));
        }
    }
}
