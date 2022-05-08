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
    
    public bool isRecording;
    public MetronomeController metronome;
    public GameObject recordedTrack;
    
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

        var isPressed = Input.GetKeyDown(activationKey);
        if (isRecording && isPressed)
        {
            var note = Instantiate(metronome.notePrefab, recordedTrack.transform);
            note.transform.position = transform.position;
            
            var noteController = note.GetComponent<NoteController>();
            noteController.noteSpeed = metronome.LineSpeed;
            noteController.isRecording = true;
            noteController.isPlaying = true;
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (isRecording) return;
        
        Debug.Log("Colliding with note");
        if (other.tag.Equals("Note") && _isActive)
        {
            PointManager.UpdatePoints(100);
            Destroy(other.gameObject);
        }
    }
    
    
}
