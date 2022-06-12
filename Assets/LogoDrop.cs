using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoDrop : MonoBehaviour
{
    Rigidbody2D rigidbody;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            rigidbody.constraints=RigidbodyConstraints2D.FreezePositionX;
        }
    }
}
