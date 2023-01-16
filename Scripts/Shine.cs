using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shine : MonoBehaviour
{
    //MeshRenderer���
    public MeshRenderer thisRenderer;
    //����һ����������ʱ��仯ֵ
    float shankeTime = 0f;
    //�Ƿ�ʼ��˸
    public bool isShake = false;


    // Update is called once per frame
    void Update()
    {
        ToChangeColor();
    }
    /// <summary>
    /// �ı���ɫ�߼�
    /// </summary>
    void ToChangeColor()
    {
        if (isShake)
        {
            shankeTime += Time.deltaTime;
            if (shankeTime % 1 > 0.5f)
            {
                thisRenderer.material.color = Color.blue;
            }
            else
            {
                thisRenderer.material.color = Color.white;
            }
        }
    }
}
