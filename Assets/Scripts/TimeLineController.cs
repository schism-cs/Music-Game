using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineController : MonoBehaviour
{

    public float speed;

    private void Update()
    {
        var transform1 = transform;
        
        var transformPosition = transform1.position;
        transformPosition.y -= speed * Time.deltaTime;
        transform1.position = transformPosition;
        
    }

    private void LateUpdate()
    {
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}
