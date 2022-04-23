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
    Vector3 enermyPosition;
    Vector3 endpos;
    public SlimonController playerScript;



    void Start()
    {
         pathFinding = new PathFinding(20, 20, collistionMap, parent, xOffset, yOffset);
        Debug.Log(pathFinding);
        Debug.Log(pathFinding.GetGrid());

    }
    private void Update()
    {
       
    }
    public void CalculatePathFinding()
    {
      

        pathFinding.GetGrid().GetXY(enermyPosition, out int sX, out int sY);
        pathFinding.GetGrid().GetXY(endpos, out int x, out int y);

        Debug.Log(x + "   " + y);

        List<PathNode> path = pathFinding.FindPath(sX, sY, x, y);
        if (path != null)
        {
            foreach (PathNode pathNode in path)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {

                    Debug.DrawLine(new Vector3(path[i].x + 0.5f + xOffset, 1, path[i].y + 0.5f + yOffset), new Vector3(path[i + 1].x + 0.5f + xOffset, 1, path[i + 1].y + 0.5f + yOffset), Color.blue, 100f);
                }
            }
            if(playerScript.GetInAIBox() == 1)
            {
                enermyPathScript.SetTargetPosition();
            }else
            {
                enermyPathScript.SetTargetHome();
            }
            
        }
        else
        {
            Debug.Log("null");
        }
        
    }
    public void CalculatePathFindingPlayer()
    {
        enermyPosition = enermyPathScript.GetPosition();
        endpos = enermyPathScript.GetPlayerPosition();
        CalculatePathFinding();
    }
    public void CalculatePathFindingHome()
    {
        enermyPosition = enermyPathScript.GetPosition();
        endpos = enermyPathScript.GetHomePosition();
        CalculatePathFinding();
    }


}
