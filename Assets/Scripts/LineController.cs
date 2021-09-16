using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LineController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bigBall;
    public GameObject points;
    private LineRenderer _lr;

    void Awake()
    {
        _lr = GetComponent<LineRenderer>();
        bigBall = GameObject.Find("Target Sphere");

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
