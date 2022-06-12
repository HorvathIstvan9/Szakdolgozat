using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideSpawn : MonoBehaviour
{
    [SerializeField] private GameObject guide;
    [SerializeField] private GameObject door;
    [SerializeField] private AudioSource audio;
    private Animator animator;
    private Collider2D collider;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            animator.SetTrigger("Hit");
            collider.enabled = false;
            guide.SetActive(true);
            door.SetActive(false);
        }
    }

    public void PlayAudio()
    {
        audio.Play();
    }
}
