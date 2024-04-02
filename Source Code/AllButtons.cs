using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AllButtons : MonoBehaviour
{
    GameObject volController;
    Vector3 pos;

    void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (GameController.instance != null)
        {
            AudioSource music = GameController.instance.GetComponent<AudioSource>();
            Slider slider = GameController.instance.volCanvas.GetComponentInChildren<Slider>();
            slider.value = music.volume;
        }

        if (currentScene.name == "ZombieLevel")
        {
            GameController.instance.player.SetActive(true);
            GameController.instance.ui.SetActive(true);
            GameController.instance.menuFunctions.SetActive(true);
            GameController.instance.menuFunctions.GetComponentInChildren<MenuFunctions>().Resume();
            GameController.instance.volCanvas.SetActive(false);
            GameController.instance.deathMenu.SetActive(false);
            GameController.instance.victoryMenu.SetActive(false);
        }
        else
        if (currentScene.name == "MainMenu")
        {
            if (GameController.instance != null)
            {
                GameController.instance.player.SetActive(false);
                GameController.instance.ui.SetActive(false);
                GameController.instance.menuFunctions.SetActive(false);
                GameController.instance.deathMenu.SetActive(false);
                GameController.instance.victoryMenu.SetActive(false);
                GameController.instance.volCanvas.SetActive(true);
            }
        }
        else
        if (currentScene.name == "BossLevel")
        {
            pos.x = 38.4f;
            pos.y = 3.652f;
            pos.z = 11.3f;
            GameController.instance.player.transform.position = pos;
            GameController.instance.deathMenu.SetActive(false);
            GameController.instance.victoryMenu.SetActive(false);
            GameController.instance.player.SetActive(true);
            GameController.instance.ui.SetActive(true);
            GameController.instance.menuFunctions.SetActive(true);
            GameController.instance.volCanvas.SetActive(false);
            GameController.instance.menuFunctions.GetComponentInChildren<MenuFunctions>().Resume();
        }


    }
    public void PlayGame()
    {
        if (GameController.instance.sceneLoad == "ZombieLevel")
            SceneManager.LoadScene("ZombieLevel");
        else if (GameController.instance.sceneLoad == "BossLevel")
        {
            SceneManager.LoadScene("BossLevel");
        }
        GameController.instance.menuFunctions.GetComponent<MenuFunctions>().GameOnPause = false;
        GameController.instance.menuFunctions.GetComponent<MenuFunctions>().LoadState();
        GameController.instance.enemies.Clear();
        GameController.instance.zombiesDetected = 0;
        GameController.instance.volCanvas.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("ZombieLevel");
        GameController.instance.menuFunctions.GetComponent<MenuFunctions>().GameOnPause = false;
        GameController.instance.player.transform.position = new Vector3(75f, 3.483939f, 160f);
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
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeVolume(float volume)
    {
        volController = GameObject.Find("GameController");
        if (volController == null) return;
        AudioSource music = volController.GetComponent<AudioSource>();
        music.volume = volume;
    }

}
