using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("KPI�뱧ԹUI")]
    public float maxTime = 300f; // ����ʱ��
    public float curTime = 0f; // ��ǰʱ��
    public float timeBarMaxLength = 275f; // ʱ���ܳ���
    public int complaintTimes = 0; // ��Թ����
    public int complaintMaxTime = 0; // ���Թ����
    public GameObject kpiBarOutside; // KPI���ⲿ(�����ڵ�)
    public GameObject kpiBarInside; // KPI���ڲ�(���ڵ�)
    public List<GameObject> complaintUI; // ��Թֵ����

    private void Start()
    {
        complaintTimes = 0;

        for (int i = 0; i < complaintUI.Count; i++)
        {
            // �޸ľ���ͼ
            complaintUI[i].gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/ComplaintOff");
        }

        curTime = Time.timeSinceLevelLoad;// ��λΪ��
    }

    private void Update()
    {
        curTime = Time.timeSinceLevelLoad;
    }

    public void SubmitWrongGift()
    {
        complaintTimes++;
        // ÿ����һ��, ��Թֵ���Ӿͻ�仯ͼ��һ��
        for (int i = 0; i < complaintTimes; i++)
        {
            // �޸ľ���ͼ
            complaintUI[i].gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/ComplaintOn");
        }
        
        if (complaintTimes >= complaintMaxTime)
        {
            // ��Ϸ����
            Debug.Log("��Ϸ����");

            // TODO: ��Ϸ�����߼�
        }
    }
}
