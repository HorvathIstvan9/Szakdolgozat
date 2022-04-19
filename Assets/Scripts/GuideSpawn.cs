using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideSpawn : MonoBehaviour
{
    [SerializeField] private GameObject guie;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            guie.SetActive(true);
        }
    }
}
