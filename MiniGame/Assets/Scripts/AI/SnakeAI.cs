using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAI : MonoBehaviour
{
    public GameObject snakePrefab;
    private GameObject snakeObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //随机造蛇，人碰到扣血
        if(collision.tag == "Player")
        {
            snakeObj = Instantiate(snakePrefab, null);
            snakeObj.transform.parent = this.transform;
            snakeObj.transform.position = this.transform.position + new Vector3(
                Random.Range(-this.transform.GetComponent<BoxCollider2D>().size.x / 2, this.transform.GetComponent<BoxCollider2D>().size.x / 2),
                Random.Range(-this.transform.GetComponent<BoxCollider2D>().size.y / 2, this.transform.GetComponent<BoxCollider2D>().size.y / 2),
                0);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(this.gameObject, 0.5f);
        }
    }
}
