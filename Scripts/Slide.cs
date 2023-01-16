using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slide : MonoBehaviour
{

    private Vector2 DeltaArea;       //二维向量，滑屏区域

    //private bool BoolSecondClick;           //是否为第二次点击
    //private float FloFirstTime = 0f;          //第一次点击时间
    //private float FloSecondTime = 0f;         //第二次点击时间


    public GameObject parentObj;
    public GameObject orchid_F;
    public GameObject orchid_T;
    public GameObject orchid_H;
    //public GameObject orchid_G;

    int a = 1;

    //public float timer = 4.0f;


    // Start is called before the first frame update
    void Start()
    {
        //初始化，测试数值
        DeltaArea = Vector2.zero;

    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(Show1());

        if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            DeltaArea = Vector2.zero;
            //DoubleClickTips.text = "";          //如果手指离开屏幕，双击效果消失
        }
        

        /* 识别手指滑屏 */
        if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            DeltaArea.x += Input.GetTouch(0).deltaPosition.x;           
            DeltaArea.y += Input.GetTouch(0).deltaPosition.y;

            if (Time.frameCount % 3 == 0)
            {
                if (DeltaArea.x < -150 && a == 1)
                {
                    // left
                    orchid_T.SetActive(true);
                    orchid_F.SetActive(false);
                    orchid_H.SetActive(false);
                    a = 2;
                }

                else if (DeltaArea.x < -150 && a == 2)
                {
                    // left
                    orchid_T.SetActive(false);
                    orchid_F.SetActive(false);
                    orchid_H.SetActive(true);
                    a = 3;
                }

                else if (DeltaArea.x > 150 && a == 3)
                {
                    //right
                    orchid_T.SetActive(true);
                    orchid_F.SetActive(false);
                    orchid_H.SetActive(false);
                    a = 2;
                }


                else if (DeltaArea.x > 150 && a == 2)
                {
                    //right
                    orchid_T.SetActive(false);
                    orchid_F.SetActive(true);
                    orchid_H.SetActive(false);
                    a = 1;
                }

            }



            /*
            if (DeltaArea.y > 150)
            {
                UpDownTips.text = "上滑屏";

            }
            else if (DeltaArea.y < -150)
            {
                UpDownTips.text = "下滑屏";
            }
            */
        }


        /* 手指双击识别*/
        /*
        if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            FloSecondTime = Time.time;
            if (FloSecondTime - FloFirstTime > 0.02F && FloSecondTime - FloFirstTime < 0.3F)
            {//当第二次点击与第一次点击的时间间隔在0.02秒至0.3秒之间时
                DoubleClickTips.text = "双击了屏幕！";
            }
            else
            {
                DoubleClickTips.text = "单击了屏幕！";
            }
            FloFirstTime = Time.time;       //记录时间

        }
        */

    }




    //private IEnumerator Show1()
    //{
    //    yield return new WaitForSeconds(4);
    //    Func1();
    //}

    //private void Func1()
    //{
    //    orchid_G.SetActive(false);
    //    orchid_F.SetActive(true);
    //    a = 1;
    //}






}
