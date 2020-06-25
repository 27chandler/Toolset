using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spherical_Gravity_Attractor : MonoBehaviour
{
    [SerializeField] private float strength;
    [SerializeField] private List<Rigidbody> physics_objects = new List<Rigidbody>();
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody[] temp_objects = FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[];

        foreach (var obj in temp_objects)
        {
            physics_objects.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var rb in physics_objects)
        {
            float gravity_force = (1.0f / Vector3.Distance(transform.position,rb.position)) * Time.deltaTime * strength;
            Vector3 gravity_direction = Vector3.Normalize(transform.position - rb.position);

            rb.AddForce(gravity_direction * gravity_force);
        }
    }
}
