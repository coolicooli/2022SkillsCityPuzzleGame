using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
public AudioSource WinningSound;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            WinningSound.Play();
            SceneManager.LoadScene("WinningScene");
        }
    }
}
