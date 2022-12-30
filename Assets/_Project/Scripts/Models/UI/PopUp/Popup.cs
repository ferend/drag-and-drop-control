using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Models.UI
{
    

    public abstract class Popup : MonoBehaviour
    {
        public CanvasGroup canvasGroup;

        public virtual void OpenPopup()
        {
            
            // UIManager.Instance.ShowPopupUnderlay(true);
            GetComponent<RectTransform>().DOScale(Vector3.one, 0.5F).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                }
            );
            

        }

        public virtual void ClosePopup()
        {
            GetComponent<RectTransform>().DOScale(Vector3.zero, 0.5F).SetEase(Ease.InBack).OnComplete(() =>
                {
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false; 
                    // UIManager.Instance.ShowPopupUnderlay(false);
                }
            );
        }
    }
}
