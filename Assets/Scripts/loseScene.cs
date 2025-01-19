using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loseScene : MonoBehaviour
{
    public GameObject black;
    public GameObject sorry;
    private float t = -2f;
    private Image image;  // ���� Image ���
    // Start is called before the first frame update
    void Start()
    {
        image = sorry.GetComponent<Image>();
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

    public void QuitGame()
    {
        // ����������ڱ༭���У�ʹ�� EditorApplication ���˳�
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // ����ǹ��������Ϸ���˳�Ӧ��
        Application.Quit();
        #endif
    }
}
