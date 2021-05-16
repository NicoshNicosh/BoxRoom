using UnityEngine;

public class FollowUser : MonoBehaviour
{
    public Rigidbody Rigidbody3D;
    public Rigidbody2D Rigidbody2D;
    public Transform Target;

    public float Force = 1;

    // Update is called once per frame
    void Update()
    {
        if (Rigidbody2D) { Rigidbody2D.AddForce((((Vector2)Target.position) - Rigidbody2D.position) * Force); }
        if (Rigidbody3D) { Rigidbody3D.AddForce((Target.position - Rigidbody3D.position) * Force); }
    }
}
