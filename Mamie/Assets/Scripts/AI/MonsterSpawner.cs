using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using TMPro;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;  // Assign your monster prefab in the inspector
    public Transform[] spawnPoints;   // Assign 2 different spawn points in the inspector
    public TextMeshProUGUI feedbackText; // Assign your TextMeshPro UI component for feedback
    public VideoPlayer tvVideoPlayer; // Reference to the TV VideoPlayer
    public TVRemoteControl tvRemote;  // Reference to the TV Remote script

    private GameObject currentMonster;
    private bool isMonsterActive = false;
    private float timeBetweenAttacks;
    private float attackTimer;
    private float monsterAttackTime = 5f; // Time player has to stop the monster attack

    void Start()
    {
        StartNewAttack();
    }

    void Update()
    {
        if (isMonsterActive)
        {
            if (tvRemote.isTVOn)
            {
                attackTimer -= Time.deltaTime;

                if (attackTimer <= 0)
                {
                    // Player gets jumpscared
                    SceneManager.LoadScene("deathscene");
                }
            }
        }
        else
        {
            timeBetweenAttacks -= Time.deltaTime;

            if (timeBetweenAttacks <= 0)
            {
                SpawnMonster();
            }
        }
    }

    void StartNewAttack()
    {
        timeBetweenAttacks = Random.Range(10f, 20f);
        isMonsterActive = false;
    }

    void SpawnMonster()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        currentMonster = Instantiate(monsterPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
        attackTimer = monsterAttackTime;
        isMonsterActive = true;
    }

    public void OnMonsterClicked()
    {
        if (isMonsterActive && tvRemote.isTVOn)
        {
            Destroy(currentMonster);
            feedbackText.text = "Good";
            Invoke("ClearFeedback", 2f);
            isMonsterActive = false;
            StartNewAttack();
        }
    }

    void ClearFeedback()
    {
        feedbackText.text = "";
    }
}
