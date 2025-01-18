using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pageAnimation : MonoBehaviour
{
    public GameObject black;
    private float stay_time = 11;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stay_time -= Time.deltaTime;
        if (stay_time < 0 )
        {
            black.SetActive(true);
            black.SendMessage("TwotoThree");
            stay_time = 100f;
        }
    }
}
