using UnityEngine;

public class Monster : MonoBehaviour
{
    private MonsterSpawner monsterSpawner;

    void Start()
    {
        monsterSpawner = FindObjectOfType<MonsterSpawner>();
    }

    void OnMouseDown()
    {
        monsterSpawner.OnMonsterClicked();
    }
}
