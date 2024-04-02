using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMenuScript : MonoBehaviour
{

    public GameObject introMenu;

    void Awake()
    {
        introMenu.SetActive(false);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            introMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameController.instance.menuFunctions.SetActive(false);
            Time.timeScale = 0f;
        }
    }

    public void ExitButton()
    {
        introMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameController.instance.menuFunctions.SetActive(true);
    }
}
