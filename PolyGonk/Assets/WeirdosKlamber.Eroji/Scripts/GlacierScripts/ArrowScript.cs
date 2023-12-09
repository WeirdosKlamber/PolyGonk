using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeirdosKlamber.PolyGonk.Glacier;

namespace WeirdosKlamber.PolyGonk.Glacier.Arrow
{
    public class ArrowScript : MonoBehaviour
    {
        private bool swingBool = true;
        private float clockLimit = -120f;
        private float antiLimit = 0f;
        public float currentAngle = 0f;
        static public bool rightwards = true;
        static public string lastDirection = "R";
        public bool newturn = true;
        private float maxspeed = 300f;
        public float currentspeed = 300f;
        private float minspeed = 30f;
        private float deceleRate = 24f;
        private bool clockwards = true;
        public GameObject redArrow;
        public GameObject greenArrow;
        public GameObject currentHex;
        static public bool arrowPressed = false;
        static public bool arrowAvailable = true;
        private float arrowtime = 1f;
        static public float currentarrowtime = 1f;
        private float accuracy = 15f;
        private float distanceMod=1.52f;
        public AudioSource TickSFX;
        public AudioSource TockSFX;
        public AudioSource BuzzSFX;
        public GameObject bangUp;
        public GameObject currentscoreobj;
        private Vector3 mousemod = new Vector3(0f, 0f, 0f);
        private float startWaiter = 0f;
        // Start is called before the first frame update
        void Start()
        {
            greenArrow.SetActive(false);
            redArrow.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
            arrowPressed = false;
            arrowAvailable = true;
            currentarrowtime = 1f;
            lastDirection = "R";
            rightwards = true;
/*            if(SingletonSimple.Instance.glacierBabyMode)
            {
                maxspeed = 150f;
                currentspeed = 150f;
            }*/
        }

        // Update is called once per frame
        void Update()
        {
            startWaiter += Time.deltaTime;
            if (startWaiter > 0.5f)
            {
                if (swingBool == true)
                {
                    if (clockwards == true)
                    {
                        transform.Rotate(0.0f, 0.0f, -currentspeed * Time.deltaTime, Space.Self);
                    }
                    else transform.Rotate(0.0f, 0.0f, currentspeed * Time.deltaTime, Space.Self);

                    currentAngle = transform.localRotation.eulerAngles[2];

                    currentAngle = OverClock(currentAngle);
                    if (currentAngle > -90f && currentAngle <= 90f) rightwards = true;
                    else rightwards = false;

                    if (currentAngle >= antiLimit && clockLimit > -179f)
                    {
                        clockwards = true;
                        if (arrowAvailable == true) TickSFX.PlayOneShot(TickSFX.clip, 1f);
                    }
                    else if (currentAngle <= clockLimit)
                    {
                        clockwards = false;
                        if (arrowAvailable == true) TockSFX.PlayOneShot(TockSFX.clip, 1f);
                    }
                    else if (clockLimit < -179f && currentAngle > 0f)
                    {
                        clockwards = false;
                        if (arrowAvailable == true) TockSFX.PlayOneShot(TockSFX.clip, 1f);
                    }
                    else if (clockLimit < -179f && currentAngle > -60f)
                    {
                        clockwards = true;
                        if (arrowAvailable == true) TickSFX.PlayOneShot(TickSFX.clip, 1f);
                    }


                    currentspeed -= deceleRate * Time.deltaTime;
                    if (currentspeed <= minspeed) currentspeed = minspeed;
                }

                if (arrowAvailable == false)
                {
                    currentarrowtime -= Time.deltaTime;
                    greenArrow.SetActive(false);
                    redArrow.SetActive(false);
                    if (currentarrowtime < arrowtime - 0.1f)
                    {
                        GetComponent<BoxCollider2D>().enabled = false;
                    };
                    if (currentarrowtime < 0f)
                    {
                        arrowAvailable = true;
                        arrowPressed = false;
                        currentarrowtime = arrowtime;
                        greenArrow.SetActive(false);
                        redArrow.SetActive(true);

                    }
                }


                if (arrowAvailable == true)
                {
                    if (clockLimit == -178f)//special
                    {
                        if (currentAngle + 120 < accuracy && currentAngle + 120 > 0f - accuracy)
                        {
                            greenArrow.SetActive(true);
                            redArrow.SetActive(false);
                        }
                        else
                        {
                            greenArrow.SetActive(false);
                            redArrow.SetActive(true);
                        }
                    }
                    else if (antiLimit == 58f)//special - also renders north and south limit unnecessary
                    {
                        if (currentAngle < accuracy && currentAngle > 0f - accuracy)
                        {
                            greenArrow.SetActive(true);
                            redArrow.SetActive(false);
                        }
                        else
                        {
                            greenArrow.SetActive(false);
                            redArrow.SetActive(true);
                        }
                    }

                    else if (transform.position[0] < -7f && currentAngle - clockLimit < accuracy) //west limit
                    {
                        greenArrow.SetActive(false);
                        redArrow.SetActive(true);
                    }

                    else if (currentAngle - clockLimit < accuracy || currentAngle - antiLimit > 0f - accuracy)
                    {
                        greenArrow.SetActive(true);
                        redArrow.SetActive(false);
                    }
                    else
                    {
                        greenArrow.SetActive(false);
                        redArrow.SetActive(true);
                    }
                }
            }
        }
        float OverClock(float degz)
        {
            if (degz >= 180f && antiLimit < 179f) degz -= 360f;
            else if (degz <= -180f && clockLimit > -179f) degz += 360f;
            return degz;
        }

