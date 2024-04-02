using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMenuScript2 : MonoBehaviour
{

    public GameObject introMenu;

    void Awake()
    {
        introMenu.SetActive(false);
    }
    void Update()
    {

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
            Debug.Log(GameController.instance.menuFunctions.GetComponent<MenuFunctions>().GameOnPause);
        }
    }

    public void ExitButton()
    {
        Debug.Log("INTRO MENU EXIT BUTON");
        introMenu.SetActive(false);
        Time.timeScale = 1f; 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameController.instance.menuFunctions.SetActive(true);
    }
}
