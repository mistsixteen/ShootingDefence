using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float SpawnTime = 5.0f;
    public float spawnMinLength = 20.0f;
    public PlayerCharacter player;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }


    Vector3 GetRandomSpawnPoint()
    {
        Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
        float length = 0.0f;
        pos.y = 1.5f;
        do
        {
            pos.x = Random.Range(-40.0f, 40.0f);
            pos.z = Random.Range(-40.0f, 40.0f);
            length = (pos - player.transform.position).magnitude;
        } while (length < spawnMinLength);

        return pos;
    }

    IEnumerator SpawnRoutine()
    {
        Vector3 pos;
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            pos = GetRandomSpawnPoint();
            //float length = (pos - player.transform.position).magnitude;
            EnemyFactory.GetInstance().CreateEnemyPaladin(pos, transform);

            yield return new WaitForSeconds(5.0f);
            pos = GetRandomSpawnPoint();
            //float length2 = (pos - player.transform.position).magnitude;
            EnemyFactory.GetInstance().CreateEnemyGunner(pos, transform);

            yield return new WaitForSeconds(5.0f);

        }
    }

}
