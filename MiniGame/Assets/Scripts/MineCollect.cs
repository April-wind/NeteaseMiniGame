using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCollect : MonoBehaviour
{
    private static MineCollect _instance;
    public static MineCollect _Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    private void Awake()
    {
        _instance = this;
    }

    public List<int> mineDic;

    // Start is called before the first frame update
    void Start()
    {
        mineDic = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
