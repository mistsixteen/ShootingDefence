using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float SpawnTime = 5.0f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
        pos.x = Random.Range(-40.0f, 40.0f);
        pos.y = 1.5f;
        pos.z = Random.Range(-40.0f, 40.0f);
        while (true)
        {

            yield return new WaitForSeconds(5.0f);
            pos.x = Random.Range(-40.0f, 40.0f);
            pos.z = Random.Range(-40.0f, 40.0f);
            EnemyFactory.GetInstance().CreateEnemyPaladin(pos);

            yield return new WaitForSeconds(5.0f);
            pos.x = Random.Range(-40.0f, 40.0f);
            pos.z = Random.Range(-40.0f, 40.0f);
            EnemyFactory.GetInstance().CreateEnemyGunner(pos);

            yield return new WaitForSeconds(5.0f);

        }
    }

}
