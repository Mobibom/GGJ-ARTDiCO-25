using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class display_sound : MonoBehaviour
{
    public AudioSource audioSource;  // 引用 AudioSource 组件
    public AudioClip clickSound;     // 要播放的点击音效

    void Start()
    {
        if (audioSource == null)
        {
            // 如果没有赋值 AudioSource，自动获取
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // 检测鼠标点击事件
        if (Input.GetMouseButtonDown(0))  // 0 代表鼠标左键
        {
            PlayClickEffect();
        }
    }

    // 播放音效的方法
    void PlayClickEffect()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);  // 播放点击音效
        }
    }
}
