using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerCollectsKeypieces : MonoBehaviour
{
   // public TextMeshProUGUI Message;
    int numberOfPiecesCollected;
    // Start is called before the first frame update
    void Start()
    {
        numberOfPiecesCollected = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "piece1")
        {
            other.gameObject.SetActive(false);
           // Message.text = "You collected Key-piece";
            numberOfPiecesCollected++;
        }
        if(other.gameObject.tag == "piece2")
        {
            other.gameObject.SetActive(false);
            //Message.text = "You collected Key-piece";
            numberOfPiecesCollected++;
        }
        if(other.gameObject.tag == "piece3")
        {
            other.gameObject.SetActive(false);
           // Message.text = "You collected Key-piece"; 
            numberOfPiecesCollected++;
        }
    }
    
}
