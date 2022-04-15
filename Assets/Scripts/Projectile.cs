using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody.velocity = transform.right * 5f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player"|| collision.tag=="Ground")
        {
            Destroy(this.gameObject);
        }
    }
}
