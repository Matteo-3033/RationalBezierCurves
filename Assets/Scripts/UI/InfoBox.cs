using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class InfoBox : MonoBehaviour
{
    [SerializeField] private Info[] keys;
    [SerializeField] private GameObject[] panels;

    private Dictionary<Info, GameObject> _infos = new();
    private float _lastTime = -DELAY * 2;
    private const float DELAY = 0.5F;
    private Info _current = Info.Idle;
    
    private enum Info
    {
        DeCasteljau,
        AddPoint,
        RemovePoint,
        Normalize,
        MoveT,
        MovePoint,
        MoveWeight,
        Idle
    }

    private void Awake()
    {
        Assert.AreEqual(keys.Length, panels.Length);
        for (var i = 0; i < keys.Length; i++)
        {
            var k = keys[i];
            if (_infos.ContainsKey(k))
                Debug.LogError($"Duplicate key {k} in InfoBox");
            else _infos.Add(k, panels[i]);
            panels[i].SetActive(false);
        }
        SetVisible(Info.Idle);
    }

    private void Start()
    {
        PointManager.Instance.OnPointAdded += OnPointAdded;
        PointManager.Instance.OnLastPointRemoved += OnLastPointRemoved;
        PointManager.Instance.OnWeightsNormalized += OnWeightsNormalized;
        TSelector.Slider.onValueChanged.AddListener(OnSelectedTChanged);
        Settings.OnShowDeCasteljauChanged += OnShowDeCasteljauChanged;
    }

    private void OnWeightsNormalized(object sender, EventArgs e)
    {
        SetVisible(Info.Normalize);
    }

    private void OnShowDeCasteljauChanged(object sender, Settings.OnShowDeCasteljauArgs e)
    {
        if (e.Show)
            SetVisible(Info.DeCasteljau);
    }

    private void OnSelectedTChanged(float t)
    {
        SetVisible(Info.MoveT);
    }

    private void OnPointAdded(object sender, PointManager.OnPointArgs e)
    {
        SetVisible(Info.AddPoint);
    }
    
    private void OnLastPointRemoved(object sender, EventArgs e)
    {
        SetVisible(Info.RemovePoint);
    }

    private void SetVisible(Info info)
    {
        if (Time.time - _lastTime < DELAY && (int)_current < (int) info)
            return;
        HideAll();
        
        if (!_infos.ContainsKey(info))
            return;
        
        _infos[info].SetActive(true);
        _lastTime = Time.time;
        _current = info;
    }

    private void HideAll()
    {
        foreach (var p in _infos.Values)
            p.SetActive(false);
    }
}
