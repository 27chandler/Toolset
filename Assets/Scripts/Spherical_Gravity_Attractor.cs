using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spherical_Gravity_Attractor : MonoBehaviour
{
    [SerializeField] private float strength;
    //
    private Rigidbody own_rb;
    private float mass;
    [SerializeField] private List<Physics_Objects> physics_objects = new List<Physics_Objects>();

    private List<Vector3> gravity_sources = new List<Vector3>();

    public class Physics_Objects
    {
        public Rigidbody rb;
        public Gyroscope gyro;
    }
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
                Physics_Objects new_obj = new Physics_Objects();
                new_obj.rb = obj;

                new_obj.gyro = obj.gameObject.GetComponent<Gyroscope>();

                physics_objects.Add(new_obj);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        gravity_sources.Clear();
        foreach (var physics_object in physics_objects)
        {
            //forceSun = G x(massPlanet x massSun) / d ^ 2;

            float gravity_force = (1.0f * (physics_object.rb.mass * mass) / (Mathf.Pow(Vector3.Distance(transform.position, physics_object.rb.position), 2)));
            Vector3 gravity_direction = Vector3.Normalize(transform.position - physics_object.rb.position);

            physics_object.rb.AddForce(gravity_direction * gravity_force);
            gravity_sources.Add(gravity_direction * gravity_force);

            if (physics_object.gyro != null)
            {
                physics_object.gyro.Add_Gravity(gravity_direction * gravity_force);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        foreach (var gravity in gravity_sources)
        {
            Gizmos.DrawLine(transform.position, transform.position + gravity);
        }
    }
}
