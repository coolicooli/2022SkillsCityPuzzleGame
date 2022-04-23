using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManagerScriptSS : MonoBehaviour
{
    // declare arrays for the parents and the children 
    public GameObject[] lifeScorePrefabs;
    private int lifeScoreIndex;
    private Transform playerPosition;
    public Transform[] respawnPosition;
    public AudioSource LosingSound;

    int key;
    [SerializeField]
    playerCollectsKeypieces keyScript;


    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        lifeScoreIndex = 0;
        
        key = keyScript.getLastCollectedKey();

    }
    /*void Update()
    {

        if(Input.GetKeyDown(KeyCode.H))
        {
            lifeLost();
        }

    }*/
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("heart"))
            lifeLost();

    }
    public void lifeLost()
    {
        if(lifeScoreIndex == 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            LosingSound.Play();
        }
        else
        {
            key = keyScript.getLastCollectedKey();
            Debug.Log(key);

            respawnPosition[key] = respawnPosition[key].transform;
            lifeScorePrefabs[lifeScoreIndex].gameObject.SetActive(false);
            playerPosition.position = new Vector3(respawnPosition[key].position.x, respawnPosition[key].position.y, respawnPosition[key].position.z);
            lifeScoreIndex++;
        }
        
    }
    private void checkPoints()
    {
        
    }
}
