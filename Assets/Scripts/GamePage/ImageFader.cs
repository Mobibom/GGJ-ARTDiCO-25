using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageSequenceFader : MonoBehaviour
{
    public Image imageA;  // ������A��Image���
    public Image imageB;  // ������B��Image���
    public float fadeDuration = 1.0f;  // ����ĳ���ʱ��

    void Start()
    {
        // ��ʼ����������A��B
        imageA.gameObject.SetActive(false);
        imageB.gameObject.SetActive(false);

        // ������������
        StartCoroutine(ShowImagesSequence());
    }

    IEnumerator ShowImagesSequence()
    {
        // �ȴ�1�룬����ʲô������ʾ
        yield return new WaitForSeconds(1f);

        // ��ʾimageA��������ʾ
        imageA.gameObject.SetActive(true);
        yield return StartCoroutine(FadeImage(imageA, 0f, 1f));

        // �ȴ�1��
        yield return new WaitForSeconds(1f);

        // ��������imageA
        yield return StartCoroutine(FadeImage(imageA, 1f, 0f));
        imageA.gameObject.SetActive(false);  // ����imageA

        // �ȴ�0.5��
        yield return new WaitForSeconds(0.5f);

        // ��ʾimageB��������ʾ
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

        // ȷ������ֵΪĿ��ֵ
        image.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
