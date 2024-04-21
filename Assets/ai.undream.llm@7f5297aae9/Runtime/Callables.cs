using UnityEngine;
using System.Collections;

namespace Callables
{    public static class CallableMethods
    {
        /// <summary>
        /// Performs a jump on the specified game object.
        /// </summary>
        /// <param name="gameObject">The game object to perform the jump on.</param>
        /// <param name="jumpHeight">The height of the jump.</param>
        
        public static void PerformJump(GameObject gameObject, float jumpHeight)
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();

            // If the object doesn't have a Rigidbody, add one
            if (rb == null)
                rb = gameObject.AddComponent<Rigidbody>();

            // Calculate jump velocity
            float jumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);

            // Apply jump velocity
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
        }


        /// <summary>
        /// Rotates the specified game object around the given rotation axis at the specified rotation speed.
        /// </summary>
        /// <param name="gameObject">The game object to rotate.</param>
        /// <param name="rotationAxis">The axis to rotate the game object around.</param>
        /// <param name="rotationSpeed">The speed at which to rotate the game object.</param>
        public static void RotateObject(GameObject gameObject, Vector3 rotationAxis, float rotationSpeed)
        {
            // Start the coroutine to rotate the object around the specified axis
            gameObject.GetComponent<MonoBehaviour>().StartCoroutine(RotateCoroutine(gameObject, rotationAxis, rotationSpeed));
        }

        private static IEnumerator RotateCoroutine(GameObject gameObject, Vector3 rotationAxis, float rotationSpeed)
        {
            while (true)
            {
                // Rotate the object around the specified axis
                gameObject.transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);

                // Wait for the next frame
                yield return null;
            }
        }


        /// <summary>
        /// Changes the color of a GameObject's Renderer material.
        /// </summary>
        /// <param name="gameObject">The GameObject to change the color of.</param>
        /// <param name="color">The new color to apply.</param>
        public static void ChangeColor(GameObject gameObject,Color color)
        {
            gameObject.GetComponent<Renderer>().material.color = color;
        }
    }

}