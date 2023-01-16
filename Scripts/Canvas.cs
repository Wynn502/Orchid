using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    public float timer = 5.0f;
    public GameObject Canvas0;
    //public GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {
        //Invoke("Canvas0", 2.0f);
     
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {

            Canvas0.SetActive(false);
            //Camera.SetActive(true);

            timer = 5.0f;

        }

        


        //void Canvas_0()
        //{

        //    Canvas0.SetActive(false);

        //}


    }
}
