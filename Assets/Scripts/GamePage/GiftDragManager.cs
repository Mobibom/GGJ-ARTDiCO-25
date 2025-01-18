using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GiftDragManager : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [Header("��Ϸ�淨")]
    public int currBoy = 1;
    public GameObject black; // �ؿ��л���Ļ(����һ�ؿ����й�)

    [Header("Ŀ������")]    
    public GameObject targetBubble; // Ŀ������
    public GameObject dreamStory; // �ξ�����
    private RectTransform rectTransform; // �϶�Ŀ��� RectTransform
    public float tolerance = 20f; // �ݲ�ֵ

    [Header("��ǰ������Ϣ")]
    private Vector3 originPos; // ��ʼλ��

    [Header("�϶�����")]
    public float returnTime = 1f; // ����ԭλ��ʱ��
    public float moveToTarPosTime = 0.5f; // �ƶ���Ŀ��λ�õ�ʱ��
    public float appearTime = 1f; // ���ֵ�ʱ��
    public float disappearTime = 1f; // ��ʧ��ʱ��

    private Coroutine returnCoroutine; // ���ڴ洢Э��
    private Coroutine moveToTarPosCoroutine; // ���ڴ洢Э��
    private Coroutine disappearCoroutine; // ���ڴ洢Э��
    private Coroutine appearCoroutine; // ���ڴ洢Э��
    private Coroutine enableActiveCoroutine; // ���ڴ洢Э��
    private Coroutine switchSceneCoroutine; // ���ڴ洢Э��

    // �����ķ���
    public delegate void OnDropHandler(); // ����ί��

    private void Awake()
    {
        // black.SetActive(false);

        // ��Ŀ�������е�GreamStory����
        dreamStory.SetActive(false);

        // ��ȡ��ǰ����� RectTransform ���
        rectTransform = GetComponent<RectTransform>();

        // �����ʼλ��
        originPos = rectTransform.localPosition;

        //Debug.Log("originPos: " + originPos);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        
        // �϶������и��� UI Ԫ�ص�λ��, ʹ���������ƶ�
        rectTransform.position = eventData.position;
    }

    // �����϶�����(���̧��)���¼�
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        // �жϵ�ǰ�϶��������Ƿ������Ŀ��������
        if (IsInTargetArea())
        {
            //Debug.Log("�϶���Ŀ��λ�ã�");
            // �����ǰ��������ֲ���RightGift����ִ�д����߼�
            if (gameObject.name != "RightGift")
            {
                BackToOriginPos();
                WrongGift();
            }
            else
            {

                RightGift();

                // �����ƶ���Ŀ��λ��
                if (moveToTarPosCoroutine != null) StopCoroutine(moveToTarPosCoroutine);
                moveToTarPosCoroutine = StartCoroutine(
                    MoveImageInDiffrenParent(rectTransform, rectTransform.position, targetBubble.GetComponent<RectTransform>().position, moveToTarPosTime)
                    );

                // ���建����ʧ
                if (disappearCoroutine != null) StopCoroutine(disappearCoroutine);
                disappearCoroutine = StartCoroutine(FadeImage(gameObject, 1f, 0f, disappearTime));

                // �ӳٺ���������Ϊactive false
                if (enableActiveCoroutine != null) StopCoroutine(enableActiveCoroutine);
                enableActiveCoroutine = StartCoroutine(DisactiveAfterDelay(10 * disappearTime, false));

            }
        }
        else
        {
            // ���û�з���Ŀ�����򣬿���ѡ�񷵻�ԭλ�����������߼�
            //Debug.Log("δ��Ŀ��λ��");
            BackToOriginPos();
        }
    }

    // �ж������Ƿ���Ŀ������
    private bool IsInTargetArea()
    {
        // ��ȡĿ���������������
        //Rect targetRect = targetArea.rect; // Ŀ������ľ���
        Vector3[] worldCorners = new Vector3[4];
        targetBubble.GetComponent<RectTransform>().GetWorldCorners(worldCorners); // ��ȡĿ�������ĸ��ǵ���������

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
        // ��main�������ҵ���ΪGreamStory��������
        GameObject image = dreamStory;

        image.GetComponent<CanvasGroup>().alpha = 0f; // ����͸����Ϊ0
        image.SetActive(true); // ����Ϊactive true

        // ���建������
        if (appearCoroutine != null) StopCoroutine(appearCoroutine);
        appearCoroutine = StartCoroutine(FadeImage(image, 0f, 1f, appearTime));

        // TODO: �̶�ʱ����л�����
        if (switchSceneCoroutine != null) StopCoroutine(switchSceneCoroutine);
        switchSceneCoroutine = StartCoroutine(SwitchSceneWithDelay(2f));
    }

    // �ӳ��л�����
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
        // TODO: UIManager�� complaintUI �еĸ��ӱ仯, ÿ��һ�ξͱ仯һ������(ʣ�µ��߼���UIManager��ʵ��)

        // �ҵ�������UIManager�ű�������
        GameObject uiManager = GameObject.FindObjectOfType<UIManager>().gameObject;

        if (uiManager == null)
        {
            Debug.LogError("Can't find UIManager in the scene!");
        }

        // ����UIManager�е� complaintUI �еĸ��ӱ仯����
        uiManager.GetComponent<UIManager>().SubmitWrongGift();


    }

    private void BackToOriginPos()
    {
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
