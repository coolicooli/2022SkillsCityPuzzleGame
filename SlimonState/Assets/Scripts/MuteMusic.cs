using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteMusic : MonoBehaviour
{
    public AudioSource BGM;
    public void MuteToggle(bool muted)
    {
        if(muted)
        {
            BGM.volume = 0;
        }
        else;
        {
            BGM.volume = 1;
        }
    }
}
