using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAnim : MonoBehaviour
{
    public GameObject snakeAnimInstance;
    private GameObject snakeAnimObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("snakeWake");
            snakeAnimObj = Instantiate(snakeAnimInstance,null);
            snakeAnimObj.transform.parent = this.transform;
            snakeAnimObj.transform.localPosition = Vector3.zero;
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, 0);
            Destroy(snakeAnimObj, 1.0f);
            Destroy(this.gameObject, 1.0f);
        }
    }
}
