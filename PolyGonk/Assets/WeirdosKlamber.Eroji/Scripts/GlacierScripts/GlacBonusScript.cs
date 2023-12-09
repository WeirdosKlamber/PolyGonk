using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace WeirdosKlamber.PolyGonk.Glacier
{
    public class GlacBonusScript : MonoBehaviour
    {
        private float timer = 0f;
        private float dilatF = -0.21f;
        private int combonumb = 1;
        // Start is called before the first frame update
        void Start()
        {
            transform.localScale = (new Vector3(0.2f, 0.2f, 0.2f));

        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            transform.Translate(Vector3.up * Time.deltaTime);
            transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
            dilatF += Time.deltaTime / 2;
            GetComponent<TextMeshPro>().fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, dilatF);
            if (timer > 1f) Destroy(gameObject);

        }

        void setNumb(int n)
        {
            combonumb = n;
            GetComponent<TextMeshPro>().text = combonumb.ToString();
            timer = 0.7f - combonumb / 10;
            if (n > 1) {
                switch (n % 5) {
                    case 0:
                    GetComponent<TextMeshPro>().color = Color.cyan;
                        break;
                    case 1:
                        GetComponent<TextMeshPro>().color = Color.yellow;
                        break;
                    case 2:
                        GetComponent<TextMeshPro>().color = Color.magenta;
                        break;
                    case 3:
                        GetComponent<TextMeshPro>().color = Color.green;
                        break;
                    case 4:
                        GetComponent<TextMeshPro>().color = Color.red;
                        break;
 
                } } }
    }
}