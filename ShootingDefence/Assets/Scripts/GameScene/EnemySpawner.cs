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
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            //Spawn

            EnemyFactory.GetInstance().CreateEnemy(pos);
            
        }
    }




}