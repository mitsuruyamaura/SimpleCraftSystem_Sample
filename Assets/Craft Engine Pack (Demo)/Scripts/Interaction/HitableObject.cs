using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftEngine {

    public class HitableObject : MonoBehaviour {
        protected AudioSource m_audioSource;
        void Start() {
            m_audioSource = GetComponent<AudioSource>();
        }
        public virtual void HandleHit(ToolType toolType) {

        }
    }
}