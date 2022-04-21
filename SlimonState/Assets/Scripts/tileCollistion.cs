using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tileCollistion : MonoBehaviour
{
    public GameObject collitionBox;
    // Start is called before the first frame update
    void Start()
    {
        var tileMap = this.gameObject.GetComponent<Tilemap>();
        var tileMapGameObject = this.gameObject.transform;
        
        int i = 0;
        foreach (Vector3Int pos in tileMap.cellBounds.allPositionsWithin)
        {
            
            TileBase tile = tileMap.GetTile(pos);
            if(tile != null)
            {
                
                i += 1;
                Vector3 coordinates = new Vector3(pos.x+0.5f, pos.z+1, pos.y+0.5f);
                GameObject newcollitionBox = GameObject.Instantiate(collitionBox, coordinates, collitionBox.transform.rotation);
                newcollitionBox.name = "collitionBox" + (i).ToString();
                newcollitionBox.transform.SetParent(tileMapGameObject);
                


            }
        }
    }
    



}
