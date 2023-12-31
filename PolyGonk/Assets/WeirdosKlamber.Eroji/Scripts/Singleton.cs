using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;
using System;

namespace WeirdosKlamber.PolyGonk
{
    [System.Serializable]
    public sealed class SingletonSimple : MonoBehaviour
    {
        private static SingletonSimple _Instance;
        public static SingletonSimple Instance
        {
            get
            {
                if (!_Instance)
                {
                    _Instance = new GameObject().AddComponent<SingletonSimple>();
                    // name it for easy recognition
                    _Instance.name = _Instance.GetType().ToString();

                    // mark root as DontDestroyOnLoad();
                    DontDestroyOnLoad(_Instance.gameObject);
                }
                return _Instance;
            }
        }
        public int totalScore = -1;
        public int[] levelScores = new int[7];

        public bool initiated = false;
        public string lastLevel = "";

        public bool lesson1Completed = false;
        public bool level1Completed = false;
        public bool lesson2Completed = false;
        public bool level2Completed = false;
        public bool lesson3Completed = false;
        public bool level3Completed = false;
        public bool lesson4Completed = false;
        public bool level4Completed = false;
        public bool lesson5Completed = false;
        public bool level5Completed = false;
        public bool lesson6Completed = false;
        public bool level6Completed = false;
        public bool lesson7Completed = false;
        public bool level7Completed = false;
        public int checkCount = 0;
        private bool speaking = false;
        private float speakwaiter = 1f;

        private string[] TTSKeysArray = new string[10];
        private float[] TTSKeysWaitArray = new float[16];
        private int keysIterator = 0;

        public bool[] checkpoints = new bool[14];

        public int[] levels3starThresholds= new int[7] { 15, 21, 26, 32, 36, 30, 50 };
        public int[] levels2starThresholds = new int[7] { 13, 18, 21, 28, 33, 25, 40 };
        public int[] levels1starThresholds = new int[7] { 11, 16, 15, 24, 30, 20, 20 };
        public bool[] levelsPerfectArray= new bool[7];

        public int[] levelsAttempts = new int[7];

        public bool isLOL = false;
        private void Awake()
        {
            print("hello singleton");
            populateCheckpointsArray();
        }

        private void Update()
        {
            if(speaking)
            {
                speakwaiter -= Time.unscaledDeltaTime;
                if (speakwaiter < 0f)
                {
                    print("KeysIterator:" + keysIterator.ToString() + " ttskeysarraylength: " + TTSKeysArray.Length.ToString());
                    if (keysIterator < TTSKeysArray.Length && TTSKeysArray[keysIterator] != null && TTSKeysArray[keysIterator] != "")
                    {
                        LOLSDK.Instance.SpeakText(TTSKeysArray[keysIterator]);
                        speakwaiter = TTSKeysWaitArray[keysIterator];
                        keysIterator++;
                    }
                    else
                    {
                        keysIterator++;
                    }

                    if (keysIterator>10)
                    {
                        speaking = false;
                        keysIterator = 0;
                        speakwaiter = 0f;
                    }               
                }
            }            
        }

        private void populateCheckpointsArray()
        {
            checkpoints[0] = lesson1Completed;
            checkpoints[1] = level1Completed;
            checkpoints[2] = lesson2Completed;
            checkpoints[3] = level2Completed;
            checkpoints[4] = lesson3Completed;
            checkpoints[5] = level3Completed;
            checkpoints[6] = lesson4Completed;
            checkpoints[7] = level4Completed;
            checkpoints[8] = lesson5Completed;
            checkpoints[9] = level5Completed;
            checkpoints[10] = lesson6Completed;
            checkpoints[11] = level6Completed;
            checkpoints[12] = lesson7Completed;
            checkpoints[13] = level7Completed;            
        }

        public void SaveGame()
        {
            populateCheckpointsArray();
            totalScore= 0;
            SumScores();
            countCheckpoints();
            if (isLOL)
            {
                LOLSDK.Instance.SubmitProgress(totalScore, checkCount, 14);
            }

            SaveData SaveMe = new SaveData
            {
                Checkpoint1lesson1 = lesson1Completed,
                Checkpoint2level1 = level1Completed,
                Checkpoint3lesson2 = lesson2Completed,
                Checkpoint4level2 = level2Completed,
                Checkpoint5lesson3 = lesson3Completed,
                Checkpoint6level3 = level3Completed,
                Checkpoint7lesson4 = lesson4Completed,
                Checkpoint8level4 = level4Completed,
                Checkpoint9lesson5 = lesson5Completed,
                Checkpoint10level5 = level5Completed,
                Checkpoint11lesson6 = lesson6Completed,
                Checkpoint12level6 = level6Completed,
                Checkpoint13lesson7 = lesson7Completed,
                Checkpoint14level7 = level7Completed,

                SaveTotalScore = totalScore,
                SaveLevelScores = levelScores,
                SaveLevelsAttempts = levelsAttempts,
                SaveLevelsPerfectArray = levelsPerfectArray,
            };
            if (isLOL)
            {
                LOLSDK.Instance.SaveState<SaveData>(SaveMe);
            }
        }

