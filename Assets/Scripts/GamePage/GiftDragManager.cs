using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GiftDragManager : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [Header("Ŀ������")]    
    public RectTransform targetArea; // Ŀ������� RectTransform
    private RectTransform rectTransform; // �϶�Ŀ��� RectTransform
    public float tolerance = 20f; // �ݲ�ֵ

    [Header("��ǰ������Ϣ")]
    private Vector3 originPos; // ��ʼλ��

    [Header("�϶�����")]
    public float returnTime = 1f; // ����ԭλ��ʱ��
    public float moveToTarPosTime = 0.5f; // �ƶ���Ŀ��λ�õ�ʱ��
    public float disappearTime = 1f; // ��ʧ��ʱ��

    private Coroutine returnCoroutine; // ���ڴ洢Э��
    private Coroutine moveToTarPosCoroutine; // ���ڴ洢Э��
    private Coroutine disappearCoroutine; // ���ڴ洢Э��
    private Coroutine enableActiveCoroutine; // ���ڴ洢Э��

    // �����ķ���
    public delegate void OnDropHandler(); // ����ί��

    private void Awake()
    {
        // ��ȡ��ǰ����� RectTransform ���
        rectTransform = GetComponent<RectTransform>();

        // �����ʼλ��
        originPos = rectTransform.localPosition;

        Debug.Log("originPos: " + originPos);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // �϶������и��� UI Ԫ�ص�λ��, ʹ���������ƶ�
        rectTransform.position = eventData.position;
    }

    // �����϶�����(���̧��)���¼�
    public void OnEndDrag(PointerEventData eventData)
    {
        // �жϵ�ǰ�϶��������Ƿ������Ŀ��������
        if (IsInTargetArea())
        {
            //Debug.Log("�϶���Ŀ��λ�ã�");
            // �����ǰ��������ֲ���RightGift����ִ�д����߼�
            if (gameObject.name != "RightGift")
            {
                WrongGift();
            }
            else
            {
                RightGift();
            }
        }
        else
        {
            // ���û�з���Ŀ�����򣬿���ѡ�񷵻�ԭλ�����������߼�
            //Debug.Log("δ��Ŀ��λ��");
            NonArrival();
        }
    }

    // �ж������Ƿ���Ŀ������
    private bool IsInTargetArea()
    {
        // ��ȡĿ���������������
        //Rect targetRect = targetArea.rect; // Ŀ������ľ���
        Vector3[] worldCorners = new Vector3[4];
        targetArea.GetWorldCorners(worldCorners); // ��ȡĿ�������ĸ��ǵ���������

        // ����Ŀ���������С�������������
        Vector2 areaMin = worldCorners[0]; // ���½�
        Vector2 areaMax = worldCorners[2]; // ���Ͻ�

        // �жϵ�ǰ�����λ���Ƿ���Ŀ�������ڣ��������ݲ�
        return rectTransform.position.x > areaMin.x + tolerance &&
               rectTransform.position.x < areaMax.x - tolerance &&
               rectTransform.position.y > areaMin.y + tolerance &&
               rectTransform.position.y < areaMax.y - tolerance;
    }

    private void RightGift()
    {
        //Debug.Log("Right Gift");

        // �����ƶ���Ŀ��λ��
        if (moveToTarPosCoroutine != null) StopCoroutine(moveToTarPosCoroutine);
        moveToTarPosCoroutine = StartCoroutine(MoveImageInDiffrenParent(rectTransform, rectTransform.position, targetArea.position, moveToTarPosTime));

        // ���建����ʧ
        if (disappearCoroutine != null) StopCoroutine(disappearCoroutine);
        disappearCoroutine = StartCoroutine(FadeImage(gameObject, 1f, 0f, disappearTime));

        // �ӳٺ���������Ϊactive false
        if (enableActiveCoroutine != null) StopCoroutine(enableActiveCoroutine);
        enableActiveCoroutine = StartCoroutine(DisactiveAfterDelay(disappearTime,false));
    }

    private void WrongGift()
    {
        //Debug.Log("Wrong Gift");

        // �����ƶ���ԭλ
        if (returnCoroutine != null) StopCoroutine(returnCoroutine);
        returnCoroutine = StartCoroutine(MoveImageInSameParent(rectTransform, rectTransform.localPosition, originPos, returnTime));
    }
    private void NonArrival()
    {
        //Debug.Log("Non Arrival");

        // �����ƶ���ԭλ
        if (returnCoroutine != null) StopCoroutine(returnCoroutine);
        returnCoroutine = StartCoroutine(MoveImageInSameParent(rectTransform, rectTransform.localPosition, originPos, returnTime));
    }

    // Я�̿���λ�ñ仯(����: Ŀ��RectTransform, ��ʼλ��, Ŀ��λ��, ����ʱ��)
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

    // Я�̿���λ�ñ仯(����: Ŀ��RectTransform, ��ʼλ��, Ŀ��λ��, ����ʱ��)
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

    // Я�̿�������͸���ȱ仯(����: Ŀ������, ��ʼ͸����, Ŀ��͸����, ����ʱ��)
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

    // ���ӳٺ���������Ϊactive false
    IEnumerator DisactiveAfterDelay(float delay, bool switchActive)
    {
        // �ȴ�һ��
        yield return new WaitForSeconds(delay);
        // ��������Ϊactive false
        gameObject.SetActive(switchActive);
    }
}
