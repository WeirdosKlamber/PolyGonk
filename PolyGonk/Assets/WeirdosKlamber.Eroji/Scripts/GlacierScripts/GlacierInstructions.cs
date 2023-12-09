using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LoLSDK;
namespace WeirdosKlamber.PolyGonk.Glacier
{
    public class GlacierInstructions : MonoBehaviour
    {
        public GameObject Instructions1;
        public GameObject Instructions2;
        public GameObject Instructions3;
        public GameObject Instructions4;
        public TextMeshProUGUI Instructions1Text;
        public TextMeshProUGUI Instructions2Text;
        public TextMeshProUGUI Instructions3Text;
        public TextMeshProUGUI Instructions4Text;

        public GameObject nextbutton;

        public AudioSource click;
        private bool showNext = false;
        private float timer = 1f;

        // Start is called before the first frame update
        void Start()
        {
/*            Instructions1Text.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("GlacierInstructions1");
            Instructions2Text.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("GlacierInstructions2");
            Instructions3Text.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("GlacierInstructions3");
            Instructions4Text.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("GlacierInstructions4");*/
            SingletonSimple.Instance.ClearText();
/*            SingletonSimple.Instance.TTSAddText("GlacierInstructions1", 3f);
            SingletonSimple.Instance.TTSAddText("GlacierInstructions2", 3f);
            SingletonSimple.Instance.TTSAddText("GlacierInstructions3", 3f);
            SingletonSimple.Instance.TTSAddText("GlacierInstructions4", 3f);*/
        }

        // Update is called once per frame
        void Update()
        {
            timer -= Time.unscaledDeltaTime;
            if (timer < 0f) showNext = true;

            if (showNext)
            {
                timer = 2f;
                showNext = false;
                if (Instructions1.activeSelf == false) Instructions1.SetActive(true);
                else if (Instructions2.activeSelf == false) Instructions2.SetActive(true);
                else if (Instructions3.activeSelf == false) Instructions3.SetActive(true);
                else if (Instructions4.activeSelf == false) Instructions4.SetActive(true);
                else if (nextbutton.activeSelf == false) nextbutton.SetActive(true);
            }
        }

        public void ClickScreen()
        {
            click.Play();
            showNext = true;

            if (nextbutton.activeSelf == true)
            {
                Time.timeScale = 1f;
                Destroy(gameObject);
            }
        }

        public void TTSButtonPressed()
        {
            SingletonSimple.Instance.Speak();
        }
    }
}
