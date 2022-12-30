using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public abstract class Mechanic : MonoBehaviour
    {
        protected bool CanPlay
        {
            get => GameFlowManager.Instance.canPlay;
            set => GameFlowManager.Instance.canPlay = value;
        }

        public virtual IEnumerator OnStart()
        {
            yield break;
        }

        public virtual IEnumerator OnEnd()
        {
            yield break;
        }

        public virtual IEnumerator OnFail()
        {
            yield break;
        }

        public virtual void OnDown()
        {
        }

        public virtual void OnDrag()
        {
        }

        public virtual void OnUp()
        {
        }

        public virtual void OnHold()
        {
        }

    }
