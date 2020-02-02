using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;  //random, coroutine, instantiate

public class NPCSpawner : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject[] npc;

    //Spawn
    private float timer = 1f;
    private int maxTimeToSpawn = 10;
    private int numClones = 0;
    private int maxClones = 1000;
    private bool spawnCollision = false;
    
    //Coroutine
    bool waitForTime = false;

    IEnumerator Start()
    {
        while (true)
        {
            yield return StartCoroutine(RandomSpawn());
        }
    }

    void Update()
    {
        timer += Time.deltaTime;    
        checkedTimeForIncreaseSpawns();
    }

    private void checkedTimeForIncreaseSpawns()
    {
        int time = (int)timer;
        //Every 5 seconds, decrease max time spawn
        if(time % 5 == 0 && maxTimeToSpawn > 2 && !waitForTime)
        {
            //Spawn enemies faster
            maxTimeToSpawn -= 2;
            StartCoroutine(WaitForNext());
        }
    }

    IEnumerator RandomSpawn()
    {
        int spawnerRandomNum = UnityEngine.Random.Range(0, spawners.Length);
        int NPCRandomNum = UnityEngine.Random.Range(0, npc.Length);
        int timeBetweenSpawns = UnityEngine.Random.Range(0, maxTimeToSpawn);

        if (spawnCollision == true)
        {
            RaycastHit2D hit = Physics2D.Raycast(spawners[spawnerRandomNum].transform.position, Vector2.zero);
            if (hit.collider == null && numClones < maxClones)   //check if spawn point has anyone on it. 
            {
                numClones++;
                GameObject enemy = Instantiate(npc[NPCRandomNum], spawners[spawnerRandomNum].transform.position, Quaternion.identity) as GameObject;
            }
        }
        else if (spawnCollision == false && numClones < maxClones)
        {
            numClones++;
            GameObject enemy = Instantiate(npc[NPCRandomNum], spawners[spawnerRandomNum].transform.position, Quaternion.identity) as GameObject;
        }

        yield return new WaitForSeconds(timeBetweenSpawns);
    }

    IEnumerator WaitForNext()
    {
        waitForTime = true;
        yield return new WaitForSeconds(1f);
        waitForTime = false;
    }
}
