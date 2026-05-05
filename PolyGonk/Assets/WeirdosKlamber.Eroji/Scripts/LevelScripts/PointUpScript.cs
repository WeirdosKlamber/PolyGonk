
using UnityEngine;
using TMPro;
using System;

namespace WeirdosKlamber.PolyGonk.UI
{
    public class PointUpScript : MonoBehaviour
    {
        public int scoreUp = 1;
        public GameObject shape;
        private float timer = 1.5f;
        private float dilatF = -0.21f;
        public Vector2 startPosition;
        private Vector2 pausePosition;
        private Vector2 midCurvePosition;
        private Vector3 rotatCorrection;
        private TextMeshPro textM;

        void Start()
        {            
            rotatCorrection = new Vector3(0f,0f,360f - shape.transform.rotation.z);
            transform.rotation = Quaternion.Euler( rotatCorrection);
            transform.SetParent(shape.transform.parent.parent,true);
            startPosition = transform.position;
            textM = gameObject.GetComponent<TextMeshPro>();
            textM.fontSize = 5f;
            if (scoreUp== 1)
            {
                textM.fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, new Color(1f, 1f, 0f));
            }
            textM.fontMaterial.SetColor(ShaderUtilities.ID_FaceColor, new Color(1f, 1f, 1f));
        }

        void Update()
        {
            timer-= Time.deltaTime;
            rotatCorrection = new Vector3(0f, 0f, 360f - shape.transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotatCorrection);

            if (timer > 1f)
            {
                textM.fontSize = 5 + Convert.ToInt32((1.5f - timer) * 60f*scoreUp);
                if (scoreUp == 1)
                {
                    transform.position += new Vector3(0f, Time.deltaTime*3f , 0f);
                }
                else
                {
                    transform.position += new Vector3(Time.deltaTime*3f, Time.deltaTime *2.5f, 0f);
                }
                pausePosition= transform.position;
            }

            if (timer > 0f)
            {
                textM.fontMaterial.EnableKeyword("GLOW_ON");
                transform.position = pausePosition;
                dilatF += Time.deltaTime * 0.4f;
                textM.fontMaterial.SetFloat(ShaderUtilities.ID_GlowOuter, dilatF);
                textM.fontMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 0.2f + 2f * dilatF);
                if (scoreUp == 2)
                {
                    textM.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, dilatF * 0.6f);
                }
            }

            else
            {
                gameObject.SetActive(false);
            }   
        }
    }
}