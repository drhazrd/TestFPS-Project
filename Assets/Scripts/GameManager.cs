using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ThorQuestSystem thor;
    public OdinEventSystem odin;
    public bool playerFreeze;
    //public PlayerController[] allActivePlayers;
    public List<PlayerController> currPlayers = new List<PlayerController>();
    public List<EnemyHealthController> currentEnemies = new List<EnemyHealthController>();
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
        thor = GetComponent<ThorQuestSystem>();
        odin = GetComponent<OdinEventSystem>();
        Cursor.lockState = CursorLockMode.Locked;
        playerFreeze = false;
        ScanForPlayers(.1f);
        FindEnemies();
    }
    void Update()
    {
        
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
    }
    public IEnumerator ScanForEnemies(float scanInterval)
    {
        Debug.Log("Scanning...");
        yield return new WaitForSeconds(scanInterval);
        this.currPlayers.Clear();
        currentEnemies.AddRange(FindObjectsOfType<EnemyHealthController>());
    }
    public void FindEnemies()
    {

        currentEnemies.AddRange(FindObjectsOfType<EnemyHealthController>());
    }
}
