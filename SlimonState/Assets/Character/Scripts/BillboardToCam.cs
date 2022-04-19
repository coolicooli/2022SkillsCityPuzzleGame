using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardToCam : MonoBehaviour
{
    Camera mainCam;
    void Start()
    {
        mainCam = Camera.main;
    }   
    void LateUpdate()
    {
        transform.LookAt(mainCam.transform);
    }
}
