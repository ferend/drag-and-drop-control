using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [Header("Button Config")]
    public bool animated = true;
    //public bool sound = true;
    public bool haptics = true;

    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // if(animated)
            // rect.DOShakeScale(1f, 0.1f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // if (animated)
            // rect.DOShakeScale(0.9f, 0.1f);

        //if (sound && SoundManager.Instance)
        //    SoundManager.Instance.Play(Sounds.UIButton);

        //if(haptics && SoundManager.Instance)
        //    HapticPatterns.PlayPreset(HapticPatterns.PresetType.MediumImpact);
    }
}
