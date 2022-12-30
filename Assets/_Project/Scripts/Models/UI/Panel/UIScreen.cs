using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Screen = UnityEngine.Device.Screen;

namespace _Project.Scripts.Models.UI
{
 
        public abstract class UIPanel : MonoBehaviour
        {
            public CanvasGroup canvasGroup;
            [SerializeField] protected float openDuration = 0.5F;
            [SerializeField] protected float closeDuration = 0.5F;

            private void Awake()
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
    
            public virtual void OpenScreen()
            {
                PlayOpenAnim();
                gameObject.SetActive(true);
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }

            public virtual void CloseScreen()
            {
                PlayCloseAnim();
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                // gameObject.SetActive(false);
            }

            protected abstract void PlayOpenAnim();
            protected abstract void PlayCloseAnim();

        }
    }
