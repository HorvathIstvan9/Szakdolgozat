using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCollectible : MonoBehaviour
{
    [SerializeField] List<GameObject> collectible;
    [SerializeField] CharacterController2D player;
    float respawnTime = 5f;
    
    private void Update()
    {
        if (player.flyTime <= 0)
        {
            respawnTime -= Time.deltaTime;
        }
        if (respawnTime <= 0)
        {
            for (int i = 0; i < collectible.Count; i++)
            {
                collectible[i].SetActive(true);
            }
            respawnTime = 5f;
        }
    }
}
