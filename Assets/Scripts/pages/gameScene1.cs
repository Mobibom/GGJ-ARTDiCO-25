using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameScene1 : MonoBehaviour
{
    public int boy = 1;
    public GameObject black;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void button_pressed()
    {
        black.SetActive(true);
        if (boy == 1) 
            black.SendMessage("ThreetoFour");
        else if (boy == 2)
            black.SendMessage("FourtoFive");
        else if (boy == 3)
            black.SendMessage("FivetoSix");
    }
}
