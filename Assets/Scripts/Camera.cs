using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public Vector2 offset;
    public float speed;

    private Vector3 velocity;
    private void Update()
    {
        
        float newPositionX = target.position.x + offset.x;
        float newPositionY = target.position.y + offset.y;
        float newPositionZ = -10;


        var position = new Vector3(newPositionX, newPositionY, newPositionZ);
        transform.position = Vector3.SmoothDamp(transform.position, position, ref velocity, speed);


        
    }
}
