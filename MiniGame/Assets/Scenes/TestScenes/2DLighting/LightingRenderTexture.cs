using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightingRenderTexture : MonoBehaviour
{
    [SerializeField]
    private Material ScreenMat;

    [SerializeField]
    private Camera LightCam;

    private RenderTexture rt;

    // Start is called before the first frame update
    private void Start()
    {
        rt = new RenderTexture(Screen.width, Screen.height, 24);
        LightCam.targetTexture = rt;
        ScreenMat.SetTexture("_RenderTexture", rt);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (ScreenMat != null)
        {
            Graphics.Blit(src, dst, ScreenMat);
        }
    }
}