using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pageStart : MonoBehaviour
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

    public void start_button()
    {
        black.SendMessage("OnetoTwo");
    }
}
