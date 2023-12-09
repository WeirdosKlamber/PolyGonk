using UnityEngine;

namespace WeirdosKlamber.PolyGonk.Lesson
{
    public class TreeLevelScript : MonoBehaviour
    {
        public GameObject[] labels;
        public GameObject[] branches;
        public bool hasBranches = true;
        private float labelDelayTimer = 0f;
        private float labelDelayTarget = 0f;

        void Start()
        {
            foreach (var labelX in labels)
            {
                if (hasBranches)
                {
                    foreach (var branch in branches)
                    {
                        branch.SetActive(true);
                        branch.SendMessage("AnimateBranch");
                    }
                }
            }
        }

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