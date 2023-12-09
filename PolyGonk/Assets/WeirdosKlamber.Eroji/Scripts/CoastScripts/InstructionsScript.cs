using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace WeirdosKlamber.PolyGonk.Coast
{
    public class InstructionsScript : MonoBehaviour
    {
        public TextMeshProUGUI InfoText1;
        public TextMeshProUGUI InfoText2;

        public GameObject Instructions1;
        public GameObject Instructions2;
        public float Instructions1Delay;
        public float Instructions2Delay;

        public float ContinueButtonDelay;
        public GameObject ContinueButton;


        public GameObject CoastMain;

        public GameObject SandButton;
        public GameObject RipRapButton;
        public GameObject GabionButton;
        public GameObject GroyneButton;
        public GameObject SeaWallButton;



        private bool buttononce = false;
        public string WhichInfo;


        // Start is called before the first frame update
        void Start()
        {
            InfoText1.text = WeirdosKlamber.PolyGonk.PolyGonkScript.GetText("CoastBuyDefences");
            InfoText2.text = WeirdosKlamber.PolyGonk.PolyGonkScript.GetText("CoastProtectHouse");
            SingletonSimple.Instance.ClearText();
            SingletonSimple.Instance.TTSAddText("CoastBuyDefences", 2f);
            SingletonSimple.Instance.TTSAddText("CoastProtectHouse", 2f);
            SandButton.SetActive(true);
            RipRapButton.SetActive(true);
            GabionButton.SetActive(true);
            GroyneButton.SetActive(true);
            SeaWallButton.SetActive(true);
            SandButton.GetComponent<Button>().enabled = false;
            RipRapButton.GetComponent<Button>().enabled = false;
            GabionButton.GetComponent<Button>().enabled = false;
            GroyneButton.GetComponent<Button>().enabled = false;
            SeaWallButton.GetComponent<Button>().enabled = false;


            ContinueButton.SetActive(false);
            Time.timeScale = 0;

        }

        // Update is called once per frame
        void Update()
        {
            Instructions1Delay -= Time.unscaledDeltaTime;  //game paused so delta won't work'
            if (Instructions1Delay < 0)
            {
                Instructions1.SetActive(true);

            }
            Instructions2Delay -= Time.unscaledDeltaTime;  //game paused so delta won't work'
            if (Instructions2Delay < 0)
            {
                Instructions2.SetActive(true);

            }



            ContinueButtonDelay -= Time.unscaledDeltaTime;  //game paused so delta won't work'
            if (ContinueButtonDelay < 0 && buttononce == false)
            {
                ContinueButton.SetActive(true);
                buttononce = true;

            }



        }

        void ContinuePressed()
        {
            SandButton.SetActive(true);
            RipRapButton.SetActive(false);
            GabionButton.SetActive(false);
            GroyneButton.SetActive(false);
            SeaWallButton.SetActive(false);
            SandButton.GetComponent<Button>().enabled = true;
            RipRapButton.GetComponent<Button>().enabled = true;
            GabionButton.GetComponent<Button>().enabled = true;
            GroyneButton.GetComponent<Button>().enabled = true;
            SeaWallButton.GetComponent<Button>().enabled = true;

            CoastMain.SendMessage("InfoScreenComplete", WhichInfo);
            Time.timeScale = 1;
            gameObject.SetActive(false);


        }

        void TTSButtonPressed()
        {
            SingletonSimple.Instance.Speak();
        }
    }
}