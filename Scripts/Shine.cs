using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shine : MonoBehaviour
{
    //MeshRenderer组件
    public MeshRenderer thisRenderer;
    //创建一个常量接收时间变化值
    float shankeTime = 0f;
    //是否开始闪烁
    public bool isShake = false;


    // Update is called once per frame
    void Update()
    {
        ToChangeColor();
    }
    /// <summary>
    /// 改变颜色逻辑
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
