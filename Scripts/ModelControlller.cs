using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ModelControlller : MonoBehaviour
{
    /// <summary>���߼�����</summary>
    private ARRaycastManager m_ARRaycastManager;
    /// <summary>�������ж��󼯺�</summary>
    private List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    /// <summary>�ڷ�����</summary>
    private Pose m_PlacementPose;
    /// <summary>AR���</summary>
    private Camera m_Camera;
    /// <summary>�Ƿ���ģ��</summary>
    private bool m_IsTouchModel = false;

    /// <summary>ѡ�е�ģ��</summary>
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


    /// <summary>���˵���㼶</summary>
    private LayerMask mask = 1 << 8;
    /// <summary>�Ƿ���ģ��</summary>
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

    /// <summary>�ж��Ƿ�����UI����</summary>
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

    /// <summary>�ƶ�ģ��</summary>
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

    /// <summary>��תģ��</summary>
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