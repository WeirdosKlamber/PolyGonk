using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeirdosKlamber.PolyGonk.Glacier;
namespace WeirdosKlamber.PolyGonk.Glacier.Hex
{
    public class GrassHexScript : MonoBehaviour
    {
        public bool hasLake=false;
        private bool hasRiver = false;
        public GameObject Main;
        public GameObject[] lakeArray;
        public GameObject[] riverArray;
        private float animrate = 0.3f;
        private float animtimer = 0f;
        private float riverRate = 0.6f;
        private float riverTimer = 0f;
        private int currentRiver = 0;
        public AudioSource lakeFX;
        public AudioSource trickleFX;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (hasLake==true)
            {
                animtimer -= Time.deltaTime;
                if (animtimer < 0f)
                {
                    animtimer = animrate;
                    if (lakeArray[0].activeSelf == false) lakeFX.PlayOneShot(lakeFX.clip, 1f);
                    if (lakeArray[4].activeSelf == true) hasRiver = true;

                    if (lakeArray[7].activeSelf == true)
                    {
                        
                        lakeArray[7].SetActive(false);
                        lakeArray[6].SetActive(false);
                    }
                    else
                    {
                        foreach(GameObject x in lakeArray)
                        {
                            if (x.activeSelf == false)
                            {
                                x.SetActive(true);
                                break;
                            }
                        }
                    }
                }
            }

            if (hasRiver == true && currentRiver<riverArray.Length)
            {
                riverTimer -= Time.deltaTime;
                
                if (riverTimer < 0f)
                {
                    riverTimer = riverRate;
                    riverArray[currentRiver].SendMessage("RiverGo",-0.1f);
                    if (currentRiver == 0) trickleFX.Play();
                    currentRiver++;
                    
                    
                }

            }

        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (hasLake == false && collision.gameObject.tag == "Banana")
            {
                print("EndLake");
                Main.SendMessage("LakeTime");
                //lakeFX.PlayOneShot(lakeFX.clip, 1f);
                if (Glacier.Arrow.ArrowScript.lastDirection == "UR")
                {
                    foreach (GameObject x in lakeArray)
                    {
                        x.transform.Rotate(0.0f, 0.0f, 60f);
                    }
                    hasLake = true;
                }
                else if (Glacier.Arrow.ArrowScript.lastDirection == "DR")
                {
                    foreach (GameObject x in lakeArray)
                    {
                        x.transform.Rotate(0.0f, 0.0f, -60f);
                    }
                }
                else if (Glacier.Arrow.ArrowScript.lastDirection == "DL")
                {
                    foreach (GameObject x in lakeArray)
                    {
                        x.transform.Rotate(0.0f, 0.0f, -120f);
                    }
                }
                hasLake = true;
            }
        }


    }
}