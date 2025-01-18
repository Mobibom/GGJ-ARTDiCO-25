using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DreamBubbleManager : MonoBehaviour
{
    [Header("������")]
    public List<GameObject> BubbleList;
    public float enterSceneDelay = 1f; // ���볡�����ӳ�ʱ��
    public float delayBetweenBubbles = 0.5f; // ÿ������֮����ӳ�ʱ��
    public float scaleUpDuration = 1f; // ���ݷŴ��ʱ��
    public float wobbleAmount = 10f;  // ���ƻζ�����
    public float wobbleSpeed = 3f;     // ���ƻζ��ٶ�

    // [Header("���ݳ�ʼ��ʱ�����������")]
    // public GameObject buttonParentObject;

    private Coroutine coroutineShowBubble; // ���ڴ洢Э��
    private Dictionary<GameObject, Vector3> originalSizes = new Dictionary<GameObject, Vector3>(); // �洢ÿ�����ݵĳ�ʼ��С

    // Start is called before the first frame update
    void Start()
    {
        // ��¼ÿ�����ݵĳ�ʼ��С
        foreach (GameObject bubble in BubbleList)
        {
            originalSizes[bubble] = bubble.transform.localScale;
        }

        // ȷ���������ݶ������ص�
        foreach (GameObject bubble in BubbleList)
        {
            bubble.SetActive(false);
        }

        // ���볡�����ӳ�һ��, ������ʾС��ͷ������
        // ����һ��������ʾ��Ϻ�, ������ʾ�ڶ�������
        // �Դ�����, ֱ������������ʾ���
        StartInitBubbles();
    }

    public void SetSizeToZero()
    {
        foreach (GameObject bubble in BubbleList)
        {
            bubble.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    public void StartInitBubbles()
    {
        if (coroutineShowBubble != null) StopCoroutine(coroutineShowBubble);
        coroutineShowBubble = StartCoroutine(ShowBubbles());
    }

    // Э��: ������ʾ����
    private IEnumerator ShowBubbles()
    {
        yield return new WaitForSeconds(enterSceneDelay);
        // ������������
        foreach (GameObject bubble in BubbleList)
        {
            // ���ŷŴ�Ч��
            StartCoroutine(ScaleBubble(bubble));

            // ���Żζ�Ч��
            StartCoroutine(WobbleBubble(bubble, wobbleSpeed + Random.Range(-0.5f, 0.5f), wobbleAmount + Random.Range(-3f, 3f)));

            // �ȴ�һ��ʱ��
            yield return new WaitForSeconds(delayBetweenBubbles);
        }
    }

    // �Ŵ�Ч�����𽥷Ŵ�����
    private IEnumerator ScaleBubble(GameObject bubble)
    {
        Vector3 originalScale = new Vector3(0, 0, 0);
        Vector3 targetScale = originalSizes[bubble]; // �Ŵ� 1.5 ���������Ŵ�ı���

        bubble.transform.localScale = originalScale; // �������ݳ�ʼ��С

        bubble.SetActive(true);// ��ʾ��ǰ����

        float elapsedTime = 0f; // �Ѿ���ȥ��ʱ��

        while (elapsedTime < scaleUpDuration) // �ڹ涨ʱ���ڷŴ�����
        {
            bubble.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / scaleUpDuration); // �Ŵ�����
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ȷ���Ŵ���������ݴﵽ��Ŀ���С
        bubble.transform.localScale = targetScale;
    }

    // �ζ�Ч�������ݻζ�
    private IEnumerator WobbleBubble(GameObject bubble, float wSpeed, float wAmount)
    {
        Vector3 initialPosition = bubble.transform.localPosition;

        while (true)
        {
            // ����ƫ������ͨ�����Һ�����������ˮƽ�ʹ�ֱ��������΢С����
            float offsetX = Mathf.Sin(Time.time * wSpeed) * wAmount;
            float offsetY = Mathf.Cos(Time.time * wSpeed) * wAmount;

            // �����µ�λ��
            bubble.transform.localPosition = initialPosition + new Vector3(offsetX, offsetY, 0f);

            yield return null;
        }
    }
}
