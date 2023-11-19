using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.XR;
using WeirdosKlamber.PolyGonk;

namespace WeirdosKlamber.PolyGonk.Shapes
{
    public class ShapeScript : MonoBehaviour
    {
        public float speed = 1f;
        public float WarmUp = 1f;
        private float strtTime;
        public float durrration = 1f;
        private float xxx;
        private float yyy;
        private float zzz;
        private Vector2 WarmUpPoint1 = new Vector2(0, 0);
        private Vector2 WarmUpPoint2 = new Vector2(0, 0);
        public Vector2 startPoint1 = new Vector2(0, 0);
        public Vector2 startpoint2 = new Vector2(1, 0);
        public Vector2 startpoint3 = new Vector2(1, 1);
        private Vector2 currentPoint1;
        private Vector2 currentPoint2;
        private Vector2 currentPoint3;
        public GameObject eyes;
        public GameObject limbs;
        public GameObject shapeObj;
        public GameObject trampoline;
        public GameObject shapeSpawner;
        public GameObject oneUp;
        public GameObject twoUp;
        private PolyGonk.Collector[] collectors;
        public bool[] collectorOkays;
        private Vector2 tramppos;
        private int bounce = 0;
        private float[] bounceXs = { -2.3f, 0.7f, 3.7f, 6.7f, 9.7f, 12.7f };
        private float[] leftwardsbounceXs = { 3.7f, 0.7f, -2.3f, -5.3f };
        public bool wideShape = false;

        public bool leftwards = false;

        public bool isShape = true;
        public bool isPolygon = false;
        public bool isQuadrilateral = false;
        public bool isTriangle = false;
        public bool isRegular = false;
        public bool isEquilateral = false;
        public bool isEquiangular = false;
        public bool isTrapezoid = false;
        public bool isParallelogram = false;
        public bool isRhombus = false;
        public bool isRectangle = false;
        public bool isSquare = false;
        public bool isScalene = false;
        public bool isIsosceles = false;
        public bool isAcute = false;
        public bool isObtuse = false;
        public bool isRightAngle = false;
        public bool[] shapeCatArray = new bool[17];
        public Sprite[] shapeMaskArray = new Sprite[8];
        public Sprite[] splashMaskArray = new Sprite[11];
        public GameObject dripper;
        public Sprite[] dripArray = new Sprite[6];
        private int currentDrip = 0;
        private float dripTimer = 0.05f;

        private float maskAnimDelay = 0.1f;
        private float maskTimer = 0f;
        public bool doMask = false;
        private int currentMask = -1;

        private bool splashing = false;
        private bool splashed = false;
        private bool finishedSwim = false;
        private float splashTimer = 1.5f;
        private float rotateTimer = 0.5f;
        private float topOfPool = -1.2f;
        private float bottomOfPool = -5f;
        private float swimSpeed = 0.5f;
        private float swimleftSpeed = 0f;
        private int score = -1;

        public int perfectCollector;
        private bool doOnce = false;
        private bool springOnce = false;
        private bool pencilShape = false;

        private bool frontSpinner = false;
        private bool backSpinner = false;
        private bool doubleFrontSpinner = false;

        private bool finalSwimDown = false;
        private bool finalFlyUp = false;
        private float flyRotat = 360f;
        private float finalTimer = 5f;
        public bool wasAboveTramp = true;

        private bool walkOut = false;


        // Start is called before the first frame update
        void Start()
        {
            strtTime = Time.time;

            currentPoint1 = startPoint1;
            currentPoint2 = startpoint2;
            currentPoint3 = startpoint3;
            WarmUpPoint1 = transform.position;
            WarmUpPoint2 = new Vector2(WarmUpPoint1.x + 1f, WarmUpPoint1.y + 1f);

            gameObject.GetComponent<SortingGroup>().sortingOrder = 0;

            popBoolArr();
            var rando = UnityEngine.Random.Range(0f, 1f);
            if (rando > 0.98f) { doubleFrontSpinner = true; frontSpinner = true; }
            else if (rando > 0.9f) { frontSpinner = true; }
            else if (rando < 0.03f) { backSpinner = true; }

            if (leftwards)
            {
                WarmUpPoint2 = new Vector2(WarmUpPoint1.x - 1f, WarmUpPoint1.y + 1f);
                startPoint1 = new Vector2(4.8f, 2.05f);
                startpoint2 = new Vector2(4.3f, 5.5f);
                startpoint3 = new Vector2(3.5f, -2.5f);
                currentPoint1 = startPoint1;
                currentPoint2 = startpoint2;
                currentPoint3 = startpoint3;
                bounceXs = leftwardsbounceXs;
                if (!doubleFrontSpinner && frontSpinner)
                {
                    frontSpinner= false;
                    backSpinner = true;
                }
                else if (backSpinner) 
                {
                    frontSpinner = true; 
                    backSpinner = false;
                }
            }
        }

