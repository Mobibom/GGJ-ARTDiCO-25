using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pageStart : MonoBehaviour
{
    public GameObject black;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyUp(KeyCode.Escape)) { QuitGame(); }
    }

    // �˳���Ϸ
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

    public void start_button()
    {
        black.SetActive(true);
        black.SendMessage("OnetoTwo");
    }
}
