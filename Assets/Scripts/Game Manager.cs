using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreNum;
    public TextMeshProUGUI hiscoreNum;
    public int enemiesKilled = 0;
    public int enemySpeedup = 0;
    public GameObject testEnemyPrefab;
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;
    public GameObject enemy4Prefab;
    public GameObject playerPrefab;

    private int currentHeight = 7;

    public delegate void speedUp(int speed);
    public static event speedUp speedUpEvent;

    private int totalPoints = 0;
    int hiscore = 0;

    public int speed = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Enemy.OnEnemyDied += EnemyDied;
        Player.OnPlayerDied += PlayerDied;
        SpawnEnemies();
    }

    void OnDestroy()
    {
        Enemy.OnEnemyDied -= EnemyDied;
        Player.OnPlayerDied -= PlayerDied;
    }

    void EnemyDied(int points)
    {
        enemiesKilled++;
        enemySpeedup++;
        UpdateScore(points);
    }

    void PlayerDied()
    {
        UpdateScore(-1);
        currentHeight = 7;
        enemiesKilled = 0;
        enemySpeedup = 0;
        Instantiate(playerPrefab);
        SpawnEnemies();
        speed = 1;
        speedUpEvent?.Invoke(speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemySpeedup >= 5)
        {
            enemySpeedup = 0;
            speed += 1;
            speedUpEvent?.Invoke(speed);
        }
        if (enemiesKilled == 20)
        {
            enemiesKilled = 0;
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 pos = new Vector3(-4 + (i * 2), currentHeight, 0);
            GameObject newObj = Instantiate(enemy4Prefab);
            newObj.transform.position = pos;
        }

        for (int i = 0; i < 5; i++)
        {
            Vector3 pos = new Vector3(-4 + (i * 2), currentHeight - 2, 0);
            GameObject newObj = Instantiate(enemy3Prefab);
            newObj.transform.position = pos;
        }
        for (int i = 0; i < 5; i++)
        {
            Vector3 pos = new Vector3(-4 + (i * 2), currentHeight - 4, 0);
            GameObject newObj = Instantiate(enemy2Prefab);
            newObj.transform.position = pos;
        }
        for (int i = 0; i < 5; i++)
        {
            Vector3 pos = new Vector3(-4 + (i * 2), currentHeight - 6, 0);
            GameObject newObj = Instantiate(enemy1Prefab);
            newObj.transform.position = pos;
        }
        speedUpEvent?.Invoke(speed);
    }

    void UpdateScore(int points)
    {
        if (points <= 0)
        {
            if (hiscore < totalPoints)
            {
                hiscore = totalPoints;
                points = 0;
            }
            totalPoints = 0;
        }
        else
        {
            totalPoints += points;
        }
        
        if (points < 10)
        {
            scoreNum.text = "000" + totalPoints.ToString();
        } else if (points < 100)
        {
            scoreNum.text = "00" + totalPoints.ToString();
        } else if (points < 1000)
        {
            scoreNum.text = "0" + totalPoints.ToString();
        }
        else
        {
            scoreNum.text = totalPoints.ToString();
        }

        if (hiscore < 10)
        {
            hiscoreNum.text = "000" + hiscore.ToString();
        } else if (hiscore < 100)
        {
            hiscoreNum.text = "00" + hiscore.ToString();
        } else if (hiscore < 1000)
        {
            hiscoreNum.text = "0" + hiscore.ToString();
        }
        else
        {
            hiscoreNum.text = hiscore.ToString();
        }
    }
}
