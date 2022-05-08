using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongController : MonoBehaviour
{
    public GameObject notesContainer;
    
    private NoteController[] notes; 
    private AudioSource _audioSource;

    private void Start()
    {
        notes = notesContainer.GetComponentsInChildren<NoteController>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartSong();
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(PlaySoundWithDelay(1));
        }
    }

    public void StartSong()
    {
        foreach (var note in notes)
        {
            note.isPlaying = true;
            note.isRecording = false;
        }

        StartCoroutine(PlaySoundWithDelay(1));
    }

    private IEnumerator PlaySoundWithDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _audioSource.Play();
    }
}
