using UnityEngine;

namespace WeirdosKlamber.PolyGonk
{
    public class PolyGonkFace : MonoBehaviour
    {
        public GameObject faceFace;
        public GameObject mainCamera;
        public GameObject mainMenu;
        public AudioSource chompSFX;
        public AudioSource dingFX;
        public GameObject blackout;
        public GameObject polygonkTitleObject;
        public GameObject polyGonkTitleTextObject;

        private bool growFace = false;
        public GameObject leftEye;
        public GameObject rightEye;
        private bool rotateOnce = false;
        public GameObject buttons;
        public PolyGonk.MainMenu.LevelButton[] buttonArray;
        public GameObject mouth;
        private bool eating = false;

        private float camTimer = 2f;
        private bool chomped = false;

        public GameObject button7Holder;
        private bool growButton7 = false;
        private float button7GrowTimer = 0f;

        public GameObject replayInfo;
        private bool showedReplay = false;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (growFace)
            {
                if (transform.localPosition.x - 150 * Time.deltaTime > -405f)
                {
                    transform.localPosition = new Vector2(transform.localPosition.x - 150 * Time.deltaTime, transform.localPosition.y);
                    polyGonkTitleTextObject.transform.localPosition = new Vector2(polyGonkTitleTextObject.transform.localPosition.x - 150 * Time.deltaTime, polyGonkTitleTextObject.transform.localPosition.y);

                }
                else
                {
                    transform.localPosition = new Vector2(-405f, -11f);
                }

                if (!rotateOnce)
                {
                    transform.Rotate(new Vector3(0f, 0f, 480f * Time.unscaledDeltaTime));
                    
                }
                transform.localScale += Vector3.one*Time.unscaledDeltaTime;
                if (transform.localScale.x>1f)
                {
                    transform.localScale = Vector3.one;
                    growFace= false;
                    leftEye.SetActive(true);
                    rightEye.SetActive(true);
                    transform.rotation = new Quaternion( 0f, 0f, 0f,0f);
                    for (int i = 0; i < buttonArray.Length; i++)
                    {
                        if ( i==0 || (i<6 && SingletonSimple.Instance.levelScores[i] > 0))
                        {
                            if (SingletonSimple.Instance.levelsAttempts[i]<2) { buttonArray[i].ButtonEnable(); }
                            else { buttonArray[i].ButtonDisable(); }
                            if (SingletonSimple.Instance.levelScores[i] >= SingletonSimple.Instance.levels3starThresholds[i] || SingletonSimple.Instance.levelsPerfectArray[i]==true) { buttonArray[i].StarsWon(3); buttonArray[i + 1].ButtonEnable(); }
                            else if (SingletonSimple.Instance.levelScores[i] >= SingletonSimple.Instance.levels2starThresholds[i]) { buttonArray[i].StarsWon(2); buttonArray[i + 1].ButtonEnable(); }
                            else if (SingletonSimple.Instance.levelScores[i] >= SingletonSimple.Instance.levels1starThresholds[i]) { buttonArray[i].StarsWon(1); buttonArray[i + 1].ButtonEnable(); }
                            else { buttonArray[i].ButtonEnable(); }

                        }
                        if (i == 6)//meaning 7
                        {
                            if (SingletonSimple.Instance.levelScores[i-1] >= SingletonSimple.Instance.levels1starThresholds[i-1] && SingletonSimple.Instance.levelsAttempts[i] < 1) 
                            { 
                                growButton7= true;
 
                                mouth.SetActive(true) ;
                            }
                            else { buttonArray[i].ButtonDisable(); }
                            if (SingletonSimple.Instance.levelScores[i] >= SingletonSimple.Instance.levels3starThresholds[i] || SingletonSimple.Instance.levelsPerfectArray[i] == true) { buttonArray[i].StarsWon(3); }
                            else if (SingletonSimple.Instance.levelScores[i] >= SingletonSimple.Instance.levels2starThresholds[i]) { buttonArray[i].StarsWon(2); }
                            else if (SingletonSimple.Instance.levelScores[i] >= SingletonSimple.Instance.levels1starThresholds[i]) { buttonArray[i].StarsWon(1); }
                        }
                    }

                    buttons.SetActive(true);
                }

                if (polyGonkTitleTextObject.activeSelf && polyGonkTitleTextObject.transform.localScale.x > 0.3f)
                {
                    polyGonkTitleTextObject.transform.localScale -= Vector3.one * Time.unscaledDeltaTime;
                }
                else
                {
                    polyGonkTitleTextObject.SetActive(false);
                }

            }           

            else if(buttons.activeSelf && !showedReplay && SingletonSimple.Instance.level1Completed && !SingletonSimple.Instance.lesson2Completed)
            {
                replayInfo.SetActive(true);
                showedReplay= true;
            }

            else if (growButton7)
            {
                button7GrowTimer += Time.deltaTime;
                if (button7GrowTimer>=1f)
                {
                    growButton7 = false;
                    dingFX.Play();
                    buttonArray[6].gameObject.SetActive(true);
                    buttonArray[6].ButtonEnable();
                }
            }

            if (eating)
            {
                camTimer-=Time.deltaTime;
                polygonkTitleObject.transform.localScale = new Vector2(1f + (2f-camTimer)*3.5f, 1f + (2f - camTimer) * 3.5f);

                if (camTimer >= 0f)
                {
                    polygonkTitleObject.transform.localPosition = new Vector2(0f, 0f + (2f - camTimer) * 350f);
                }

                if (camTimer >= 1f)
                {
                    mouth.transform.localScale = new Vector2(130f - (camTimer-1f) * 130f, 130f - (camTimer - 1f) * 130f);
                }
                else if (camTimer>0f)
                {
                    mouth.transform.localScale = new Vector2(50f + camTimer * 80f, 50f + camTimer * 80f);
                }

                if (camTimer<1f&&camTimer>-1f)
                {
                    if (!chomped) 
                    { 
                        chompSFX.PlayOneShot(chompSFX.clip);
                        chomped= true; 
                    }
                    blackout.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f + (1f - camTimer) / 2f);
                }
                if (camTimer< -2f)
                {
                    mainMenu.SendMessage("LevelButtonPress", 7);
                    eating = false;
                }
            }

        }
        void WakeUp ()
        {
            growFace= true;
        }

        void Eat()
        {
            eating= true;
        }
    }
}