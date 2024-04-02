using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenerateZombies : MonoBehaviour
{
    public GameObject zombiePrefab;
    public int zombieNumber;
    public float radius;

    void InstantiateZombies()
    {

        for (int i = 0; i < zombieNumber; i++)
        {
            Vector3 position = this.transform.position + Random.onUnitSphere * radius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(position, out hit, 10.0f, NavMesh.AllAreas))
                Instantiate(zombiePrefab, hit.position, Quaternion.identity);
            else
                i--;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && GameController.instance.enemies.Count == 0)
        {

            if (this.tag == "SpawnPoint1Tag" && GameController.instance.checkPoint == 0)
            {
                InstantiateZombies();
                GameController.instance.zombiesDetected = 1;
            }
            else if (this.tag == "SpawnPoint2Tag" && GameController.instance.checkPoint == 1)
            {
                InstantiateZombies();
                GameController.instance.zombiesDetected = 1;
            }
            else if (this.tag == "SpawnPoint3Tag" && GameController.instance.checkPoint == 2)
            {
                InstantiateZombies();
                GameController.instance.zombiesDetected = 1;
            }
        }
    }

}
