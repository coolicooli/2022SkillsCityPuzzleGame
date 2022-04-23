using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimonController : MonoBehaviour
{
    private enum MoveDirection
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3,
        None = 4
    }

    private enum CollisionResults
    {
        None = 0,
        SolidObject = 1,
        ClimbableObject = 2
    }

    public enum States
    {
        Slime = 0,
        Liquid = 1,
        Solid = 2
    }

    public float walkSpeed = 4;
    public bool isMoving;
    public bool isClimbing;
    private bool hasStarted = false;
    private Vector3 input;
    private Animator animator;
    public AudioSource bumpSound;
    float delay = 0.15f;
    float remainingDelay;
    Vector3 positionChange;
    MoveDirection playerFacing = MoveDirection.Down;
    MoveDirection prevPlayerFacing;
    private Camera mainCam;
    private CameraRotate cameraRot;
    public States currentState;
    private Rigidbody playerRigidbody;
    [Header("States")]
    [SerializeField]
    private Color slimeColour;
    [SerializeField]
    private Color solidColour;
    [SerializeField]
    private Color liquidColour;
    [Header("Inherited")]
    [SerializeField]
    private GameObject playerSprite;
    [SerializeField]
    private GameObject pivotObj;
    public AudioSource SMWalkSound;
    public gridtestScript gridtestscript;

    [Header("Layers")]
    [SerializeField]
    private LayerMask solidObjectsLayer;
    [SerializeField]
    private LayerMask climbableObjectsLayer;
    [SerializeField]
    GameManagerScriptSS gameManager;
    int calPath = 0;

    bool teleport;

    void Start()
    {
        animator = playerSprite.GetComponent<Animator>();
        mainCam = Camera.main;
        cameraRot = pivotObj.GetComponent<CameraRotate>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerSprite.GetComponent<SpriteRenderer>().color = slimeColour;
        hasStarted = true;
        teleport = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "acid" || other.gameObject.tag == "enermy")
        {
            teleport = true;
            gameManager.lifeLost();
            teleport = true;
        }
        if (other.gameObject.tag == "AiBox")
        {
            calPath = 1;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "AiBox")
        {
            calPath = 2;
        }
    }

    public bool getIsMoving()
    {
        return isMoving;
    }

    // Update is called once per frame
    public void Update()
    {
        if (isClimbing)
        {

        }
        else if (!isClimbing)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentState == States.Slime)
                {
                    currentState = States.Solid;
                    playerSprite.GetComponent<SpriteRenderer>().color = solidColour;
                    isMoving = false;
                    isClimbing = false;
                    walkSpeed = 1.0f;
                    Debug.Log("Space");
                }
                else if (currentState == States.Solid)
                {
                    currentState = States.Liquid;
                    playerSprite.GetComponent<SpriteRenderer>().color = liquidColour;
                    isMoving = true;
                    walkSpeed = 0.0f;
                }
                else if (currentState == States.Liquid)
                {
                    currentState = States.Slime;
                    playerSprite.GetComponent<SpriteRenderer>().color = slimeColour;
                    isMoving = false;
                    walkSpeed = 4.0f;
                }
            }
        }

        if (!isMoving && !cameraRot.cameraTurn)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.y != 0)
            {
                input.x = 0;
            }

            if (input != Vector3.zero)
            {
                prevPlayerFacing = playerFacing;
                SetFacingDirection();
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                if (playerFacing != prevPlayerFacing)
                {
                    remainingDelay = delay;
                }
                if (remainingDelay > 0f)
                {
                    remainingDelay -= Time.deltaTime;
                    return;
                }

                Vector3 targetPos = transform.position;
                Vector3 checkDownPos = targetPos + Vector3.down;
                CollisionResults collDownTest = CollisionTest(checkDownPos);
                SMWalkSound.Play();
                if (isClimbing && collDownTest == CollisionResults.None)
                {
                    positionChange = ClimbingInput();
                }
                else
                {
                    if ((pivotObj.transform.localEulerAngles.x != 0.0f | pivotObj.transform.localEulerAngles.y != 0.0f) && isClimbing == true)
                    {
                        cameraRot.cameraTurn = true;
                        StartCoroutine(cameraRot.TurnCameraAngled());
                    }
                    positionChange = OrientatedInput();
                    isClimbing = false;
                }

                Vector3 checkForwardPos = targetPos + positionChange;
                CollisionResults collDirectionTest = CollisionTest(checkForwardPos);
                if (collDirectionTest == CollisionResults.None)
                {
                    targetPos += positionChange;
                    StartCoroutine(MovePlayer(targetPos));
                }
                else if (collDirectionTest == CollisionResults.SolidObject)
                {
                    bumpSound.Play();
                }
                else if (collDirectionTest == CollisionResults.ClimbableObject)
                {
                    if (input.y > 0.0f && currentState == States.Slime)
                    {
                        cameraRot.cameraTurn = true;
                        isClimbing = true;
                        StartCoroutine(MovePlayer(transform.position + Vector3.up));
                        StartCoroutine(cameraRot.TurnCameraFlat());
                    }
                    else
                    {
                        bumpSound.Play();
                    }

                }

            }

            if (currentState != States.Liquid)
            {
                animator.SetBool("isMoving", isMoving);
            }
        }
    }

    void LateUpdate()
    {
        playerSprite.transform.LookAt(mainCam.transform);
    }

    Vector3 OrientatedInput()
    {
        Vector3 returnVec = new Vector3();
        var cameraDirection = cameraRot.cameraDirection;
        if (cameraDirection == CameraRotate.CameraFacing.North)
        {
            returnVec.x += input.x;
            returnVec.z += input.y;
        }
        else if (cameraDirection == CameraRotate.CameraFacing.East)
        {
            returnVec.x += input.y;
            returnVec.z -= input.x;
        }
        else if (cameraDirection == CameraRotate.CameraFacing.South)
        {
            returnVec.x -= input.x;
            returnVec.z -= input.y;
        }
        else if (cameraDirection == CameraRotate.CameraFacing.West)
        {
            returnVec.x -= input.y;
            returnVec.z += input.x;
        }
        return returnVec;
    }

    Vector3 ClimbingInput()
    {
        Vector3 returnVec = new Vector3();
        var cameraDirection = cameraRot.cameraDirection;
        if (cameraDirection == CameraRotate.CameraFacing.North)
        {
            returnVec.x += input.x;
            returnVec.y += input.y;
        }
        else if (cameraDirection == CameraRotate.CameraFacing.East)
        {
            returnVec.z -= input.x;
            returnVec.y += input.y;
        }
        else if (cameraDirection == CameraRotate.CameraFacing.South)
        {
            returnVec.x -= input.x;
            returnVec.y += input.y;
        }
        else if (cameraDirection == CameraRotate.CameraFacing.West)
        {
            returnVec.z += input.x;
            returnVec.y += input.y;
        }
        return returnVec;
    }

    IEnumerator MovePlayer(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > 0.000001f)
        {
            if (teleport)
            {
                teleport = false;
                isMoving = false;
                gridtestscript.CalculatePathFinding();
                yield break;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPos, walkSpeed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
        RoundPos();

        if (calPath == 1)
        {
            gridtestscript.CalculatePathFindingPlayer();
        }
        else if (calPath == 2)
        {
            gridtestscript.CalculatePathFindingHome();
            calPath = 0;
        }

        yield return null;
    }

    void RoundPos()
    {
        Vector3 roundedVec = transform.position;
        roundedVec.x = Mathf.Round(roundedVec.x / 0.5f) * 0.5f;
        roundedVec.y = Mathf.Round(roundedVec.y / 0.5f) * 0.5f;
        roundedVec.z = Mathf.Round(roundedVec.z / 0.5f) * 0.5f;
        transform.position = roundedVec;
    }

    private CollisionResults CollisionTest(Vector3 targetPos)
    {
        Collider[] solidObjArray = Physics.OverlapBox(targetPos, new Vector3(0.3f, 0.3f, 0.3f), Quaternion.identity, solidObjectsLayer);
        if (solidObjArray.Length > 0)
        {
            return CollisionResults.SolidObject;
        }
        Collider[] climbableObjArray = Physics.OverlapBox(targetPos, new Vector3(0.3f, 0.3f, 0.3f), Quaternion.identity, climbableObjectsLayer);
        if (climbableObjArray.Length > 0)
        {
            return CollisionResults.ClimbableObject;
        }

        return CollisionResults.None;
    }

    void SetFacingDirection()
    {
        if (input.x == 1f)
        {
            playerFacing = MoveDirection.Right;
        }
        if (input.x == -1f)
        {
            playerFacing = MoveDirection.Left;
        }
        if (input.y == 1f)
        {
            playerFacing = MoveDirection.Up;
        }
        if (input.y == -1f)
        {
            playerFacing = MoveDirection.Down;
        }
    }
    public int GetInAIBox()
    {
        return calPath;
    }



}
