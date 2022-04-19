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

    void Start()
    {
        numberOfPiecesCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        pieceCountUI.GetComponent<TextMeshProUGUI>().text = numberOfPiecesCollected.ToString();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "keyPiece")
        {
            other.gameObject.SetActive(false);
            numberOfPiecesCollected++;
        }
    }
    
}
