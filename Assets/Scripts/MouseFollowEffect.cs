using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollowEffect : MonoBehaviour
{
    public GameObject effectPrefab;        // 要显示的特效预设
    public Canvas uiCanvas;                // UI Canvas，设置为 Screen Space
    private GameObject effectInstance;     // 实例化的特效

    void Start()
    {
        // 实例化特效并将其设置为 UI Canvas 的子物体
        effectInstance = Instantiate(effectPrefab);
        effectInstance.transform.SetParent(uiCanvas.transform, false);
    }

    void Update()
    {
        // 获取鼠标的屏幕位置
        Vector2 mousePos = Input.mousePosition;

        // 将屏幕坐标转换为 Canvas 的本地坐标
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            uiCanvas.GetComponent<RectTransform>(),
            mousePos,
            uiCanvas.worldCamera,
            out localPos
        );

        // 设置特效的位置（保持在 UI 层中）
        effectInstance.GetComponent<RectTransform>().localPosition = localPos;
    }
}
