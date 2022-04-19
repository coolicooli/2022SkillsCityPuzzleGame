using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinding
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGINAL_COST = 14;

    public static PathFinding Instance { get; private set; }


    private Tilemap collistionMap;
    private Transform parent;
    private Grid<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closedList;
    private float xOffset;
    private float yOffset;

    public PathFinding(int width, int height, Tilemap collistionMap, Transform parent,float xOffset,float yOffset  )
    {
        Instance = this;
        this.collistionMap = collistionMap;
        this.parent = parent;
        this.xOffset = xOffset;
        this.yOffset = yOffset;

        grid = new Grid<PathNode>(width, height, 1, collistionMap, new Vector3(xOffset, 0, yOffset), (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y), parent);
        
    }
    public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition)
    {
        grid.GetXY(startWorldPosition, out int startX, out int startY);
        grid.GetXY(endWorldPosition, out int endX, out int endY);

        List<PathNode> path = FindPath(startX, startY, endX, endY);
        if(path == null)
        {
            return null;
        }
        else
        {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach(PathNode pathNode in path)
            {
                vectorPath.Add(new Vector3(pathNode.x, pathNode.y) * grid.cellSize + Vector3.one * grid.cellSize * 0.5f);

            }
            return vectorPath;
        }
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject(endX, endY);

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathNode pathNode = grid.GetGridObject(x, y);
                checkedWalkable(new Vector3Int(x+(int)xOffset, y+ (int)yOffset, 0));
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }
        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();
        while(openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
            {
                return CalculatePath(endNode);
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeigbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;
                if (!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }
                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();
                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }

        }
        return null;
    }
    public void checkedWalkable(Vector3Int pos)
    {
        
        TileBase tile = collistionMap.GetTile(pos);
        if (tile != null)
        {
            Debug.Log(pos + "walkable Check");
            GetNode(pos.x - (int)xOffset, pos.y - (int)yOffset).isWalkable = false;
            
        }
    }
    private List<PathNode>GetNeigbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();
        if(currentNode.x -1 >= 0)
        {
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
        }
        if (currentNode.x + 1 < grid.GetWidth())
        { 
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
        }
        if (currentNode.y - 1 >=0)
        {
            neighbourList.Add(GetNode(currentNode.x, currentNode.y-1));
        }
        if (currentNode.y + 1 < grid.GetHeight())
        {
            neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));
        }
        return neighbourList;
    }
    public PathNode GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }
    public Grid<PathNode> GetGrid()
    {
        return grid;
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }
    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        int moveCost = MOVE_DIAGINAL_COST*Mathf.Min(xDistance,yDistance)+MOVE_STRAIGHT_COST*remaining;
        return moveCost;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowerFCostNode = pathNodeList[0];
        for(int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowerFCostNode.fCost)
            {
                lowerFCostNode = pathNodeList[i];
            }
        }
            return lowerFCostNode;
    }
}
