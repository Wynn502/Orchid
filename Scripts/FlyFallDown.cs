using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyFallDown : MonoBehaviour
{

    private Vector2 DeltaArea;
    public GameObject Plane1;
    public GameObject Plane2;
    public GameObject Bee1;
    public GameObject Bee2;
    public GameObject Bee3;
    public GameObject half_body1;
    public GameObject half_body2;
    public GameObject pollen;
    public GameObject bee_pollen;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject particle;

    public int Bee_status = 0; 


    void Start()
    {
        Plane1.SetActive(true);
        Plane2.SetActive(false);
        half_body1.SetActive(true);
        half_body2.SetActive(false);

        //pollen.SetActive(true);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "plane")
        {
            //m_meshrenderer.material.color = Color.green;
            //Destroy(other.gameObject);

            Plane1.SetActive(false);
            Plane2.SetActive(true);
            //cube2.SetActive(false);

        }

        //pollen
        if (other.tag == "pollen")
        { 
            pollen.SetActive(false); 
        
        }

        bee_pollen.SetActive(true);


    }


    void OnTriggerExit(Collider other)
    {
        if (other.tag == "plane1")
            //m_meshrenderer.material.color = Color.green;
            //Destroy(other.gameObject);

        Plane1.SetActive(true);
        Plane2.SetActive(false);
        //cube2.SetActive(false);
        

    }





    void Update()
    {


        //Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Began)
        //Input.GetMouseButtonDown(0)

        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
            {


                //Click the Plane
                if (hit.collider.gameObject.name == "Plane.001_Glinting")
                {

                    Bee2.SetActive(true);
                    Bee1.SetActive(false);

                    text1.SetActive(true);

                    particle.SetActive(false);

                    //Using delay to control the time
                    Bee_status = 1;
                    StartCoroutine(Show());
                }

                //Click the Body
                if (hit.collider.gameObject.name == "half body.001_Glinting")
                {

                    half_body1.SetActive(true);
                    half_body2.SetActive(false);

                    text2.SetActive(true);
                    text1.SetActive(false);

                    Bee3.SetActive(true);
                    Bee2.SetActive(false);

                    StartCoroutine(Show2());
                    //UI set here

                }




            }

        }



    }


    //control the orchid_body
    private IEnumerator Show()
    {
        yield return new WaitForSeconds(10);
        Func();
    }

    private void Func()
    {
      
        half_body2.SetActive(true);
        half_body1.SetActive(false);
    }

    private IEnumerator Show2()
    {
        yield return new WaitForSeconds(6);
        Func2();
    }

    private void Func2()
    {
        text2.SetActive(false);
        text3.SetActive(true);
    }



}
