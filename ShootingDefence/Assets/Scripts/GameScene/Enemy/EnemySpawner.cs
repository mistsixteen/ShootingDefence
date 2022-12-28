using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float SpawnTime = 5.0f;
    public float spawnMinLength = 20.0f;
    public PlayerCharacter player;

    private static EnemySpawner instance = null;

    private void Awake()
    {
        instance = this;
    }

    public static EnemySpawner GetInstance() { return instance; }

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

    public void SpawnEnemyInRandomPoint()
    {
        var pos = GetRandomSpawnPoint();
        EnemyFactory.GetInstance().CreateEnemyPaladin(pos, transform);
        pos = GetRandomSpawnPoint();
        EnemyFactory.GetInstance().CreateEnemyGunner(pos, transform);
    }
}
