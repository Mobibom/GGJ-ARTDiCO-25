using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DreamBubbleManager : MonoBehaviour
{
    [Header("梦气泡")]
    public List<GameObject> BubbleList;
    public float enterSceneDelay = 1f; // 进入场景后延迟时间
    public float delayBetweenBubbles = 0.5f; // 每个气泡之间的延迟时间
    public float scaleUpDuration = 1f; // 气泡放大的时间
    public float wobbleAmount = 10f;  // 控制晃动幅度
    public float wobbleSpeed = 3f;     // 控制晃动速度

    // [Header("气泡初始化时禁用线索点击")]
    // public GameObject buttonParentObject;

    private Coroutine coroutineShowBubble; // 用于存储协程
    private Dictionary<GameObject, Vector3> originalSizes = new Dictionary<GameObject, Vector3>(); // 存储每个气泡的初始大小

    // Start is called before the first frame update
    void Start()
    {
        // 记录每个气泡的初始大小
        foreach (GameObject bubble in BubbleList)
        {
            originalSizes[bubble] = bubble.transform.localScale;
        }

        // 确保所有气泡都是隐藏的
        foreach (GameObject bubble in BubbleList)
        {
            bubble.SetActive(false);
        }

        // 进入场景后延迟一秒, 依次显示小孩头顶气泡
        // 当第一个气泡显示完毕后, 依次显示第二个气泡
        // 以此类推, 直到所有气泡显示完毕
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

    // 协程: 依次显示气泡
    private IEnumerator ShowBubbles()
    {
        yield return new WaitForSeconds(enterSceneDelay);
        // 遍历所有气泡
        foreach (GameObject bubble in BubbleList)
        {
            // 播放放大效果
            StartCoroutine(ScaleBubble(bubble));

            // 播放晃动效果
            StartCoroutine(WobbleBubble(bubble, wobbleSpeed + Random.Range(-0.5f, 0.5f), wobbleAmount + Random.Range(-3f, 3f)));

            // 等待一段时间
            yield return new WaitForSeconds(delayBetweenBubbles);
        }
    }

    // 放大效果：逐渐放大气泡
    private IEnumerator ScaleBubble(GameObject bubble)
    {
        Vector3 originalScale = new Vector3(0, 0, 0);
        Vector3 targetScale = originalSizes[bubble]; // 放大 1.5 倍，调整放大的比例

        bubble.transform.localScale = originalScale; // 设置气泡初始大小

        bubble.SetActive(true);// 显示当前气泡

        float elapsedTime = 0f; // 已经过去的时间

        while (elapsedTime < scaleUpDuration) // 在规定时间内放大气泡
        {
            bubble.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / scaleUpDuration); // 放大气泡
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保放大结束后气泡达到了目标大小
        bubble.transform.localScale = targetScale;
    }

    // 晃动效果：气泡晃动
    private IEnumerator WobbleBubble(GameObject bubble, float wSpeed, float wAmount)
    {
        Vector3 initialPosition = bubble.transform.localPosition;

        while (true)
        {
            // 计算偏移量，通过正弦函数让物体在水平和垂直方向上做微小的振动
            float offsetX = Mathf.Sin(Time.time * wSpeed) * wAmount;
            float offsetY = Mathf.Cos(Time.time * wSpeed) * wAmount;

            // 设置新的位置
            bubble.transform.localPosition = initialPosition + new Vector3(offsetX, offsetY, 0f);

            yield return null;
        }
    }
}
