
using UnityEngine;
using TMPro;
using System.Linq;

namespace WeirdosKlamber.PolyGonk
{
    public class Collector : MonoBehaviour
    {
        public GameObject main;
        public GameObject squareObj;
        public TextMeshPro labelX;
        private AudioSource audioSource;
        private AudioClip splashFX;
        public AudioClip whistle;
        public AudioClip[] yeahfxs;
        private AudioClip yeahfx;
        public GameObject sky;
        public string nameX;

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
        public bool[] collectorCatArray = new bool[17];

        public int collectorNumber;
        public GameObject[] myShapes = new GameObject[50];
        private GameObject[] myShapesTemp = new GameObject[50];
        private int tempShapesIterator = 0;

        private int numShapes = 0;
        private bool reversed = false;

        private bool specificMatch = false;
        private bool okMatch = false;
        private float flashTime = 0f;
        private float flashTimerMaster = 0.3f;

        public GameObject splash1;
        public GameObject splash2;
        public GameObject splash3;
        public Sprite[] splashSprites = new Sprite[7];

        private float splashTimerMaster = 0.1f;
        private float splashTimer1 = 0.1f;
        private float splashTimer2 = 0.1f;
        private float splashTimer3 = 0.1f;
        private int splashInt1 = -1;
        private int splashInt2 = -1;  
        private int splashInt3 = -1;

        void Start()
        {
            popColBoolArr();
            audioSource = GetComponent<AudioSource>();
            splashFX = audioSource.clip;
            var textX = labelX.GetComponent<TMP_Text>();
            textX.text = PolyGonkScript.GetText(nameX.ToUpper());
            textX.fontSize = 24;
            textX.alignment = TextAlignmentOptions.Center;           
        }

