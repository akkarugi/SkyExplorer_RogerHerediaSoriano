using UnityEngine;

public class RainFollowPlayer : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position;
            transform.rotation = Quaternion.identity;
        }
    }
}
