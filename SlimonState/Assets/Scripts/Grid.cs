using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid<TGridObject> 
{
    public event EventHandler<OnGridObjectChangeEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangeEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private Tilemap collistionMap;
    private int width;
    private int height;
    public float cellSize;
    private TGridObject[,] gridArray;
    string collNumb;
    private TextMesh[,] debugTextArray;
    private Vector3 originPosition;
    private Transform parent;
    public Grid(int width, int height, float cellSize, Tilemap collistionMap, Vector3 originPosition, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject, Transform parent)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.collistionMap = collistionMap;
        this.originPosition = originPosition;
        this.parent = parent;

        gridArray = new TGridObject[width, height];
        debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }

                bool showingDebug = false;
        if (showingDebug)
        {
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    var pos = new Vector3Int(x + (int)originPosition.x, y + (int)originPosition.z, 0);

                    float xOff = (float)x + 0.5f;
                    float yOff = (float)y + 0.5f;
                    debugTextArray[x, y] = createTextInWorld.CreateWorldText(parent, gridArray[x, y]?.ToString(), GetWorldPosition(xOff, yOff), 8, Color.red, TextAnchor.MiddleCenter, TextAlignment.Center, 1);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.green, 200f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.green, 200f);
                    checkedWalkable(pos);
                }
                Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.green, 200f);
                Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.green, 200f);

                OnGridObjectChanged += (object sender, OnGridObjectChangeEventArgs eventArgs) =>
                {
                    debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
                };

            }
        }

    }
    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }



    public Vector3 GetWorldPosition(float x, float y)
    {
        return new Vector3(x, 1, y) * cellSize + originPosition;
    }
    public void GetXY(Vector3 worldPostion, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPostion -originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPostion -originPosition).z/ cellSize);
    }

    public void checkedWalkable(Vector3Int pos)
    {
        TileBase tile = collistionMap.GetTile(pos);
        if (tile != null)
        {
            //SetValue(pos.x+10, pos.y+10, 1);
        }   
    }

    public void TriggerGridObjectChanged(int x, int y)
    {
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangeEventArgs { x = x, y = y });
    }

    public void SetGridObject(int x, int y, TGridObject value)
    {
        if(x >= 0 && y >=0 && x<= width && y <= height)
        {
            gridArray[x, y] = value;
            if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangeEventArgs { x = x, y = y });
        }
        
    }
    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        Debug.Log(x + "   " + y);
        SetGridObject(x, y, value);

    }
    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x <= width && y <= height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }
    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }
}








        