        public void popBoolArr()
        {
            shapeCatArray[0] = isShape;
            shapeCatArray[1] = isPolygon;
            shapeCatArray[2] = isQuadrilateral;
            shapeCatArray[3] = isTriangle;
            shapeCatArray[4] = isRegular;
            shapeCatArray[5] = isEquilateral;
            shapeCatArray[6] = isEquiangular;
            shapeCatArray[7] = isTrapezoid;
            shapeCatArray[8] = isParallelogram;
            shapeCatArray[9] = isRhombus;
            shapeCatArray[10] = isRectangle;
            shapeCatArray[11] = isSquare;
            shapeCatArray[12] = isScalene;
            shapeCatArray[13] = isIsosceles;
            shapeCatArray[14] = isAcute;
            shapeCatArray[15] = isObtuse;
            shapeCatArray[16] = isRightAngle;
        }

        // Update is called once per frame
        void Update()
        {
            //    time += Time.deltaTime * speed;
            //  if (time < 1f)
            // {
            //    transform.localPosition = new Vector2(Bezier(time,point1,point2,point3);
            //}
            if (!splashed && !splashing && !walkOut)
            {
                if (trampoline)
                {
                    tramppos = trampoline.gameObject.transform.position;
                }
                if (WarmUp > 0f && WarmUp < 0.55f)
                {
                    if (!doOnce)
                    {
                        strtTime = Time.time;
                        doOnce = true;
                    }
                    var t = (Time.time - strtTime) * 2f;
                    Vector2 positionn;

                    positionn = Bezier(t, WarmUpPoint1, WarmUpPoint2, startPoint1);
                    transform.position = new Vector2(positionn.x, positionn.y);
                    if (!springOnce && WarmUp < 0.12f)
                    {
                        springOnce = true;
                        limbs.SendMessage("springLegs");
                    }

                }
                else if (WarmUp>0f)
                {
                    gameObject.GetComponent<SortingGroup>().sortingOrder = 1 + Convert.ToInt32((2f - WarmUp) * 5f);

                }

                if (WarmUp > 0f && WarmUp - Time.deltaTime <= 0f)
                {
                    strtTime = Time.time;
                }
                WarmUp -= Time.deltaTime;

                if (WarmUp > -0.5f && WarmUp - Time.deltaTime <= -0.5f)
                {
                    gameObject.GetComponent<SortingGroup>().sortingOrder = 1;
                }

                if (WarmUp < 0f)
                {
                    var t = 0f;
                    if (Time.time == strtTime) { t = 0f; } //shouldn't but ipads are stupid
                    else 
                    {
                         t = (Time.time - strtTime) / durrration; //it goes up
                    }
                    Vector2 positionn;
                    if (t < 1.0f || wasAboveTramp)
                    {
                        positionn = Bezier(t, currentPoint1, currentPoint2, currentPoint3);
                        if (!pencilShape && t < 0.3f)
                        {
                            limbs.SendMessage("pencilShape");
                            pencilShape = true;
                        }
                        if (positionn.y < tramppos.y - 0.1 && doMask == false)
                        {
                            eyes.SetActive(false);
                            doMask = true;

                            if (bounce < collectors.Length)
                            {
                                collectors[bounce].SendMessage("Splash", this);
                            }
                            else if (!walkOut) //crash landing
                            {
                                doMask = false;
                                eyes.SetActive(true);
                                shapeSpawner.SendMessage("ShapeGrounded");
                                walkOut = true;
                            }
                        }



                        transform.position = new Vector2(positionn.x, positionn.y);
                        if (bounce == 0 && (frontSpinner || backSpinner))
                        {
                            float rotato = frontSpinner ? -720f : 720f;
                            if (t > 0.1f && t < 0.6f)
                            {
                                transform.Rotate(0f, 0f, rotato * Time.deltaTime);
                            }
                        }
                        else if (bounce == 1 && doubleFrontSpinner)
                        {
                            if (t > 0.3f && t < 0.8f)
                            {
                                transform.Rotate(0f, 0f, -720f * Time.deltaTime);
                            }
                        }


                        if (bounce < bounceXs.Length - 1 && trampoline)
                        {
                            if ((transform.position.y - tramppos.y < 0.1 && transform.position.y - tramppos.y > -0.1) ||
                                (wasAboveTramp && transform.position.y < tramppos.y))
                            //&& bounce == 0 && tramppos.x>-4.6f && tramppos.x<-4f)
                            {
                                shapeSpawner.SendMessage("waitForMe");
                                if (bounceXs[bounce] - tramppos.x < 0.5 && bounceXs[bounce] - tramppos.x > -0.8)
                                {
                                    
                                    TestNext();
                                    pencilShape = false;

                                    doMask= false; //doesn't make any sense but anyway, ipads are stupid
                                    gameObject.GetComponent<SpriteMask>().enabled = false;
                                    splashed = false;
                                    splashing= false;

                                    shapeSpawner.SendMessage("trampBounce");
                                    limbs.SendMessage("bounce");
                                    bounce++;
                                    strtTime = Time.time;
                                    if (!leftwards)
                                    {
                                        currentPoint2 = new Vector2(currentPoint3.x + 2f, startpoint2.y * ((12f - (2f * bounce)) / 10f));
                                        currentPoint1 = new Vector2(transform.position.x, transform.position.y);

                                        currentPoint3 = new Vector2(currentPoint3.x + 3f, currentPoint3.y);
                                    }
                                    else
                                    {
                                        currentPoint2 = new Vector2(currentPoint3.x - 2f, startpoint2.y * ((12f - (2f * bounce)) / 10f));
                                        currentPoint1 = new Vector2(transform.position.x, transform.position.y);
                                        currentPoint3 = new Vector2(currentPoint3.x - 3f, currentPoint3.y);

                                    }
                                }
                            }
                            if (transform.position.y < tramppos.y)
                            {
                                wasAboveTramp = false;
                            }
                            else
                            {
                                wasAboveTramp = true;
                            }
                        }
                    }
                    else
                    {
                        positionn = currentPoint3; //1 or larger means we reached the end
                        transform.position = new Vector2(positionn.x, positionn.y);
                        if (!splashing && !walkOut)
                        {
                            splashing = true;
                            limbs.SendMessage("splashDown");
                            SplashDown();
                        }
                    }
                }
            }
            else if (splashing)
            {
                doMask = true;
                splashTimer -= Time.deltaTime;
                if (splashTimer < 0f)
                {
                    if (score == 1) //bad
                    {
                        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);

                        splashing = false;
                        splashed = true;
                        currentMask = 0;
                        eyes.SetActive(true);
                    }
                    else //ok or good
                    {
                       // print("rotattimer" + rotateTimer);
                        rotateTimer -= Time.deltaTime;
                        transform.Rotate(new Vector3(0f, Time.deltaTime*360f, 0f));

                        if (rotateTimer < 0.25f)
                        {
                            eyes.SetActive(false);
                        }
                        if (rotateTimer < 0f)
                        {
                            splashing = false;
                            splashed = true;
                            currentMask = 0;
                            eyes.SetActive(false);
                            transform.rotation = new Quaternion(0f, 180f, 0f, 0f);

                        }
                    }


                }
            }
            else if (walkOut)
            {
                if (leftwards)
                {
                    transform.position = new Vector2(transform.position.x - Time.deltaTime, transform.position.y);                   
                }
                else
                {
                    transform.position = new Vector2(transform.position.x + Time.deltaTime, transform.position.y);
                }
                if(transform.position.x > 17f || transform.position.x < -17f)
                {
                    shapeSpawner.SendMessage("ShapeSplashed");
                    gameObject.SetActive(false);
                }

            }
            else //splashed
            {
                if (!finishedSwim)
                {
                    if (score == 1) //bad
                    {
                        eyes.SetActive(true);
                        transform.position = new Vector2(transform.position.x, transform.position.y - swimSpeed * Time.deltaTime);
                    }
                    else // ok or good
                    {
                        eyes.SetActive(false);
                        transform.position = new Vector2(transform.position.x - (swimleftSpeed * Time.deltaTime), transform.position.y + swimSpeed * Time.deltaTime);
                        transform.localScale = new Vector3(transform.localScale.x - 0.1f * Time.deltaTime, transform.localScale.y - 0.1f * Time.deltaTime, transform.localScale.z);
                    }
                    if (transform.position.y < bottomOfPool)
                    {
                        finishedSwim = true;
                        gameObject.SetActive(false);
                        shapeSpawner.SendMessage("ShapeSplashed");

                    }

                    else if (transform.position.y > topOfPool)
                    {
                        finishedSwim = true;
                        eyes.SetActive(true);
                        shapeSpawner.SendMessage("ShapeSplashed");

                    }
                    else if (transform.position.y > topOfPool - 0.25)
                    {
                        transform.Rotate(0f, Time.deltaTime * 360f, 0f);
                    }
                }
            }
            if (doMask)
            {
                maskTimer -= Time.deltaTime;
                if (splashing && currentMask > 4 && currentMask < 8)
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y + swimSpeed * Time.deltaTime);
                    maskTimer -= Time.deltaTime * 10;
                }

