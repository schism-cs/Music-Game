using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public float noteSpeed = 6f;
    public bool isRecording;
    public bool isPlaying;

    public Color activeColor;
    public Color inactiveColor;

    public float timestampNote;
    
    private PointManager _pointManager;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        _pointManager = FindObjectOfType<PointManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = inactiveColor;

        _boxCollider2D = GetComponent<BoxCollider2D>();
        
    }

    private void Update()
    {
        if (isPlaying)
        {
            var transform1 = transform;
            var transformPosition = transform1.position;
            transformPosition.y -= noteSpeed * Time.deltaTime;
            transform1.position = transformPosition;    
        }
    }

    private void LateUpdate()
    {
        if (transform.position.y < -4.5f && !isRecording)
        {
            _spriteRenderer.color = Color.red;
            StartCoroutine(DestroyNote(gameObject));
        }
    }
    
    IEnumerator DestroyNote(GameObject noteGO)
    {
        yield return new WaitForSeconds(0.2f);
        _pointManager.UpdatePoints(-100);
        Destroy(noteGO);
    }

    public void SetTime()
    {
        timestampNote = Time.time;
    }
}
