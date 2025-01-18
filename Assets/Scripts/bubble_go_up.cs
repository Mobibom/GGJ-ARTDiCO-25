using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubble_go_up : MonoBehaviour
{
    public RectTransform rectTransform;  // �������� UI Ԫ�ص� RectTransform
    public float go_up = 0f;
    public float speedx;
    public float speedy;
    public float slow_down = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // ������� x �� y ����ĳ�ʼ�ٶ�
        speedx = Random.Range(-10.0f, 10.0f);
        speedy = Random.Range(-10.0f, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // ���Ƽ���
        slow_down -= Time.deltaTime;
        if (slow_down > 0)
        {
            // ����λ�ã�ʹ�� anchoredPosition ������ͨ�� Transform.Translate
            rectTransform.anchoredPosition += new Vector2(speedx * slow_down, speedy * slow_down);
        }

        // �������ϸ�����Ч��
        go_up += Time.deltaTime;
        rectTransform.anchoredPosition += new Vector2(0, go_up);
    }
}
