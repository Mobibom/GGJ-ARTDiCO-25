using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loseScene : MonoBehaviour
{
    public GameObject black;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void return_home()
    {
        black.SetActive(true);
        black.SendMessage("SeventoOne");
    }
}
