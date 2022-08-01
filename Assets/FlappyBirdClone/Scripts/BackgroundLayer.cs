using UnityEngine;

namespace FlappyBirdClone.Scripts
{
    public class BackgroundLayer : MonoBehaviour
    {
        private float xOffset = 10.5f;
        private int numSections = 6;
        private Vector3 originalPosition;
    
        // Start is called before the first frame update
        private void Start()
        {
            originalPosition = transform.position;
        }

        // Update is called once per frame
        private void Update()
        {
            var position = transform.position;
            if (position.x <= -GameEvents.Current.ScreenBounds.x - (xOffset * 2f))
            {
                transform.position = new Vector3(position.x + (xOffset * numSections), position.y, position.z);
            }
        }
    }
}
