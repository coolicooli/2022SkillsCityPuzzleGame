using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timerScript : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    float currentTime = 0f;
    float seconds = 0f;
    float mins = 0f;
    string displaySeconds;
    string displayMins;
    string timeString;

    // Start is called before the first frame update
    void Start()
    {
         currentTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        displaySeconds = Mathf.Floor(currentTime % 60).ToString("00");
        displayMins = Mathf.Floor(currentTime / 60).ToString("00");
        
        timeString = (displayMins + ":" + displaySeconds);
        textMesh.text = timeString;

    }
}
