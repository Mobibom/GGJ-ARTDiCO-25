using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("KPI与抱怨UI")]
    public float maxTime = 300f; // 限制时间
    public float curTime = 0f; // 当前时间
    public float timeBarMaxLength = 275f; // 时间总长度
    public int complaintTimes = 0; // 抱怨次数
    public int complaintMaxTime = 0; // 最大抱怨次数
    public GameObject kpiBarOutside; // KPI条外部(不被遮挡)
    public GameObject kpiBarInside; // KPI条内部(被遮挡)
    public List<GameObject> complaintUI; // 抱怨值格子

    private void Start()
    {
        complaintTimes = 0;

        for (int i = 0; i < complaintUI.Count; i++)
        {
            // 修改精灵图
            complaintUI[i].gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/ComplaintOff");
        }

        curTime = Time.timeSinceLevelLoad;// 单位为秒
    }

    private void Update()
    {
        curTime = Time.timeSinceLevelLoad;
    }

    public void SubmitWrongGift()
    {
        complaintTimes++;
        // 每调用一次, 抱怨值格子就会变化图案一个
        for (int i = 0; i < complaintTimes; i++)
        {
            // 修改精灵图
            complaintUI[i].gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/ComplaintOn");
        }
        
        if (complaintTimes >= complaintMaxTime)
        {
            // 游戏结束
            Debug.Log("游戏结束");

            // TODO: 游戏结束逻辑
        }
    }
}
