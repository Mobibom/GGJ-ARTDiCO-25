using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class santa_wave : MonoBehaviour
{
    public Image imageComponent;  // �������� Image ���
    public Sprite image1;         // ��һ��ͼƬ
    public Sprite image2;         // �ڶ���ͼƬ

    private float timer = 0f;     // ������ʱ�ı���
    private float switchInterval = 0.5f;  // �л����ʱ�䣨�룩
    private bool toggle = false;  // �����жϵ�ǰ��ʾ��������ͼƬ

    // Start is called before the first frame update
    void Start()
    {
        // ȷ�� Image ����Ѱ�
        if (imageComponent == null)
        {
            imageComponent = GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �ۼӾ�����ʱ��
        timer += Time.deltaTime;

        // �ж��Ƿ�ﵽ�л�ʱ��
        if (timer >= switchInterval)
        {
            // �л�ͼƬ
            if (toggle)
            {
                imageComponent.sprite = image1;
            }
            else
            {
                imageComponent.sprite = image2;
            }

            // �л���־
            toggle = !toggle;

            // ���ü�ʱ��
            timer = 0f;
        }
    }
}
