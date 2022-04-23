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
    private bool playerInside = false;
    private bool isPressed;
    public AudioSource TeleporterS;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && playerInside && player.GetComponent<SlimonController>().currentState == SlimonController.States.Liquid)
        {
            player.GetComponent<SlimonController>().isMoving = true;
            isUsable = false;
            StartCoroutine(Teleport());
            player.GetComponent<SlimonController>().isMoving = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isUsable = true;
            playerInside = false;
        }
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.1f);
        player.transform.position = new Vector3(
            teleporter2.transform.position.x,
            teleporter2.transform.position.y,
            teleporter2.transform.position.z);
        teleporter2.GetComponent<TeleporterScript>().isUsable = false;
        isUsable = false;
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

