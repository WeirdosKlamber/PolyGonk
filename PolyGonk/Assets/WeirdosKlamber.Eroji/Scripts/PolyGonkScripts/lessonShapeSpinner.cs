using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeirdosKlamber.PolyGonk.Lesson.ShapeSpinner
{
    public class lessonShapeSpinner : MonoBehaviour
    {
        public GameObject shapeToSpin;
        public int degreesToSpin;
        private float timeSpin = 0.3f;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            timeSpin -= Time.deltaTime;
            if (timeSpin > 0f)
            {
                shapeToSpin.transform.Rotate(0f, 0f, (degreesToSpin * Time.deltaTime) / 0.3f);
            }
            else if (timeSpin < -0.3f) timeSpin = 0.3f;
            else shapeToSpin.transform.rotation = new Quaternion (0f, 0f, 0f, 0f);
        }
    }
}