using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("��Ϸʧ��")]
    public int currBoy = 1;
    public GameObject black; // �ؿ��л���Ļ(����һ�ؿ����й�)

    [Header("��ʱ�뱧Թ����")]
    public float maxTime = 300f; // ����ʱ��
    public float curTime = 0f; // ��ǰʱ��
    //public float timeBarMaxLength = 275f; // ʱ���ܳ���
    public int complaintTimes = 0; // ��Թ����
    public int complaintMaxTime = 3; // ���Թ����

    [Header("UI���")]
    public GameObject kpiBarOutside; // KPI���ⲿ(�����ڵ�)
    public GameObject kpiBarInside; // KPI���ڲ�(���ڵ�)
    public List<GameObject> complaintUI; // ��Թֵ����

    private void Start()
    {
        complaintTimes = 0;

        for (int i = 0; i < complaintUI.Count; i++)
        {
            // �޸ľ���ͼ
            complaintUI[i].gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ComplaintOff");
        }

        curTime = Time.timeSinceLevelLoad;// ��λΪ��
    }

    public void FixedUpdate() // �̶�ʱ��
    {
        curTime = Time.timeSinceLevelLoad;

        // ����ʱ����������
        float newScaleX = curTime / maxTime;
        kpiBarInside.transform.localScale = new Vector3(newScaleX, kpiBarInside.transform.localScale.y, kpiBarInside.transform.localScale.z);

        // ����λ�ƣ�ʹ����������࿪ʼ����
        float offsetX = (1 - newScaleX) * kpiBarInside.GetComponent<RectTransform>().rect.width / 2;
        kpiBarInside.transform.localPosition = new Vector3(-offsetX, kpiBarInside.transform.localPosition.y, kpiBarInside.transform.localPosition.z);

        if (curTime >= maxTime)
        {
            GameFailed();
        }
    }

    public void SubmitWrongGift()
    {
        complaintTimes++;
        // ÿ����һ��, ��Թֵ���Ӿͻ�仯ͼ��һ��
        for (int i = 0; i < complaintTimes; i++)
        {
            // �޸ľ���ͼ
            complaintUI[i].gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/ComplaintOn");

        }

        if (complaintTimes >= complaintMaxTime)
        {
            GameFailed();
        }
    }

    public void GameFailed()
    {
        black.SetActive(true);
        if (currBoy == 1)
            black.SendMessage("ThreetoSeven");
        else if (currBoy == 2)
            black.SendMessage("FourtoSeven");
        else if (currBoy == 3)
            black.SendMessage("FivetoSeven");
    }
}
