using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace WeirdosKlamber.PolyGonk.River
{
    public class HelloScript : MonoBehaviour
    {
        public GameObject QuizMaster;
        public GameObject WelcomeText;
        public GameObject Text1;
        public GameObject Text2;
        public GameObject Text3;
        public GameObject ContinueButton;
        private float animTimer = 11f;
        private float anotherTimer = 3f;
        public bool instructions = false;
        private int stepper = 0;

        // Start is called before the first frame update
        void Start()
        {
            SingletonSimple.Instance.ClearText();
            if (!instructions)
            {
/*                WelcomeText.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("WelcomeRiver");
                Text1.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("WelcomeRiver1");
                Text2.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("WelcomeRiver2");
                Text3.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("WelcomeRiver3");
                SingletonSimple.Instance.TTSAddText("WelcomeRiver", 3f);
                SingletonSimple.Instance.TTSAddText("WelcomeRiver1", 4f);
                SingletonSimple.Instance.TTSAddText("WelcomeRiver2", 6f);
                SingletonSimple.Instance.TTSAddText("WelcomeRiver3", 6f);*/
            }
            else
            {
/*                WelcomeText.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverInstructions");
                Text1.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverInstructions1");
                Text2.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverInstructions2");
                Text3.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverInstructions3");
                SingletonSimple.Instance.TTSAddText("RiverInstructions", 2.5f);
                SingletonSimple.Instance.TTSAddText("RiverInstructions1", 8f);
                SingletonSimple.Instance.TTSAddText("RiverInstructions2", 4f);
                SingletonSimple.Instance.TTSAddText("RiverInstructions3", 5f);*/
            }

        }

        // Update is called once per frame
        void Update()
        {
            animTimer -= Time.unscaledDeltaTime;
            anotherTimer -= Time.unscaledDeltaTime;
            if (animTimer < 9f) Text1.SetActive(true);
            if (animTimer < 6f) {
                if (!instructions) Text2.SetActive(true);
            }
            if (animTimer < 2f) {
                if (!instructions) Text3.SetActive(true);
            }
            if (animTimer < 0f)
            {
                if (!instructions) 
                ContinueButton.SetActive(true);             
            }

            if (instructions && anotherTimer<0f) ContinueButton.SetActive(true); 
        }

        public void Continue()
        {

            if (instructions)
            {
                stepper++;
                anotherTimer = 3f;
                ContinueButton.SetActive(false);
                if (stepper == 1)
                {
                    Text2.SetActive(true);
                    Text1.GetComponent<TextMeshProUGUI>().enabled = false;
                }
                else if (stepper == 2)
                {
                    Text3.SetActive(true);
                    Text2.GetComponent<TextMeshProUGUI>().enabled = false;
                }
                else if (stepper == 3)
                {
                    QuizMaster.SendMessage("questionFinish", 5);
                    Destroy(gameObject);
                }
            }

            else
            {
                QuizMaster.SendMessage("questionFinish", -1);
                Destroy(gameObject);
            }
        }
    }
}