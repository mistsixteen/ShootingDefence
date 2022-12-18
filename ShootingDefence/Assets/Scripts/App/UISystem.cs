using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISystem : MonoBehaviour
{
    private static UISystem instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    void Start()
    {

    }

    public static UISystem GetInstance(){
        return instance;
    }
}
