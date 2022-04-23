using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerCollectsKeypieces : MonoBehaviour
{
    public int numberOfPiecesCollected;

    [Header("Inhereted")]
    [SerializeField]
    private GameObject pieceCountUI;
    int lastCollectedKey;

    public GameObject Door;
    public GameObject DoorTrigger;
    public AudioSource DoorSound;
    public bool SwitchedOn;
    public bool CanPlayS;
    

    void Start()
    {
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
        }

        pieceCountUI.GetComponent<TextMeshProUGUI>().text = numberOfPiecesCollected.ToString();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "piece1")
        {
            lastCollectedKey = 1;
        }
        else if (other.gameObject.tag == "piece2")
        {
            lastCollectedKey = 2;
        }
        else if (other.gameObject.tag == "piece3")
        {
            lastCollectedKey = 3;
            
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
        // if(SwitchedOn == true && DoorSound.isPlaying == false)
        // {
        //     DoorSound.Play();
        // }
        // else if(SwitchedOn == false && DoorSound.isPlaying == true)
        // {
        //     DoorSound.Stop();
        //     DoorSound.volume = 0;
        //     SwitchedOn = false;
        // }
    }
    

}
