using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControllor : MonoBehaviour
{
    float moveSpeed =4;
    public float walkSpeed =4;
    public float runSpeed =8;
    private bool isMoving;
    private Vector3 input;
    private Vector3 prevInput;
    private Animator animator;
    public GameObject playerSprite;
    public AudioSource bumbSound;
    public LayerMask solidObjectsLayer;
    public GameObject box;
   
    float delay = 0.15f;
    float remainingDelay;
    bool incTimer = false;
    UnityEngine.Object collidedObject;
    //bool isRunning;
    bool m_Started;
    float worldWidth;
    float worldHeight;




    Vector3 positionChange;
    Vector3 prevPositionChange;


    public enum MoveDirection
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3,
        None = 4
    }
    MoveDirection playerFaceing = MoveDirection.Down;
    MoveDirection prevPlayerFaceing;


    void Start()
    {
        animator = playerSprite.GetComponent<Animator>();
       
        m_Started = true;
        worldWidth = box.GetComponent<MeshRenderer>().bounds.size.x;
        worldHeight = box.GetComponent<MeshRenderer>().bounds.size.y;
        Debug.Log(worldWidth + "= width,  " + worldHeight + " = height");
    }

    // Update is called once per frame
    public void Update()
    {

       

        if (!isMoving )
        {
            
            //CheckRunning();


            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            

            if (input.y != 0)
            {
                input.x = 0;
            }
            

            if (input != Vector3.zero)
            {
                prevPlayerFaceing = playerFaceing;
                SetFaceingDirection();
                Debug.Log(prevPlayerFaceing+ "   previous");
                Debug.Log(playerFaceing + "   current");
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                if (playerFaceing != prevPlayerFaceing)
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
                positionChange.x += input.x;
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
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
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
            playerFaceing = MoveDirection.Right;
        }
        if (input.x == -1f)
        {
            playerFaceing = MoveDirection.Left;
        }
        if (input.y == 1f)
        {
            playerFaceing = MoveDirection.Up;
        }
        if (input.y == -1f)
        {
            playerFaceing = MoveDirection.Down;
        }

    }/*
    void CheckRunning()
    {
        if (Input.GetKey(KeyCode.C)){
            moveSpeed = runSpeed;
            isRunning = true;   
        }
        else
        {
            moveSpeed = walkSpeed;
            isRunning = false;
            
        }
    }*/


}
