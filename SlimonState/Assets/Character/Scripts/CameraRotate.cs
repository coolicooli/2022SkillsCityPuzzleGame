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
    public bool cameraTurn = false;
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
    }

    void LateUpdate()
    {
        if (!cameraTurn)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //Negative Z
                cameraTurn = true;
                StartCoroutine(TurnCamera(true));
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                //Positive Z
                cameraTurn = true;
                StartCoroutine(TurnCamera(false));
            }
        }
    }

    public IEnumerator TurnCamera(bool isLeft)
    {
        float targetZRot = transform.rotation.z + 90.0f;
        float currentRotation = 0.0f;
        while (currentRotation <= targetZRot)
        {
            if (isLeft)
                transform.Rotate(Vector3.forward * Time.deltaTime * -rotationSpeed);
            else if (!isLeft)
                transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
            currentRotation += 1 * Time.deltaTime * rotationSpeed;
            yield return null;
        }
        RoundZAngle(90.0f);
        cameraTurn = false;
        yield return null;
    }

    public IEnumerator TurnCameraFlat()
    {
        float targetRot = 40.0f;
        float currentRotation = 0.0f;
        while (currentRotation <= targetRot)
        {
            transform.Rotate(Vector3.left * Time.deltaTime * rotationSpeed);
            currentRotation += 1 * Time.deltaTime * rotationSpeed;
            yield return null;
        }
        RoundXAngle(40.0f);
        RoundYAngle(40.0f);
        cameraTurn = false;
        yield return null;
    }

    public IEnumerator TurnCameraAngled()
    {
        float targetRot = 40.0f;
        float currentRotation = 0.0f;
        while (currentRotation <= targetRot)
        {
            transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
            currentRotation += 1 * Time.deltaTime * rotationSpeed;
            yield return null;
        }
        RoundXAngle(40.0f);
        RoundYAngle(40.0f);
        cameraTurn = false;
        yield return null;
    }

    private void RoundAngles(float roundAngle)
    {
        Vector3 roundedVec = transform.localEulerAngles;
        roundedVec.x = Mathf.Round(roundedVec.x / roundAngle) * roundAngle;
        roundedVec.x = Mathf.Round(roundedVec.x / roundAngle) * roundAngle;
        roundedVec.x = Mathf.Round(roundedVec.x / roundAngle) * roundAngle;
        transform.localEulerAngles = roundedVec;
    }

    private void RoundXAngle(float roundAngle)
    {
        Vector3 roundedVec = transform.localEulerAngles;
        roundedVec.x = Mathf.Round(roundedVec.x / roundAngle) * roundAngle;
        transform.localEulerAngles = roundedVec;
    }

    private void RoundYAngle(float roundAngle)
    {
        Vector3 roundedVec = transform.localEulerAngles;
        roundedVec.y = Mathf.Round(roundedVec.y / roundAngle) * roundAngle;
        transform.localEulerAngles = roundedVec;
    }

    private void RoundZAngle(float roundAngle)
    {
        Vector3 roundedVec = transform.localEulerAngles;
        roundedVec.z = Mathf.Round(roundedVec.z / roundAngle) * roundAngle;
        transform.localEulerAngles = roundedVec;
    }
}
