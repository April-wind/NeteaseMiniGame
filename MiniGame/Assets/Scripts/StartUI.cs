using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartUI : MonoBehaviour
{
    public GameObject player;
    public GameObject begin;
    public GameObject foodSlider1;
    public GameObject foodSlider2;

  void Awake()
    {
        this.GetComponent<CameraMove>().enabled = false;
        Time.timeScale = 0;
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {

    }
    public void EnterGame()
    {
        Destroy(begin);
        transform.DOMove(new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z), 2).SetUpdate(true);
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;
        this.GetComponent<CameraMove>().enabled = true;
        foodSlider1.GetComponent<Image>().enabled = true;
        foodSlider2.GetComponent<Image>().enabled = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
