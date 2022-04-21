using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public Vector3 offset = new Vector3(0, -8, -8);
    public GameObject pivot;
    bool turnning;
    bool onWall;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!turnning)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                turnning = true;
                StartCoroutine(switchCameraPosition());
            }
        }
        
       
    }

    IEnumerator switchCameraPosition()
    {
        float turnAngle;
        if (!onWall)
        {
             turnAngle = 1;
        }
        else
        {
             turnAngle = -1;
        }
        float rotation=0;
        while (90f  > rotation)
        {
            pivot.transform.Rotate(Vector3.forward * Time.deltaTime * turnAngle *90);
            rotation += 1*Time.deltaTime * 90;


             yield return null;
        }
        if (!onWall)
        {
            onWall = true;
        }
        else
        {
            onWall = false;
        }
        turnning = false;
        yield return null;
    }
}
