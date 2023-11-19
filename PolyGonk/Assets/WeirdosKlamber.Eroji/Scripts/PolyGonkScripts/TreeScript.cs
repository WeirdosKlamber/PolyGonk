using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WeirdosKlamber.PolyGonk;

namespace WeirdosKlamber.PolyGonk.Lesson
{
    public class TreeScript : MonoBehaviour
    {

        public GameObject Level1;
        public float Level1Duration = 0f;

        public GameObject Level2;
        public bool hasLevel2 = true;
        public float Level2Duration = 0f;

        public GameObject Level3;
        public bool hasLevel3 = true;
        public float Level3Duration = 0f;

        public GameObject Level4;
        public bool hasLevel4 = true;
        public float Level4Duration = 0f;

        private float animTimer = 0f;
        private int stepper = 0;
        private float quickReaderMod = 0.7f;
        private float buttonTimerMaster = 0.5f;
        private float buttonTimer = 0f;


        // Start is called before the first frame update
        void Start()
        {
            if (Level1Duration <= 0.1f) { Level1Duration = 5f; }
            if (Level2Duration <= 0.1f) { Level2Duration = 5f; }
            if (Level3Duration <= 0.1f) { Level3Duration = 5f; }
            if (Level4Duration <= 0.1f) { Level4Duration = 5f; }
        }

        // Update is called once per frame
        void Update()
        {
            animTimer += Time.unscaledDeltaTime;
            buttonTimer += Time.unscaledDeltaTime;
            if (stepper == 0 && animTimer > 0f)
            {
                Level1.SetActive(true);
                Level1.SendMessage("SetLabelDelay", 0.01f);
                stepper++;
                animTimer = 0f;
            }
            if (stepper == 1 && animTimer > Level1Duration * quickReaderMod)
            {
                Level1.SendMessage("SkipNow");
                if (hasLevel2)
                {
                    Level2.SetActive(true);
                    Level2.SendMessage("SetLabelDelay", 0.4f);
                }
                stepper++;
                animTimer = 0f;
            }
            if (stepper == 2 && animTimer > Level2Duration * quickReaderMod)
            {
                if (hasLevel2) { Level2.SendMessage("SkipNow"); }
                    if (hasLevel3)
                {
                    Level3.SetActive(true);
                    Level3.SendMessage("SetLabelDelay", 0.4f);
                }
                stepper++;
                animTimer = 0f;
            }
            if (stepper == 3 && animTimer > Level3Duration * quickReaderMod)
            {
                if (hasLevel3) { Level3.SendMessage("SkipNow"); }
                if (hasLevel4)
                {
                    Level4.SetActive(true);
                    Level4.SendMessage("SetLabelDelay", 0.4f);
                }
                stepper++;
                animTimer = 0f;
            }

            if (buttonTimer > buttonTimerMaster)
            {
                if (hasLevel4) { Level4.SendMessage("SkipNow"); }
                if (Input.GetMouseButton(0) || Input.anyKeyDown)
                {
                    buttonTimer = 0f;
                    animTimer = 20f;
                }

            }
        }
        void SlowDown()
        {
            quickReaderMod = 1f;
        }
    }
}