using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("游戏失败")]
    public int currBoy = 1;
    public GameObject black; // 关卡切换黑幕(与下一关控制有关)

    [Header("耗时与抱怨属性")]
    public float maxTime = 300f; // 限制时间
    public float curTime = 0f; // 当前时间
    //public float timeBarMaxLength = 275f; // 时间总长度
    public int complaintTimes = 0; // 抱怨次数
    public int complaintMaxTime = 3; // 最大抱怨次数

    [Header("UI组件")]
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

    public void FixedUpdate() // 固定时间
    {
        curTime = Time.timeSinceLevelLoad;

        // 更新时间条
        kpiBarInside.transform.localScale = new Vector3((curTime / maxTime), kpiBarInside.transform.localScale.y, kpiBarInside.transform.localScale.z);

        if (curTime >= maxTime)
        {
            GameFailed();
        }
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
