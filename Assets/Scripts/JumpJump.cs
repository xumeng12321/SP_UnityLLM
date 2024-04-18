using System.Collections;

using UnityEngine;

public class JumpJump : MonoBehaviour
{
    public float jumpHeight = 1f; // Height of the jump
    public float jumpInterval = 1f; // Time interval between jumps

    private Rigidbody rb;

    void Start()
    {
        if(this.gameObject.GetComponent<Rigidbody>() != null)
        {

        }
        else
        {
            this.gameObject.AddComponent<Rigidbody>();
        }
        rb = GetComponent<Rigidbody>();
        StartCoroutine(JumpCoroutine());
    }

    IEnumerator JumpCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(jumpInterval);
            Jumping();
        }
    }

    void Jumping()
    {
        
        float jumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);

        
        rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
    }
}