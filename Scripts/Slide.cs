using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slide : MonoBehaviour
{

    private Vector2 DeltaArea;       //��ά��������������

    //private bool BoolSecondClick;           //�Ƿ�Ϊ�ڶ��ε��
    //private float FloFirstTime = 0f;          //��һ�ε��ʱ��
    //private float FloSecondTime = 0f;         //�ڶ��ε��ʱ��


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
        //��ʼ����������ֵ
        DeltaArea = Vector2.zero;

    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(Show1());

        if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            DeltaArea = Vector2.zero;
            //DoubleClickTips.text = "";          //�����ָ�뿪��Ļ��˫��Ч����ʧ
        }
        

        /* ʶ����ָ���� */
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
                UpDownTips.text = "�ϻ���";

            }
            else if (DeltaArea.y < -150)
            {
                UpDownTips.text = "�»���";
            }
            */
        }


        /* ��ָ˫��ʶ��*/
        /*
        if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            FloSecondTime = Time.time;
            if (FloSecondTime - FloFirstTime > 0.02F && FloSecondTime - FloFirstTime < 0.3F)
            {//���ڶ��ε�����һ�ε����ʱ������0.02����0.3��֮��ʱ
                DoubleClickTips.text = "˫������Ļ��";
            }
            else
            {
                DoubleClickTips.text = "��������Ļ��";
            }
            FloFirstTime = Time.time;       //��¼ʱ��

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
