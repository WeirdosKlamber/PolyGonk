using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeirdosKlamber.PolyGonk.River;

namespace WeirdosKlamber.PolyGonk.River.Fruit
{
    public class Fruit : MonoBehaviour
    {
        public int points = 1;
        public bool isMelon = false;
        public GameObject RiverPath;
        // Start is called before the first frame update
        void Start()
        {
            if (isMelon)
            {
                GetComponent<Animator>().enabled = false;
                RiverPath.SendMessage("AddMelon", this.gameObject);
            }

        }
        private void Update()
        {
            
        }

    }
}