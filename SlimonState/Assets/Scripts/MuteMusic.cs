using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteMusic : MonoBehaviour
{
    public AudioSource BGM;
    public Button MuteButton;
    
        
    public void MuteToggle(bool muted)
    {
        
        print(muted);
        if(muted == false)
        {
            BGM.volume = 1;
        }
        else
        {
            BGM.volume = 0;
        }
    }
}
