using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubble_shake : MonoBehaviour
{
    public float wobbleAmount = 10f;  // ���ƻζ�����
    public float wobbleSpeed = 3f;     // ���ƻζ��ٶ�
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���Żζ�Ч��
        StartCoroutine(WobbleBubble(this.gameObject, wobbleSpeed + Random.Range(-0.5f, 0.5f), wobbleAmount + Random.Range(-3f, 3f)));

    }

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
