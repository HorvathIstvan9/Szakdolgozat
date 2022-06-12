using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject gameObject;
    private float timer = 5f;
    private bool startTimer = false;
    private void FixedUpdate()
    {
        if (startTimer)
        {
            timer -= Time.fixedDeltaTime;          
        }
        if (timer<=0)
        {
            gameObject.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetTrigger("Talk");
            text.SetActive(true);
            startTimer = true;
        }
    }
}
