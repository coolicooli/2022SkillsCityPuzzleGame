using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyTurner : MonoBehaviour
{
    GameObject sprite;
    Vector3 rotation;
    public GameObject pivotObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sprite = this.gameObject.transform.GetChild(0).gameObject;
        rotation = new Vector3(this.gameObject.transform.eulerAngles.x, pivotObj.transform.eulerAngles.y, pivotObj.transform.eulerAngles.z);
        this.gameObject.transform.eulerAngles = rotation;
    }
}
