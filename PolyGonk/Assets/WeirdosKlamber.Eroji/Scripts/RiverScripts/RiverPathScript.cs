using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace WeirdosKlamber.PolyGonk.River.RiverPath
{
    public class RiverPathScript : MonoBehaviour
    {

        public float[] riverPathX = Enumerable.Repeat(0.0f, 70).ToArray();
        public float[] riverPathY = Enumerable.Repeat(0.0f, 70).ToArray();
        public GameObject pathFollower;
        public GameObject RiverMaster;
        public GameObject Riverleftcollider;
        private float speed = 0.2f;
        static public float currentspeed = 0.2f;
        public int currentTN = 32;
        private Vector2 currentTarget = new Vector2(-4.269f, 0.6f);
        private Vector2 currentPosition;
        private float stupid = 8.1f;
        private bool endFollow = false;
        private float turnspeed = 125f;
        private float currentturnspeed = 125f;
        private float turnmod = 1f;
        public Camera cam;
        private bool doOnce = false;
        public GameObject Boat;
        public GameObject Oar;
        private bool turbo = false;
        private float turboaccelerator = 0.1f;
        private float maxspeed = 1f;
        private bool caveBoo = false;
        private bool caveAllowBoo = false;
        private bool oxBoo = false;
        private bool oxbowAllowBoo = false;
        public GameObject oxbowRightCollider;
        private bool floodBoo = false;
        private bool floodAllowBoo = false;
        public GameObject caveButton;
        public GameObject caveCutColliderleft;
        public GameObject caveCutColliderright;
        public GameObject caveAnimObj;
        public GameObject caveentrance;
        public GameObject caveexit;
        public GameObject caveHUDMask;
        public GameObject caveGlare;
        public GameObject spiders;
        public GameObject[] spidersArray;
        private Image caveHUDGlare;
        private float transTime = 3f;
        public bool glareBool;
        Color alphaZero = new Color(1f, 1f, 1f, 0f);
        Color alphahigh = new Color(1f, 1f, 1f, 0.9f);
        float glareTimePassed = 0.0f; 

        public GameObject caveRiverMud;
        public GameObject caveBlackMap;
        public GameObject oxbowButton;
        public GameObject oxbowCutCollider;
        // public GameObject oxbowAnimObj;
        public GameObject oxbow1;
        public GameObject oxbow2;
        public GameObject oxbow3;
        public GameObject oxbow4;
        public GameObject oxbow5;
        public GameObject floodButton;
        public GameObject floodleftmask;
        public GameObject floodrightmask;
        public GameObject floodisle;
        public GameObject floodnobow;
        private float floodtimer = 0f;
        public GameObject[] floodAnim;
        public GameObject leftCollider;
        public GameObject rightCollider;
        public GameObject islandCollider;
        private List<GameObject> Melons=new List<GameObject>(16);
        public GameObject[] Crocodiles;
        public GameObject[] LogsArr;
        public GameObject[] MonkeysArr;

        public AudioSource MountainRiver;
        public AudioSource CaveRiver;
        public AudioSource JungleRiver;
        public AudioSource GrassRiver;

       
        public AudioSource CaveSFX;
        public AudioSource OxbowSFX;
        public AudioSource FloodSFX;

        public AudioSource CardSFX;
        public AudioSource dripSFX;


        private bool crossfadeJungle = false;
        private bool crossfadeGrass = false;
        private bool fadeoutcave = false;
        private bool fadeall = false;

        private float timeTaken = 0f;

        private bool fadeIn = true;

        Animator oxbAnim;
        // Start is called before the first frame update
        void Start()
        {
            //oxbAnim = oxbowAnimObj.GetComponent<Animator>();
            caveHUDGlare = caveGlare.GetComponent<Image>();
            print("start");
            MountainRiver.Play();
            MountainRiver.volume = 0f;
            JungleRiver.volume = 0f;
            GrassRiver.volume = 0f;
            currentspeed = 0.2f;
        }

        // Update is called once per frame
        void Update()
        {
            if (fadeIn)
            {
                MountainRiver.volume += Time.deltaTime / 2f;
                
                if (MountainRiver.volume > 0.9f)
                {
                    MountainRiver.volume=0.9f;
                    fadeIn = false;
                }
            }

            if (fadeall)
            {
                MountainRiver.volume -= Time.deltaTime / 2f;
                if (MountainRiver.volume < 0.05f)
                {
                    MountainRiver.Stop();
                }
                JungleRiver.volume -= Time.deltaTime / 2f;
                if (JungleRiver.volume < 0.05f)
                {
                    JungleRiver.Stop();
                }
                GrassRiver.volume -= Time.deltaTime / 2f;
                if (GrassRiver.volume < 0.05f)
                {
                    GrassRiver.Stop();
                }
                CaveRiver.volume -= Time.deltaTime / 2f;
                if (CaveRiver.volume < 0.05f)
                {
                    CaveRiver.Stop();
                }
            }

            if (endFollow == false)
            { 

                if (crossfadeJungle)
                {
                    MountainRiver.volume -= Time.deltaTime / 5f;
                    JungleRiver.volume += Time.deltaTime / 5f;
                    if (MountainRiver.volume<0.05f)
                    {
                        MountainRiver.Stop();
                        JungleRiver.volume = 1f;
                        crossfadeJungle = false;
                    }
                }
                if (crossfadeGrass)
                {
                    JungleRiver.volume -= Time.deltaTime / 5f;
                    GrassRiver.volume += Time.deltaTime / 5f;
                    if (JungleRiver.volume < 0.05f)
                    {
                        JungleRiver.Stop();
                        GrassRiver.volume = 1f;
                        crossfadeGrass = false;
                    }
                }
                if(fadeoutcave)
                {
                
                    CaveRiver.volume -= Time.deltaTime / 2f;
                    if (CaveRiver.volume < 0.05f)
                    {
                        CaveRiver.Stop();
                    }
                }
                
                if (floodtimer>0f)
                {
                    floodtimer -= Time.deltaTime;
                    if (floodtimer < 0.7f && floodAnim[0].activeSelf == false) floodAnim[0].SetActive(true);
                    else if (floodtimer < 0.4f && floodAnim[1].activeSelf == false) floodAnim[1].SetActive(true);
                    else if (floodtimer < 0.1f && floodAnim[2].activeSelf == false) floodAnim[2].SetActive(true);
                }
                if (turbo == true)
                {
                    currentspeed += Time.deltaTime * turboaccelerator;
                    if (currentspeed > maxspeed) currentspeed = maxspeed;
                }
                else
                {
                    if (currentspeed > speed) currentspeed -= Time.deltaTime * turboaccelerator;
                }

                timeTaken += Time.deltaTime;
                

                
                Oar.GetComponent<Animator>().speed = 0.2f + currentspeed * 0.75f;

                if (doOnce == false)
                {
                    currentPosition = pathFollower.transform.localPosition;
                    currentTarget[0] = riverPathX[currentTN] + stupid;
                    currentTarget[1] = riverPathY[currentTN];
                    doOnce = true;
                }




                pathFollower.transform.localPosition = Vector2.MoveTowards(pathFollower.transform.localPosition, currentTarget, currentspeed * Time.deltaTime);

                // pathFollower.transform.right = currentTarget - Vector2.MoveTowards(pathFollower.transform.localPosition, currentTarget, step);

                float angle = Mathf.Atan2(currentTarget[1] - pathFollower.transform.localPosition.y, currentTarget[0] - pathFollower.transform.localPosition.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                pathFollower.transform.rotation = Quaternion.RotateTowards(pathFollower.transform.rotation, targetRotation, currentturnspeed * currentspeed * Time.deltaTime);

                if (pathFollower.transform.localPosition[0] == currentTarget[0] && pathFollower.transform.localPosition[1] == currentTarget[1]) //if hit a navpoint
                {
                    if (currentTN == 27)
                    {
                        caveButton.GetComponent<Image>().color = Color.yellow;
                        caveAllowBoo = true;
                    }
                    if (currentTN == 26)
                    {
                        caveButton.GetComponent<Image>().color = Color.white;
                        if (caveAllowBoo == true)
                        {
                            caveAllowBoo = false;
                            caveButton.SetActive(false);

                        }
                        else
                        {
                            currentTN = 55;
                            caveCutColliderleft.SetActive(true);
                            caveCutColliderright.SetActive(true);
                            Riverleftcollider.SetActive(false);
                            caveentrance.SetActive(true);
                            caveRiverMud.SetActive(true);
                            
                        }
                    }
                    if (currentTN == 54)
                    {
                        CaveRiver.Play();
                        dripSFX.Play();
                        caveexit.SetActive(true);
                        caveBlackMap.SetActive(true);
                        caveHUDMask.SetActive(true);
                        caveentrance.SetActive(false);
                        spiders.SetActive(true);
                        //caveRiverMud.SetActive(false);
                    }
                    if (currentTN == 53)
                    {
                        spidersArray[0].SetActive(false);
                    }
                    if (currentTN == 52)
                    {
                        spidersArray[1].SetActive(false);
                    }
                    if (currentTN == 51)
                    {
                        spidersArray[2].SetActive(false);
                    }

                    if (currentTN == 50)
                    {
                        caveGlare.SetActive(true);
                        glareBool = true;
                    }


                    if (currentTN == 49)
                    {
                        currentTN = 20;
                        CaveRiver.Stop();
                        dripSFX.Stop();
                        caveexit.SetActive(false);
                        Riverleftcollider.SetActive(true);
                        caveBlackMap.SetActive(false);
                        caveHUDMask.SetActive(false);
                    }

                    if (currentTN <= 20 ) 
                    {
                        foreach (GameObject logg in LogsArr)
                        {
                            if (logg.activeSelf == true)
                                logg.SendMessage("LogNav", currentTN);
                        }
                        foreach (GameObject monk in MonkeysArr)
                        {
                            if (monk.activeSelf == true)
                                monk.SendMessage("MonkNav", currentTN);
                        }
                    }

                    if (currentTN == 19)
                    {
                        crossfadeJungle=true;
                        JungleRiver.Play();
                        caveCutColliderleft.SetActive(false);
                        caveCutColliderright.SetActive(false);
                        maxspeed *= 0.8f;
                        speed *= 0.8f;
                    }

         
                    if (currentTN == 15)
                    {
                        caveGlare.SetActive(false);
                        oxbowButton.GetComponent<Image>().color = Color.yellow;
                        oxbowAllowBoo = true;
                        
                        foreach (GameObject croc in Crocodiles)
                        {
                        croc.SetActive(true);
                        }                        
                    }

                    if (currentTN == 56)
                    {
                        oxbow5.SetActive(true);
                        currentTN = 7;
                    }
                    if (currentTN == 57)
                    {
                        oxbow4.SetActive(true);
                        currentTN = 56;
                    }
                    if (currentTN == 58)
                    {
                        oxbow3.SetActive(true);
                        currentTN = 57;
                    }
                    if (currentTN == 59)
                    {
                        oxbow2.SetActive(true);
                        currentTN = 58;
                    }

                    if (currentTN == 14)
                    {
                        maxspeed *= 0.8f;
                        speed *= 0.8f;
                        
                        oxbowButton.GetComponent<Image>().color = Color.white;
                        if (oxbowAllowBoo == true)
                        {
                            oxbowAllowBoo = false;
                            oxbowButton.SetActive(false);

                        }
                        else
                        {
                            oxbow1.SetActive(true);
                            currentTN = 59;
                            crossfadeGrass = true;
                            GrassRiver.Play();
                            OxbowSFX.PlayOneShot(OxbowSFX.clip);
                            //currentTN = 7;

                            floodButton.GetComponent<Image>().color = Color.yellow;
                            floodButton.SetActive(true);
                            floodAllowBoo = true;
                        }
                    }
                    if (currentTN == 12)
                    {
                        crossfadeGrass = true;
                        GrassRiver.Play();
                        floodButton.GetComponent<Image>().color = Color.yellow;
                        floodButton.SetActive(true);
                        floodAllowBoo = true;
                    }
                    if (currentTN <= 12 && floodAllowBoo==false) //change back to 12 later
                    {
                        foreach(GameObject croc in Crocodiles)
                        {
                            if (croc.activeSelf==true)
                            croc.SendMessage("CrocNav", currentTN);
                        }
                    }
                    if (currentTN == 7)
                    {
                        maxspeed *= 0.8f;
                        speed *= 0.8f;
                    }
                    if (currentTN < 0)                   
                    {
                        print("endingsoon");

                        print("currentTN is: " + currentTN);
                        if (currentTN < -1)
                        {
                            endFollow = true;
                            fadeall = true;
                            Boat.SendMessage("Kill");
                            RiverMaster.SendMessage("addTime", timeTaken);
                            RiverMaster.SendMessage("NavCount", currentTN);
                        }
                        else currentTN--;
                    }

                    else
                    {
                        currentTarget[0] = riverPathX[currentTN] + stupid;
                        currentTarget[1] = riverPathY[currentTN];
                        currentTN--;

                        switch (currentTN)
                        {
                            case 6:
                            case 20:
                                turnmod = 1.3f;
                                break;
                            case -1:
                            case 0:
                            case 2:
                            case 5:
                            case 8:
                            case 9:
                            case 11:
                            case 13:
                            case 17:
                            case 22:
                            case 23:
                            case 24:
                                turnmod = 0.75f;
                                break;
                            case 3:
                            case 15:
                            case 21:
                            case 28:
                            case 29:
                                turnmod = 0.5f;
                                break;
                            case 16:
                            case 30:
                            case 31:
                                turnmod = 0.3f;
                                break;
                            default:
                                turnmod = 1f;
                                break;
                        }
                        currentturnspeed = turnspeed * turnmod;
                        RiverMaster.SendMessage("NavCount", currentTN);
                    }

                }
            }
            else //if endfollow==true
            {
                Oar.GetComponent<Animator>().enabled = false;

            }
        }

        void FixedUpdate()
        {
            if (glareBool && glareTimePassed < transTime)
            {
                glareTimePassed += Time.fixedDeltaTime * currentspeed * 5;
                caveHUDGlare.color = Color.Lerp(alphaZero, alphahigh, glareTimePassed / transTime);
                if (glareTimePassed >= transTime)
                {
                    glareBool = false;
                    transTime *= 2;


                }
            }
            else if (glareTimePassed > 0f)
            {
                glareTimePassed -= Time.fixedDeltaTime;
                caveHUDGlare.color = Color.Lerp(alphaZero, alphahigh, glareTimePassed / transTime);
            }
        }

        void AddNav(Vector3 tran)
        {
            // print((int)tran[0]);

            riverPathX[(int)tran[0]] = tran[1];

            riverPathY[(int)tran[0]] = tran[2];

        }

        void turboboost(bool turbob)
        {
            turbo = turbob;

        }


        void CaveCardPress()
        {
            if (caveAllowBoo == true)
            {
                caveBoo = true;
                caveAllowBoo = false;
                caveButton.SetActive(false);
                caveentrance.SetActive(true);
                caveRiverMud.SetActive(true);
                CardSFX.PlayOneShot(CardSFX.clip);
                CaveSFX.Play();
            }
            else
            {
                caveButton.GetComponent<Image>().color = Color.red;
            }

        }


        void CaveCardRelease()
        {
            caveButton.GetComponent<Image>().color = Color.white;
        }
        void OxbowCardPress()
        {
            if (oxbowAllowBoo == true)
            {
                oxBoo = true;
                oxbowAllowBoo = false;
                oxbowButton.SetActive(false);
                oxbowRightCollider.SetActive(true);
                Destroy(oxbowCutCollider);
                CardSFX.PlayOneShot(CardSFX.clip);
                // oxbowAnimObj.SetActive(true);
                // oxbAnim.speed = (currentspeed*1.5f ) + 0f; //seems to only show up as paddle over it
                // oxbAnim.SetBool("DoOxbow", true);

            }
            else
            {
                oxbowButton.GetComponent<Image>().color = Color.red;
            }
        }

        void OxbowCardRelease()
        {
            oxbowButton.GetComponent<Image>().color = Color.white;
        }

        void FloodCardPress()
        {
            if (floodAllowBoo == true)
            {
                floodBoo = true;
                floodAllowBoo = false;
                floodButton.SetActive(false);
                floodtimer = 1f;
                floodisle.SetActive(true);
                islandCollider.SetActive(false);
                floodrightmask.SetActive(true);
                floodleftmask.SetActive(true);
                leftCollider.SetActive(false);
                rightCollider.SetActive(false);
                CardSFX.PlayOneShot(CardSFX.clip);
                if (oxBoo==false)
                {
                    oxbowCutCollider.SetActive(false);
                    floodnobow.SetActive(true);
                }
                foreach (GameObject M in Melons)
                {
                    M.GetComponent<Animator>().enabled = true;
                }
                foreach (GameObject croc in Crocodiles)
                {
                    if (croc.activeSelf==true)
                        croc.SendMessage("CrocNav", currentTN);
                }
                FloodSFX.PlayOneShot(FloodSFX.clip);
            }
            else
            {
                floodButton.GetComponent<Image>().color = Color.red;
            }

        }


        void FloodCardRelease()
        {
            floodButton.GetComponent<Image>().color = Color.white;
            if (floodBoo == true) Destroy(floodButton);
        }

        public void AddMelon(GameObject melon)
        {
            
            //print(melon.name);
            Melons.Add(melon);
        }

        public void Kill()
        {
            endFollow = true;
            fadeall = true;
            Boat.SendMessage("Kill");

        }







    }
}