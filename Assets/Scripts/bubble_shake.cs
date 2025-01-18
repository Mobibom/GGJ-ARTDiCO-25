using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubble_shake : MonoBehaviour
{
    public float wobbleAmount = 10f;  // 控制晃动幅度
    public float wobbleSpeed = 3f;     // 控制晃动速度
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 播放晃动效果
        StartCoroutine(WobbleBubble(this.gameObject, wobbleSpeed + Random.Range(-0.5f, 0.5f), wobbleAmount + Random.Range(-3f, 3f)));

    }

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
