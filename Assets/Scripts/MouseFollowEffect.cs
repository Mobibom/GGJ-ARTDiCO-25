using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollowEffect : MonoBehaviour
{
    public GameObject effectPrefab;        // Ҫ��ʾ����ЧԤ��
    public Canvas uiCanvas;                // UI Canvas������Ϊ Screen Space
    private GameObject effectInstance;     // ʵ��������Ч

    void Start()
    {
        // ʵ������Ч����������Ϊ UI Canvas ��������
        effectInstance = Instantiate(effectPrefab);
        effectInstance.transform.SetParent(uiCanvas.transform, false);
    }

    void Update()
    {
        // ��ȡ������Ļλ��
        Vector2 mousePos = Input.mousePosition;

        // ����Ļ����ת��Ϊ Canvas �ı�������
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            uiCanvas.GetComponent<RectTransform>(),
            mousePos,
            uiCanvas.worldCamera,
            out localPos
        );

        // ������Ч��λ�ã������� UI ���У�
        effectInstance.GetComponent<RectTransform>().localPosition = localPos;
    }
}
