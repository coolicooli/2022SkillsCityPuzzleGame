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
    [SerializeField]
    private GameObject playerSprite;
    public AudioSource bumbSound;
    public LayerMask solidObjectsLayer;
    public GameObject box;
    float delay = 0.15f;
    float remainingDelay;
    bool m_Started;
    float worldWidth;
    float worldHeight;
    Vector3 positionChange; 
    MoveDirection playerFacing = MoveDirection.Down;
    MoveDirection prevPlayerFacing;
    private Camera mainCam;
    private Transform spriteTransform;

    void Start()
    {
        animator = playerSprite.GetComponent<Animator>();
        m_Started = true;
        worldWidth = box.GetComponent<MeshRenderer>().bounds.size.x;
        worldHeight = box.GetComponent<MeshRenderer>().bounds.size.y;
        Debug.Log(worldWidth + "= width,  " + worldHeight + " = height");
        mainCam = Camera.main;
        spriteTransform = playerSprite.GetComponent<Transform>();
        Debug.Log(spriteTransform);
    }

    // Update is called once per frame
    public void Update()
    {
        if (!isMoving )
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
                Debug.Log(prevPlayerFacing+ "   previous");
                Debug.Log(playerFacing + "   current");
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
                var targetPos = transform.position;
                positionChange = Vector3.zero;
                positionChange.x -= input.x;
                positionChange.y += input.y;
                var test = targetPos + positionChange;
                if (isWalkerble(test))
                {
 
                    targetPos += positionChange;

                    
                    StartCoroutine(Move(targetPos));
                }

            }
            //animator.SetBool("isRunning", isRunning);
            animator.SetBool("isMoving", isMoving);
        }
    }

    void LateUpdate()
    {
        
    }
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, walkSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    private bool isWalkerble(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.3f, solidObjectsLayer) != null)
        {
            bumbSound.Play();
            return false;
        }
        return true;
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
