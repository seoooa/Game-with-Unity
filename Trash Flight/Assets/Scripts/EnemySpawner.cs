using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private GameObject boss;
    private float[] arrPosX = { -2.2f, -1.1f, 0f, 1.1f, 2.2f };

    [SerializeField]
    private float spawnInterval = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartEnemyRoutine();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy(float posX, int index, float moveSpeed) {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);

        if (Random.Range(0, 5) == 0) {  // 20% 확률로 다음 레벨의 적 소환
            index++;
        }

        if (index >= enemies.Length) {
            index = enemies.Length - 1;
        }

        GameObject enemyObject = Instantiate(enemies[index], spawnPos, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.SetMoveSpeed(moveSpeed);
    }

    void SpawnBoss() {
        Instantiate(boss, transform.position, Quaternion.identity);
    }

    void StartEnemyRoutine() {
        // Corutine을 쓰게 되면 EnemyRoutine에서 기다리는 작업이 있어도, 다른 작업은 원활히 수행됨
        StartCoroutine("EnemyRoutine");
    }

    public void StopEnemyRoutine() {
        StopCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine() {
        yield return new WaitForSeconds(3f); // 3초 대기

        int enemyIndex = 0;
        int spawnCounter = 0;
        float moveSpeed = 5f;

        while (true)
        {
            foreach (float posX in arrPosX)
            {
                SpawnEnemy(posX, enemyIndex, moveSpeed);
            }

            spawnCounter++;
            if (spawnCounter % 10 == 0)
            {
                enemyIndex++;
                moveSpeed += 2;
            }

            if (enemyIndex >= enemies.Length) {
                SpawnBoss();
                enemyIndex = 0;
                moveSpeed = 5f;
            }

            yield return new WaitForSeconds(spawnInterval); 
        }
    }
}
