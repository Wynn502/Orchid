using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class PlacementController : MonoBehaviour
{
    /// <summary>需要摆放的模型</summary>
    public GameObject Orchard;
    /// <summary>准星模型</summary>
    public GameObject placementIndicator;
    /// <summary>放置后就不给他再放置模型了）</summary>
    private bool m_IsPlaceObject = false;
    /// <summary>射线检测管理</summary>
    private ARRaycastManager m_ARRaycastManager;
    /// <summary>摆放姿势</summary>
    private Pose m_PlacementPose;
    /// <summary>是否可以摆放</summary>
    private bool m_PlacementPoseIsValid = false;
    /// <summary>射线命中对象集合</summary>
    private List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    /// <summary>屏幕中心位置</summary>
    private Vector2 m_ScreenCenter;



    private void Awake()
    {
        m_ARRaycastManager = FindObjectOfType<ARRaycastManager>();
        if (m_ARRaycastManager == null)
        {
            Debug.LogError(GetType() + "/Awake()/ ARSessionOrigin is null");
        }

        placementIndicator.SetActive(false);
    }

    private void Update()
    {
        if (m_IsPlaceObject || isTouchUI()) return;

        if (Orchard == null)
        {
            placementIndicator?.SetActive(false);
            return;
        }




        updatePlacementPose();
        updatePlacementIndicator();

        if (m_PlacementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            placeObject();
        }
    }

    /// <summary>更新摆放姿态</summary>
    private void updatePlacementPose()
    {
        // change this code
        m_ScreenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        m_ARRaycastManager.Raycast(m_ScreenCenter, m_Hits, TrackableType.Planes);

        m_PlacementPoseIsValid = m_Hits.Count > 0;
        if (m_PlacementPoseIsValid)
        {
            m_PlacementPose = m_Hits[0].pose;

            var cameraForward = Camera.main.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            m_PlacementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    /// <summary>更新展示位置指示器</summary>
    private void updatePlacementIndicator()
    {
        placementIndicator?.SetActive(m_PlacementPoseIsValid);

        if (m_PlacementPoseIsValid)
        {
            placementIndicator.transform.SetPositionAndRotation(m_PlacementPose.position, m_PlacementPose.rotation);
        }
    }

    /// <summary>放置物件</summary>
    private void placeObject()
    {
        if (Orchard == null) return;
        GameObject item = Instantiate(Orchard, m_PlacementPose.position, m_PlacementPose.rotation);
        item.name = Orchard.name;
        //item.transform.Find("Selected").gameObject.SetActive(false);
        m_IsPlaceObject = true;

        placementIndicator.SetActive(false);
    }

    // <summary>判断是否点击在UI上面</summary>
    /*private bool isTouchUI()
    {
        if (UnityEngine.EventSystems.EventSystem.current == null) return false;

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount < 1) return false;
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return true;
        }
        else
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return true;
        }

        return false;
    }*/

}

