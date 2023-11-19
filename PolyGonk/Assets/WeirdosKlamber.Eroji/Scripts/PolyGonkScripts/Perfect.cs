using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System.Dynamic;
using System;


namespace WeirdosKlamber.PolyGonk
{
    public class Perfect : MonoBehaviour
    {
        public TextMeshProUGUI textM;
        private float timer = 2f;
        private float dilatF = 0f;
        // Start is called before the first frame update
        void Start()
        {
            textM = gameObject.GetComponent<TextMeshProUGUI>();
            textM.fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, new Color(1f, 1f, 0f));
            textM.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("Perfect");
           // textM.fontSize = 5f;

        }

        // Update is called once per frame
        void Update()
        {
            timer -= Time.deltaTime;
            print("dilat" + dilatF);
            if (timer > 0f)
            {
                textM.fontMaterial.EnableKeyword("GLOW_ON");
                dilatF += Time.deltaTime * 0.3f;
                textM.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, dilatF);
       /*         textM.fontMaterial.SetFloat(ShaderUtilities.ID_GlowOuter, dilatF);
                textM.fontMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 2f * dilatF);*/
            }
        }
    }
}