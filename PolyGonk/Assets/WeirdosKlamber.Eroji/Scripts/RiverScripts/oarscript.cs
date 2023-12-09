using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WeirdosKlamber.PolyGonk.River
{
    public class oarscript : MonoBehaviour
    {

        public AudioSource[] Strokes;
        public AudioSource[] slowStrokes;
        private int Stroker=0;
        private bool skipStroke = true;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void strokePlay()
        {
            if (skipStroke) skipStroke = false;
            else
            {
                if (GetComponent<Animator>().speed > 1f) Strokes[Stroker].Play();
                else slowStrokes[Stroker].Play();
                Stroker++;
                if (Stroker >= 5) Stroker = 0;
            }

        }

    }
}
