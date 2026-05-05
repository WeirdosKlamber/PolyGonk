using UnityEngine;

namespace WeirdosKlamber.PolyGonk.Lesson
{
    public class VennScript : MonoBehaviour
    {
        public GameObject level1;
        public float level1Duration = 0f;

        public GameObject level2;
        public bool hasLevel2 = true;
        public float level2Duration = 0f;

        public GameObject level3;
        public bool hasLevel3 = true;
        public float level3Duration = 0f;

        public GameObject level4;
        public bool hasLevel4 = true;
        public float level4Duration = 0f;

        private float animTimer = 0f;
        private int stepper = 0;
        private float quickReaderMod = 0.7f;
        private float buttonTimerMaster = 0.5f;
        private float buttonTimer = 0f;

        void Start()
        {
            if (level1Duration <= 0.1f) { level1Duration = 5f; }
            if (level2Duration <= 0.1f) { level2Duration = 5f; }
            if (level3Duration <= 0.1f) { level3Duration = 5f; }
            if (level4Duration <= 0.1f) { level4Duration = 5f; }
        }

        void Update()
        {
            animTimer += Time.unscaledDeltaTime;
            buttonTimer += Time.unscaledDeltaTime;
            if (stepper == 0 && animTimer > 0f)
            {
                level1.SetActive(true);
                stepper++;
                animTimer = 0f;
            }
            if (stepper == 1 && animTimer > level1Duration * quickReaderMod)
            {
                level1.SendMessage("SkipNow");
                if (hasLevel2)
                {
                    level2.SetActive(true);
                }
                stepper++;
                animTimer = 0f;
            }
            if (stepper == 2 && animTimer > level2Duration * quickReaderMod)
            {
                if (hasLevel2) { level2.SendMessage("SkipNow"); }
                if (hasLevel3)
                {
                    level3.SetActive(true);
                }
                stepper++;
                animTimer = 0f;
            }
            if (stepper == 3 && animTimer > level3Duration * quickReaderMod)
            {
                if (hasLevel3) { level3.SendMessage("SkipNow"); }
                if (hasLevel4)
                {
                    level4.SetActive(true);
                }
                stepper++;
                animTimer = 0f;
            }

            if (buttonTimer > buttonTimerMaster)
            {
                if (hasLevel4) { level4.SendMessage("SkipNow"); }
                if (Input.GetMouseButton(0) || Input.anyKeyDown)
                {
                    buttonTimer = 0f;
                    animTimer = 20f;
                }
            }
        }

        void SlowDown()
        {
            quickReaderMod= 1f;
        }
    }
}