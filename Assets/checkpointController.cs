using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class checkpointController : MonoBehaviour
{

    public string cpName;
    public bool isActive;
    private GameObject graphics;
    public Material[] color;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_cp"))
        {
            if(PlayerPrefs.GetString(SceneManager.GetActiveScene().name+"_cp")== cpName)
            {
                PlayerController.instance.transform.position = transform.position;
                Debug.Log("Player starting at " + cpName);
            }
        }
        rend = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
           // rend.material.color = color[0].color;
            rend.material.SetColor("_EmissionColor", color[0].color);
        }
        else if (isActive)
        {
            //rend.material.color = color[1].color;
            rend.material.SetColor("_EmissionColor", color[1].color);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Lose Level Progress");
            isActive = false;
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", "");

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", cpName);
            isActive = true;
            Debug.Log("Player hit the " + cpName);
        }
    }
}
