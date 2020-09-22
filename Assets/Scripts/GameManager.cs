using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool playerFreeze;
    //public PlayerController[] allActivePlayers;
    public List<PlayerController> currPlayers = new List<PlayerController>();
    int numPlayers;
    public static bool xAxisFlag, yAxisFlag, useController;
    public float timeToScan = 10f;

    void Awake()
    {
        instance = this;
    }
    public float respawnWait;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerFreeze = false;
        ScanForPlayers(.1f);
    }
    void Update()
    {
        ScanForPlayers(timeToScan);
    }
    public void PlayerDied()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManager.instance.StopBGM();
    }

    public IEnumerator PlayerDeath()
    {
        yield return new WaitForSeconds(respawnWait);
    }

    public void PlayerRespawn()
    {

    }
    public void AddPlayer()
    {

    }
    public IEnumerator ScanForPlayers(float scanInterval)
    {
        Debug.Log("Scanning...");
        yield return new WaitForSeconds(scanInterval);
        this.currPlayers.Clear();
        var myPlayer = FindObjectsOfType<PlayerController>();
        foreach ( PlayerController i in myPlayer) { this.currPlayers.Add(myPlayer[0]); }
    }
}
