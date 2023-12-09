using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace WeirdosKlamber.PolyGonk.River
{
    public class BangUp : MonoBehaviour
    {
        public GameObject scoreobj;
        public GameObject currentscoreobj;
        // Start is called before the first frame update
        void Start()
        {


        }

        // Update is called once per frame
        void Update()
        {

        }

        void BangUpSpawn(int scoreInt)
        {
            currentscoreobj = Instantiate(scoreobj, transform);
            currentscoreobj.SendMessage("BangUpSet", scoreInt);


        }
    }
}