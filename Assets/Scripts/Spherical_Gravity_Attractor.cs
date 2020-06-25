using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spherical_Gravity_Attractor : MonoBehaviour
{
    [SerializeField] private float strength;
    //
    private Rigidbody own_rb;
    private float mass;
    [SerializeField] private List<Rigidbody> physics_objects = new List<Rigidbody>();
    // Start is called before the first frame update
    void Start()
    {
        own_rb = GetComponent<Rigidbody>();
        if (own_rb != null)
        {
            mass = own_rb.mass;
        }
        else
        {
            mass = 999999.0f;
        }

        Rigidbody[] temp_objects = FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[];

        foreach (var obj in temp_objects)
        {
            if (obj != own_rb)
            {
                physics_objects.Add(obj);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var rb in physics_objects)
        {
            //forceSun = G x(massPlanet x massSun) / d ^ 2;

            float gravity_force = (1.0f * (rb.mass * mass) / (Mathf.Pow(Vector3.Distance(transform.position, rb.position), 2)));
            Vector3 gravity_direction = Vector3.Normalize(transform.position - rb.position);

            rb.AddForce(gravity_direction * gravity_force);
        }
    }
}
