using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    public GameObject lemming;
    public GameObject backpack;
    public Camera ScaleUICamera;
    public Vector3 lemmingScreenPosition;
    public LemmingSumControl lemmingSumControl;

    void Update()
    {
        BackpackMove();
        Print();
    }

    private void BackpackMove()
    {
        // backpack.transform.localPosition = WorldPos2Rect(MainCamera, lemming.transform.position) - new Vector3(MainCamera.pixelWidth / 2, MainCamera.pixelHeight / 2, lemming.transform.position.z);
        lemmingScreenPosition = Camera.main.WorldToScreenPoint(lemming.transform.position);
        this.transform.position = ScaleUICamera.ScreenToWorldPoint(lemmingScreenPosition);
    }


    private void Print()
    {
        Vector2 worlPos = ScaleUICamera.WorldToScreenPoint(backpack.transform.position);

        Vector2 worlPos2 = Camera.main.WorldToScreenPoint(lemming.transform.position);
        if (Input.GetKeyDown(KeyCode.J))
            Debug.Log(worlPos);
        if (Input.GetKeyDown(KeyCode.K))
            Debug.Log(worlPos2);

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(lemmingSumControl.LemmingNum);
        }
    }
}
