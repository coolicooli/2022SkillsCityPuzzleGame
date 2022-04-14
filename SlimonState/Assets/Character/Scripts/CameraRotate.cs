using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public enum CameraFacing
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }
    private bool cameraTurn = false;
    private Camera mainCam;
    [SerializeField]
    private float rotationSpeed = 90.0f;
    public CameraFacing cameraDirection = CameraFacing.North;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        if (rotationSpeed <= 0.0f)
        {
            rotationSpeed = 90.0f;
        }
    }

    void Update()
    {
        Vector3 cameraForward = mainCam.transform.forward;
        cameraForward.x = Mathf.Round(cameraForward.x);
        cameraForward.z = Mathf.Round(cameraForward.z);
        Debug.Log(cameraForward);
        if (cameraForward.x == 0.0f)
        {
            if (cameraForward.z > 0.0f)
            {
                cameraDirection = CameraFacing.North;
            }
            else if (cameraForward.z < 0.0f)
            {
                cameraDirection = CameraFacing.South;
            }
        }
        else if (cameraForward.z == 0.0f)
        {
            if (cameraForward.x > 0.0f)
            {
                cameraDirection = CameraFacing.East;
            }
            else if (cameraForward.x < 0.0f)
            {
                cameraDirection = CameraFacing.West;
            }
        }
        Debug.Log(cameraDirection);
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
        while (currentRotation >= targetZRot)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * -rotationSpeed);
            currentRotation += 1 * Time.deltaTime * -rotationSpeed;
            yield return null;
        }
        RoundZAngle();
        cameraTurn = false;
        yield return null;
    }

    IEnumerator TurnCameraRight()
    {
        float targetZRot = transform.rotation.z + 90.0f;
        float currentRotation = 0.0f;
        while (currentRotation <= targetZRot)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
            currentRotation += 1 * Time.deltaTime * rotationSpeed;
            yield return null;
        }
        RoundZAngle();
        cameraTurn = false;
        yield return null;
    }

    void RoundAngles(float roundTo)
    {
        Vector3 roundedVec = transform.localEulerAngles;
        roundedVec.x = Mathf.Round(roundedVec.x / roundTo) * roundTo;
        roundedVec.x = Mathf.Round(roundedVec.x / roundTo) * roundTo;
        roundedVec.x = Mathf.Round(roundedVec.x / roundTo) * roundTo;
        transform.localEulerAngles = roundedVec;
    }

        void RoundZAngle()
    {
        Vector3 roundedVec = transform.localEulerAngles;
        roundedVec.z = Mathf.Round(roundedVec.z / 90) * 90;
        transform.localEulerAngles = roundedVec;
    }
}
