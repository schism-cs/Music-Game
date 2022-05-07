using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoteTargetController : MonoBehaviour
{

    public KeyCode activationKey;
    public Color inactiveColor;
    public Color activeColor;
    public PointManager PointManager;
    
    private bool _isActive;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    
    private void Start()
    {
        _isActive = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = inactiveColor;

        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        _spriteRenderer.color = _isActive ? activeColor : inactiveColor;
        _isActive = Input.GetKey(activationKey);
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Colliding with note");
        if (other.tag.Equals("Note") && _isActive)
        {
            PointManager.UpdatePoints(100);
            Destroy(other.gameObject);
        }
    }
    
    
}
