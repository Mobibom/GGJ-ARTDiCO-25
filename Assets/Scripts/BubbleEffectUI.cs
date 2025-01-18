using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleEffectUI : MonoBehaviour
{
    public GameObject bubblePrefab;       // ��������Ԥ��
    public Canvas uiCanvas;               // UI Canvas��ȷ���� Screen Space ģʽ
    public float bubbleSpawnRate = 0.1f;  // �������ɵ�Ƶ��

    private float nextBubbleTime = 0f;    // �����������ɵ�ʱ��

    void Update()
    {
        // ��ȡ���λ��
        Vector2 mousePosition = Input.mousePosition;

        // �����������ݵ�Ƶ��
        if (Time.time > nextBubbleTime)
        {
            nextBubbleTime = Time.time + bubbleSpawnRate;
            SpawnBubble(mousePosition);
        }
    }

    void SpawnBubble(Vector2 position)
    {
        // �����λ����������
        GameObject bubble = Instantiate(bubblePrefab, position, Quaternion.identity);

        // ��������Ϊ UI Canvas ��������
        bubble.transform.SetParent(uiCanvas.transform, false);

        // ������λ������Ϊ UI ��ı�������
        RectTransform bubbleRect = bubble.GetComponent<RectTransform>();
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            uiCanvas.GetComponent<RectTransform>(),
            position,
            uiCanvas.worldCamera,
            out localPos
        );
        bubbleRect.localPosition = localPos;

        // �������������ϵͳ����������Ϊ���ϵ�Ч��
        ParticleSystem ps = bubble.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            // ������������ϵͳ�ĳ�ʼ����
            var main = ps.main;
            main.startRotation = Random.Range(0f, 360f);
            main.startSpeed = Random.Range(2f, 4f);
            main.startSize = Random.Range(0.3f, 0.6f);
        }

        // �������ݣ��������ڳ��������ô���
        Destroy(bubble, 3f);  // ������Ҫ��������ʱ��
    }
}
