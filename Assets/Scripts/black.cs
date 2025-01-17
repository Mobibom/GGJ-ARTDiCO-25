using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class black : MonoBehaviour
{
    public float speed = 1f; //变黑的速度

    public GameObject scene1;
    public GameObject scene2;
    public GameObject scene3;
    public GameObject scene4;
    public GameObject scene5;

    private GameObject sceneNow; // 当前场景
    private GameObject sceneNext; // 下一个场景
    public bool start_turn_black = false; // 是否开始变黑
    public bool start_turn_alpha = false; // 是否开始变亮
    private Image image;  // 引用 Image 组件
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        sceneNow = scene1;
        sceneNext = null;
    }

    // Update is called once per frame
    void Update()
    {
        // 如果sceneNext不为Null,则说明需要进行场景转换
        if (sceneNext != null)
        {
            if (start_turn_black) turn_black();
            else if (start_turn_alpha) turn_alpha();
        }
    }

    private void turn_black()
    {
        float alpha = image.color.a;
        alpha += speed * Time.deltaTime;
        if (alpha > 1f) // 后面开始变亮
        { 
            alpha = 1f;
            start_turn_black = false;
            start_turn_alpha = true;
            // 场景转换
            page_switch();
        }
        Color currentColor = image.color;
        currentColor.a = alpha;
        image.color = currentColor;
    }

    private void turn_alpha()
    {
        float alpha = image.color.a;
        alpha -= speed * Time.deltaTime;
        if (alpha < 0f) // 后面停止
        {
            alpha = 0f;
            start_turn_alpha = false;
        }
        Color currentColor = image.color;
        currentColor.a = alpha;
        image.color = currentColor;
    }

    private void page_switch()
    {
        sceneNow.SetActive(false);
        sceneNext.SetActive(true);
    }

    public void OnetoTwo()
    {
        sceneNow = scene1;
        sceneNext = scene2;
        start_turn_black = true;
    }
}
