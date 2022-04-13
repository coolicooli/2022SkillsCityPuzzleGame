using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private bool cameraTurn = false;
    private Camera mainCam;
    [SerializeField]
    private float rotationSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        if (rotationSpeed <= 0.0f)
        {
            rotationSpeed = 0.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    void LateUpdate()
    {
        if (!cameraTurn)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //Negative Z
                cameraTurn = true;
                StartCoroutine(TurnCameraLeft());
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                //Positive Z
                cameraTurn = true;
                StartCoroutine(TurnCameraRight());
            }
        }
    }

    IEnumerator TurnCameraLeft()
    {
        float targetZRot = transform.rotation.z - 90.0f;
        float currentRotation = 0.0f;
        Debug.Log("Target Z: " + targetZRot);
        while (currentRotation > targetZRot)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime  * -rotationSpeed);
            currentRotation += 1 * Time.deltaTime * -rotationSpeed;
            yield return null;
        }
        cameraTurn = false;
        yield return null;
    }

    IEnumerator TurnCameraRight()
    {
        float targetZRot = transform.rotation.z + 90.0f;
        float currentRotation = 0.0f;
        Debug.Log("Target Z: " + targetZRot);
        while (currentRotation < targetZRot)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime  * rotationSpeed);
            currentRotation += 1 * Time.deltaTime * rotationSpeed;
            yield return null;
        }
        cameraTurn = false;
        yield return null;
    }
}
