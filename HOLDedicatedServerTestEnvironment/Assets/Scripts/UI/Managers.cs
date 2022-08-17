using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is just a holder for all managers
 */
public class Managers : MonoBehaviour
{
    public static Managers instance = null;

    [SerializeField] private UIManager m_UIManager;
    [SerializeField] private ButtonManager m_ButtonManager;

    public static UIManager UIManager => instance.m_UIManager;
    public static ButtonManager ButtonManager => instance.m_ButtonManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
}
