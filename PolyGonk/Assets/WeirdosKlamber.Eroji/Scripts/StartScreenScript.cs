
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

namespace WeirdosKlamber.PolyGonk
{
    [System.Serializable]
    public class SaveData
    {
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
        public PolyGonk.SquareHello helloSquare;
        private bool loaded = false;
        public AudioSource clickFX;
        public GameObject fadeOut;
        private float fadeTimer = 0f;
        private float tryAgainTimer = 1f;
        private bool triedOnce = false;
        public GameObject mainMenu;

        public static void StateButtonInitialize<T>(Button newGameButton, Button continueButton, System.Action<T> callback)
            where T : class
        {
            // Hide while checking for data.
            continueButton.gameObject.SetActive(false);
            
            // Check for valid state data, from server or fallback local ( PlayerPrefs )
            LOLSDK.Instance.LoadState<T>(state =>
            {
                if (state != null)
                {                    
                    LOLSDK.Instance.SubmitProgress(state.score, state.currentProgress, state.maximumProgress);
                    continueButton.gameObject.SetActive(true);
                    newGameButton.gameObject.SetActive(true);
                    callback(state.data);
                }
                else
                {
                    newGameButton.gameObject.SetActive(true);
                }
            });
        }

        private void Start()
        {
            if (SingletonSimple.Instance.totalScore > 0 || SingletonSimple.Instance.checkCount>0)
            {
                mainMenu.SendMessage("StarttheShow");
                helloSquare.GoDown();
                Destroy(gameObject);
            }
            else
            {                
                LOLSDK.Instance.GameIsReady();
            }
        }
        void Update()
        {
            if(!loaded) tryAgainTimer -= Time.deltaTime;
            if (tryAgainTimer < 0f)
            {
                tryAgainTimer = 5f;
   
                if (triedOnce)
                {
                    if (newGameButton.gameObject.activeSelf == false)
                    {
                        SingletonSimple.Instance.ResetGame(); 
                        mainMenu.SendMessage("StarttheShow");
                        helloSquare.GoDown();
                        Destroy(gameObject);
                    }
                }
                else triedOnce = true;
                StateButtonInitialize<SaveData>(newGameButton, continueButton, OnLoad);
            }

            if (fadeTimer > 0f)
            {
                fadeTimer -= Time.deltaTime;
                if (fadeTimer <= 0f) 
                { 

                    mainMenu.SendMessage("StarttheShow");
                    helloSquare.GoDown();
                    Destroy(gameObject);
                }
                else
                    fadeOut.GetComponent<Image>().color = new Color(1f, 1f, 1f, 2f * fadeTimer);
            }

            if (!loaded && SingletonSimple.Instance.totalScore > 0 && fadeTimer == 0f) 
            { 
                fadeTimer = 0.5f; 
            }
        }

        public void OnLoad(SaveData loadedPolyGonkData)
        {
            SingletonSimple.Instance.LoadGame(loadedPolyGonkData);
            loaded = true;
        }

        public void continueButtonClick()
        {
            if (loaded)
            {
                clickFX.Play();
                fadeTimer = 0.5f;
                continueButton.gameObject.SetActive(false);
                newGameButton.gameObject.SetActive(false);
            }
        }

        public void newGameButtonClick()
        {
            clickFX.Play();
            fadeTimer = 0.5f;
            SingletonSimple.Instance.ResetGame();
            continueButton.gameObject.SetActive(false);
            newGameButton.gameObject.SetActive(false);
        }
    }
}
