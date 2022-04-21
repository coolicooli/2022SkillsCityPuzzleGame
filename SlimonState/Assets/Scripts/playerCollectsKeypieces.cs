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

    void Start()
    {
        numberOfPiecesCollected = 0;
        lastCollectedKey = 0;
    }

    // Update is called once per frame
    void Update()
    {
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

}