        void Update()
        {
            if (flashTime>0f)
            {
                flashTime -= Time.deltaTime;
                if (flashTime<0f)
                {
                    specificMatch = false;
                    okMatch = false;
                    squareObj.gameObject.GetComponent<SpriteRenderer>().color= Color.white; 
                }
                else if (specificMatch)
                {
                    squareObj.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                }
                else if (okMatch)
                {
                    squareObj.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                }
                else
                {
                    squareObj.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }

            if (splashInt1>=0)
            {
                splashTimer1-=Time.deltaTime;
                if (splashTimer1 < 0f)
                {
                    splashInt1++;
                    if (splashInt1 > 6)
                    {
                        splash1.GetComponent<SpriteRenderer>().sprite = splashSprites[0];
                        splashInt1 = -1;
                        splash1.SetActive(false);
                    }
                    else
                    {
                        splash1.GetComponent<SpriteRenderer>().sprite = splashSprites[splashInt1];
                        splashTimer1 = splashTimerMaster;
                    }
                }     
            }

            if (splashInt2 >= 0)
            {
                splashTimer2 -= Time.deltaTime;
                if (splashTimer2 < 0f)
                {
                    splashInt2++;
                    if (splashInt2 > 6)
                    {
                        splashInt2 = -1;
                        splash2.GetComponent<SpriteRenderer>().sprite = splashSprites[0];
                        splash2.SetActive(false);
                    }
                    else
                    {
                        splash2.GetComponent<SpriteRenderer>().sprite = splashSprites[splashInt2];
                        splashTimer2 = splashTimerMaster;
                    }
                }
            }

            if (splashInt3 >= 0)
            {
                splashTimer3 -= Time.deltaTime;
                if (splashTimer3 < 0f)
                {
                    splashInt3++;
                    if (splashInt3 > 6)
                    {
                        splashInt3 = -1;
                        splash3.GetComponent<SpriteRenderer>().sprite = splashSprites[0];
                        splash3.SetActive(false);
                    }
                    else
                    {
                        splash3.GetComponent<SpriteRenderer>().sprite = splashSprites[splashInt3];
                        splashTimer3 = splashTimerMaster;
                    }
                }
            }
        }

        void Splash()
        {
            audioSource.pitch = 0.9f + (Random.value / 5f);
            audioSource.PlayOneShot(splashFX);
            if (splashInt1==-1) 
            { 
                splashInt1 = 0;
                splash1.SetActive(true);
            }
            else if (splashInt2 == -1) 
            { 
                splashInt2 = 0;
                splash2.SetActive(true);
            }
            else if (splashInt3 == -1) 
            {
                splashInt3 = 0;
                splash3.SetActive(true);
            }
        }
        void Collection(Shapes.ShapeScript shape)
        {
            specificMatch = false;
            okMatch = true;
            if (shape.perfectCollector == collectorNumber)
            {
                specificMatch = true;
                myShapes[numShapes] = shape.gameObject;
                if (numShapes < 49)
                {
                    numShapes++;
                }
                int[] anObjectTHen = { 3, numShapes };
                shape.SendMessage("Result", anObjectTHen);
                yeahfx = yeahfxs[Random.Range(0, 10)];
                audioSource.PlayOneShot(yeahfx);
            }

            else
            {
                for (int i = 0; i < collectorCatArray.Length; i++)
                {
                    if (collectorCatArray[i] == true && shape.shapeCatArray[i] == false)
                    {
                        specificMatch = false;
                        okMatch = false;
                        int[] anObjectTHen = { 1, 0 };
                        shape.SendMessage("Result", anObjectTHen);
                        audioSource.PlayOneShot(whistle,0.5f);
                        sky.SendMessage("FlashRed");
                        main.SendMessage("KillHeart");
                        main.SendMessage("NotPerfect"); //3 star no longer possible unless exceed score threshold
                    }
      
                }
            }

            if (okMatch && !specificMatch)
            {
                

                myShapes[numShapes] = shape.gameObject;
                if (numShapes < 49)
                {
                    numShapes++;
                }
                int[] anObjectTHen = { 2, numShapes };
                shape.SendMessage("Result", anObjectTHen);
                main.SendMessage("NotPerfect"); //3 star no longer possible unless exceed score threshold
            }

            flashTime = flashTimerMaster;
        }

        private void popColBoolArr()
        {
            collectorCatArray[0] = isShape;
            collectorCatArray[1] = isPolygon;
            collectorCatArray[2] = isQuadrilateral;
            collectorCatArray[3] = isTriangle;
            collectorCatArray[4] = isRegular;
            collectorCatArray[5] = isEquilateral;
            collectorCatArray[6] = isEquiangular;
            collectorCatArray[7] = isTrapezoid;
            collectorCatArray[8] = isParallelogram;
            collectorCatArray[9] = isRhombus;
            collectorCatArray[10] = isRectangle;
            collectorCatArray[11] = isSquare;
            collectorCatArray[12] = isScalene;
            collectorCatArray[13] = isIsosceles;
            collectorCatArray[14] = isAcute;
            collectorCatArray[15] = isObtuse;
            collectorCatArray[16] = isRightAngle;
        }

        public void SendScore(int scoreX) { main.SendMessage("addScore", scoreX); }

        public void Celebrate() 
        {
            yeahfx = yeahfxs[Random.Range(0, 10)];
            audioSource.PlayOneShot(yeahfx);
        }

        public void ReverseArray()
        {
            if (!reversed)
            {
                for (int i = 49; i >= 0; i--)
                {
                    if(myShapes[i] != null)
                    {
                        myShapesTemp[tempShapesIterator] = myShapes[i];
                        tempShapesIterator++;
                    }
                }
                myShapes = myShapesTemp;
                reversed = true;
            }
        }

        public bool TestShape(Shapes.ShapeScript shape)
        {
            if (shape.perfectCollector == collectorNumber)
            { return true; }
                        
            for (int i = 0; i < collectorCatArray.Length; i++)
            {
                if (collectorCatArray[i] == true && shape.shapeCatArray[i] == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
