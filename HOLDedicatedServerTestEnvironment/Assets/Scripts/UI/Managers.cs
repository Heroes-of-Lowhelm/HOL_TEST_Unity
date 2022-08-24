using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is just a holder for all managers
 */
public class Managers : MonoBehaviour
{
    public static Managers instance = null;

    [SerializeField] private bool m_DontDestroyOnLoad;
    [SerializeField] private UIManager m_UIManager;
    [SerializeField] private ButtonManager m_ButtonManager;
    [SerializeField] private Loading_Manager m_LoadingManager;

    public static UIManager UIManager => instance.m_UIManager;
    public static ButtonManager ButtonManager => instance.m_ButtonManager;
    public static Loading_Manager LoadingManager => instance.m_LoadingManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (m_DontDestroyOnLoad == true)
            DontDestroyOnLoad(gameObject);

    }
}
