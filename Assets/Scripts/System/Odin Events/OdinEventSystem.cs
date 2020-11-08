using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;


public class OdinEventSystem : MonoBehaviour
{
    public static OdinEventSystem current;

    private void Awake()
    {
        current = this;
    }
    public event Action<int> onDoorTriggerEnter;
    public event Action<int> onDoorTriggerExit;

    public void OpenDoorTrigger(int id)
    {
        if (onDoorTriggerEnter != null)
        {
            onDoorTriggerEnter(id);
        }
    }
    public void CloseDoorTrigger(int id)
    {
        if (onDoorTriggerExit != null)
        {
            onDoorTriggerExit(id);
        }
    }
}
