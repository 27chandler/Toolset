using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gyroscope : MonoBehaviour
{
    [SerializeField] private Vector3 face_direction;
    [SerializeField] private Vector3 offset;
    private Rigidbody rb;

    private Vector3 gravity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Add_Gravity(Vector3 i_direction)
    {
        gravity += i_direction;
    }

    // Update is called once per frame
    void Update()
    {
        face_direction = gravity;
        Vector3 rotate_dir = transform.up - face_direction;

        //rb.MoveRotation(Quaternion.LookRotation(face_direction));

        Quaternion to_rotation = Quaternion.FromToRotation(-transform.up, face_direction) * transform.rotation;
        transform.rotation = to_rotation;

        //Reset gravity direction
        gravity = Vector3.zero;
    }
}
