using UnityEngine;

namespace SickscoreGames.ExampleScene
{
    public class ExampleRotatePrism : MonoBehaviour
    {
        #region Variables
        [Range(0f, 100f)]
        public float rotationSpeed = 75f;
        public Vector3 position;
        #endregion


        #region Main Methods
        void Update()
        {
            // rotate prism
            if (rotationSpeed > 0f)
                transform.Rotate(position.x * rotationSpeed * Time.deltaTime, position.y * rotationSpeed * Time.deltaTime, position.z * rotationSpeed * Time.deltaTime);
        }
        #endregion
    }
}
