using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Script : MonoBehaviour
{
    public GameObject player;
    public GameObject fullPlayerPrefab;
    GameObject eve;
    public List<GameObject> bossZombie = new List<GameObject>();


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        bossZombie = new List<GameObject>(GameObject.FindGameObjectsWithTag("BossZombieTag"));
        if (bossZombie.Count == 0 && player != null && eve == null) 
        {
            Vector3 position = new Vector3(player.transform.position.x, Terrain.activeTerrain.SampleHeight(player.transform.position), player.transform.position.z);
            eve = Instantiate(fullPlayerPrefab, position, player.transform.rotation);
            eve.GetComponent<Animator>().SetTrigger("victory");
            GameController.instance.menuFunctions.SetActive(false);
            GameController.instance.victoryMenu.SetActive(true);
            GameController.instance.volCanvas.SetActive(false);
            GameController.instance.gameOver = true;
            GameController.instance.player.SetActive(false);
        }
    }
}
