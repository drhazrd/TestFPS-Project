using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestTrigger : MonoBehaviour
{
    public Quest quest;
    public PlayerController player;
    public GameObject questWindow;
    ThorQuestSystem thor;
    public bool inQuestArea;
    public string ifActive;
    public Text questTitleText;
    public Text questDesriptionText;
    public Text questExpText;
    public Text questScoreText;
    public ParticleSystem[] questGFX;

    private void Start()
    {
        questGFX = GetComponentsInChildren<ParticleSystem>();
        thor = FindObjectOfType<ThorQuestSystem>();
    }
    private void Update()
    {
        quest.goal.currentAmount = thor.questCurrentCollectableAmount;

        if (Input.GetKeyDown(KeyCode.Q) && inQuestArea && this.quest.isActive == false)
        {
            AcceptQuest();

        }
        else if (Input.GetKeyDown(KeyCode.Q) && inQuestArea && this.quest.isActive == true && this.quest.goal.isReached())
        {
            StartCoroutine("ReturnQuest");
        }

        if(this.quest.goal.isReached())
        {
            ifActive = " (Completed)";
        }
        else if (this.quest.isActive)
        {
            ifActive = " (Active)";
        } 
        else
        {
            ifActive = "";
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        inQuestArea = true;
        if (!this.quest.isActive && this.inQuestArea && !player.isQuesting || this.quest.isActive && this.inQuestArea && player.isQuesting)
        {
            OpenQuestWindow();
        }
        else
        {
            return;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        CloseQuestWindow();
        inQuestArea = false;
    }

    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        UpdateQuestWindow();
    }
    public void UpdateQuestWindow()
    {
        questTitleText.text = quest.questTitle + ifActive;
        questDesriptionText.text = quest.questDesription;
        questExpText.text = quest.questExp.ToString();
        questScoreText.text = quest.questExp.ToString();
    }
    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        quest.isActive = true;
        player.quest = quest;
        player.isQuesting = true;
    }
    public void CloseQuestWindow()
    {
        questWindow.SetActive(false);
    }
    public void NoNewQuest()
    {
        player.quest = quest;
    }
    public IEnumerator ReturnQuest()
    {
        this.quest.QuestComplete();
        player.isQuesting = false;
        thor.questCurrentCollectableAmount = 0;
        for (int i = 0; i < questGFX.Length; i++)
        {
            questGFX[i].Stop();
        }
        yield return new WaitForSeconds(.25f);
        questWindow.SetActive(false);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
