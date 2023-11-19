using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace WeirdosKlamber.PolyGonk
{
    public class ShapeSpawner : MonoBehaviour
    {
        public GameObject main;
        public AudioSource groundLanding;
        public PolyGonk.Collector[] collectors;
        public GameObject trampoline;
        public PolyGonk.Shapes.ShapeScript[] shapeArray;
        public GameObject Board;
        public int[] perfectCollectors;
        public bool isRightSpawner = false;
        private int[] regulateShapes = new int[18];
        private int regulator = 0;
        public float timingMaster = 3f;
        public float timingCurrent = 3f;
        private float timingTimer = 0.1f;
        private float minimumGap = 0.7f;
        private float absoluteMinimumGap = 0.5f;
        private bool breather = true;
        private float breatherchance = 0.2f;
        private bool waitBool=false;
        private float waitMaster = 0.1f;
        private float currentWait = 0.1f;
        private bool playEnding = false;
        private bool playEnded = false;
        private int shapesSpawnedN = 0;
        private int shapesLandedN = 0;
        private bool[] collectorOkays = new bool[8];

        private PolyGonk.Shapes.ShapeScript oldShape;
        private int strictRegulator = 0;
        private int startRegulator = 0;
        private int regulatorToUse = 0;

        public bool randomColourShapes = false;
        private Color[] colourArray = { Color.blue, Color.cyan, new Color(1f, 0.65f, 0.05f, 1f), Color.green, Color.magenta, Color.red, new Color(1f, 1f, 0f, 1f) };
        private int lastColour = 1;
        public bool beMean = false;


        // Start is called before the first frame update
        void Start()
        {
            int divvy = (regulateShapes.Length / shapeArray.Length);
            timingCurrent = timingMaster;
            oldShape = shapeArray[0];
            if (isRightSpawner)
            {
                timingTimer += 0.5f;

            }
            for (int i = 0; i < shapeArray.Length; i++)
            {
                for (int j = 0; j < divvy; j++)
                {
                    if (i * divvy + j < regulateShapes.Length)
                    {
                        regulateShapes[i * divvy + j] = i;
                    }
                    else print(i * divvy + j);
                }
            }

            var rng = new System.Random();
            var keys = regulateShapes.Select(e => rng.NextDouble()).ToArray();

            Array.Sort(keys, regulateShapes);
            if (isRightSpawner)
            {
                regulator++;
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (!playEnding)
            {
                timingTimer -= Time.deltaTime;
                if (timingTimer < 0f && !waitBool)
                {
                    PolyGonk.Shapes.ShapeScript newShape;
                    print("old shape name: " + oldShape.name);
                    string oldShapeNameShort = oldShape.name.Split('(')[0];
                    if ((oldShape.perfectCollector >= 3 && !beMean)||(oldShape.perfectCollector >= 4 && beMean))
                    {
                        print("make easy" + oldShape.name);
                        newShape = Instantiate(shapeArray[0], transform);
                        regulatorToUse = 0;

                    }
                    else if (startRegulator < shapeArray.Length && shapesSpawnedN % 2 == 0)
                    {
                        
                        if (startRegulator>=1 && shapeArray[startRegulator].name == oldShapeNameShort)
                        {
                            print("startregulator-1");
                            newShape = Instantiate(shapeArray[startRegulator-1], transform);
                            regulatorToUse = startRegulator-1;
                        }
                        else
                        {
                            print("startregulator");
                            newShape = Instantiate(shapeArray[startRegulator], transform);
                            regulatorToUse = startRegulator;
                        }
                                                                     
                        startRegulator++;
                    }
                    else if (shapeArray[regulateShapes[regulator]].name == oldShapeNameShort)
                    {
                        print("REDUCE DUPLICATES" + oldShape.name);
                        if (shapeArray[strictRegulator].name == oldShapeNameShort)
                        {
                            strictRegulator++;
                            if (strictRegulator >= shapeArray.Length) strictRegulator = 0;
                        }
                        newShape = Instantiate(shapeArray[strictRegulator], transform);
                        regulatorToUse  = strictRegulator;
                        strictRegulator++;
                        if (strictRegulator>=shapeArray.Length) strictRegulator= 0;
                    }
                    else
                    {
                        if (shapeArray[regulateShapes[regulator]].name == oldShapeNameShort)
                        {
                            print("ERROR STUPID ERROR");
                        }
                        if (beMean && regulateShapes[regulator] == 0)
                        {
                            newShape = Instantiate(shapeArray[1], transform);
                            regulatorToUse = 1;
                            print("BEING MEAN");
                        }
                        else
                        {
                            print("regular production   oldshape name:" + oldShape.name + "  newshape.name" + shapeArray[regulateShapes[regulator]].name);

                            newShape = Instantiate(shapeArray[regulateShapes[regulator]], transform);
                            regulatorToUse = regulateShapes[regulator];
                        }
                    }
                    shapesSpawnedN++;
                    oldShape= newShape;
                    newShape.SetSpawner(this.gameObject);
                    newShape.SetTrampoline(trampoline);
                    newShape.SetCollectors(collectors);
                    print(newShape.name + " collectors n:" + collectors.Length);
                    newShape.SetPerfectCollector(perfectCollectors[regulatorToUse]);
                    newShape.SetLeftwards(isRightSpawner);
                    if (randomColourShapes)
                    {
                        int randy = UnityEngine.Random.Range(0, colourArray.Length);                        
                        if (randy == lastColour)
                        {
                            int randy2 = UnityEngine.Random.Range(0, colourArray.Length);
                            if (randy2 == lastColour)
                            {
                                int randy3 = UnityEngine.Random.Range(0, colourArray.Length);
                                newShape.SetRandomColour(colourArray[randy3]);
                                lastColour = randy3;

                            }
                            else
                            {
                                newShape.SetRandomColour(colourArray[randy2]);
                                lastColour = randy2;
                            }
                        }
                        else
                        {
                            newShape.SetRandomColour(colourArray[randy]);
                            lastColour = randy;
                        }
                    }
                    newShape.popBoolArr();
                    for (int i = 0; i < collectors.Length; i++)
                    {
                        collectorOkays[i] = collectors[i].TestShape(newShape);
                    }
                    newShape.SetCollectorOks(collectorOkays);
                    Board.SendMessage("Spring");

                    regulator++;
                    if (regulator > regulateShapes.Length - 1) regulator = 0;
                    timingCurrent -= 0.1f;
                    if (timingCurrent < minimumGap) timingCurrent = minimumGap;
                    timingTimer = UnityEngine.Random.Range(0.5f, 1.5f) * timingCurrent;
                    if (timingCurrent < absoluteMinimumGap) timingTimer = absoluteMinimumGap;
                    if (breather && UnityEngine.Random.Range(0f, 1f) < breatherchance)
                    {
                        timingTimer = UnityEngine.Random.Range(0.5f, 1.5f) * timingMaster;
                    }
                }

                if (waitBool)
                {
                    currentWait -= Time.deltaTime;
                    if (currentWait < 0f)
                    {
                        waitBool = false;

                    }
                }
            }
            else if (!playEnded) //todo something for two boards?
            {
                if (shapesSpawnedN == shapesLandedN)
                {
                    playEnded = true;
                    main.SendMessage("veryEndPlay");
                    print("shapes spawned" + shapesSpawnedN);
                    print("shapes landed" + shapesLandedN);
                }
            }
        }

        void waitForMe()
        {
            waitBool = true;
            currentWait = waitMaster;
        }

        void trampBounce()
        {
            trampoline.SendMessage("trampBounce");
        }

        void ShapeSplashed()
        {
            shapesLandedN++;
        }

        void ShapeGrounded()
        {
            groundLanding.PlayOneShot(groundLanding.clip);
        }

        void endPlay()
        {
            playEnding = true;
        }

        public void Flash(int colour)
        {
            if (colour== 0)
            {
                trampoline.SendMessage("FlashRed");
            }
            if (colour == 1)
            {
                trampoline.SendMessage("FlashYellow");
            }
            if (colour == 2)
            {
                trampoline.SendMessage("FlashGreen");
            }
        }
    }
}
