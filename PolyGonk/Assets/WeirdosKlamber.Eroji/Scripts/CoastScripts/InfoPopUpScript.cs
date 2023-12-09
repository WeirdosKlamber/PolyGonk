using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace WeirdosKlamber.PolyGonk.Coast
{
    public class InfoPopUpScript : MonoBehaviour
    {
        public TextMeshProUGUI InfoText;
        public TextMeshProUGUI DefenceText;
        public TextMeshProUGUI DefendScoreText;
        public float ContinueButtonDelay;
        public GameObject ContinueButton;
        public GameObject CoastMain;

        public GameObject SandButton;
        public GameObject RipRapButton;
        public GameObject GabionButton;
        public GameObject GroyneButton;
        public GameObject SeaWallButton;


        private bool oldsand = false;
        private bool oldriprap = false;
        private bool oldgabion = false;
        private bool oldgroyne = false;
        private bool oldseawall = false;

        private bool buttononce = false;
        public string WhichInfo;


        // Start is called before the first frame update
        void Start()
        {
            oldsand = SandButton.activeSelf;
            oldriprap = RipRapButton.activeSelf;
            oldgabion = GabionButton.activeSelf;
            oldgroyne = GroyneButton.activeSelf;
            oldseawall = SeaWallButton.activeSelf;

            SandButton.SetActive(false);
            RipRapButton.SetActive(false);
            GabionButton.SetActive(false);
            GroyneButton.SetActive(false);
            SeaWallButton.SetActive(false);

            SingletonSimple.Instance.ClearText();
            // InfoText.text = "Change into language thing";
            DefenceText.text = WeirdosKlamber.PolyGonk.PolyGonkScript.GetText("CoastDefence");

            switch(WhichInfo)
            {
                case ("Sand"):
                    InfoText.text = WeirdosKlamber.PolyGonk.PolyGonkScript.GetText("CoastSand");
                    SingletonSimple.Instance.TTSAddText("CoastSand", 15f);
                    break;
                case ("RipRap"):
                    InfoText.text = WeirdosKlamber.PolyGonk.PolyGonkScript.GetText("CoastRipRap");
                    SingletonSimple.Instance.TTSAddText("CoastRipRap", 15f);
                    break;
                case ("Gabion"):
                    InfoText.text = WeirdosKlamber.PolyGonk.PolyGonkScript.GetText("CoastGabion");
                    SingletonSimple.Instance.TTSAddText("CoastGabion", 15f);
                    break;
                case ("Groyne"):
                    InfoText.text = WeirdosKlamber.PolyGonk.PolyGonkScript.GetText("CoastGroyne");
                    SingletonSimple.Instance.TTSAddText("CoastGroyne", 15f);
                    break;
                case ("SeaWall"):
                    InfoText.text = WeirdosKlamber.PolyGonk.PolyGonkScript.GetText("CoastSeaWall");
                    SingletonSimple.Instance.TTSAddText("CoastSeaWall", 15f);
                    break;

            }


            ContinueButton.SetActive(false);
            Time.timeScale = 0;

        }

        // Update is called once per frame
        void Update()
        {
            ContinueButtonDelay -= Time.unscaledDeltaTime;  //game paused so delta won't work'
            if (ContinueButtonDelay < 0 && buttononce == false)
            {
                ContinueButton.SetActive(true);
                buttononce = true;

            }



        }

        void ContinuePressed()
        {
            SandButton.SetActive(oldsand);
            RipRapButton.SetActive(oldriprap);
            GabionButton.SetActive(oldgabion);
            GroyneButton.SetActive(oldgroyne);
            SeaWallButton.SetActive(oldseawall);

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