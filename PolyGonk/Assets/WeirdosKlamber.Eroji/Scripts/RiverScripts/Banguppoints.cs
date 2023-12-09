using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace WeirdosKlamber.PolyGonk.River
{
    public class Banguppoints : MonoBehaviour
    {
        private TextMeshProUGUI scoreX;
        private float dilatF = -0.21f;
        private float killTimer = 2f;
        // Start is called before the first frame update
        void Start()
        {
            scoreX = GetComponent<TextMeshProUGUI>();

        }

        // Update is called once per frame
        void Update()
        {
            transform.localPosition -= new Vector3(0f, 0f, Time.deltaTime / 10f);
            dilatF += Time.deltaTime / 3;
            scoreX.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, dilatF);
            killTimer -= Time.deltaTime;
            if (killTimer < 0f) Destroy(this.gameObject);
        }

        void BangUpSet(int scoreInt)
        {
            scoreX = GetComponent<TextMeshProUGUI>();
            scoreX.text = scoreInt.ToString();/*
            switch (scoreInt)  //still not working - stuff it
             {
                 case 10:
                     GetComponent<TextMeshPro>().color = Color.red; 
                     break;
                 case 20:
                     GetComponent<TextMeshPro>().color = Color.yellow; 
                     break;
                 case 50:
                    GetComponent<TextMeshPro>().color = Color.magenta;
                    break;
             }*/
        }
    }
}