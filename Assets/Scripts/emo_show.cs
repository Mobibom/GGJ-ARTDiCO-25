using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class emo_show : MonoBehaviour
{
    public float t;
    private Image image;  // 引用 Image 组件
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (1f >= t && t > 0f) // 开始变亮
        {
            Color currentColor = image.color;
            currentColor.a = t;
            image.color = currentColor;
        }
        
    }
}
