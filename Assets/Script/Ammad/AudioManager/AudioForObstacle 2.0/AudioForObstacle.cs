using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioForObstacle : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioClip clip;

    public void UponPlay()
    {
        audioManager.Play(clip);
    }
}
