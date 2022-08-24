using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingCode : MonoBehaviour
{
    [SerializeField] private Slider m_LoadingSlider;

    // Start is called before the first frame update
    void Start()
    {
        DOVirtual.DelayedCall(2f, () =>
        {
            DOTween.To(x => m_LoadingSlider.value = x, 0, m_LoadingSlider.maxValue, 2f).OnComplete(() =>
            {
                SceneManager.LoadScene(Managers.LoadingManager.SceneToLoad);
            });
        });
    }
}
