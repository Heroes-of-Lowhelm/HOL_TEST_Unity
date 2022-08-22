using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectScaler : MonoBehaviour
{
    [SerializeField] private Ease m_ScaleEase;
    [SerializeField] private Vector3 m_TargetScale = Vector3.one;
    [SerializeField] private float m_ScaleSpeed;

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(m_TargetScale, m_ScaleSpeed);
    }
}
