using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class emo_show : MonoBehaviour
{
    public float t;
    private Image image;  // ���� Image ���
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (1f >= t && t > 0f) // ��ʼ����
        {
            Color currentColor = image.color;
            currentColor.a = t;
            image.color = currentColor;
        }
        
    }
}
