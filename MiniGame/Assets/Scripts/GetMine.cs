﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetMine : MonoBehaviour
{
    public int ID;
    public LayerMask layer; 

    //可以开采
    private bool canMine;
    //开采进度
    private float progress;

    //UI
    private GameObject sliderObj;
    private Slider slider;

    //player
    private GameObject player;
    private LemmingMove testMove;
     
    // Start is called before the first frame update
    void Start()
    {
        name = this.gameObject.name;

        canMine = false;
        progress = 0;

        //ui
        sliderObj = this.transform.GetChild(0).GetChild(0).gameObject;
        slider = sliderObj.GetComponent<Slider>();

        //player
        player = GameObject.FindWithTag("Player");
        //player运动的脚本
        testMove = player.GetComponent<LemmingMove>();
    }

    // Update is called once per frame
    void Update()
    {
        MineProgress();
        Interrupt();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //Debug.Log(1);
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.back, 5f,layer);
                if(hit && hit.collider.tag == "Mine")
                {
                    Debug.Log("Click");
                    canMine = true;
                }
            }
        }
    }

    void MineProgress()
    {
        if (canMine)
        {
            sliderObj.SetActive(true);
            progress += Time.deltaTime;
            slider.value = progress;
            if(slider.value >= slider.maxValue)
            {
                slider.value = slider.maxValue;
                Debug.Log(this.name + "开采成功");
                Destroy(gameObject,0.5f);
                //TODO

            }
            //Debug.Log(progress);
        }
    }

    void Interrupt()
    {
        if(testMove.moveDir.x != 0 || testMove.moveDir.y != 0)
        {
            canMine = false;
            sliderObj.SetActive(false);
        }
    }
}
