using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Vector2 effectMultiplier;
    private Transform cameraTransform;
    private Vector3 lastCameraPos;
    private float textureSizeX;
    private float textureSizeY;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPos = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureSizeX = texture.width / sprite.pixelsPerUnit;
        textureSizeY = texture.height / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPos;
        transform.position += new Vector3(deltaMovement.x * effectMultiplier.x, deltaMovement.y * effectMultiplier.y);
        lastCameraPos = cameraTransform.position;

        if (Mathf.Abs(cameraTransform.position.x-transform.position.x) >= textureSizeX)
        {
            float offsetPosX = (cameraTransform.position.x - transform.position.x) % textureSizeX;
            transform.position = new Vector3(cameraTransform.position.x + offsetPosX, transform.position.y);
        }
        /*if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureSizeY)
        {
            float offsetPosY = (cameraTransform.position.y - transform.position.y) % textureSizeY;
            transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offsetPosY);
        }*/
    }
}
