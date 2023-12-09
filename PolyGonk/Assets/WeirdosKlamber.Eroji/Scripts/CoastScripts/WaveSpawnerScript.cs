using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace WeirdosKlamber.PolyGonk.Coast
{
    public class WaveSpawnerScript : MonoBehaviour
    {
        public GameObject CoastMain;
        public GameObject wavebase;
        public GameObject smallwave;
        public GameObject bigwave;
        public GameObject tidalwave;
        public GameObject typhoonwave;
        public GameObject RedScreen;
        private float RedFlashTimer1 =0f;
        private float RedFlashTimer2 =0f;
        public float timer;
        private float timecount = 0f;
        private float smalltimecount = 0f;
        private float bigtimecount = 0f;
        private float tidalwavetimecount = 0f;
        private float typhoontimecount = 0f;
        private float WinGametimecount = 16f;
        private bool wincountbool = false;


        public AudioSource smallfx;
        public AudioSource mediumfx;
        public AudioSource tidalfx;
        public AudioSource typhoonfx;
        public AudioSource alarmfx;

        public GameObject stormsurge;
        private bool surging = false;
        private bool surgeup = true;
        private float surgelimit;
        public float surgelimit1;
        public float surgelimit2;
        private float surgeamount;
        private float surgestarth;
        public float surgespeed = 0.3f;
        private float surgedelay = 7f;

        private float furthestright = 0f;
        private float furthdelay = 0f;
        private float furthmod = 0.5f;

        public Text WavefText;
        private bool endOnce = true;

        // Start is called before the first frame update
        void Start()
        {
            surgestarth = stormsurge.transform.position[1];
            surgelimit += stormsurge.transform.position[1];
        }

        // Update is called once per frame
        void Update()
        {
            if (wincountbool == true)
            {
                WinGametimecount -= Time.deltaTime;
                if (WinGametimecount < 0f && endOnce)
                {
                    endOnce = false;
                    CoastMain.SendMessage("EndGame", 1);
                }
            }


            WavefText.text = "wf " + furthestright.ToString();

            if (surging == true)
            {
                surgeamount = Time.deltaTime * surgespeed;
                if (surgeup == true)
                {

                    stormsurge.transform.position = (new Vector2(stormsurge.transform.position[0], stormsurge.transform.position[1] + surgeamount));
                    if (stormsurge.transform.position[1] > surgelimit)
                    {
                        surgeup = false;
                        surgedelay = 7f;
                    }
                }
                else if (surgedelay > 0f)
                {
                    surgedelay -= Time.deltaTime;
                }
                else
                {
                    stormsurge.transform.position = (new Vector2(stormsurge.transform.position[0], stormsurge.transform.position[1] - surgeamount));
                    if (stormsurge.transform.position[1] < surgestarth)
                    {
                        surgeup = true;
                        surging = false;
                    }


                }

            }


            timecount += Time.deltaTime;
            smalltimecount += Time.deltaTime;
            bigtimecount += Time.deltaTime;
            tidalwavetimecount += Time.deltaTime;
            typhoontimecount += Time.deltaTime;

            if (RedFlashTimer2 > 0f && RedFlashTimer2<3f)
            {
                RedScreen.SetActive(true);
                RedFlashTimer1 += Time.deltaTime;
                RedFlashTimer2 += Time.deltaTime;
                if (RedFlashTimer2 >= 4f)
                {
                    RedScreen.SetActive(false);
                }
                else if (RedFlashTimer1 <= 0.5f)
                {
                    RedScreen.GetComponent<Image>().color = new Color(1, 0, 0, RedFlashTimer1 * 2);
                }
                else if (RedFlashTimer1 < 1f)
                {
                    RedScreen.GetComponent<Image>().color = new Color(1, 0, 0, 2f - RedFlashTimer1 * 2);
                }
                else {
                    alarmfx.Play();
                    RedFlashTimer1 = 0f; 
                }
            }

                if (typhoontimecount > timer * 180)
            {
                if (RedFlashTimer2 <= 0f) 
                {
                    alarmfx.Play();
                    RedFlashTimer2 = 0.01f;
                }
                Instantiate(typhoonwave, transform);
                timecount = -5f; //because wave takes up two spaces
                smalltimecount = 0f;
                bigtimecount = 0f;
                tidalwavetimecount = 0f;
                typhoontimecount = 0f;
                surging = true;
                surgelimit = stormsurge.transform.position[1] + surgelimit2;
                transform.parent.gameObject.BroadcastMessage("Typhoon");

                wincountbool = true;
            }


            if (tidalwavetimecount > timer * 60)
            {
                Instantiate(tidalwave, transform);
                timecount = -5f; //because wave takes up two spaces
                smalltimecount = 0f;
                bigtimecount = 0f;
                tidalwavetimecount = 0f;
                surging = true;
                surgelimit = stormsurge.transform.position[1] + surgelimit1;

            }

            if (bigtimecount > timer * 15)
            {
                Instantiate(bigwave, transform);
                timecount = 0f;
                smalltimecount = 0f;
                bigtimecount = 0f;

            }
            else if (smalltimecount > timer * 5)
            {
                Instantiate(smallwave, transform);
                timecount = 0f;
                smalltimecount = 0f;


            }
            else if (timecount > timer)
            {
                Instantiate(wavebase, transform);
                timecount = 0f;


            }
        }
        IEnumerator playSoundAfterSeconds(float waitt, int whatn)
        {
            yield return new WaitForSeconds(waitt);
            if (whatn == 1)
                smallfx.PlayOneShot(smallfx.clip, 1);
            if (whatn == 2)
                mediumfx.PlayOneShot(mediumfx.clip, 1);
            if (whatn == 3)
                tidalfx.PlayOneShot(tidalfx.clip, 1);
            if (whatn == 4)
                typhoonfx.PlayOneShot(typhoonfx.clip, 1);

        }
        void PlayWaveSFX(string wavesize)
        {
            furthdelay = 5f - (furthestright * furthmod);
            if (furthdelay < 0) furthdelay = 0;
            if (wavesize == "small") { StartCoroutine(playSoundAfterSeconds(furthdelay, 1)); }
            else if (wavesize == "medium") { StartCoroutine(playSoundAfterSeconds(furthdelay, 2)); }
            else if (wavesize == "tidal") { StartCoroutine(playSoundAfterSeconds(furthdelay, 3)); }
            else if (wavesize == "typhoon") { typhoonfx.PlayOneShot(typhoonfx.clip, 1); }
        }

        void StormCheat()
        {
            tidalwavetimecount = timer * 60;
        }

        public void TyphoonCheat()
        {
            typhoontimecount = timer * 175;
        }

        void FurthestRight(float furtx)
        {
            furthestright = furtx;

        }
    }
}