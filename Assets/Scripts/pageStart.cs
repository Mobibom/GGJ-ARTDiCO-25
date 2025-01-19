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

    // 退出游戏
    public void QuitGame()
    {
        // 如果是运行在编辑器中，使用 EditorApplication 来退出
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // 如果是构建后的游戏，退出应用
        Application.Quit();
        #endif
    }

    public void start_button()
    {
        black.SetActive(true);
        black.SendMessage("OnetoTwo");
    }
}
