using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeirdosKlamber.PolyGonk.Glacier;

namespace WeirdosKlamber.PolyGonk.Glacier.FlowCollider {

    public class FlowColliderScript : MonoBehaviour
    {
        public string floPosition;
        public GameObject Hexbase;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            
            if (collision.gameObject.tag != "Banana" && Glacier.Arrow.ArrowScript.arrowPressed == true)
            {
                print("Bang");
                Glacier.Arrow.ArrowScript.arrowPressed = false;
                Hexbase.SendMessage("collided", floPosition);
            }
        }

    

    }

    
}