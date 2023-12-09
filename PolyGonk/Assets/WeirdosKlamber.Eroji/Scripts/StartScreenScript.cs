using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using LoLSDK;
using SimpleJSON;
using WeirdosKlamber;
namespace WeirdosKlamber.PolyGonk
{
    [System.Serializable]
    public class CookingData
    {
        /* public int coins = 200;
         // use _ and not camelCasing for easy porting to server if needed.
         public int cost_of_pan = 70;
         public int num_of_pans;
         // You can use types not supported by unity serialization
         public Dictionary<int, string> food_in_pan = new Dictionary<int, string>();
         // Also nested types of other serialized objects.
         public List<FoodData> food;*/
        public bool Checkpoint1lesson1 = false;
        public bool Checkpoint2level1 = false;
        public bool Checkpoint3lesson2 = false;
        public bool Checkpoint4level2 = false;
        public bool Checkpoint5lesson3 = false;
        public bool Checkpoint6level3 = false;
        public bool Checkpoint7lesson4 = false;
        public bool Checkpoint8level4 = false;
        public bool Checkpoint9lesson5 = false;
        public bool Checkpoint10level5 = false;
        public bool Checkpoint11lesson6 = false;
        public bool Checkpoint12level6 = false;
        public bool Checkpoint13lesson7 = false;
        public bool Checkpoint14level7 = false;
        public int[] SaveLevelScores = new int[7];
        public int[] SaveLevelsAttempts = new int[7];
        public bool[] SaveLevelsPerfectArray = new bool[7];
        public int SaveTotalScore = 0;        
    }



    public class StartScreenScript : MonoBehaviour
    {
        public Button newGameButton;
        public Button continueButton;
        public PolyGonk.squarehello helloSquare;
        private bool Loaded = false;
        public AudioSource ClickFX;
        public GameObject FadeOut;
        private float Fader = 0f;
        private float TryAgain = 1f;
        private bool triedonce = false;
        public GameObject MainMenu;
        public GameObject FailedtoConnect;
        private float timerx = 0f;
        private int xnullllytry = 0;
        private int triedoncetry = 0;
        /// <summary>
        /// Helper to handle your required NEW GAME and CONTINUE buttons.
        /// Stops double clicking of buttons and shows the continue button only when needed.
        /// Also handles broadcasting out the serialized progress back to the teacher app.
        /// <para>NOTE: This is just a helper method, you can implement this flow yourself but it must send Progress when the state loads.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="newGameButton"></param>
        /// <param name="continueButton"></param>
        /// <param name="callback"></param>
        public static void StateButtonInitialize<T>(Button newGameButton, Button continueButton, System.Action<T> callback)
            where T : class
        {
            // Invoke callback with null to use the default serialized values of the state data from the editor.
           
            /*newGameButton.onClick.AddListener(() =>
            {
                newGameButton.gameObject.SetActive(true);
                continueButton.gameObject.SetActive(true);
                callback(null);
            });*/

            // Hide while checking for data.          
            
            continueButton.gameObject.SetActive(false);
            
            // Check for valid state data, from server or fallback local ( PlayerPrefs )
            LOLSDK.Instance.LoadState<T>(state =>
            {
                if (state != null)
                {
                    // Hook up and show continue only if valid data exists.
                    /*continueButton.onClick.AddListener(() =>
                    {
                      //  newGameButton.gameObject.SetActive(true);
                       // continueButton.gameObject.SetActive(false);
                        callback(state.data);
                    // Broadcast saved progress back to the teacher app.
                    
                    });*/
                    
                    print("State!=null ");
                    LOLSDK.Instance.SubmitProgress(state.score, state.currentProgress, state.maximumProgress);
                    continueButton.gameObject.SetActive(true);
                    newGameButton.gameObject.SetActive(true);
                    callback(state.data);
                }
                else
                {
                    print("State!=null");
                    newGameButton.gameObject.SetActive(true);
                }
            });
        }

        private void Start()
        {
            if (SingletonSimple.Instance.totalScore > 0 || SingletonSimple.Instance.CheckCount>0)
            {
                MainMenu.SendMessage("StarttheShow");
                helloSquare.GoDown();
                Destroy(gameObject);
            }
            else
            {
                
                LOLSDK.Instance.GameIsReady();
               // print("LoLisinitisitialised:" + LOLSDK.Instance.IsInitialized + "  name: " + LOLSDK.Instance.name);
                //StateButtonInitialize<CookingData>(newGameButton, continueButton, OnLoad);
                //Helper.StateButtonInitialize<SingletonSimple>(newGameButton, continueButton, OnLoad);
            }
        }
        void Update()
        {
            timerx += Time.unscaledDeltaTime;
            if(!Loaded) TryAgain -= Time.deltaTime;
            if (TryAgain < 0f)
            {
                TryAgain = 5f;
                if (LOLSDK.Instance) print("ThereisaLOLSDK");
                else print("No LOLSDKinstance");
   
                if (triedonce)
                {
                    triedoncetry++;
                    print("secondtrytried try: " + triedoncetry);

                    //FailedtoConnect.SetActive(true);
                    // newGameButton.gameObject.SetActive(true);
                    if (newGameButton.gameObject.activeSelf == false)
                    {
                        SingletonSimple.Instance.ResetGame(); //harsh
                        MainMenu.SendMessage("StarttheShow");
                        helloSquare.GoDown();

                        Destroy(gameObject);


                    }
                }
                else triedonce = true;
                StateButtonInitialize<CookingData>(newGameButton, continueButton, OnLoad);
            }

            if (Fader > 0f)
            {
                Fader -= Time.deltaTime;
                if (Fader <= 0f) 
                { 

                    MainMenu.SendMessage("StarttheShow");
                    helloSquare.GoDown();
                    Destroy(gameObject);
                }
                else
                    FadeOut.GetComponent<Image>().color = new Color(1f, 1f, 1f, 2f * Fader);
            }



            if (!Loaded && SingletonSimple.Instance.totalScore > 0 && Fader == 0f) 
            { 
                Fader = 0.5f; 
            }

        }
        public void OnLoad(CookingData loadedPolyGonkData)
        {

                SingletonSimple.Instance.LoadGame(loadedPolyGonkData);
                Loaded = true;
                FailedtoConnect.SetActive(false);
        }

        public void continueButtonClick()
        {
            if (Loaded)
            {
                ClickFX.Play();
                Fader = 0.5f;
                continueButton.gameObject.SetActive(false);
                newGameButton.gameObject.SetActive(false);
            }
        }
        public void newGameButtonClick()
        {
            ClickFX.Play();
            Fader = 0.5f;
            SingletonSimple.Instance.ResetGame();
            continueButton.gameObject.SetActive(false);
            newGameButton.gameObject.SetActive(false);
        }

    }

}
