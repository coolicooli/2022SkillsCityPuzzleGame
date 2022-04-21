using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    public Transform thisPortal;
    public Transform targetPortal;

    private void Start()
    {
        thisPortal = this.transform;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.transform.position = targetPortal.GetChild(0).transform.position;
        }
    }
}
