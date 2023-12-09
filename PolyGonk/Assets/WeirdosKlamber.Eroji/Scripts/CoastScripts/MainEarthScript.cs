using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WeirdosKlamber.PolyGonk.Coast
{
    public class MainEarthScript : MonoBehaviour
    {
        public GameObject[] CliffSections;

        public AudioSource cliff0fx;
        public AudioSource cliff1fx;
        public AudioSource cliff2fx;
        public AudioSource landingfx;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator playSoundAfterSeconds(float waitt)
        {
            yield return new WaitForSeconds(waitt);
            landingfx.PlayOneShot(landingfx.clip, 1f);
        }

        void CliffFall(float landingdelay)
        {
            if (landingdelay < 0.3) { cliff0fx.PlayOneShot(cliff0fx.clip, 0.8f); }
            else if (landingdelay < 0.8) { cliff1fx.PlayOneShot(cliff1fx.clip, 0.9f); }
            else { cliff2fx.PlayOneShot(cliff2fx.clip, 1f); }



            StartCoroutine(playSoundAfterSeconds(landingdelay));
        }

    }
}