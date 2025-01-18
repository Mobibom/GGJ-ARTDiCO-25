using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubble_go_up : MonoBehaviour
{
    public RectTransform rectTransform;  // 用来操作 UI 元素的 RectTransform
    public float go_up = 0f;
    public float speedx;
    public float speedy;
    public float slow_down = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // 随机生成 x 和 y 方向的初始速度
        speedx = Random.Range(-10.0f, 10.0f);
        speedy = Random.Range(-10.0f, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // 控制减速
        slow_down -= Time.deltaTime;
        if (slow_down > 0)
        {
            // 更新位置，使用 anchoredPosition 而非普通的 Transform.Translate
            rectTransform.anchoredPosition += new Vector2(speedx * slow_down, speedy * slow_down);
        }

        // 控制向上浮动的效果
        go_up += Time.deltaTime;
        rectTransform.anchoredPosition += new Vector2(0, go_up);
    }
}
