using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MetronomeController : MonoBehaviour
{

    public int bpm;
    public GameObject timeLine;
    
    public AudioSource metronomeSound;

    public GameObject noteContainer;
    public GameObject notePrefab;
    public GameObject[] noteSpawningPoints;

    public bool isRecording;
    
    private float _frequency;
    private float _count;
    private float _lineSpeed;

    public float LineSpeed => _lineSpeed;

    private void Start()
    {
        _count = 0f;
        _lineSpeed = 0f;
    }

    private void Update()
    {
        _frequency = 60f / bpm;
        _lineSpeed = 6 / _frequency;
        
        _count += Time.deltaTime;
        
        if (_count > _frequency)
        {
            var line = Instantiate(timeLine, transform);
            line.GetComponent<TimeLineController>().speed = _lineSpeed;
            
            if (!isRecording)
            {
                var index = (int) (Math.Truncate(Random.value * noteSpawningPoints.Length));
            
                var note = Instantiate(notePrefab, noteSpawningPoints[index].transform);
                note.GetComponent<NoteController>().noteSpeed = _lineSpeed;    
                
                StartCoroutine(PlaySoundWithDelay(_frequency));
            }
            
            _count = 0f;
        }
    }

    private IEnumerator PlaySoundWithDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        metronomeSound.Play();
    }
    
}
