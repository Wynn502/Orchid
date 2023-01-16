using System.Collections;
using UnityEngine;

public class Skode_Glinting : MonoBehaviour
{
    /// <summary>
    /// ��˸��ɫ
    /// </summary>
    public Color color = new Color(61 / 255f, 226 / 255f, 131 / 255, 1);

    /// <summary>
    /// ��ͷ������ȣ�ȡֵ��Χ[0,1]����С����߷������ȡ�
    /// </summary>
    [Tooltip("��ͷ������ȣ�ȡֵ��Χ[0,1]����С����߷������ȡ�")]
    [Range(0.0f, 1.0f)]
    public float minBrightness = 0.0f;

    /// <summary>
    /// ��߷������ȣ�ȡֵ��Χ[0,1]���������ͷ������ȡ�
    /// </summary>
    [Tooltip("��߷������ȣ�ȡֵ��Χ[0,1]���������ͷ������ȡ�")]
    [Range(0.0f, 1)]
    public float maxBrightness = 0.5f;

    /// <summary>
    /// ��˸Ƶ�ʣ�ȡֵ��Χ[0.2,30.0]��
    /// </summary>
    [Tooltip("��˸Ƶ�ʣ�ȡֵ��Χ[0.2,30.0]��")]
    [Range(0.2f, 30.0f)]
    public float rate = 1;

    //�Ƿ���˸
    [HideInInspector]
    public bool isGlinting = false;


    [Tooltip("��ѡ����������ʱ�Զ���ʼ��˸")]
    [SerializeField]
    private bool _autoStart = false;

    private float _h, _s, _v;           // ɫ�������Ͷȣ�����
    private float _deltaBrightness;     // ���������Ȳ�
    private Renderer _renderer;

    //private Material _material;
    private Material[] _materials;

    private readonly string _keyword = "_EMISSION";
    private readonly string _colorName = "_EmissionColor";

    private Coroutine _glinting;

    private void OnEnable()
    {
        _renderer = gameObject.GetComponent<Renderer>();

        //_material = _renderer.material;
        _materials = _renderer.materials;

        if (_autoStart)
        {
            StartGlinting();
        }
    }

    /// <summary>
    /// У�����ݣ�����֤����ʱ���޸��ܹ��õ�Ӧ�á�
    /// �÷���ֻ�ڱ༭��ģʽ����Ч������
    /// </summary>
    private void OnValidate()
    {
        // �������ȷ�Χ
        if (minBrightness < 0 || minBrightness > 1)
        {
            minBrightness = 0.0f;
            Debug.LogError("������ȳ���ȡֵ��Χ[0, 1]��������Ϊ0��");
        }
        if (maxBrightness < 0 || maxBrightness > 1)
        {
            maxBrightness = 1.0f;
            Debug.LogError("������ȳ���ȡֵ��Χ[0, 1]��������Ϊ1��");
        }
        if (minBrightness >= maxBrightness)
        {
            minBrightness = 0.0f;
            maxBrightness = 1.0f;
            Debug.LogError("�������[MinBrightness]��������������[MaxBrightness]���ѷֱ�����Ϊ0/1��");
        }

        // ������˸Ƶ��
        if (rate < 0.2f || rate > 30.0f)
        {
            rate = 1;
            Debug.LogError("��˸Ƶ�ʳ���ȡֵ��Χ[0.2, 30.0]��������Ϊ1.0��");
        }

        // �������Ȳ�
        _deltaBrightness = maxBrightness - minBrightness;

        // ������ɫ
        // ע�ⲻ��ʹ�� _v ������������ʱ�޸Ĳ����ᵼ������ͻ��
        float tempV = 0;
        Color.RGBToHSV(color, out _h, out _s, out tempV);
    }

    /// <summary>
    /// ��ʼ��˸��
    /// </summary>
    public void StartGlinting()
    {
        isGlinting = true;
        if (_materials != null)
        {
            if (_materials.Length > 0)
            {
                //_material.EnableKeyword(_keyword);
                for (int i = 0; i < _materials.Length; i++)
                {
                    _materials[i].EnableKeyword(_keyword);
                }

                if (_glinting != null)
                {
                    StopCoroutine(_glinting);
                }
                _glinting = StartCoroutine(IEGlinting());
            }
        }
    }

    /// <summary>
    /// ֹͣ��˸��
    /// </summary>
    public void StopGlinting()
    {
        isGlinting = false;
        //_material.DisableKeyword(_keyword);
        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i].DisableKeyword(_keyword);
        }

        if (_glinting != null)
        {
            StopCoroutine(_glinting);
        }
    }

    /// <summary>
    /// �����Է���ǿ�ȡ�
    /// </summary>
    /// <returns></returns>
    private IEnumerator IEGlinting()
    {
        Color.RGBToHSV(color, out _h, out _s, out _v);
        _v = minBrightness;
        _deltaBrightness = maxBrightness - minBrightness;

        bool increase = true;
        while (true)
        {
            if (increase)
            {
                _v += _deltaBrightness * Time.deltaTime * rate;
                increase = _v <= maxBrightness;
            }
            else
            {
                _v -= _deltaBrightness * Time.deltaTime * rate;
                increase = _v <= minBrightness;
            }
            //_material.SetColor(_colorName, Color.HSVToRGB(_h, _s, _v));

            for (int i = 0; i < _materials.Length; i++)
            {
                _materials[i].SetColor(_colorName, Color.HSVToRGB(_h, _s, _v));
            }
            //_renderer.UpdateGIMaterials();
            yield return null;
        }
    }
}
