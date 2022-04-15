using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCollectible : MonoBehaviour
{
    [SerializeField] GameObject collectible;
    GameObject player;
    float respawnTime = 5f;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (!collectible.activeSelf&&player.GetComponent<CharacterController2D>().flyTime <= 0)
        {
            respawnTime -= Time.deltaTime;
        }
        if (respawnTime <= 0)
        {
            collectible.SetActive(true);
            respawnTime = 5f;
        }
    }
}
