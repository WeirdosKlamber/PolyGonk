using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

namespace WeirdosKlamber.PolyGonk.Lesson
{
    public class TreeLevelScript : MonoBehaviour
    {
        public GameObject[] labels;
        public GameObject[] branches;
        public bool hasBranches = true;
        private float labelDelayTimer = 0f;
        private float labelDelayTarget = 0f;

        // Start is called before the first frame update
        void Start()
        {
            foreach (var labell in labels)
            {
                if (hasBranches)
                {
                    foreach (var branch in branches)
                    {
                        branch.SetActive(true);
                        branch.SendMessage("AnimateBranch");
                    }
                }
                //TODO add language script
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (labelDelayTarget > -0.1f)
            {
                labelDelayTimer += Time.unscaledDeltaTime;
                if (labelDelayTimer > labelDelayTarget)
                {
                    foreach (var labell in labels)
                    {
                        labell.SetActive(true);
                    }
                    labelDelayTarget = -0.5f;
                }
            }

        }

        void SetLabelDelay(float delayy)
        {
            labelDelayTarget = delayy;
        }

        void SkipNow()
        {
            labelDelayTimer = 100f;
        }
    }
}