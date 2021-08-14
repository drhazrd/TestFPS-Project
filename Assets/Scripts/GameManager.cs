using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ThorQuestSystem thor;
    public OdinEventSystem odin;
    public bool playerFreeze, levelEnding;
    //public PlayerController[] allActivePlayers;
    public GameObject[] m_EnemyPrefabs;
    public EnemyManager[] m_Enemies;

    public List<PlayerController> currPlayers = new List<PlayerController>();
    public List<EnemyHealthController> currentEnemies = new List<EnemyHealthController>();
    public List<Transform> wayPointsForAI;
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
        levelEnding = false;
        ScanForPlayers(.1f);
        FindEnemies();
        EnemySpawn();
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
    public void EnemySpawn()
    {
        for (int i = 0; i < m_Enemies.Length; i++)
        {
            m_Enemies[i].m_Instance = Instantiate(m_EnemyPrefabs[i], m_Enemies[i].m_SpawnPoint.position, m_Enemies[i].m_SpawnPoint.rotation) as GameObject;
            m_Enemies[i].m_PlayerNumber = i + 1;
            if (m_Enemies[i].isAI)
            {
                m_Enemies[i].SetupAI(wayPointsForAI);
            } 
            else
            {
                return;
            //    m_Enemies[i].SetupPlayer(m_Camera);
            }
        }
    }
}
