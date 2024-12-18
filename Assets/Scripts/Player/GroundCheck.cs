using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.5f) IsGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        IsGrounded = false;
    }
}