        public void LoadGame(SaveData LoadData)
        {
            lesson1Completed = LoadData.Checkpoint1lesson1;
            level1Completed = LoadData.Checkpoint2level1;
            lesson2Completed = LoadData.Checkpoint3lesson2;
            level2Completed = LoadData.Checkpoint4level2;
            lesson3Completed = LoadData.Checkpoint5lesson3;
            level3Completed = LoadData.Checkpoint6level3;
            lesson4Completed = LoadData.Checkpoint7lesson4;
            level4Completed = LoadData.Checkpoint8level4;
            lesson5Completed = LoadData.Checkpoint9lesson5;
            level5Completed = LoadData.Checkpoint10level5;
            lesson6Completed = LoadData.Checkpoint11lesson6;
            level6Completed = LoadData.Checkpoint12level6;
            lesson7Completed = LoadData.Checkpoint13lesson7;
            level7Completed = LoadData.Checkpoint14level7;

            totalScore = LoadData.SaveTotalScore;
            levelScores = LoadData.SaveLevelScores;
            levelsAttempts = LoadData.SaveLevelsAttempts;
            levelsPerfectArray = LoadData.SaveLevelsPerfectArray;
            countCheckpoints();
        }

        public void ResetGame()
        {
            totalScore = 0;        
            lastLevel = "";

            lesson1Completed = false;
            level1Completed = false;
            lesson2Completed = false;
            level2Completed = false;
            lesson3Completed = false;
            level3Completed = false;
            lesson4Completed = false;
            level4Completed = false;
            lesson5Completed = false;
            level5Completed = false;
            lesson6Completed = false;
            level6Completed = false;
            lesson7Completed = false;
            level7Completed = false;
            checkCount = 0;

            levelsPerfectArray = new bool[7];
            levelScores = new int[7];
            levelsAttempts = new int[7];
        }

        public void ClearText()
        {
            Array.Clear(TTSKeysArray, 0, 10);
            keysIterator = 0;
            speaking = false;
        }

        public void TTSAddText(string langKey,float langtime)
        {
            for (int i = 0; i < TTSKeysArray.Length; i++)
            {
                if (TTSKeysArray[i] == null|| TTSKeysArray[i] == "")
                {
                    TTSKeysArray[i] = langKey;
                    TTSKeysWaitArray[i] = langtime;
                    break;
                }
            }
        }

        public int SumScores()
        {
            totalScore = 0;
            foreach (var score in levelScores)
            {
                totalScore += score;
            }
            return totalScore;
        }

        public int countCheckpoints()
        {
            populateCheckpointsArray();
            checkCount = 0;
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (checkpoints[i] == true) checkCount++;
            }
            return checkCount;
        }

        public void Speak()
        {
            keysIterator = 0;
            speakwaiter = 0f;
            speaking = true;
        }

        public void LessonCompleted(int lessonN)
        {
            switch (lessonN) 
            {
                case 0:
                    break; 
                case 1:
                    lesson1Completed= true; break;
                case 2:
                lesson2Completed= true; break;
                case 3:
                lesson3Completed= true; break;
                case 4:
                lesson4Completed= true; break;
                case 5:
                lesson5Completed= true; break;
                case 6:
                lesson6Completed= true; break;
                case 7:
                lesson7Completed= true; break;
            }
            countCheckpoints();
            if (isLOL)
            {
                SaveGame();
            }
        }
        public void LevelCompleted(int levelN, int levelScore, bool perfecto)
        {
            if (levelScore > levelScores[levelN - 1])
            {
                levelScores[levelN - 1] = levelScore;
            }
            if (perfecto)
            {
                levelsPerfectArray[levelN - 1] = true;
            }
            levelsAttempts[levelN - 1] ++;

            switch (levelN)
            {               
                case 0:
                    break;
                case 1:
                    level1Completed = true; break;
                case 2:
                    level2Completed = true; break;
                case 3:
                    level3Completed = true; break;
                case 4:
                    level4Completed = true; break;
                case 5:
                    level5Completed = true; break;
                case 6:
                    level6Completed = true; break;
                case 7:
                    level7Completed = true; break;
            }
            countCheckpoints();
            if (isLOL)
            {
                SaveGame();
            }
        }
    }
}
