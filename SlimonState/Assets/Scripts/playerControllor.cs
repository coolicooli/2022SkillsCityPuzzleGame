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
    public AudioSource SMWalkSound;
   
    float delay = 0.15f;
    float remainingDelay;
    bool incTimer = false;
    UnityEngine.Object collidedObject;
    //bool isRunning;
    bool m_Started;

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
        
    }

    // Update is called once per frame
    public void Update()
    {

        

        if (!isMoving )
        {
            SMWalkSound.Play();
            //CheckRunning();


            input.x = Input.GetAxisRaw("Horizontal");
            input.z = Input.GetAxisRaw("Vertical");
            

            if (input.z != 0)
            {
                input.x = 0;
            }
            

            if (input != Vector3.zero)
            {
                
                prevPlayerFaceing = playerFaceing;
                SetFaceingDirection();
               
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.z);
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
                animator.SetFloat("moveY", input.z);
                var targetPos = transform.position;
                positionChange = Vector3.zero;
                positionChange.x += input.x;
                positionChange.z+= input.z;
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

        while ((targetPos - transform.position).sqrMagnitude > 0.00000000001f)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            Debug.Log((targetPos - transform.position).sqrMagnitude);
            yield return null;
        }
        
        transform.position = targetPos;
        isMoving = false;
        
        yield return null;

    }

    /*private bool isWalkerble(Vector3 targetPos)
    {

        if (Physics2D.OverlapCircle(targetPos, 0.3f, solidObjectsLayer) != null)
        {
            bumbSound.Play();
            return false;
        }
        return true;
    }*/
    private bool isWalkerble(Vector3 targetPos)
    {
        
        Collider[] hitCollider = Physics.OverlapBox(targetPos, new Vector3(0.3f, 0.3f, 0.3f), Quaternion.identity, solidObjectsLayer);
        if (hitCollider.Length > 0)
        {
            bumbSound.Play();
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
            if (playerFaceing == MoveDirection.Up)
            {
                test.z += 1;
            }
            else if (playerFaceing == MoveDirection.Down)
            {
                test.z -= 1;
            }
            else if (playerFaceing == MoveDirection.Left)
            {
                test.x -= 1;
            }
            else if (playerFaceing == MoveDirection.Right)
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
            playerFaceing = MoveDirection.Right;
        }
        if (input.x == -1f)
        {
            playerFaceing = MoveDirection.Left;
        }
        if (input.z == 1f)
        {
            playerFaceing = MoveDirection.Up;
        }
        if (input.z == -1f)
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
