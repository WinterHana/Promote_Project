using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // BGM ���
    public AudioSource BGM;
    void Start()
    {
        BGM.Play();
    }
}
