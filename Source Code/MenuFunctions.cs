using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
    public bool GameOnPause = false;
    public GameObject player;
    public GameObject pauseMeniu;
    Scene currentScene;
    public string aux;

    void Awake()
    {
        pauseMeniu.SetActive(false);
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        aux = currentScene.name;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameOnPause)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Resume();
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMeniu.SetActive(false);
        Time.timeScale = 1f; 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameOnPause = false;
        GameController.instance.volCanvas.SetActive(false);
    }

    void Pause()
    {
        pauseMeniu.SetActive(true);
        Time.timeScale = 0f;
        GameOnPause = true;
        GameController.instance.volCanvas.SetActive(true);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        GameController.instance.volCanvas.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DeathToMenu()
    {
        GameController.instance.gameOver = false;
        GameController.instance.player.SetActive(true);
        GameController.instance.ui.SetActive(true);
        GameController.instance.menuFunctions.SetActive(true);
        GameController.instance.deathMenu.SetActive(false);
        GameController.instance.volCanvas.SetActive(true);
        Destroy(GameController.instance.player.GetComponent<FPController>().eve);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void NewGame()
    {
        SceneManager.LoadScene("ZombieLevel");
        GameOnPause = false;
        GameController.instance.player.transform.position = new Vector3(152.7952f, 3.483939f, 125.2991f);
        GameController.instance.player.GetComponent<FPController>().ammo = 10;
        GameController.instance.player.GetComponent<FPController>().clip = 10;
        GameController.instance.player.GetComponent<FPController>().health = 100;
        GameController.instance.player.GetComponent<FPController>().healthbar.value = 100;
        GameController.instance.player.GetComponent<FPController>().bulletInClip.text = 10 + "";
        GameController.instance.player.GetComponent<FPController>().bulletReserves.text = 10 + "";
        GameController.instance.varDest1 = true;
        GameController.instance.varDest2 = false;
        GameController.instance.varDest3 = false;
        GameController.instance.menuFunctions.GetComponent<MenuFunctions>().aux = "ZombieLevel";
        GameController.instance.checkPoint = 0;
        GameController.instance.menuFunctions.GetComponent<MenuFunctions>().SaveState();
        GameController.instance.menuFunctions.GetComponent<MenuFunctions>().LoadState();
        GameController.instance.enemies.Clear();
        GameController.instance.zombiesDetected = 0;
        GameController.instance.volCanvas.SetActive(false);
    }
    public void VictoryToMenu()
    {
        GameController.instance.gameOver = false;
        GameController.instance.player.SetActive(true);
        GameController.instance.ui.SetActive(true);
        GameController.instance.menuFunctions.SetActive(true);
        GameController.instance.victoryMenu.SetActive(false);
        GameController.instance.volCanvas.SetActive(true);
        Destroy(GameController.instance.player.GetComponent<FPController>().eve);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void TryAgain()
    {
        Vector3 pos;
        GameController.instance.gameOver = false;
        GameController.instance.volCanvas.SetActive(false);
        GameController.instance.player.SetActive(true);
        GameController.instance.ui.SetActive(true);
        GameController.instance.menuFunctions.SetActive(true);
        GameController.instance.deathMenu.SetActive(false);
        Destroy(GameController.instance.player.GetComponent<FPController>().eve);
        if (GameController.instance.sceneLoad == "ZombieLevel")
            SceneManager.LoadScene("ZombieLevel");
        else if (GameController.instance.sceneLoad == "BossLevel")
        {
            SceneManager.LoadScene("BossLevel");
            pos.x = 38.4f;
            pos.y = 3.652f;
            pos.z = 11.3f;
            GameController.instance.player.transform.position = pos;
        }
        GameOnPause = false;
        LoadState();
        GameController.instance.enemies.Clear();
        GameController.instance.zombiesDetected = 0;
    }



    public void SaveState()
    {
        string s = "";

        s += player.transform.position.x.ToString();
        s += '|';
        s += player.transform.position.y.ToString();
        s += '|';
        s += player.transform.position.z.ToString();
        s += '|';
        s += player.GetComponent<FPController>().health;
        s += '|';
        s += player.GetComponent<FPController>().healthbar.value;
        s += '|';
        s += player.GetComponent<FPController>().ammo;
        s += '|';
        s += player.GetComponent<FPController>().clip;
        s += '|';
        s += player.GetComponent<FPController>().bulletInClip.text;
        s += '|';
        s += player.GetComponent<FPController>().bulletReserves.text;
        s += '|';
        s += GameController.instance.varDest1;
        s += '|';
        s += GameController.instance.varDest2;
        s += '|';
        s += GameController.instance.varDest3;
        s += '|';
        s += aux;
        s += '|';
        s += GameController.instance.checkPoint.ToString();
        PlayerPrefs.SetString("saveState", s);
    }

    public void LoadState()
    {
        if (!PlayerPrefs.HasKey("saveState"))
            return;

        string[] data = PlayerPrefs.GetString("saveState").Split('|');

        player.transform.position = new Vector3(float.Parse(data[0]), float.Parse(data[1]), float.Parse(data[2]));
        player.GetComponent<FPController>().health = int.Parse(data[3]);
        player.GetComponent<FPController>().healthbar.value = float.Parse(data[4]);
        player.GetComponent<FPController>().ammo = int.Parse(data[5]);
        player.GetComponent<FPController>().clip = int.Parse(data[6]);
        player.GetComponent<FPController>().bulletInClip.text = data[7];
        player.GetComponent<FPController>().bulletReserves.text = data[8];

        GameController.instance.varDest1 = bool.Parse(data[9]);
        GameController.instance.varDest2 = bool.Parse(data[10]);
        GameController.instance.varDest3 = bool.Parse(data[11]);
        GameController.instance.sceneLoad = data[12];
        GameController.instance.checkPoint = int.Parse(data[13]);
    }
}
