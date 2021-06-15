using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float rotation_speed = 5f;
    public float impulse_force = 15f;
    public GameObject turret = null;
    public GameObject target;
    public KeyCode key;
    public GameObject bullet_prefab;

    [SerializeField]
    bool on_wall = false;
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        if (!on_wall)
        {
            SeekTarget();
        }

        Shoot();

    }

    void Move()
    {

        //if (Input.GetKeyDown(key))
        if (Keyboard.current.FindKeyOnCurrentKeyboardLayout(key.ToString()).wasPressedThisFrame)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            
            if (turret != null)
            {
                rb.AddForce((-turret.transform.forward) * impulse_force, ForceMode.Impulse);
            }
            else
            {
                rb.AddForce((-transform.forward) * impulse_force, ForceMode.Impulse);
            }
            on_wall = false;
        }
    }

    void SeekTarget()
    {
        if(turret != null)
        {
            //Vector3 relativePos = target.transform.position - turret.transform.position;
            //Quaternion toRotation = Quaternion.LookRotation(relativePos);
            //turret.transform.rotation = Quaternion.Lerp(turret.transform.rotation, toRotation, rotation_speed * Time.deltaTime);
            turret.transform.LookAt(new Vector3(target.transform.position.x, 0f, target.transform.position.z));
        }
        else
        {
            //Vector3 relativePos = target.transform.position - transform.position;
            //Quaternion toRotation = Quaternion.LookRotation(relativePos);
            //transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotation_speed * Time.deltaTime);
            transform.LookAt(new Vector3(target.transform.position.x, 0f, target.transform.position.z));
        }
    }

    void Shoot()
    {
        //if(!on_wall && Input.GetKeyDown(key))
        if (!on_wall && Keyboard.current.FindKeyOnCurrentKeyboardLayout(key.ToString()).wasPressedThisFrame)
        {
            GameObject bullet_instance;

            if (turret != null)
            {
                //bullet_instance = Instantiate(bullet_prefab, transform.position + transform.forward * 2f, Quaternion.LookRotation(transform.forward));
                bullet_instance = Instantiate(bullet_prefab, turret.transform.position + turret.transform.forward * 2f, Quaternion.LookRotation(turret.transform.forward));
            }
            else
            {
                bullet_instance = Instantiate(bullet_prefab, (transform.position+Vector3.up*1.25f) + transform.forward * 2f, Quaternion.LookRotation(transform.forward));
            }

            bullet_instance.GetComponent<MeshRenderer>().material = this.GetComponent<MeshRenderer>().material;

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bound"))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep();

            if (turret != null)
            {
                transform.rotation = Quaternion.LookRotation(-collision.GetContact(0).normal);
                turret.transform.rotation = Quaternion.LookRotation(-collision.GetContact(0).normal);
            }
            else
            {
                transform.rotation = Quaternion.LookRotation(-collision.GetContact(0).normal);
            }
            on_wall = true;
        }
    }

    //void OnDrawGizmos()
    //{
    //    Debug.DrawLine(transform.position, transform.position + transform.forward * 3f, Color.red);
    //}
}
