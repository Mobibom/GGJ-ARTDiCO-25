using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loseScene : MonoBehaviour
{
    public GameObject black;
    public GameObject sorry;
    private float t = -2f;
    private Image image;  // 引用 Image 组件
    // Start is called before the first frame update
    void Start()
    {
        image = sorry.GetComponent<Image>();
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
}
