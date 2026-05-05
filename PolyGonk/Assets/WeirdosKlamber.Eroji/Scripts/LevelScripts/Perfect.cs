
using UnityEngine;
using TMPro;

namespace WeirdosKlamber.PolyGonk
{
    public class Perfect : MonoBehaviour
    {
        public TextMeshProUGUI textM;
        private float timer = 2f;
        private float dilatF = 0f;

        void Start()
        {
            textM = gameObject.GetComponent<TextMeshProUGUI>();
            textM.fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, new Color(1f, 1f, 0f));
            textM.text = WeirdosKlamber.PolyGonk.PolyGonkScript.GetText("Perfect");
        }

        void Update()
        {
            timer -= Time.deltaTime;
            if (timer > 0f)
            {
                textM.fontMaterial.EnableKeyword("GLOW_ON");
                dilatF += Time.deltaTime * 0.3f;
                textM.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, dilatF);
            }
        }
    }
}