using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WeirdosKlamber.PolyGonk.Coast
{
    public class CoastScript : MonoBehaviour
    {
        public GameObject Crosshair;
        public GameObject SandButton;
        public GameObject RipRapButton;
        public GameObject GabionButton;
        public GameObject GroyneButton;
        public GameObject SeaWallButton;
        public TextMeshProUGUI SandButtonTxt;
        public TextMeshProUGUI RipRapButtonTxt;
        public TextMeshProUGUI GabionButtonTxt;
        public TextMeshProUGUI GroyneButtonTxt;
        public TextMeshProUGUI SeaWallButtonTxt;
        public Text MoneyText;
        public GameObject bonus;
        public GameObject waveSpawner;

        public GameObject SandInfo;
        public GameObject RipRapInfo;
        public GameObject GabionInfo;
        public GameObject GroyneInfo;
        public GameObject SeaWallInfo;
        private bool shownsandinfo = false;
        private bool shownriprapinfo = false;
        private bool showngabioninfo = false;
        private bool showngroyneinfo = false;
        private bool shownseawallinfo = false;
        public GameObject WinScreen;

        public GameObject GroyneMaster;
        public GameObject Scaredface;
        public GameObject Scaredface2;
        private bool ScaredBool = false;
        static public bool Evacuated = false;
        private float ScaredTimer = 4f;

        // public float buttonDuration;
        //private float buttonTimer = 0.0f;
        public float MoneyMultiplier;
        public int MoneyInt = 0;
        private float MoneyFloat = 0f;
        private bool butt1 = false;
        private bool butt2 = false;
        private bool butt3 = false;
        private bool butt4 = false;
        public float moneyinflator = 1f;

        private int earthleft = 12;
        private int score = 0;

        private int[] stuffforendscreen = new int[3];



        public AudioSource buttonpressfx;
        public AudioSource buttonreleasefx;
        public AudioSource seagullHitSFX;
        public AudioSource seaBGDLoop;

        private float doOnce = 0f;

        // Start is called before the first frame update
        void Start()
        {
/*            SandButtonTxt.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("CoastSandButton");
            RipRapButtonTxt.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("CoastRipRapButton");
            GabionButtonTxt.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("CoastGabionButton");
            GroyneButtonTxt.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("CoastGroyneButton");
            SeaWallButtonTxt.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("CoastSeaWallButton");*/
            Evacuated = false;
/*            if (SingletonSimple.Instance.coastBabyMode)
            {
                MoneyMultiplier = 3f;
                moneyinflator = 4f;
            }
            else
            {
                MoneyMultiplier = 2f;
                moneyinflator = 2f;
            }*/
        }

        // Update is called once per frame
        void Update()
        {
            if (doOnce >= 0f) doOnce +=Time.deltaTime;
            if (doOnce >0.5f)
            {
               doOnce = -1f;
               seaBGDLoop.Play();
            }

            if (ScaredBool == true)
            {
                ScaredTimer -= Time.deltaTime;



                Scaredface2.transform.Rotate(Vector3.forward * 90 * Time.deltaTime);
                Scaredface.transform.Translate(Vector3.left * 0.7f * Time.deltaTime);

                if (ScaredTimer < 0)
                {
                    Scaredface.SetActive(false);
                    ScaredBool = false;                    
                }
            }


            MoneyFloat += Time.deltaTime * MoneyMultiplier;
            MoneyInt = (int)MoneyFloat;
            if (MoneyInt < 100)
            {
                MoneyText.text = "$ " + MoneyInt.ToString();
            }
            else if (MoneyInt < 1000)
            {
                MoneyText.text = "$" + MoneyInt.ToString();
            }
            else { MoneyText.text = MoneyInt.ToString(); }

            if (butt1 == false)
            {
                if (MoneyInt >= 10)
                {
                    butt1 = true;
                    RipRapButton.SetActive(true);

                }

            }
            if (butt2 == false)
            {
                if (MoneyInt >= 100)
                {
                    butt2 = true;
                    GabionButton.SetActive(true);

                }

            }
            if (butt3 == false) //is always false
            {
                if (MoneyInt >= 200)
                {
                    //butt3 = true;

                    GroyneMaster.SendMessage("GroyneTest");

                }

            }
            if (butt4 == false)
            {
                if (MoneyInt >= 1000)
                {
                    butt4 = true;
                    SeaWallButton.SetActive(true);
                    SeaWallInfo.SetActive(true);
                    shownseawallinfo = true;

                }

            }

        }

        void SandButtonPress()
        {
            if (MoneyFloat > 1.0f)
            {
                Crosshair.SendMessage("SandButtonPress", true);
                buttonpressfx.PlayOneShot(buttonpressfx.clip, 1f);
            }
        }

        void SandButtonRelease()
        {
            Crosshair.SendMessage("SandButtonPress", false);
            buttonreleasefx.PlayOneShot(buttonreleasefx.clip, 1f);

            if (shownsandinfo == false)
            {
                shownsandinfo = true;
                SandInfo.SetActive(true);


            }

        }

        void RipRapButtonPress()
        {
            if (MoneyFloat > 10.0f)
            {
                Crosshair.SendMessage("RipRapButtonPress", true);
                buttonpressfx.PlayOneShot(buttonpressfx.clip, 1f);
            }
        }

        void RipRapButtonRelease()
        {
            Crosshair.SendMessage("RipRapButtonPress", false);
            buttonreleasefx.PlayOneShot(buttonreleasefx.clip, 1f);

            if (shownriprapinfo == false)
            {
                shownriprapinfo = true;
                RipRapInfo.SetActive(true);


            }
        }


        void GabionButtonPress()
        {
            if (MoneyFloat > 100.0f)
            {
                Crosshair.SendMessage("GabionButtonPress", true);
                buttonpressfx.PlayOneShot(buttonpressfx.clip, 1f);
            }
        }

        void GabionButtonRelease()
        {
            Crosshair.SendMessage("GabionButtonPress", false);
            buttonreleasefx.PlayOneShot(buttonreleasefx.clip, 1f);

            if (showngabioninfo == false)
            {
                showngabioninfo = true;
                GabionInfo.SetActive(true);

            }
        }

        void GroyneButtonPress()
        {
            buttonpressfx.PlayOneShot(buttonpressfx.clip, 1f);

        }

        void GroyneButtonRelease()
        {
            if (MoneyFloat > 200.0f)
            {
                MoneyFloat -= 200f;
                GroyneMaster.SendMessage("BuyGroyne");
                buttonreleasefx.PlayOneShot(buttonreleasefx.clip, 1f);

                if (showngroyneinfo == false)
                {
                    showngroyneinfo = true;
                    GroyneInfo.SetActive(true);

                }
            }

        }



        void SeaWallButtonPress()
        {
            if (MoneyFloat > 1000.0f)
            {
                Crosshair.SendMessage("SeaWallButtonPress", true);
                buttonpressfx.PlayOneShot(buttonpressfx.clip, 1f);
            }
        }

        void SeaWallButtonRelease()
        {
            Crosshair.SendMessage("SeaWallButtonPress", false);
            buttonreleasefx.PlayOneShot(buttonreleasefx.clip, 1f);
            shownseawallinfo = true;
        }



        void BuyFunc(string product)
        {
            if (product == "Sand")
            {
                MoneyFloat--;
                if (MoneyFloat < 1.0f)
                {
                    Crosshair.SendMessage("SandButtonPress", false);
                }
            }
            if (product == "RipRap")
            {
                MoneyFloat -= 10f;
                if (MoneyFloat < 10.0f)
                {
                    Crosshair.SendMessage("RipRapButtonPress", false);
                }
            }
            if (product == "Gabion")
            {
                MoneyFloat -= 100f;
                if (MoneyFloat < 100.0f)
                {
                    Crosshair.SendMessage("GabionButtonPress", false);
                }
            }
            if (product == "SeaWall")
            {
                MoneyFloat -= 1000f;
                if (MoneyFloat < 1000.0f)
                {
                    Crosshair.SendMessage("SeaWallButtonPress", false);                    
                }
                waveSpawner.SendMessage("TyphoonCheat");
            }

        }

        void SectionFall()
        {
            MoneyMultiplier += moneyinflator;
            earthleft--;
        }

        void InfoScreenComplete(string WhichInfo)
        {
            buttonreleasefx.PlayOneShot(buttonreleasefx.clip, 1f);

        }

        void ScaredRoll()
        {
            if (Evacuated == false)
            {
                ScaredBool = true;
                Scaredface.SetActive(true);
                Evacuated = true;
            }
        }

        void EndGame(int victory)
        {
            WinScreen.SetActive(true);

            stuffforendscreen[0] = victory;
            stuffforendscreen[1] = earthleft;
            stuffforendscreen[2] = MoneyInt;
            WinScreen.SendMessage("WinLose", stuffforendscreen);
            int vict = 1000;
/*            if (SingletonSimple.Instance.coastBabyMode)
            {
                vict = 10;
            }
            if (victory == 1)
            {
                score = vict + earthleft * (vict/10) + (MoneyInt*(vict/1000));
                SingletonSimple.Instance.coastTotalScore = Mathf.Max(score, SingletonSimple.Instance.coastTotalScore);

            }*/
        }

        void hitSeagull(Transform position100)
        {
            MoneyFloat+=100f;
            seagullHitSFX.PlayOneShot(seagullHitSFX.clip);
            Instantiate(bonus, position100);
        }


    }
}
