using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageSequenceFader : MonoBehaviour
{
    public Image imageA;  // 子物体A的Image组件
    public Image imageB;  // 子物体B的Image组件
    public float fadeDuration = 1.0f;  // 渐变的持续时间

    void Start()
    {
        // 初始化，先隐藏A和B
        imageA.gameObject.SetActive(false);
        imageB.gameObject.SetActive(false);

        // 启动渐变序列
        StartCoroutine(ShowImagesSequence());
    }

    IEnumerator ShowImagesSequence()
    {
        // 等待1秒，保持什么都不显示
        yield return new WaitForSeconds(1f);

        // 显示imageA，渐变显示
        imageA.gameObject.SetActive(true);
        yield return StartCoroutine(FadeImage(imageA, 0f, 1f));

        // 等待1秒
        yield return new WaitForSeconds(1f);

        // 渐变隐藏imageA
        yield return StartCoroutine(FadeImage(imageA, 1f, 0f));
        imageA.gameObject.SetActive(false);  // 隐藏imageA

        // 等待0.5秒
        yield return new WaitForSeconds(0.5f);

        // 显示imageB，渐变显示
        imageB.gameObject.SetActive(true);
        yield return StartCoroutine(FadeImage(imageB, 0f, 1f));
    }

    IEnumerator FadeImage(Image image, float startAlpha, float endAlpha)
    {
        Color color = image.color;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timeElapsed / fadeDuration);
            image.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // 确保最终值为目标值
        image.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
