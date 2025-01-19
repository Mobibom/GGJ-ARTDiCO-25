using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winScene : MonoBehaviour
{
    public GameObject background;
    private float size_now = 1f;
    private float speed = 0.1f;
    // ��Ҫ�ı�Сͼ��λ�ã���˴����Ӧ�ø����޸ģ�background��posX��posYҲҪ�ĳɶ�Ӧ�ĸ���
    private int pos_x = 1091;
    private int pos_y = 1777;

    public GameObject button;
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
            button.SetActive(true);
        }
    }

    public void pressed_button()
    {
        black.SetActive(true);
        black.SendMessage("SixtoOne");
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
