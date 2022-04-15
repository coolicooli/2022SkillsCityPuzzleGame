using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthTest : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public int health;


    void Start()
    {
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 3)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }
        else if (health == 2)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);
        }
        else if (health == 1)
        {
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
        }
        else if (health == 0)
        {
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (health > 0)
            {
                health -= 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            health = 3;   
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (health < 3)
            {
                health += 1;
            }
        }
    }
}
