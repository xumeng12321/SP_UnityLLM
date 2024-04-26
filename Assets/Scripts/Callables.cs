using UnityEngine;
using System.Collections;
using RootMotion.FinalIK;

namespace Callables
{    public static class CallableMethods
    {
        private static bool pickUpStartCalled = false;
        private static bool isPickingUp = false;
        private static bool transformParentSet = false;
        /// <summary>
        /// Picks up a game object.
        /// </summary>
        /// <param name="obj">The game object to pick up.</param>
        public static void PickUpObject(GameObject obj)
        {
            UnityEngine.AI.NavMeshAgent agent= GameObject.FindObjectOfType<UnityEngine.AI.NavMeshAgent>();
            if (!pickUpStartCalled)
            {

                agent.SetDestination(obj.transform.position);

                BipedIK bipedIK;
                bipedIK = agent.gameObject.GetComponent<BipedIK>();

                bipedIK.solvers.lookAt.target = obj.transform;
                bipedIK.solvers.lookAt.SetLookAtWeight(0.3f, 0.5f, 1f, 1f, 0.5f, 0.5f, 0.5f);
                
                isPickingUp = false;
                transformParentSet = false;
                pickUpStartCalled = true;
                return;
            }

            if (!isPickingUp && Vector3.Distance(agent.transform.position, obj.transform.position) < 1.15f)
            {
                isPickingUp = true;
            
                // Start picking up the object
                PickUpObject(agent.gameObject.GetComponent<BipedIK>(),obj);
            }

            if (!transformParentSet && isPickingUp)
            {
                agent.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(SetTransformParent(agent.gameObject.GetComponent<BipedIK>(),obj,transformParentSet));
            }
        }

        private static void PickUpObject(BipedIK bipedIK, GameObject obj)
        {
            bipedIK.solvers.rightHand.target = obj.transform;
            bipedIK.solvers.pelvis.target = obj.transform;
            bipedIK.solvers.rightHand.IKPositionWeight = 1f;
            bipedIK.solvers.pelvis.positionWeight = 0.3f;
            
            Debug.Log("pickedUp");
        }

        private static IEnumerator SetTransformParent(BipedIK bipedIK, GameObject obj, bool transformParentSet)
        {
            yield return new WaitForSeconds(1f);
            bipedIK.solvers.rightHand.target = null;
            bipedIK.solvers.pelvis.target = null;
            obj.transform.parent = bipedIK.references.rightHand.transform;
            obj.transform.localPosition = new Vector3(-0.1f,0.05f,0f);
            transformParentSet = true;
        }



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