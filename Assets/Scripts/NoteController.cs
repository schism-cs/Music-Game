using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public float noteSpeed = 6f;

    private PointManager _pointManager;
    
    private void Start()
    {
        _pointManager = FindObjectOfType<PointManager>();
    }

    private void Update()
    {
        var transform1 = transform;
        
        var transformPosition = transform1.position;
        transformPosition.y -= noteSpeed * Time.deltaTime;
        transform1.position = transformPosition;
    }

    private void LateUpdate()
    {
        if (transform.position.y < -3.5f)
        {
            _pointManager.UpdatePoints(-100);
            Destroy(gameObject);
        }
    }
}
