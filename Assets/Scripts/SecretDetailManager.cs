using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecretDetailManager : MonoBehaviour
{
    [Header("ͼƬ����")]
    public Image showSecretsDetailImage;
    public Vector2 maxSize = new(1920, 1200);
    public Vector2 minSize = new(192, 120);

    [Header("��Ҫ���ص�����UIԪ��")]
    public List<GameObject> otherUIElements;

    [Header("�Ŵ���С")]
    public float duration = 1f; // �Ŵ���С��ʱ��
    private Coroutine resizeCoroutine; // ���ڴ洢Э��

    private void Start()
    {
        showSecretsDetailImage.gameObject.SetActive(false);
    }
    public void ShowPicSprite()
    {
        string buttonName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        // 1. �����µ�ͼƬ������ͼƬ��С
        Debug.Log("�����µ�ͼƬ");
        showSecretsDetailImage.sprite = Resources.Load<Sprite>(buttonName);
        showSecretsDetailImage.rectTransform.sizeDelta = minSize;

        // 2. ���س����������UIԪ��
        Debug.Log("���س����������UIԪ��");
        foreach (GameObject ui in otherUIElements)
        {
            ui.SetActive(false);
        }

        // 3. ��ʾShowSecretsDetailͼƬ(��ѡ: ������ʾ)
        Debug.Log("��ʾShowSecretsDetailͼƬ");
        showSecretsDetailImage.gameObject.SetActive(true);

        // 4. �𽥷Ŵ�ͼƬ��maxSize�Ĵ�С
        Debug.Log("�𽥷Ŵ�ͼƬ"); 
        if (resizeCoroutine != null) StopCoroutine(resizeCoroutine);
        resizeCoroutine = StartCoroutine(ResizeImage(showSecretsDetailImage.rectTransform, minSize, maxSize, duration));
    }

    public void HidePicSprite()
    {
        // 1. ����СͼƬ
        Debug.Log("����СͼƬ"); 
        if (resizeCoroutine != null) StopCoroutine(resizeCoroutine);
        resizeCoroutine = StartCoroutine(ResizeImage(showSecretsDetailImage.rectTransform, maxSize, minSize, duration));

        // 2. �ӳ�����ShowSecretsDetailͼƬ
        Debug.Log("����ShowSecretsDetailͼƬ");
        StartCoroutine(HideWithDelay(duration));

        // 3. ��ʾ�����������UIԪ��
        Debug.Log("��ʾ�����������UIԪ��");
        foreach (GameObject ui in otherUIElements)
        {
            ui.SetActive(true);
        }
    }

    // Э�̿��ƴ�С�仯(����: Ŀ��RectTransform, ��ʼ��С, Ŀ���С, ����ʱ��)
    private IEnumerator ResizeImage(RectTransform rectTransform, Vector2 startSize, Vector2 endSize, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            rectTransform.sizeDelta = Vector2.Lerp(startSize, endSize, t);
            yield return null;
        }
        rectTransform.sizeDelta = endSize;
    }

    // �ӳ�����ShowSecretsDetailͼƬ
    private IEnumerator HideWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        showSecretsDetailImage.gameObject.SetActive(false);
    }
}
