using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator doorAnimator;
    [SerializeField] private AudioSource doorOpenAudio;
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        doorAnimator.SetTrigger("Open");
    }
    public void AudioPlay()
    {
        doorOpenAudio.Play();
    }
}
