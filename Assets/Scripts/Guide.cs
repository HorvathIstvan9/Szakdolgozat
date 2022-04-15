using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    [SerializeField] private List<Transform> points;
    [SerializeField] private float speed = 100;
    Transform guide;
    int goal = 0;
    private void Awake()
    {
        guide = this.gameObject.transform;
    }
    private void Update()
    {
        ToNextPoint();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"&&Vector2.Distance(guide.position, points[goal].position) < 0.1f)
        {
            if (goal >= (points.Count - 1))
            {
                goal = points.Count - 1;
            }
            else
            {
                goal++;
            }
        }
    }
    
        
    void ToNextPoint()
    {
        guide.position = Vector2.MoveTowards(guide.position, points[goal].position,speed*Time.deltaTime);
    }
}
