using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SecretDetailManager : MonoBehaviour
{
    [Header("ͼƬ����")]
    public Image showSecretsDetailImage;
    public Vector2 maxSize = new(1920, 1200);
    public Vector2 minSize = new(0, 0);
    public bool isClickable = true;

    [Header("��Ҫ���ص�����UIԪ��")]
    public List<GameObject> otherUIElements;
    public GameObject bubbleParentObject;

    [Header("�Ŵ���С")]
    public float duration = 1f; // �Ŵ���С��ʱ��

    private Coroutine resizeCoroutine; // ���ڴ洢Э��
    private Coroutine enableClickCoroutine; // ���ڴ洢Э��
    private Coroutine hidePicCoroutine; // ���ڴ洢Э��
    private Coroutine showOtherUIElementsCoroutine; // ���ڴ洢Э��

    private void Start()
    {
        // ȷ����������ͼƬ�����ص�
        showSecretsDetailImage.gameObject.SetActive(false);
    }
    public void ShowPicSprite()
    {
        if (!isClickable) return;
        string buttonName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        Debug.Log(buttonName);

        // 0. ���ð�ť���
        isClickable = false;
        // ����Э�̣�1���ָ���ť���
        if (enableClickCoroutine != null) StopCoroutine(enableClickCoroutine);
        enableClickCoroutine = StartCoroutine(EnableClickAfterDelay(duration));

        // 1. �����µ�ͼƬ������ͼƬ��С
        // Debug.Log("�����µ�ͼƬ");
        showSecretsDetailImage.sprite = Resources.Load<Sprite>("Sprites/" + buttonName);
        // Debug.Log(showSecretsDetailImage.sprite);
        showSecretsDetailImage.rectTransform.sizeDelta = minSize;

        // 2. ���س����������UIԪ��
        // Debug.Log("���س����������UIԪ��");
        foreach (GameObject ui in otherUIElements)
        {
            ui.SetActive(false);
        }

        // 3. ��ʾShowSecretsDetailͼƬ(��ѡ: ������ʾ)
        // Debug.Log("��ʾShowSecretsDetailͼƬ");
        showSecretsDetailImage.gameObject.SetActive(true);

        // 4. �𽥷Ŵ�ͼƬ��maxSize�Ĵ�С
        // Debug.Log("�𽥷Ŵ�ͼƬ"); 
        if (resizeCoroutine != null) StopCoroutine(resizeCoroutine);
        resizeCoroutine = StartCoroutine(ResizeImage(showSecretsDetailImage.rectTransform, minSize, maxSize, duration));
    }

    public void HidePicSprite()
    {
        if (!isClickable) return;

        // 0. ���ð�ť���
        isClickable = false;

        // ����Э�̣�1���ָ���ť���
        if (enableClickCoroutine != null) StopCoroutine(enableClickCoroutine);
        enableClickCoroutine = StartCoroutine(EnableClickAfterDelay(duration));

        // 1. ����СͼƬ
        // Debug.Log("����СͼƬ");
        if (resizeCoroutine != null) StopCoroutine(resizeCoroutine);
        resizeCoroutine = StartCoroutine(ResizeImage(showSecretsDetailImage.rectTransform, maxSize, minSize, duration));

        // 2. �ӳ�����ShowSecretsDetailͼƬ
        // Debug.Log("����ShowSecretsDetailͼƬ");
        if (hidePicCoroutine != null) StopCoroutine(hidePicCoroutine);
        hidePicCoroutine = StartCoroutine(HideWithDelay(duration));

        // 3. ��ʾ�����������UIԪ��
        // Debug.Log("��ʾ�����������UIԪ��");
        if (showOtherUIElementsCoroutine != null) StopCoroutine(showOtherUIElementsCoroutine);
        showOtherUIElementsCoroutine = StartCoroutine(ShowOtherUIElementsWithDelay(duration));
    }

    // ���ӳٺ�ָ���ť���
    private IEnumerator EnableClickAfterDelay(float delay)
    {
        // �ȴ�һ��
        yield return new WaitForSeconds(delay);

        // �ָ���ť�������
        isClickable = true;
        // Debug.Log("�ָ���ť���");
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

    // �ӳ���ʾ�����������UIԪ��
    private IEnumerator ShowOtherUIElementsWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (GameObject ui in otherUIElements)
        {
            ui.SetActive(true);
        }
        bubbleParentObject.GetComponent<DreamBubbleManager>().StartInitBubbles();
    }
}
