using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecretDetailManager : MonoBehaviour
{
    [Header("图片参数")]
    public Image showSecretsDetailImage;
    public Vector2 maxSize = new(1920, 1200);
    public Vector2 minSize = new(192, 120);

    [Header("需要隐藏的其他UI元素")]
    public List<GameObject> otherUIElements;

    [Header("放大缩小")]
    public float duration = 1f; // 放大缩小的时间
    private Coroutine resizeCoroutine; // 用于存储协程

    private void Start()
    {
        showSecretsDetailImage.gameObject.SetActive(false);
    }
    public void ShowPicSprite()
    {
        string buttonName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        // 1. 载入新的图片并设置图片大小
        Debug.Log("载入新的图片");
        showSecretsDetailImage.sprite = Resources.Load<Sprite>(buttonName);
        showSecretsDetailImage.rectTransform.sizeDelta = minSize;

        // 2. 隐藏场景里的其他UI元素
        Debug.Log("隐藏场景里的其他UI元素");
        foreach (GameObject ui in otherUIElements)
        {
            ui.SetActive(false);
        }

        // 3. 显示ShowSecretsDetail图片(可选: 渐变显示)
        Debug.Log("显示ShowSecretsDetail图片");
        showSecretsDetailImage.gameObject.SetActive(true);

        // 4. 逐渐放大图片到maxSize的大小
        Debug.Log("逐渐放大图片"); 
        if (resizeCoroutine != null) StopCoroutine(resizeCoroutine);
        resizeCoroutine = StartCoroutine(ResizeImage(showSecretsDetailImage.rectTransform, minSize, maxSize, duration));
    }

    public void HidePicSprite()
    {
        // 1. 逐渐缩小图片
        Debug.Log("逐渐缩小图片"); 
        if (resizeCoroutine != null) StopCoroutine(resizeCoroutine);
        resizeCoroutine = StartCoroutine(ResizeImage(showSecretsDetailImage.rectTransform, maxSize, minSize, duration));

        // 2. 延迟隐藏ShowSecretsDetail图片
        Debug.Log("隐藏ShowSecretsDetail图片");
        StartCoroutine(HideWithDelay(duration));

        // 3. 显示场景里的其他UI元素
        Debug.Log("显示场景里的其他UI元素");
        foreach (GameObject ui in otherUIElements)
        {
            ui.SetActive(true);
        }
    }

    // 协程控制大小变化(参数: 目标RectTransform, 起始大小, 目标大小, 持续时间)
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

    // 延迟隐藏ShowSecretsDetail图片
    private IEnumerator HideWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        showSecretsDetailImage.gameObject.SetActive(false);
    }
}
