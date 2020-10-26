using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class StartUI : MonoBehaviour
{
    public GameObject buttons;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            buttons.SetActive(true);
        }
    }
    public void EnterGame()
    {
        SceneManager.LoadScene("MoveScene");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
