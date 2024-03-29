using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory
{
    private static EnemyFactory instance;

    GameObject enemyPaladin = null;
    GameObject enemyGunner = null;

    public static EnemyFactory GetInstance()
    {
        if (instance == null)
            instance = new EnemyFactory();
        return instance;
    }

    public EnemyFactory()
    {
        enemyPaladin = Resources.Load<GameObject>("Prefabs/PaladinEnemy");
        enemyGunner = Resources.Load<GameObject>("Prefabs/SoliderEnemy");
    }

    public GameObject CreateEnemyPaladin(in Vector3 Pos, in Transform transform)
    {
        GameObject newObject = GameObject.Instantiate(enemyPaladin, Pos, Quaternion.identity, transform);
        return newObject;
    }

    public GameObject CreateEnemyGunner(in Vector3 Pos, in Transform transform)
    {
        GameObject newObject = GameObject.Instantiate(enemyGunner, Pos, Quaternion.identity, transform);
        return newObject;
    }


}
