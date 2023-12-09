using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace WeirdosKlamber.PolyGonk
{
    public class MainPolyGonkScript : MonoBehaviour
    {
        public int levelNumber;
        public PolyGonk.Tramp mainTramp;
        public GameObject mainCamera;
        public GameObject perfectVictory;
        public GameObject shapeSpawnerLeft;
        private bool hasShapeSpawnerRight = false;
        public GameObject shapeSpawnerRight;
        public PolyGonk.Collector[] liveCollectors;
        private int currentCollector = 0;
        private int currentShape = 0;
        public AudioSource ding;
        public AudioSource woopdyUp;
        public AudioSource music;
        public AudioSource deathFX;
        public TextMeshProUGUI timeDisplay;
        public GameObject timeDisplayDisplay;
        public TextMeshProUGUI scoreDisplay;
        public GameObject scoreDisplayDisplay;
        private float cameraSpeed = 10f;
        private float currentCamSpeed = 0.5f;
        public float centreCamX = 3.7f;
        private float centreCamY = 0.2f;
        private float centreCamZ = -7.6f;
        public float[] bounceXs = { -2.3f, 0.7f, 3.7f, 6.7f, 9.7f };
        public float trampPosX = -2.2f;
        public float camPosX = 0f;
        public float camPosY = 0f;
        private float camPosZ = -7.6f;

        public float targetCamPosZ = -7.6f;
        private bool camStartZoomed = false;

        public float cameraMaxX = 5f;
        public bool cameraCanMove = true;
        private int score = 0;
        public float mainTime = 60f;
        private int intTime = 60;
        private bool playEnding = false;
        private bool playEnded = false;
        private bool finale = false;
        private bool finalePan = false;
        private bool fademusic = false;
        private float zoomTimerMaster = 2f;
        private float zoomTimer = 2f;
        private float countTimerMaster = 0.5f;
        private float countTimer = 0.5f;
        private bool finaleZoomIn = false;
        private bool finaleCountUp = false;
        private bool finaleZoomOut = false;
        public int threeStarScore;   // belongs in singleton or main
        public int twoStarScore;
        public int oneStarScore;
        private bool perfectPerformance = true;

        public bool heartsLevel = false;
        public GameObject[] hearts;
        private int lives;

        public bool hasIntro = false;

        // Start is called before the first frame update
        void Start()
        {
            if (hasIntro) 
            {
                Time.timeScale = 0f;
            }
            else
            {
                music.PlayOneShot(music.clip);
            }

            intTime = Mathf.FloorToInt(mainTime);
            if (heartsLevel)
            {
                lives = hearts.Length;
            }
            camPosY = mainCamera.transform.position.y;
            camPosZ = mainCamera.transform.position.z;
            mainTramp.numberOfCollectors = liveCollectors.Length;
            if (shapeSpawnerRight != null) hasShapeSpawnerRight = true;
        }

        // Update is called once per frame
        void Update()
        {
            if(!hasIntro && !camStartZoomed)
            {
                //print("camzooming camposz:" + camPosZ + "  targetposz: " + targetCamPosZ);
                if(camPosZ+Time.deltaTime*cameraSpeed>targetCamPosZ)
                {
                    camPosZ = targetCamPosZ;
                    camStartZoomed= true;
                }
                else
                {
                    camPosZ += Time.deltaTime * cameraSpeed/1.5f;
                }
                mainCamera.transform.localPosition = new Vector3(mainCamera.transform.localPosition.x, mainCamera.transform.localPosition.y, camPosZ);
            }

            if (!playEnding)
            {
                mainTime -= Time.deltaTime;
                if (intTime > mainTime)
                {
                    intTime = Mathf.FloorToInt(mainTime);
                    timeDisplay.text = intTime.ToString();
                    if (intTime <= 0)
                    {
                        endPlay();
                    }

                }
            }
            if (!playEnded)
            {
                trampPosX = mainTramp.transform.position.x;
                camPosX = mainCamera.transform.position.x;
                currentCamSpeed = cameraSpeed * Time.deltaTime;
                if (trampPosX > 3f && cameraCanMove)
                {
                    if (trampPosX > camPosX + currentCamSpeed && trampPosX < cameraMaxX)
                    {
                        mainCamera.transform.position = new Vector3(camPosX + currentCamSpeed, camPosY, camPosZ);
                    }
                    else if (trampPosX < camPosX - currentCamSpeed)
                    {
                        mainCamera.transform.position = new Vector3(camPosX - currentCamSpeed, camPosY, camPosZ);
                    }
                }
                else if (trampPosX < cameraMaxX && cameraCanMove)
                {
                    if (camPosX + currentCamSpeed < 0f)
                    {
                        mainCamera.transform.position = new Vector3(camPosX + currentCamSpeed, camPosY, camPosZ);
                    }
                    else if (camPosX - currentCamSpeed > 0f)
                    {
                        mainCamera.transform.position = new Vector3(camPosX - currentCamSpeed, camPosY, camPosZ);
                    }

                }
            }

            if (playEnded && !finale)
            {
                if (!hasShapeSpawnerRight) mainTramp.gameObject.SetActive(false);
                finalePan = true;
                finale = true;
                cameraSpeed = liveCollectors[currentCollector].transform.position.x - camPosX;
                if (cameraSpeed<0f) cameraSpeed = 0f-cameraSpeed;


            }

            if (finale)
            {
                if (fademusic)
                {
                    if (music.volume - Time.deltaTime > 0f)
                    {
                        music.volume -= Time.deltaTime;
                    }
                    else
                    {
                        music.volume = 0f;
                        fademusic = false;
                    }
                }
                if (finalePan)
                {
                    currentCamSpeed = cameraSpeed * Time.deltaTime;

                    camPosX = mainCamera.transform.position.x;
                    var liveX = liveCollectors[currentCollector].transform.position.x;
                    if ( liveX > camPosX + currentCamSpeed)
                    {
                        mainCamera.transform.position = new Vector3(camPosX + currentCamSpeed, camPosY, camPosZ);
                    }
                    else if (liveX < camPosX - currentCamSpeed)
                    {
                        mainCamera.transform.position = new Vector3(camPosX - currentCamSpeed, camPosY, camPosZ);
                    }
                    else
                    {
                        finalePan= false;
                        finaleZoomIn = true;
                        cameraSpeed = 3f;
                        liveCollectors[currentCollector].ReverseArray();                        
                    }
                }

                if (finaleZoomIn)
                {
                    currentCamSpeed = cameraSpeed * Time.deltaTime;
                    timeDisplayDisplay.SetActive(false);
                    scoreDisplayDisplay.SetActive(true);
                    camPosY = mainCamera.transform.position.y;
                    camPosZ = mainCamera.transform.position.z;
                    var nextY = camPosY;
                    var nextZ = camPosZ;
                    if (-1f < camPosY - currentCamSpeed)
                    {
                        nextY -= currentCamSpeed/4f;
                    }

                    if (-3f > camPosZ + currentCamSpeed)
                    {
                       nextZ += currentCamSpeed;
                    }
                    mainCamera.transform.position = new Vector3(camPosX, nextY, nextZ);


                    if (-0.9f > nextY && -3.1f < nextZ)
                    {
                        finaleZoomIn = false;
                        finaleCountUp = true;
                        if (liveCollectors.Length > currentCollector)
                        {
                            if (liveCollectors[currentCollector].myShapes?.Length > currentShape && liveCollectors[currentCollector].myShapes[currentShape] != null)
                            {
                                countTimer = countTimerMaster;
                            }
                        }
                    }
                    else if (-1f > camPosY - currentCamSpeed && -3f > camPosZ + currentCamSpeed)
                    {
                        cameraSpeed *=0.9f;
                    }

                }

                if (finaleCountUp)
                {
                    countTimer-=Time.deltaTime;
                    if (countTimer < 0f)
                    {
                        if (liveCollectors.Length > currentCollector)
                        {
                            if (liveCollectors[currentCollector].myShapes?.Length > currentShape && liveCollectors[currentCollector].myShapes[currentShape]!=null)
                            {
                                liveCollectors[currentCollector].myShapes[currentShape].SendMessage("CountUp");
                                currentShape++;
                                countTimer = countTimerMaster;
                            }
                            else
                            {
                                currentShape = 0;
                                currentCollector++;
                                finaleCountUp = false;
                                if (liveCollectors.Length > currentCollector)
                                {
                                    finalePan = true;
                                }
                                else
                                {
                                    print("EndCountUp");
                                    finaleCountUp = false;
                                    finaleZoomOut = true;
                                    if (perfectPerformance)
                                    {
                                        perfectVictory.SetActive(true);
                                        print("PerfectVictory");
                                        foreach(Collector x in liveCollectors)
                                        {
                                            x.Celebrate();
                                        }
                                    }
                                    cameraSpeed = 3f;
                                }
                            }
                        }
                        else //redundant
                        {
                            print("EndCountUp");
                            finaleCountUp = false;
                            finaleZoomOut = true;
                            cameraSpeed = 3f;
                        }
                    }
                }

                if (finaleZoomOut)
                {
                    currentCamSpeed = cameraSpeed * Time.deltaTime;
                    fademusic = true;
                    camPosX = mainCamera.transform.position.x;
                    camPosY = mainCamera.transform.position.y;
                    camPosZ = mainCamera.transform.position.z;
                    var nextX = camPosX;
                    var nextY = camPosY;
                    var nextZ = camPosZ;
                    if (centreCamX < camPosX - currentCamSpeed)
                    {
                        nextX -= currentCamSpeed*2f;
                    }
                    if (centreCamY > camPosY + currentCamSpeed)
                    {
                        nextY += currentCamSpeed / 4f;
                    }
                    if (centreCamZ < camPosZ - currentCamSpeed)
                    {
                        nextZ -= currentCamSpeed;
                    }
                    mainCamera.transform.position = new Vector3(nextX, nextY, nextZ);


                    if (centreCamX > nextX-0.1f && centreCamY < nextY +0.1f && centreCamZ > nextZ -0.1f)
                    {
                        finaleZoomOut = false;
                        //finale = false;
                        print("END LEVEL");
                        EndLevel();
                    }
                    else 
                    {
                        cameraSpeed -=Time.deltaTime;
                        if (cameraSpeed < 0.5f) cameraSpeed = 0.5f;

                    }

                }

            }
        
        }
        private void endPlay()
        {
            playEnding = true;
            print("EndingPlay");
            shapeSpawnerLeft.SendMessage("endPlay");
            if (hasShapeSpawnerRight)
            {
                shapeSpawnerRight.SendMessage("endPlay");
            }
        }

        private void veryEndPlay()
        {
            playEnded = true;
            if (mainTramp.gameObject.activeSelf==true) 
            {
                mainTramp.SendMessage("endPlay");
            }
   

            print("veryENDPLAY");
           
        }

        private void EndLevel()
        {
            SingletonSimple.Instance.LevelCompleted(levelNumber, score, perfectPerformance);
            SceneManager.LoadScene("Main");

        }
        public void addScore(int scoreX)
        {
            score += scoreX;
            scoreDisplay.text = score.ToString();
            if (scoreX == 1) { ding.PlayOneShot(ding.clip); }
            if (scoreX == 2) { woopdyUp.PlayOneShot(woopdyUp.clip); }

        }

        public void NotPerfect() //just to ensure 3 star possible
        {
            perfectPerformance = false;
        }

        public void KillHeart()
        {
            if(heartsLevel)
            {
                lives--;
                if (lives >= 0)
                {
                    hearts[lives].SetActive(false);
                }
                if (lives ==0)
                {
                    deathFX.PlayOneShot(deathFX.clip);
                    mainTime = 0;
                }
            }
        }

        public void endIntro()
        {
            hasIntro = false;
            Time.timeScale = 1f;
            music.PlayOneShot(music.clip);

        }
    }
}