                if (maskTimer < 0f)
                {
                    currentMask++;
                    if (splashing)
                    {
                        if (currentMask < 11)
                        {
                            gameObject.GetComponent<SpriteMask>().enabled = true;
                            gameObject.GetComponent<SpriteMask>().sprite = splashMaskArray[currentMask];
                            if (currentMask > 4)
                            {
                                transform.position = new Vector2(transform.position.x, transform.position.y + swimSpeed * 5 * Time.deltaTime);
                            }
                        }
                        else
                        {
                            if (rotateTimer > 0.25f)
                            {
                                eyes.SetActive(true);
                            }
                            gameObject.GetComponent<SpriteMask>().sprite = shapeMaskArray[currentMask % 8];
                        }
                    }
                    else
                    {
                        if (currentMask >= 8)
                        {
                            currentMask = 0;

                        }
                        gameObject.GetComponent<SpriteMask>().sprite = shapeMaskArray[currentMask];
                    }
                    maskTimer = maskAnimDelay;
                }
            }
            if (finalSwimDown) 
            {
                finalTimer -= Time.deltaTime;
                if (finalTimer< 0f) 
                {
                    finalSwimDown= false;
                    gameObject.SetActive(false);
                }
                else
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y - swimSpeed * Time.deltaTime);
                }
            }
            if (finalFlyUp)
            {
                dripTimer-= Time.deltaTime;
                if (dripTimer< 0f)
                {
                    dripTimer = 0.1f;
                    dripper.GetComponent<SpriteRenderer>().sprite= dripArray[currentDrip];
                    currentDrip++;
                    if (currentDrip>=dripArray.Length)
                    {
                        dripTimer = 50f;
                        dripper.SetActive(false);
                    }   
                    
                }

                finalTimer -= Time.deltaTime;
                if (finalTimer > 4.95f)
                {
                    gameObject.GetComponent<SpriteMask>().sprite = shapeMaskArray[1];

                }
                else if (finalTimer > 4.9f)
                {
                    gameObject.GetComponent<SpriteMask>().sprite = shapeMaskArray[0];

                }
                else if (finalTimer > 4.85f)
                {
                    doMask = false;
                    gameObject.GetComponent<SpriteMask>().sprite = null;
                }
                else
                {
                    transform.Rotate(0f, 0f, Time.deltaTime * flyRotat);
                }

                if (finalTimer < 0f)
                {
                    finalFlyUp = false;
                    gameObject.SetActive(false);
                }
                else
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y + swimSpeed * Time.deltaTime * (6.5f-finalTimer));
                    dripper.transform.position = new Vector2(dripper.transform.position.x, dripper.transform.position.y - (swimSpeed * Time.deltaTime * (6.5f - finalTimer)));

                }
            }
        }

        public Vector2 Bezier(float t, Vector2 a, Vector2 b, Vector2 c)
        {
            var ab = Vector2.Lerp(a, b, t);
            var bc = Vector2.Lerp(b, c, t);
            return Vector2.Lerp(ab, bc, t);
        }

        public void SetSpawner(GameObject spawner){shapeSpawner = spawner;}
        public void SetTrampoline(GameObject tramp){trampoline = tramp;}

        public void SetCollectors(PolyGonk.Collector[] collection){collectors = collection;}

        public void SetPerfectCollector(int perfectN) { perfectCollector = perfectN; }

        public void SetCollectorOks(bool[] oks) 
        {
            collectorOkays= new bool[oks.Length];
            for (int i = 0; i < oks.Length; i++)
            {
                collectorOkays[i] = oks[i];
            }
            if(leftwards)//hacky
            {
                bool tempok = false;
                if (collectorOkays[0]) tempok = true;
                if (collectorOkays[2]) collectorOkays[0] = true;
                else collectorOkays[0 ]=false;
                collectorOkays[2] = tempok;


            }
        }

        public void SetLeftwards(bool way){leftwards = way;}

        public void SetRandomColour(Color colourX)
        {
            shapeObj.GetComponent<SpriteRenderer>().color = colourX;
        }

        private void SplashDown()
        {
            collectors[bounce].SendMessage("Collection", this);
        }

        public void Result(int[] stuff)
        {
            score = stuff[0];
            if (score > 1)
            {
                swimleftSpeed = ((stuff[1] - 1) % 5) / 10f;
                gameObject.GetComponent<SortingGroup>().sortingLayerName = "ShapesBackLayer";
                gameObject.GetComponent<SortingGroup>().sortingOrder = stuff[1];
                topOfPool -= stuff[1] * 0.05f;
                if (stuff[1] == 1 && !wideShape)
                {
                    limbs.SendMessage("edgeRest");
                }
            }
            else
            {
                limbs.SendMessage("SwimDown");
            }
        }

        public void CountUp()
        {
            collectors[bounce].SendMessage("SendScore", score - 1);
            if (score == 2) 
            {
                limbs.SendMessage("SwimDown");
                finalSwimDown = true;
                oneUp.SetActive(true);
            }
            if (score == 3)
            {
                limbs.SendMessage("FlyUp");
                finalFlyUp = true;
                twoUp.SetActive(true);
                dripper.SetActive(true);
                if (UnityEngine.Random.Range(0f,1f)<0.5f)
                {
                    flyRotat = 0f - flyRotat;
                }
            }
        }

        void TestNext()
        {
            int zeroooo = 0;
            if (bounce>=collectors.Length-1)
            {
                shapeSpawner.SendMessage("Flash", zeroooo);
            }
            else if (!leftwards) 
            {
                if (bounce+1 == perfectCollector)
                {
                    shapeSpawner.SendMessage("Flash", 2);

                }
                else 
                {
                    bool testy = false;
                    for (int i = 0; i < collectorOkays.Length; i++)
                    {
                        if (i > bounce && collectorOkays[i] == true)
                        {
                            testy = true; break;
                        }
                    }
                    if (testy == false)
                    {
                        shapeSpawner.SendMessage("Flash", zeroooo);
                    }
                    else
                    {
                        shapeSpawner.SendMessage("Flash", 1);

                    }
                }
            }
            else //leftwards , not tested
            {
                if (1-bounce == perfectCollector) //hacky
                {
                    shapeSpawner.SendMessage("Flash", 2);

                }
                else if (1-bounce > perfectCollector)
                {
                    shapeSpawner.SendMessage("Flash", 1);

                }

                else
                {
                    bool testy = false;
                    for (int i = 1; i >= 0; i--) //hacky
                    {
                        if (i >= 0 + bounce && collectorOkays[i] == true)
                        {
                            testy = true; break;
                        }
                    }
                    if (testy == false)
                    {
                        shapeSpawner.SendMessage("Flash", zeroooo);
                    }
                    else
                    {
                        shapeSpawner.SendMessage("Flash", 1);

                    }
                }
            }
        }
    }
}