using UnityEngine;

namespace WeirdosKlamber.PolyGonk
{
    public class Cloudscript : MonoBehaviour
    {
        public float cloudSpeed = 1.0f;
        public float minX = 0f;
        public float maxX = 10f;
        private float currentX = 0f;
        public float minY = 0f;
        public float maxY = 10f;
        private float currentY = 0f;

        void Start()
        {
            currentX = transform.localPosition.x;
            currentY = transform.localPosition.y;
        }

        void Update()
        {
            currentX -= Time.deltaTime * cloudSpeed;
            transform.localPosition = new Vector2(currentX, currentY);

            if (currentX < minX)
            {
                currentX = maxX;
                currentY = UnityEngine.Random.Range(minY, maxY);
            }
        }
    }
}