using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;  // Assign your monster prefab in the inspector
    public Transform[] spawnPoints;   // Assign 2 different spawn points in the inspector
    public TextMeshProUGUI feedbackText; // Assign your TextMeshPro UI component for feedback
    public Transform playerTransform;  // Reference to the player's transform
    private TVRemoteControl tvRemote;  // Reference to the TV Remote script

    private GameObject currentMonster;
    private bool isMonsterActive = false;
    private bool spawningEnabled = false;
    private float timeBetweenAttacks;
    private float attackTimer;
    private float monsterAttackTime = 5f; // Time player has to stop the monster attack

    void Start()
    {
        tvRemote = FindObjectOfType<TVRemoteControl>();
        if (tvRemote == null)
        {
            Debug.LogError("TVRemoteControl component not found in the scene!");
        }
    }

    void Update()
    {
        if (!spawningEnabled) return;

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

    public void StartSpawning()
    {
        spawningEnabled = true;
        StartNewAttack();
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

        if (playerTransform != null)
        {
            currentMonster.transform.LookAt(playerTransform);
            Vector3 lookDirection = playerTransform.position - currentMonster.transform.position;
            lookDirection.y = 0;  // Keep the y-axis level
            currentMonster.transform.rotation = Quaternion.LookRotation(lookDirection);
        }

        attackTimer = monsterAttackTime;
        isMonsterActive = true;
    }

    public void OnMonsterClicked()
    {
        if (isMonsterActive && !tvRemote.isTVOn) // Ensure the TV is off
        {
            Destroy(currentMonster);
            feedbackText.text = "Good";
            Invoke("ClearFeedback", 2f);
            isMonsterActive = false;
            StartNewAttack();
        }
        else if (isMonsterActive && tvRemote.isTVOn)
        {
            feedbackText.text = "Turn off the TV first!";
            Invoke("ClearFeedback", 2f);
        }
    }

    void ClearFeedback()
    {
        feedbackText.text = "";
    }
}
