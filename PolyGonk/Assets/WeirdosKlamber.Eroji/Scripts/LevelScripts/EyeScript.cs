using System;
using UnityEngine;

namespace WeirdosKlamber.PolyGonk.Shapes
{
    public class EyeScript : MonoBehaviour
    {
        public GameObject eyeball;
        private Animator m_Animator;
        private bool winking = false;
        private float winkTimeMaster = 1f;
        private float winkTimer = 1f;
        private float testTimeMaster = 2f;
        private float testTimer = 0.5f;

        void Start()
        {
            m_Animator = gameObject.GetComponent<Animator>();
            m_Animator.SetBool("Wink", false);
        }

        void Update()
        {
            if (winking)
            {
                winkTimer -= Time.deltaTime;
                if (winkTimer < 0f)
                {
                    winking = false;
                    winkTimer = winkTimeMaster;
                }
            }
            else
            {
                testTimer -= Time.deltaTime;
                if (testTimer < 0f)
                {
                    wink(true);
                    testTimer = testTimeMaster;
                }
            }
        }

        void wink(bool winky)
        {
            winking = winky;
            m_Animator.SetBool("Wink", winky);
            eyeball.SetActive(!winky);
        }
    }
}