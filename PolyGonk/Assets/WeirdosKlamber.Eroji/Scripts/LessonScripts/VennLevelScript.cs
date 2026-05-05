using UnityEngine;
using TMPro;

namespace WeirdosKlamber.PolyGonk.Lesson
{
    public class VennLevelScript : MonoBehaviour
    {
        public GameObject label;
        public TextMeshProUGUI labelText;
        public GameObject[] shapes;
        public bool hasCircle = true;
        private float labelDelayTimer = 0f;
        private float labelDelayTarget = 0.3f;
        private float shapeDelayTimer = 0f;
        private float shapeDelayTarget = 0.3f;
        private int currentShape = 0;
        void Start()
        {
            if (!hasCircle)
            { 
                labelDelayTimer = 5f;
            }
        }
        void Update()
        {
            if (labelDelayTarget > -0.1f)
            {
                labelDelayTimer += Time.unscaledDeltaTime;
                if (labelDelayTimer > labelDelayTarget)
                {
                    label.SetActive(true);                   
                    labelDelayTarget = -0.5f;
                }
            }
            else
            {
                shapeDelayTimer += Time.unscaledDeltaTime;
                if (shapeDelayTimer>shapeDelayTarget) 
                {
                    if (currentShape < shapes.Length) 
                    {
                        shapes[currentShape].SetActive(true);
                        shapeDelayTimer = shapeDelayTarget / 2f;
                        currentShape++;
                    }                
                }
            }
        }

        void SkipNow()
        {
            labelDelayTimer = 100f;
            if (shapes.Length > 0)
            {
                foreach (var shape in shapes)
                {
                    shape.SetActive(true);
                }
            }
        }
    }
}