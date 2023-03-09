using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    [SerializeField] AudioClip[] clip;

    public void ChangeClip()
    {
        AudioSource myAudio = GetComponent<AudioSource>();

        myAudio.clip = clip[Random.Range(0, clip.Length)];
        myAudio.Play();
    }
}
