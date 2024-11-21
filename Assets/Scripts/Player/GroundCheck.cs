using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private bool isGrounded = true;

    public bool IsGrounded => isGrounded;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
