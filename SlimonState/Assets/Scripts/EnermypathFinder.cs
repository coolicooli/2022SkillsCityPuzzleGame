using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermypathFinder : MonoBehaviour
{
    public GameObject enermyObject;
    public GameObject enermySprite;
    Animator enermyAnim;
    public LayerMask solidObjectsLayer;
    private bool isMoving;
    public float moveSpeed = 4;
    private int currentPathIndex;
    private List<Vector3> pathVectorList;
    public GameObject player;
    public Grid grid;


    public enum MoveDirection
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3,
        None = 4
    }

    MoveDirection enermyFaceing = MoveDirection.Down;
    MoveDirection prevenermyFaceing;
    // Start is called before the first frame update
    void Start()
    {
        enermyAnim = enermySprite.GetComponent<Animator>();
        Debug.Log(transform.localPosition + ":  TransformPos");
    }

    // Update is called once per frame
    void Update()
    {

        HandleMovement();

    }
    void HandleMovement()
    {
        if (pathVectorList != null)
        {
            Vector3 tragetposition = pathVectorList[currentPathIndex];
            
            
            Vector3 tragetpositionAjusted = new Vector3(tragetposition.x - 10, tragetposition.y-10, -1.5f);
            Debug.Log(transform.localPosition +":  TransformPos");
            if (Vector3.Distance(transform.localPosition, tragetpositionAjusted) > 0.1f)
            {
                
                Vector3 moveDir = (tragetpositionAjusted - transform.localPosition).normalized;
                Debug.Log(moveDir);

                float distanceBefore = Vector3.Distance(transform.localPosition, tragetpositionAjusted);
                enermyAnim.SetFloat("moveX", moveDir.x);
                enermyAnim.SetFloat("moveY", moveDir.y);
                enermyAnim.SetBool("isMoving", true);
                transform.localPosition = transform.localPosition + moveDir * moveSpeed * Time.deltaTime;
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                    enermyAnim.SetBool("isMoving", false);
                }
            }
        }
        else
        {
            enermyAnim.SetBool("isMoving", false);
        }
    }
    private void StopMoving()
    {
        pathVectorList = null;
    }

    void SetFaceingDirection()
    {

        /*if (input.x == 1f)
        {
            enermyFaceing = MoveDirection.Right;
        }
        if (input.x == -1f)
        {
            enermyFaceing = MoveDirection.Left;
        }
        if (input.z == 1f)
        {
            enermyFaceing = MoveDirection.Up;
        }
        if (input.z == -1f)
        {
            enermyFaceing = MoveDirection.Down;
        }*/

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
    
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public Vector3 GetPlayerPosition()
    {
        return player.transform.position;
    }
    public void SetTargetPosition()
    {
        currentPathIndex = 0;
        pathVectorList = PathFinding.Instance.FindPath(GetPosition(), GetPlayerPosition());
        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }


}
