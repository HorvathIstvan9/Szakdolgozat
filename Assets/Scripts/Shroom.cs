using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom : MonoBehaviour
{
    private Animator shroomAnimator;
    [SerializeField] private AudioSource shroomAudio;
    void Start()
    {
        shroomAnimator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.y <= Vector2.down.y)
        {
            shroomAnimator.SetTrigger("Jumped");
        }
    }

    public void AudioPlay()
    {
        shroomAudio.Play();
    }
}
