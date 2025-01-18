using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleEffectUI : MonoBehaviour
{
    public GameObject bubblePrefab;       // 泡泡粒子预设
    public Canvas uiCanvas;               // UI Canvas，确保是 Screen Space 模式
    public float bubbleSpawnRate = 0.1f;  // 泡泡生成的频率

    private float nextBubbleTime = 0f;    // 控制泡泡生成的时间

    void Update()
    {
        // 获取鼠标位置
        Vector2 mousePosition = Input.mousePosition;

        // 控制生成泡泡的频率
        if (Time.time > nextBubbleTime)
        {
            nextBubbleTime = Time.time + bubbleSpawnRate;
            SpawnBubble(mousePosition);
        }
    }

    void SpawnBubble(Vector2 position)
    {
        // 在鼠标位置生成泡泡
        GameObject bubble = Instantiate(bubblePrefab, position, Quaternion.identity);

        // 设置泡泡为 UI Canvas 的子物体
        bubble.transform.SetParent(uiCanvas.transform, false);

        // 将泡泡位置设置为 UI 层的本地坐标
        RectTransform bubbleRect = bubble.GetComponent<RectTransform>();
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            uiCanvas.GetComponent<RectTransform>(),
            position,
            uiCanvas.worldCamera,
            out localPos
        );
        bubbleRect.localPosition = localPos;

        // 如果泡泡是粒子系统，将其设置为向上的效果
        ParticleSystem ps = bubble.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            // 设置泡泡粒子系统的初始设置
            var main = ps.main;
            main.startRotation = Random.Range(0f, 360f);
            main.startSpeed = Random.Range(2f, 4f);
            main.startSize = Random.Range(0.3f, 0.6f);
        }

        // 销毁泡泡，避免它在场景中永久存在
        Destroy(bubble, 3f);  // 根据需要调整销毁时间
    }
}
