using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetMine : MonoBehaviour
{
    public string name;
    public LayerMask layer; 

    //可以开采
    private bool canMine;
    //开采进度
    private float progress;

    //UI
    private Slider slider;
    //private 
    // Start is called before the first frame update
    void Start()
    {
        name = this.gameObject.name;

        canMine = false;
        progress = 0;

        //ui
        slider = this.transform.GetChild(0).GetChild(0).GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        MineProgress();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log(1);
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.back, 5f,layer);
                if(hit.collider.tag == "Mine")
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
            progress += Time.deltaTime;
            slider.value = progress;
            if(slider.value >= slider.maxValue)
            {
                slider.value = slider.maxValue;
                Debug.Log(this.name + "开采成功");
                //TODO

            }
            //Debug.Log(progress);
        }
    }
}
