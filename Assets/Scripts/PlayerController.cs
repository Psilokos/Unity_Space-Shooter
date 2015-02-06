using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin;
    public float xMax;
    public float zMin;
    public float zMax;
}

public class PlayerController : MonoBehaviour
{
    public float    speed;
    public float    tilt;
    public Boundary boundary;

    void FixedUpdate()
    {
        float   h_move      = Input.GetAxis("Horizontal");
        float   v_move      = Input.GetAxis("Vertical");
        Vector3 movement    = new Vector3(h_move, 0.0f, v_move);

        this.rigidbody.velocity = movement * this.speed;
        this.rigidbody.position = new Vector3
        (
            Mathf.Clamp(this.rigidbody.position.x, this.boundary.xMin, this.boundary.xMax),
            0.0f,
            Mathf.Clamp(this.rigidbody.position.z, this.boundary.zMin, this.boundary.zMax)
        );
        this.rigidbody.rotation = Quaternion.Euler(this.rigidbody.velocity.z * this.tilt / 1.5f, 0.0f, this.rigidbody.velocity.x * -this.tilt);
    }
}
