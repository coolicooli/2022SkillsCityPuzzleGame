using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimonController : MonoBehaviour
{
    public enum MoveDirection
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3,
        None = 4
    }

    public float walkSpeed = 4;
    private bool isMoving;
    private Vector3 input;
    private Animator animator;
    public AudioSource bumpSound;
    public LayerMask solidObjectsLayer;
    float delay = 0.15f;
    float remainingDelay;
    bool m_Started;
    Vector3 positionChange;
    MoveDirection playerFacing = MoveDirection.Down;
    MoveDirection prevPlayerFacing;
    private Camera mainCam;
    private Transform spriteTransform;
    [Header("Inherited")]
    [SerializeField]
    private GameObject playerSprite;
    [SerializeField]
    private GameObject pivotObj;

    void Start()
    {
        animator = playerSprite.GetComponent<Animator>();
        m_Started = true;
        mainCam = Camera.main;
        spriteTransform = playerSprite.GetComponent<Transform>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (!isMoving)
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
                SetFaceingDirection();
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

                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                Vector3 targetPos = transform.position;
                positionChange = OrientatedInput();
                Vector3 test = targetPos + positionChange;
                if (isWalkable(test))
                {
                    targetPos += positionChange;
                    StartCoroutine(Move(targetPos));
                }

            }
            animator.SetBool("isMoving", isMoving);
        }
    }

    void LateUpdate()
    {
        playerSprite.transform.LookAt(mainCam.transform);
    }

    Vector3 OrientatedInput()
    {
        Vector3 returnVec = new Vector3();
        var cameraDirection = pivotObj.GetComponent<CameraRotate>().cameraDirection;
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
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > 0.000001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, walkSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;

        yield return null;
    }

    private bool isWalkable(Vector3 targetPos)
    {
        Collider[] hitCollider = Physics.OverlapBox(targetPos, new Vector3(0.3f, 0.3f, 0.3f), Quaternion.identity, solidObjectsLayer);
        if (hitCollider.Length > 0)
        {
            bumpSound.Play();
            return false;
        }
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (m_Started)
        {
            Vector3 test = transform.position;
            if (playerFacing == MoveDirection.Up)
            {
                test.z += 1;
            }
            else if (playerFacing == MoveDirection.Down)
            {
                test.z -= 1;
            }
            else if (playerFacing == MoveDirection.Left)
            {
                test.x -= 1;
            }
            else if (playerFacing == MoveDirection.Right)
            {
                test.x += 1;
            }


            Gizmos.DrawWireCube(test, new Vector3(0.3f, 0.3f, 0.3f));
        }
    }

    void SetFaceingDirection()
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
}
