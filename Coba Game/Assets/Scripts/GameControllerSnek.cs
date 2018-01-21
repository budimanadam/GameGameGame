using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerSnek : MonoBehaviour {
    public int maxSize;
    public int currSize;
    public int xBound;
    public int zBound;
    public int score;
    public GameObject foodPrefab;
    public GameObject currFood;
    public GameObject snakePrefab;
    public Snake head;
    public Snake tail;
    public int NESW;
    public Vector3 nextPos;
    public Text scoreValue;
    private float nextInput = 0.0f;

    void OnEnable()
    {
        Snake.hit += hit;
    }

    // Use this for initialization
    void Start () {
        InvokeRepeating("TimerInvoke", 0, .1f);
        FoodFunction();
    }

    void OnDisable()
    {
        Snake.hit -= hit;
    }

    // Update is called once per frame
    void Update () {
        ComChangeD();
	}

    void TimerInvoke()
    {
        Movement();

        if (currSize >= maxSize)
        {
            TailFunction();
        }

        else
        {
            currSize++;
        }
    }

    void Movement()
    {
        GameObject temp;
        nextPos = head.transform.position;

        switch (NESW)
        {
            //north
            case 0:
                {
                    nextPos = new Vector3(nextPos.x, nextPos.y, nextPos.z + 1);
                    break;
                }

            //east
            case 1:
                {
                    nextPos = new Vector3(nextPos.x + 1, nextPos.y, nextPos.z);
                    break;
                }

            //south
            case 2:
                {
                    nextPos = new Vector3(nextPos.x, nextPos.y, nextPos.z - 1);
                    break;
                }

            //west
            case 3:
                {
                    nextPos = new Vector3(nextPos.x - 1, nextPos.y, nextPos.z);
                    break;
                }
        }
        temp = (GameObject)Instantiate(snakePrefab, nextPos, transform.rotation);
        head.SetNext(temp.GetComponent<Snake>());
        head = temp.GetComponent<Snake>();

        return;
    }

    void ComChangeD()
    {
        if (NESW != 2 && Input.GetKeyDown(KeyCode.UpArrow) && Time.time > nextInput)
        {
            nextInput = Time.time + .1f;
            NESW = 0;
        }

        if (NESW != 0 && Input.GetKeyDown(KeyCode.DownArrow) && Time.time > nextInput)
        {
            nextInput = Time.time + .1f;
            NESW = 2;
        }

        if (NESW != 3 && Input.GetKeyDown(KeyCode.RightArrow) && Time.time > nextInput)
        {
            nextInput = Time.time + .1f;
            NESW = 1;
        }

        if (NESW != 1 && Input.GetKeyDown(KeyCode.LeftArrow) && Time.time > nextInput)
        {
            nextInput = Time.time + .1f;
            NESW = 3;
        }
    }

    void TailFunction()
    {
        Snake tempSnake = tail;
        tail = tail.GetNext();
        tempSnake.RemoveTail();
    }

    void FoodFunction()
    {
        int xPos = Random.Range(-xBound, xBound);
        int yPos = 0;
        int zPos = Random.Range(-zBound, zBound);

        currFood = (GameObject)Instantiate(foodPrefab, new Vector3(xPos, yPos, zPos), transform.rotation);
        StartCoroutine(CheckRender(currFood));
    }

    IEnumerator CheckRender(GameObject IN)
    {
        yield return new WaitForEndOfFrame();

        if (IN.GetComponent<Renderer>().isVisible == false)
        {
            if (IN.tag == "Food")
            {
                Destroy(IN);
                FoodFunction();
            }
        }
    }

    void hit(string WhatWasSent)
    {
        if (WhatWasSent == "Food")
        {
            FoodFunction();
            maxSize++;
            score++;
            scoreValue.text = score.ToString();            
        }

        if (WhatWasSent == "Snake" || WhatWasSent == "Wall")
        {
            CancelInvoke("TimerInvoke");
            Exit();
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
