using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class playerCollectsKeypieces : MonoBehaviour
{
    public int numberOfPiecesCollected;

    [Header("Inhereted")]
    [SerializeField]
    private GameObject pieceCountUI;
    public AudioSource keySound;
    public AudioSource WinningSound;
    int lastCollectedKey;

    public GameObject Door;
    public GameObject DoorOpens;
    public GameObject DoorTrigger;
    public GameObject DoorCollision;
    public AudioSource DoorSound;
    public bool SwitchedOn;
    public bool CanPlayS;

    public AudioSource CollectSound;
    

    void Start()
    {
        DoorOpens.SetActive(false);
        Door.SetActive(true);
        numberOfPiecesCollected = 0;
        lastCollectedKey = 0;
        DoorSound.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(numberOfPiecesCollected > 2)
        {
            SwitchedOn = true;
            DoorCollision.SetActive(false);
        }

        pieceCountUI.GetComponent<TextMeshProUGUI>().text = numberOfPiecesCollected.ToString();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "piece1")
        {
            lastCollectedKey = 1;
            CollectSound.Play();
        }
        else if (other.gameObject.tag == "piece2")
        {
            lastCollectedKey = 2;
            CollectSound.Play();
        }
        else if (other.gameObject.tag == "piece3")
        {
            lastCollectedKey = 3;
            CollectSound.Play();
        }
        else
        {
            return;
        }
        other.gameObject.SetActive(false);
        numberOfPiecesCollected++;
    }
    public int getNumberOfPiecesCollected()
    {
        return numberOfPiecesCollected;
    }
    public int getLastCollectedKey()
    {
        return lastCollectedKey;
    }
    
    void OnCollisionEnter(Collision other)
    {
        if(SwitchedOn)
        {
           if(other.gameObject.tag == "Door")
           {
            SwitchedOn = true;
            Door.gameObject.SetActive(false);
            DoorOpen();
           }
            
        }
    }
    
    void DoorOpen()
    {
        DoorSound.volume = 100;
        DoorSound.Play();
        DoorOpens.SetActive(true);
    }
}
