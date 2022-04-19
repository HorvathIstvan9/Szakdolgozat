using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform self;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private Transform raycastStart;
    [SerializeField] private Transform raycastEnd;
    [SerializeField] private Transform shootingPos;
    [SerializeField] private LayerMask layer;
    [SerializeField] private GameObject prefab;

    private bool faceRight = false;
    private float shootTimer = 1f;
    private Transform nextPosition;

    private void Awake()
    {
        nextPosition = right;
    }
    void Update()
    {
        RaycastHit2D hit = Physics2D.Linecast(raycastStart.position, raycastEnd.position, layer);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Shoot();
            }
        }
    }
    private void FixedUpdate()
    {
        MoveToNext();
    }

    void Shoot()
    {
        if (shootTimer<=0)
        {
            Instantiate(prefab, shootingPos.position, shootingPos.rotation);
            shootTimer += 1f;
        }
        shootTimer -= Time.deltaTime;
    }

    void MoveToNext()
    {
        if (Vector2.Distance(self.position, right.position) < 0.1f)
        {
            Flip();
            nextPosition = left;
        }
        else if (Vector2.Distance(self.position, left.position) < 0.1f)
        {
            Flip();
            nextPosition = right;
        }
        self.position = Vector2.MoveTowards(self.position, nextPosition.position, Time.deltaTime * 2.5f);
    }

    void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