        void arrowButtonPress()
        {
            if (arrowAvailable == true)
            {
                arrowAvailable = false;
                arrowPressed = true;

                print("currentangle: " + currentAngle + "  antilimit: "+antiLimit);
                if (greenArrow.activeSelf==true)
                {
  
                    GetComponent<BoxCollider2D>().enabled = true;
                    Glacier.GlacierMainScript.comboCount ++;

                    mousemod[0] = Camera.main.ScreenToWorldPoint(Input.mousePosition)[0];
                    mousemod[1] = Camera.main.ScreenToWorldPoint(Input.mousePosition)[1];
                    currentscoreobj = Instantiate(bangUp, mousemod, currentHex.transform.rotation);
                    print(Input.mousePosition);
                    currentscoreobj.SendMessage("setNumb", Glacier.GlacierMainScript.comboCount);
                    /*
                    if (currentAngle < 0f + accuracy && currentAngle > 0f - accuracy) currentHex.SendMessage("collided", "B");
                    else if (currentAngle < 180f + accuracy && currentAngle > 180f - accuracy) currentHex.SendMessage("collided", "B");
                    else if (currentAngle < -180f + accuracy && currentAngle > -180f - accuracy) currentHex.SendMessage("collided", "B");

                    else if (currentAngle < -120f + accuracy && currentAngle > -120f - accuracy) currentHex.SendMessage("collided", "BR");
                    else if (currentAngle < 60f + accuracy && currentAngle > 60f - accuracy) currentHex.SendMessage("collided", "BR");

                    else if (currentAngle < -60f + accuracy && currentAngle > -60f - accuracy) currentHex.SendMessage("collided", "BL");
                 */
                }
                else
                {
                    Glacier.GlacierMainScript.comboCount = 0;
                    print("miss");
                    BuzzSFX.PlayOneShot(BuzzSFX.clip, 1f);
                    currentspeed = maxspeed;
                    GetComponent<BoxCollider2D>().enabled = false;
                }
                greenArrow.SetActive(false);
                redArrow.SetActive(false);
            }
        }

        void NewTurn(Vector3 info)     //0 is for switching but use if instead in case float not exact enough
        {
            arrowPressed = true;
            arrowAvailable = false;
            greenArrow.SetActive(false);
            redArrow.SetActive(false);

            if (info[0] < 1.1f)
            {
                //arrive at BL corner pointing DR
                clockLimit = -120f;
                antiLimit = 0f;
                transform.position = new Vector2(info[1] - 0.3f * distanceMod, info[2] - 0.54f * distanceMod);
                lastDirection = "DR";
            }
    /*        else if (info[0] < 2.1f) //arrive at L corner
            {
                clockLimit = -180f;
                antiLimit = -60f;
                transform.position = new Vector2(info[1] - 0.58f * distanceMod, info[2] - 0.03f * distanceMod);
            }
    */
            else if (info[0] < 2.1f)
            {
            //arrive at BR corner pointing R
                clockLimit = -60f;
                antiLimit = 60f;
                transform.position = new Vector2(info[1] + 0.29f*distanceMod, info[2] - 0.54f*distanceMod);
                lastDirection = "R";
            }
            else if (info[0] < 3.1f)
                //arrive at Rpoint pointing UR
            {
                clockLimit = -60f;
                antiLimit = 58f;  //other special, upleft not allowed
                transform.position = new Vector2(info[1] + 0.58f * distanceMod, info[2] - 0.03f * distanceMod);
                lastDirection = "UR";
            }

            else if (info[0] < 4.1f)  //arrive at BR corner pointing DL
            {
                clockLimit = -180f;
                antiLimit = -60f;
                transform.position = new Vector2(info[1] + 0.29f * distanceMod, info[2] - 0.54f * distanceMod);
                lastDirection = "DL";
            }

            else 
            {
                //arrive at BL corner pointing L
                clockLimit = -178f;  //special case no upleft allowed
                antiLimit = -60f;
                transform.position = new Vector2(info[1] - 0.3f * distanceMod, info[2] - 0.54f * distanceMod);
                lastDirection = "L";
            }

            
            currentspeed = maxspeed;
            RandAim();
        }

        void RandAim()
        {
            currentAngle = Random.Range(clockLimit, antiLimit);
            Vector3 eulerRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, currentAngle);
        }

    }
}