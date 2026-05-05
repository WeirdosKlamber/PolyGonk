using UnityEngine;
namespace WeirdosKlamber.PolyGonk
{
    public class PolyGonkTitle : MonoBehaviour
    {
        public GameObject face;
        public AudioSource menuMusic;
        public AudioSource voicefx;
        private float startDelay = 0.95f;
        private bool started = false;
        private bool finished = false;
        private float timerX = 0f;
        private float slideTime = 1.5f;
        private float currentX = 0f;
        private Vector3 startPos = new Vector3(1605f, 11f,0f);
        private Vector3 endPos = new Vector3(405f, 11f,0f);
        private bool doneVoice = false;
        private bool playingMusic = false;
        private bool doingTotalVictory = false;

        void Start()
        {
            transform.localPosition = startPos;
        }

        // Update is called once per frame
        void Update()
        {
            timerX += Time.unscaledDeltaTime;
            if (!started && timerX > startDelay)
            {
                started = true;
                timerX = 0f;
            }
            if (started && !doneVoice) 
            {
                voicefx.Play();
                doneVoice= true;
            }
            if (finished && !playingMusic && !doingTotalVictory)
            {
                menuMusic.Play();
                playingMusic= true;
            }
            if (started && !finished)
            {
                if (timerX < slideTime)
                {
                    currentX = (startPos.x + 2f*endPos.x) - (BounceEaseOut(timerX, endPos.x, startPos.x, slideTime)); ;
                }

                transform.localPosition = new Vector3(currentX, transform.localPosition.y,0f);

                if (timerX > slideTime*1.1f)
                {
                    finished = true;
                    face.SendMessage("WakeUp");
                }
            }
        }

        public static float BounceEaseOut(float t, float b, float c, float d)
        {
            float modifierX = 7.5625f;

            if ((t /= d) < (1f / 2.75f))
                return c * (modifierX * t * t) + b;
            else if (t < (2f / 2.75f))
                return c * (modifierX * (t -= (1.5f / 2.75f)) * t + .75f) + b;
            else if (t < (2.5f / 2.75f))
                return c * (modifierX * (t -= (2.25f / 2.75f)) * t + .9375f) + b;
            else
                return c * (modifierX * (t -= (2.625f / 2.75f)) * t + .984375f) + b;
        }

        public void TotalVictory()
        {
            doingTotalVictory = true;
        }
    }
}