using System;
using System.Collections;
using System.Collections.Generic;
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
        // Start is called before the first frame update
        void Start()
        {
            m_Animator = gameObject.GetComponent<Animator>();
            m_Animator.SetBool("Wink", false);
        }

        // Update is called once per frame
        void Update()
        {
            if (winking)
            {
                winkTimer -= Time.deltaTime;
                if (winkTimer < 0f)
                {
                    winking = false;

                    //m_Animator.SetBool("Wink", false);
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

        void wink(Boolean winky)
        {
            winking = winky;
            m_Animator.SetBool("Wink", winky);
            eyeball.SetActive(!winky);
        }
    }
}