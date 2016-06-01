using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Spawn : MonoBehaviour
{
    public Vector2 spawnRange;
    public  GameObject[] gb;
    public float delaySpeedUp;
    public List<GameObject> goList = new List<GameObject>();

    public float delay;
    private float delayInstance;

    public void StartSpawning()
    {  
        StartCoroutine("GbSpawner");
    }

    public void StopSpawning()
    {  
        StopAllCoroutines();
    }

    IEnumerator GbSpawner()
    {
        GameObject instance = Instantiate(gb[Random.Range(0, gb.Length)], new Vector2(Random.Range(spawnRange.x, spawnRange.y), 270), Quaternion.identity) as GameObject;

        //flips the zombie randomly
        if(instance.tag == "Zombie")
        {
            int rand = Random.Range(0,2);
            if(rand == 0)
            {
                EnemyMovement speedInstance = instance.GetComponent<EnemyMovement>();
                speedInstance.speed = -speedInstance.speed;
                instance.GetComponent<EnemyMovement>().speed = speedInstance.speed;
            }
        }
            
        instance.transform.SetParent(GameObject.Find("Spawner").transform); //parent to spawner
        goList.Add(instance);
        yield return new WaitForSeconds(delayInstance);
        DelayIncrease();
        StartCoroutine("GbSpawner");
    }

    public void DelayIncrease()
    {
        delayInstance /= delaySpeedUp;
    }

    public void ResetDelay()
    {
        delayInstance = delay;
    }

    public void gbListDestroy()
    {
        foreach (GameObject go in goList)
        {
            if (go != null)
            {
                go.SetActive(false); 
            }

        }
        goList.Clear();    
    }
}
