using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject menuFunctions;
    public GameObject player;
    public GameObject ui;
    public GameObject volCanvas;
    public GameObject deathMenu;
    public GameObject victoryMenu;

    public int checkPoint = 0;
    public bool varDest1;
    public bool varDest2;
    public bool varDest3;
    public string sceneLoad;
    public int zombiesDetected = 0;
    public bool gameOver = false;
    public bool canShoot = false;
    public List<GameObject> enemies = new List<GameObject>();
    GameObject spawningPoint1, spawningPoint2, spawningPoint3;

    void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            Destroy(menuFunctions);
            Destroy(player);
            Destroy(ui);
            Destroy(volCanvas);
            Destroy(deathMenu);
            Destroy(victoryMenu);
            return;
        }
        else
        {
            instance = this;
        }

        player.SetActive(false);
        ui.SetActive(false);
        menuFunctions.SetActive(false);
        deathMenu.SetActive(false);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(ui);
        DontDestroyOnLoad(menuFunctions);
        DontDestroyOnLoad(this.gameObject);

        DontDestroyOnLoad(volCanvas);
        DontDestroyOnLoad(deathMenu);
        DontDestroyOnLoad(victoryMenu);

    }
    void Update()
    {
        if (GameController.instance.enemies.Count == 0)
        {
            GameController.instance.enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("ZombieTag"));
        }

        if (GameController.instance.enemies.Count == 0 && GameController.instance.zombiesDetected == 1)
        {
            if (GameObject.FindGameObjectWithTag("SpawnPoint1Tag") != null) 
            {
                spawningPoint1 = GameObject.FindGameObjectWithTag("SpawnPoint1Tag");
                Destroy(spawningPoint1); 
                GameController.instance.zombiesDetected = 0;
            }
            else if (GameObject.FindGameObjectWithTag("SpawnPoint2Tag") != null) 
            {
                spawningPoint2 = GameObject.FindGameObjectWithTag("SpawnPoint2Tag");
                Destroy(spawningPoint2);
                GameController.instance.zombiesDetected = 0;
            }
            else if (GameObject.FindGameObjectWithTag("SpawnPoint3Tag") != null) 
            {
                spawningPoint3 = GameObject.FindGameObjectWithTag("SpawnPoint3Tag");
                Destroy(spawningPoint3);
                GameController.instance.zombiesDetected = 0;
            }
        }
    }

}
