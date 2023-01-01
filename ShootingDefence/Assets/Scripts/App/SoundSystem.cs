using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//TODO : Audio 관리 추가

public class SoundSystem : MonoBehaviour
{
    public enum SoundIndex
    {
        BGM = 0,
    };

    private static SoundSystem instance;

    [SerializeField]
    private AudioSource[] sounds;

    private void Start()
    {
        instance = this;
    }

    public static SoundSystem GetInstance()
    {
        return instance;
    }

    public void PlayBGM()
    {
        sounds[(int)SoundIndex.BGM].Play();
    }

    public void StopBGM()
    {
        sounds[(int)SoundIndex.BGM].Stop();
    }

}
