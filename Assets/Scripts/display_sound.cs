using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class display_sound : MonoBehaviour
{
    public AudioSource audioSource;  // ���� AudioSource ���
    public AudioClip clickSound;     // Ҫ���ŵĵ����Ч

    void Start()
    {
        if (audioSource == null)
        {
            // ���û�и�ֵ AudioSource���Զ���ȡ
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // ���������¼�
        if (Input.GetMouseButtonDown(0))  // 0 ����������
        {
            PlayClickEffect();
        }
    }

    // ������Ч�ķ���
    void PlayClickEffect()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);  // ���ŵ����Ч
        }
    }
}
