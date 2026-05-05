using System.Collections;
using System.IO;
using LoLSDK;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WeirdosKlamber.PolyGonk
{
    [System.Serializable]

    public class PolyGonkScript : MonoBehaviour
    {

        [SerializeField] Button continueButton, newGameButton;
        [SerializeField] TextMeshProUGUI feedbackText, newGameText, continueText;

        bool _init;
        WaitForSeconds _feedbackTimer = new WaitForSeconds(2);
        Coroutine _feedbackMethod;
        static JSONNode _langNode;
        string _langCode = "en";


        void Start()
        {

            if (SingletonSimple.Instance.initiated == false)
            {
                SingletonSimple.Instance.initiated = true;
                // Create the WebGL (or mock) object
                // This will all change in SDK V6 to be simplified and streamlined.
#if UNITY_EDITOR
                ILOLSDK sdk = new LoLSDK.MockWebGL();
#elif UNITY_WEBGL
            ILOLSDK sdk = new LoLSDK.WebGL();
#else 
            ILOLSDK sdk = null;
#endif

                LOLSDK.Init(sdk, "com.legends-of-learning.unity.sdk.v5.3.Weirdos_Klamber");
               
                // Register event handlers
                LOLSDK.Instance.StartGameReceived += new StartGameReceivedHandler(StartGame);
                LOLSDK.Instance.GameStateChanged += new GameStateChangedHandler(gameState => Debug.Log(gameState));
                LOLSDK.Instance.QuestionsReceived += new QuestionListReceivedHandler(questionList => Debug.Log(questionList));
                LOLSDK.Instance.LanguageDefsReceived += new LanguageDefsReceivedHandler(LanguageUpdate);

                // Used for player feedback. Not required by SDK.
                LOLSDK.Instance.SaveResultReceived += OnSaveResult;

                // Call GameIsReady before calling LoadState or using the helper method.
                LOLSDK.Instance.GameIsReady();

#if UNITY_EDITOR
                UnityEditor.EditorGUIUtility.PingObject(this);
                LoadMockData();
#endif
            }
        }
        private void OnDestroy()
        {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
                return;
#endif
            LOLSDK.Instance.SaveResultReceived -= OnSaveResult;
        }

        void OnSaveResult(bool success)
        {
            if (!success)
            {
                return;
            }

            if (_feedbackMethod != null)
                StopCoroutine(_feedbackMethod);
            // ...Auto Saving Complete
            _feedbackMethod = StartCoroutine(_Feedback(GetText("autoSave")));
        }

        void StartGame(string startGameJSON)
        {
            if (string.IsNullOrEmpty(startGameJSON))
            {
                return;
            }

            JSONNode startGamePayload = JSON.Parse(startGameJSON);
            // Capture the language code from the start payload. Use this to switch fonts
            _langCode = startGamePayload["languageCode"];
        }

        void LanguageUpdate(string langJSON)
        {
            if (string.IsNullOrEmpty(langJSON))
            {
                return;
            }
            _langNode = JSON.Parse(langJSON);
            print("language update");
            TextDisplayUpdate();
        }


        public static bool IsThereText(string key)
        {
            return _langNode?[key]; 
        }

        public static string GetText(string key)
        {
            print("get text: " + key);
            string valueX = _langNode?[key];

            if (valueX != null)
            {
                print("successful, valueX: " + valueX);
                return valueX;
            }

            //added in a backup dictionary for testing/Itch           
            else if (EnglishLang.englishDictionary.ContainsKey(key))
            {
                return EnglishLang.englishDictionary[key]; 
            }

            else
            {
                print("abort");
                return "-" + key;
            }
        }
        IEnumerator _Feedback(string text)
        {
            feedbackText.text = text;
            yield return _feedbackTimer;
            feedbackText.text = string.Empty;
            _feedbackMethod = null;
        }
        // This could be done in a component with a listener attached to an lang change
        // instead of coupling all the text to a controller class.
        void TextDisplayUpdate()
        {
            newGameText.text = GetText("newGame");
            continueText.text = GetText("continue");
        }

        /// <summary>
        /// This is the setting of your initial state when the scene loads.
        /// The state can be set from your default editor settings or from the
        /// users saved data after a valid save is called.
        /// </summary>
        /// <param name="loadedPolyGonkData"></param>      

#if UNITY_EDITOR
        // Loading Mock Gameframe data
        // This will all be changed and streamlined in SDK V6
        private void LoadMockData()
        {
            // Load Dev Language File from StreamingAssets

            string startDataFilePath = Path.Combine(Application.streamingAssetsPath, "startGame.json");

            if (File.Exists(startDataFilePath))
            {
                string startDataAsJSON = File.ReadAllText(startDataFilePath);
                StartGame(startDataAsJSON);
            }

            // Load Dev Language File from StreamingAssets
            string langFilePath = Path.Combine(Application.streamingAssetsPath, "language.json");
            if (File.Exists(langFilePath))
            {
                string langDataAsJson = File.ReadAllText(langFilePath);
                var lang = JSON.Parse(langDataAsJson)[_langCode];
                LanguageUpdate(lang.ToString());
            }
        }
#endif

    }
}
