using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeirdosKlamber.PolyGonk.Glacier
{
    public class GlacRiverScript : MonoBehaviour
    {
        public GameObject[] riverAnimArray;
        private bool AnimateBool = false;
        private bool FullBool = false;
        private float AnimSpeed = 0.1f;
        private float AnimTimer = 0f;
        // Start is called before the first frame update
        void Start()
        {
            foreach (GameObject x in riverAnimArray)
            {
                x.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (AnimateBool == true)
            {
                AnimTimer -= Time.deltaTime;
                if (AnimTimer < 0f)
                {
                    AnimTimer = AnimSpeed;

                    if (FullBool == true)
                    {
                        riverAnimArray[5].SetActive(false);
                        riverAnimArray[6].SetActive(false);
                        riverAnimArray[7].SetActive(false);
                        riverAnimArray[8].SetActive(false);
                        FullBool = false;
                    }

                    foreach (GameObject x in riverAnimArray)
                    {
                        if (x.activeSelf == false)
                        {
                            x.SetActive(true);
                            if (x == riverAnimArray[8]) FullBool = true;
                            break;
                        }
                    }

                }
            }
        }

        void RiverGo(float delay)
        {
            AnimateBool = true;
            AnimTimer = delay;
        }
    }
}