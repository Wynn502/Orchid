using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ModelControlller : MonoBehaviour
{
    /// <summary>射线检测管理</summary>
    private ARRaycastManager m_ARRaycastManager;
    /// <summary>射线命中对象集合</summary>
    private List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    /// <summary>摆放姿势</summary>
    private Pose m_PlacementPose;
    /// <summary>AR相机</summary>
    private Camera m_Camera;
    /// <summary>是否触摸模型</summary>
    private bool m_IsTouchModel = false;

    /// <summary>选中的模型</summary>
    private GameObject m_SelectedModel;

    private void Awake()
    {
        m_ARRaycastManager = FindObjectOfType<ARRaycastManager>();
        m_Camera = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (!isTouchUI())
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_IsTouchModel = isClickModel(Input.mousePosition);
            }

            if (Input.touchCount == 1 && m_IsTouchModel) moveModel();
            if (Input.touchCount >= 2) rotateModel();
        }
    }


    /// <summary>过滤点击层级</summary>
    private LayerMask mask = 1 << 8;
    /// <summary>是否点击模型</summary>
    private bool isClickModel(Vector2 vector2)
    {
        Ray ray = m_Camera.ScreenPointToRay(vector2);
        RaycastHit hitInfo;

        bool isCollider = Physics.Raycast(ray, out hitInfo, 1000, mask);
        if (isCollider)
        {
            GameObject selectedModel = hitInfo.transform.gameObject;
            m_SelectedModel = selectedModel;
        }

        return isCollider;
    }

    /// <summary>判断是否点击在UI上面</summary>
    private bool isTouchUI()
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
    }

    /// <summary>移动模型</summary>
    private void moveModel()
    {
        if (Input.touchCount == 0 || m_ARRaycastManager == null || m_SelectedModel == null) return;

        var touch = Input.GetTouch(0);

        m_ARRaycastManager.Raycast(touch.position, m_Hits, TrackableType.Planes);
        if (m_Hits.Count > 0)
        {
            m_PlacementPose = m_Hits[0].pose;
            m_SelectedModel.transform.position = m_PlacementPose.position;
        }
    }

    /// <summary>旋转模型</summary>
    private void rotateModel()
    {
        if (m_SelectedModel == null) return;

        Touch oneFingerTouch;
        oneFingerTouch = Input.GetTouch(0);
        if (oneFingerTouch.phase == TouchPhase.Moved)
        {
            Vector2 deltaPos = oneFingerTouch.deltaPosition;
            m_SelectedModel.transform.Rotate(new Vector3(0, deltaPos.x * 0.2f, 0), Space.World);
        }
    }

}