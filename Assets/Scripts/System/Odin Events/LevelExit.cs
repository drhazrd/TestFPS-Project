using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelExit : MonoBehaviour
{
    public string nextLevel;
    public float waitToEndLevel;
    public GameManager gM;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(EndLevelCo());
            AudioManager.instance.PlayVictory();
        }
    }
    private IEnumerator EndLevelCo()
    {
        PlayerPrefs.SetString(nextLevel + "_cp", "");
        PlayerPrefs.SetString("CurrentLevel",nextLevel);
        gM.levelEnding = true;
        yield return new WaitForSeconds(waitToEndLevel);
        SceneManager.LoadScene(nextLevel);

    }
}
