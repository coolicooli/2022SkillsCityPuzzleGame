using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevelTrigger : MonoBehaviour
{
    public AudioSource playCSound;

    private void OnTriggerEnter(Collider other) 
    {
        playCSound.Play();
    }
}
