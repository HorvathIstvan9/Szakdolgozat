using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_move : MonoBehaviour
{
    [SerializeField] private List<Transform> points;
    int goal = 0;
    [SerializeField] private float speed = 2;
    Transform platform;
    private void Awake()
    {
        platform = this.gameObject.transform;
    }

    private void Update()
    {
        ToNextPoint();
    }
    void ToNextPoint()
    {
        platform.position = Vector2.MoveTowards(platform.position, points[goal].position,Time.deltaTime * speed);
        
        if (Vector2.Distance(platform.position, points[goal].position)<0.1f)
        {
            if (goal==(points.Count-1))
            {
                goal = 0;
            }
            else
            {
                goal++;
            }
            
        }
    }
}
