using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory
{
    private static EnemyFactory instance;

    GameObject enemyObject = null;

    public static EnemyFactory GetInstance()
    {
        if (instance == null)
            instance = new EnemyFactory();
        return instance;
    }

    public EnemyFactory()
    {
        enemyObject = Resources.Load<GameObject>("Prefabs/PaladinEnemy");
    }

    public GameObject CreateEnemy(in Vector3 Pos)
    {
        GameObject newObject = GameObject.Instantiate(enemyObject, Pos, Quaternion.identity);
        if (newObject == null)
        {
            Debug.Log("NULL EXEP");
        }
        return newObject;
    }


}
