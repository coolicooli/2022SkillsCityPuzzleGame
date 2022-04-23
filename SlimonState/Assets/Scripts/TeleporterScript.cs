using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour
{

    
    [SerializeField] Transform teleporter2;

    [SerializeField] GameObject player;
    [SerializeField]
    private bool isUsable = true;
    [SerializeField]
    private bool isPressed;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.T))
        {
            isPressed = true;
            Debug.Log("t");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isUsable == true)
        {
            if(isPressed == true)
            {
               StartCoroutine(Teleport());
                isPressed = false;
                isUsable = false;
            }

        }
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(1);
        player.transform.position = new Vector3(
            teleporter2.transform.position.x,
            teleporter2.transform.position.y,
            teleporter2.transform.position.z);
        teleporter2.GetComponent<TeleporterScript>().isUsable = false;
        isUsable = false;
    }
    private void OnTriggerExit(Collider other)
    {
        isUsable = true;
        isPressed = false;
    }
}
   /* public Transform thisPortal;
    public Transform targetPortal;
    private bool isUsable = true;

    private void Start()
    {
        thisPortal = this.transform;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (isUsable == true)
        {
            yield return new WaitForSeconds(3);
            Debug.Log("col");
            col.transform.position = targetPortal.GetChild(0).transform.position;
            isUsable = false;
            targetPortal.GetComponent<TeleporterScript>().isUsable = false;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        isUsable = true;
        other.transform.
    }
   */

