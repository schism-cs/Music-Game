using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTargetController : MonoBehaviour
{

    public KeyCode activationKey;
    public Color inactiveColor;
    public Color activeColor;
    
    private bool _isActive;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _isActive = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = inactiveColor;
    }

    private void Update()
    {
        _spriteRenderer.color = _isActive ? activeColor : inactiveColor;

        _isActive = Input.GetKey(activationKey);
        
        
    }
}
