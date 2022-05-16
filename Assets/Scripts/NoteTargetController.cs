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

    public GameObject goodPanel;
    public GameObject fataPanel;

    public GameObject endGamePanel;
    
    private bool _isActive;
    private bool _isDestroying;
    
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private Camera _camera;

    private SongController _songController;
    
    private void Start()
    {
        _camera = Camera.main;
        _isActive = false;
        _isDestroying = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = inactiveColor;

        _boxCollider2D = GetComponent<BoxCollider2D>();

        _songController = FindObjectOfType<SongController>();
    }

    private void Update()
    {
        _spriteRenderer.color = _isActive ? activeColor : inactiveColor;
        _isActive = _isActive || Input.GetKey(activationKey);

        CheckIfIsActive();

        if (_isDestroying)
            _isActive = false;

        var isPressed = Input.GetKeyDown(activationKey);
        if (isRecording && isPressed)
        {
            var note = Instantiate(metronome.notePrefab, recordedTrack.transform);
            note.transform.position = transform.position;
            
            var noteController = note.GetComponent<NoteController>();
            noteController.noteSpeed = metronome.LineSpeed;
            noteController.isRecording = true;
            noteController.isPlaying = true;
            noteController.SetTime();
        }

        if (Input.GetKeyUp(activationKey))
        {
            _isActive = false;
        }
    }

    private void CheckIfIsActive()
    {
        if (Input.touchCount <= 0) return;

        for (var i = 0; i < Input.touchCount; ++i)
        {
            var wp = _camera.ScreenToWorldPoint(Input.GetTouch(i).position);
            var touchPos = new Vector2(wp.x, wp.y);
            
            if (_boxCollider2D == Physics2D.OverlapPoint(touchPos))
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    _spriteRenderer.color = activeColor;
                    _isActive = true;
                }
                else if (Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    _spriteRenderer.color = inactiveColor;
                    _isActive = false;
                }
            }
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (isRecording) return;
        
        if (other.tag.Equals("Note") && _isActive)
        {
            var noteGO = other.gameObject;
            noteGO.GetComponent<SpriteRenderer>().color = Color.white;
            _isDestroying = true;
            StartCoroutine(DestroyNote(other));
        }

        if (other.tag.Equals("EndNote"))
        {
            OpenEndGameModal();
        }
    }

    private void OpenEndGameModal()
    {
        _songController.StopSong();
        endGamePanel.SetActive(true);
    }


    IEnumerator DestroyNote(Collider2D other)
    {
        var distance = Math.Abs(other.gameObject.transform.position.y - transform.position.y);

        StartCoroutine(distance < 0.25 ? TogglePanel(fataPanel) : TogglePanel(goodPanel));
        
        yield return new WaitForSeconds(0.2f);
        PointManager.UpdatePoints(100);
        Destroy(other.gameObject);
        _isDestroying = false;
    }
    
    IEnumerator TogglePanel(GameObject panelGO)
    {
        panelGO.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        panelGO.SetActive(false);
    }

    private void OnMouseDown()
    {
        _spriteRenderer.color = activeColor;
        _isActive = true;
    }

    private void OnMouseUpAsButton()
    {
        _spriteRenderer.color = inactiveColor;
        _isActive = false;
    }
    
    private void OnMouseUp()
    {
        _spriteRenderer.color = inactiveColor;
        _isActive = false;
    }
}
