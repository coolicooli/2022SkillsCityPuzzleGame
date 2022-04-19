using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScriptSS : MonoBehaviour
{
    // declare arrays for the parents and the children 
    public GameObject[] lifeScorePrefabs;
    private int lifeScoreIndex;
    private Transform playerPosition;
    public Transform[] respawnPosition;
 

    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        lifeScoreIndex = 0;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            respawnPosition[lifeScoreIndex] = respawnPosition[lifeScoreIndex].transform;
            lifeScorePrefabs[lifeScoreIndex].gameObject.SetActive(false);
            playerPosition.position = new Vector3(respawnPosition[lifeScoreIndex].position.x, respawnPosition[lifeScoreIndex].position.y, respawnPosition[lifeScoreIndex].position.z);
            lifeScoreIndex++;
        }
    }
}
