using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevelTrigger : MonoBehaviour
{
    public AudioSource playCSound;
    // Soon as player hits the finish trigger the finish sound plays
    private void OnTriggerEnter(Collider other) 
    {
        playCSound.Play();
    }
}
