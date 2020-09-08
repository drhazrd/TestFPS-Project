using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool playerFreeze;
    public PlayerController[] currPlayers;
    int numPlayers;

    void Awake()
    {
        instance = this;
    }
    public float respawnWait;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerFreeze = false;

    }
    void Update()
    {
        
    }
    public void PlayerDied()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator PlayerDeath()
    {
        yield return new WaitForSeconds(respawnWait);
    }

    public void PlayerRespawn()
    {

    }
    
}
