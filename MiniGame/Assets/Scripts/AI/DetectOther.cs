using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectOther : MonoBehaviour
{
    private FixedPathAI FixedPathAI;
    private RandomPathAI RandomPathAI;
    // Start is called before the first frame update
    void Start()
    {
        if(this.transform.parent.GetComponent<FixedPathAI>())
            FixedPathAI = this.transform.parent.GetComponent<FixedPathAI>();
        if (this.transform.parent.GetComponent<RandomPathAI>())
            RandomPathAI = this.transform.parent.GetComponent<RandomPathAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(FixedPathAI)
                FixedPathAI.state = State.Track;
            if (RandomPathAI)
                RandomPathAI.state = State.Track;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (FixedPathAI)
                FixedPathAI.state = State.Idle;
            if (RandomPathAI)
                RandomPathAI.state = State.Idle;
        }
    }
}
