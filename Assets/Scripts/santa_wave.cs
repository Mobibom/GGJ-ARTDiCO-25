using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class santa_wave : MonoBehaviour
{
    public Image imageComponent;  // 用于引用 Image 组件
    public Sprite image1;         // 第一张图片
    public Sprite image2;         // 第二张图片

    private float timer = 0f;     // 用来计时的变量
    private float switchInterval = 0.5f;  // 切换间隔时间（秒）
    private bool toggle = false;  // 用于判断当前显示的是哪张图片

    // Start is called before the first frame update
    void Start()
    {
        // 确保 Image 组件已绑定
        if (imageComponent == null)
        {
            imageComponent = GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 累加经过的时间
        timer += Time.deltaTime;

        // 判断是否达到切换时间
        if (timer >= switchInterval)
        {
            // 切换图片
            if (toggle)
            {
                imageComponent.sprite = image1;
            }
            else
            {
                imageComponent.sprite = image2;
            }

            // 切换标志
            toggle = !toggle;

            // 重置计时器
            timer = 0f;
        }
    }
}
