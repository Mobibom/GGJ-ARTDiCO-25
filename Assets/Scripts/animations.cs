using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animations : MonoBehaviour
{
    public float wait_time;
    public float target_x;
    public float dir; // 1 »ò -1
    private float moveSpeed = 1000f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        wait_time -= Time.deltaTime;
        if (wait_time < 0 && transform.position.x < target_x && dir > 0)
        {
            transform.Translate(dir * Time.deltaTime * moveSpeed, 0, 0);
        }
        if (wait_time < 0 && transform.position.x > target_x && dir < 0)
        {
            transform.Translate(dir * Time.deltaTime * moveSpeed, 0, 0);
        }
    }
}
