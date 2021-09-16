using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Numartor : MonoBehaviour
{
    // Start is called before the first frame update
    public static int PinCount;
    public TextMeshProUGUI text;
    void Start()
    {
        PinCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = PinCount.ToString();
        
    }
}
