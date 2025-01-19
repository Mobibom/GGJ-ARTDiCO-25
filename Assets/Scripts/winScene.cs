using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winScene : MonoBehaviour
{
    public GameObject background;
    private float size_now = 1f;
    private float speed = 0.1f;
    // 若要改变小图的位置，则此处左边应该跟着修改，background的posX和posY也要改成对应的负数
    private int pos_x = 1091;
    private int pos_y = 1777;

    public GameObject black;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rate = Screen.width / 2560f;
        if (size_now > 0.2f)
        {
            size_now -= speed * Time.deltaTime;
            background.transform.localScale = new Vector3(size_now, size_now, 1);
            background.transform.Translate(pos_x * Time.deltaTime * speed * 1.25f * rate, pos_y * Time.deltaTime * speed * 1.25f * rate, 0);
        }
        else if (size_now < 0.2f)
        {
            size_now = 0.2f;
            background.transform.localScale = new Vector3(size_now, size_now, 1);
            background.transform.Translate(pos_x * Time.deltaTime * speed * 1.25f * rate, pos_y * Time.deltaTime * speed * 1.25f * rate, 0);

        }
    }

    public void pressed_button()
    {
        black.SetActive(true);
        black.SendMessage("SixtoOne");
    }
}
