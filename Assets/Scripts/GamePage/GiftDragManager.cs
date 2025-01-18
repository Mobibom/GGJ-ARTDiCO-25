using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GiftDragManager : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [Header("游戏玩法")]
    public int currBoy = 1;
    public GameObject black; // 关卡切换黑幕(与下一关控制有关)

    [Header("目标区域")]    
    public GameObject targetBubble; // 目标气泡
    public GameObject dreamStory; // 梦境故事
    private RectTransform rectTransform; // 拖动目标的 RectTransform
    public float tolerance = 20f; // 容差值

    [Header("当前礼物信息")]
    private Vector3 originPos; // 初始位置

    [Header("拖动参数")]
    public float returnTime = 1f; // 返回原位的时间
    public float moveToTarPosTime = 0.5f; // 移动到目标位置的时间
    public float appearTime = 1f; // 出现的时间
    public float disappearTime = 1f; // 消失的时间

    private Coroutine returnCoroutine; // 用于存储协程
    private Coroutine moveToTarPosCoroutine; // 用于存储协程
    private Coroutine disappearCoroutine; // 用于存储协程
    private Coroutine appearCoroutine; // 用于存储协程
    private Coroutine enableActiveCoroutine; // 用于存储协程
    private Coroutine switchSceneCoroutine; // 用于存储协程

    // 触发的方法
    public delegate void OnDropHandler(); // 定义委托

    private void Awake()
    {
        // black.SetActive(false);

        // 将目标气泡中的GreamStory隐藏
        dreamStory.SetActive(false);

        // 获取当前物体的 RectTransform 组件
        rectTransform = GetComponent<RectTransform>();

        // 保存初始位置
        originPos = rectTransform.localPosition;

        //Debug.Log("originPos: " + originPos);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        
        // 拖动过程中更新 UI 元素的位置, 使其跟随鼠标移动
        rectTransform.position = eventData.position;
    }

    // 处理拖动结束(鼠标抬起)的事件
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        // 判断当前拖动的物体是否放置在目标区域内
        if (IsInTargetArea())
        {
            //Debug.Log("拖动到目标位置！");
            // 如果当前物体的名字不是RightGift，则执行错误逻辑
            if (gameObject.name != "RightGift")
            {
                BackToOriginPos();
                WrongGift();
            }
            else
            {

                RightGift();

                // 缓慢移动到目标位置
                if (moveToTarPosCoroutine != null) StopCoroutine(moveToTarPosCoroutine);
                moveToTarPosCoroutine = StartCoroutine(
                    MoveImageInDiffrenParent(rectTransform, rectTransform.position, targetBubble.GetComponent<RectTransform>().position, moveToTarPosTime)
                    );

                // 物体缓慢消失
                if (disappearCoroutine != null) StopCoroutine(disappearCoroutine);
                disappearCoroutine = StartCoroutine(FadeImage(gameObject, 1f, 0f, disappearTime));

                // 延迟后将物体设置为active false
                if (enableActiveCoroutine != null) StopCoroutine(enableActiveCoroutine);
                enableActiveCoroutine = StartCoroutine(DisactiveAfterDelay(10 * disappearTime, false));

            }
        }
        else
        {
            // 如果没有放在目标区域，可以选择返回原位或其他处理逻辑
            //Debug.Log("未到目标位置");
            BackToOriginPos();
        }
    }

    // 判断物体是否在目标区域
    private bool IsInTargetArea()
    {
        // 获取目标区域的世界坐标
        //Rect targetRect = targetArea.rect; // 目标区域的矩形
        Vector3[] worldCorners = new Vector3[4];
        targetBubble.GetComponent<RectTransform>().GetWorldCorners(worldCorners); // 获取目标区域四个角的世界坐标

        // 计算目标区域的最小和最大世界坐标
        Vector2 areaMin = worldCorners[0]; // 左下角
        Vector2 areaMax = worldCorners[2]; // 右上角

        // 判断当前物体的位置是否在目标区域内，并且有容差
        return rectTransform.position.x > areaMin.x + tolerance &&
               rectTransform.position.x < areaMax.x - tolerance &&
               rectTransform.position.y > areaMin.y + tolerance &&
               rectTransform.position.y < areaMax.y - tolerance;
    }

    private void RightGift()
    {
        // 在main气泡中找到名为GreamStory的子物体
        GameObject image = dreamStory;

        image.GetComponent<CanvasGroup>().alpha = 0f; // 设置透明度为0
        image.SetActive(true); // 设置为active true

        // 物体缓慢出现
        if (appearCoroutine != null) StopCoroutine(appearCoroutine);
        appearCoroutine = StartCoroutine(FadeImage(image, 0f, 1f, appearTime));

        // TODO: 固定时间后切换场景
        if (switchSceneCoroutine != null) StopCoroutine(switchSceneCoroutine);
        switchSceneCoroutine = StartCoroutine(SwitchSceneWithDelay(2f));
    }

    // 延迟切换场景
    private IEnumerator SwitchSceneWithDelay(float delay)
    {
        Debug.Log("SwitchSceneWithDelay");
        yield return new WaitForSeconds(delay);

        black.SetActive(true);
        if (currBoy == 1)
            black.SendMessage("ThreetoFour");
        else if (currBoy == 2)
            black.SendMessage("FourtoFive");
        else if (currBoy == 3)
            black.SendMessage("FivetoSix");

        Debug.Log("SwitchSceneWithDelayEnd");
    }

    private void WrongGift()
    {
        // TODO: UIManager的 complaintUI 中的格子变化, 每错一次就变化一个格子(剩下的逻辑在UIManager中实现)

        // 找到挂在有UIManager脚本的物体
        GameObject uiManager = GameObject.FindObjectOfType<UIManager>().gameObject;

        if (uiManager == null)
        {
            Debug.LogError("Can't find UIManager in the scene!");
        }

        // 调用UIManager中的 complaintUI 中的格子变化方法
        uiManager.GetComponent<UIManager>().SubmitWrongGift();


    }

    private void BackToOriginPos()
    {
        // 缓慢移动到原位
        if (returnCoroutine != null) StopCoroutine(returnCoroutine);
        returnCoroutine = StartCoroutine(MoveImageInSameParent(rectTransform, rectTransform.localPosition, originPos, returnTime));
    }

    // 携程控制位置变化(参数: 目标RectTransform, 起始位置, 目标位置, 持续时间)
    private IEnumerator MoveImageInSameParent(RectTransform rectTransform, Vector3 startPos, Vector3 endPos, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            rectTransform.localPosition = Vector3.Lerp(startPos, endPos, t);
            //Debug.Log("rectTransform.position: " + rectTransform.localPosition);
            yield return null;
        }
        rectTransform.localPosition = endPos;
    }

    // 携程控制位置变化(参数: 目标RectTransform, 起始位置, 目标位置, 持续时间)
    private IEnumerator MoveImageInDiffrenParent(RectTransform rectTransform, Vector3 startPos, Vector3 endPos, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            rectTransform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
        rectTransform.position = endPos;
    }

    // 携程控制物体透明度变化(参数: 目标物体, 起始透明度, 目标透明度, 持续时间)
    private IEnumerator FadeImage(GameObject image, float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            image.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            yield return null;
        }
        image.GetComponent<CanvasGroup>().alpha = endAlpha;
    }

    // 在延迟后将物体设置为active false
    IEnumerator DisactiveAfterDelay(float delay, bool switchActive)
    {
        // 等待一秒
        yield return new WaitForSeconds(delay);
        // 设置物体为active false
        gameObject.SetActive(switchActive);
    }
}
