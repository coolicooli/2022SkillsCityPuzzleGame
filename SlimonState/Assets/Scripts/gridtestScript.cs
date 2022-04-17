using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class gridtestScript : MonoBehaviour
{
    public Tilemap collistionMap;
    public Transform parent;
    public Camera worldCamera;
    private PathFinding pathFinding;
    public float xOffset =-10f;
    public float yOffset = -10f;
    Vector3 worldPosition = new Vector3(10, 1, 18);
    public EnermypathFinder enermyPathScript;




    void Start()
    {
         pathFinding = new PathFinding(20, 20, collistionMap, parent, xOffset, yOffset);
        Debug.Log(pathFinding);
        Debug.Log(pathFinding.GetGrid());

    }
    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(worldPosition.x -1 >= 0)
            {
                worldPosition += new Vector3(-1, 0, 0);
                Debug.Log(worldPosition);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (worldPosition.x + 1 < pathFinding.GetGrid().GetWidth())
            {
                worldPosition += new Vector3(1, 0, 0);
                Debug.Log(worldPosition);
            }

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (worldPosition.y - 1 >= 0)
            {
                worldPosition += new Vector3(0, 0, -1);
                Debug.Log(worldPosition);
            }

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (worldPosition.z + 1 < pathFinding.GetGrid().GetHeight())
            {
                worldPosition += new Vector3(0, 0, 1);
                Debug.Log(worldPosition);
            }

        }*/
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 enermyPosition = enermyPathScript.GetPosition();
            Vector3 endpos = enermyPathScript.GetPlayerPosition();


            pathFinding.GetGrid().GetXY(enermyPosition, out int sX, out int sY);
            pathFinding.GetGrid().GetXY(endpos, out int x, out int y);
            
            Debug.Log(x + "   " + y);
            
            List<PathNode> path = pathFinding.FindPath(sX, sY, x, y);
            if(path != null)
            {
                foreach(PathNode pathNode in path)
                {
                    for(int i = 0; i<path.Count -1; i++)
                    {
                       
                        Debug.DrawLine(new Vector3(path[i].x +0.5f+ xOffset, 1, path[i].y + 0.5f+ yOffset) , new Vector3(path[i+1].x + 0.5f+ xOffset, 1, path[i+1].y + 0.5f+ yOffset), Color.blue, 100f);
                    }
                }
                enermyPathScript.SetTargetPosition();
            }
            else
            {
                Debug.Log("null");
            }
        }
    }
    
}
