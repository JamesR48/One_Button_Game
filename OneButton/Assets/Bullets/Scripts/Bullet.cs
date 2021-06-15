using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public int shooter_id;

    [SerializeField]
    private float bullet_force = 0.25f;
    private int bullet_damage = 1;

    Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bullet_force;
    }

    private void FixedUpdate()
    {
        //rb.AddForce(transform.forward * bullet_force, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = transform.forward * bullet_force * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject collision_object = collision.gameObject;
        if (collision_object.CompareTag("Player"))
        {
            if(collision_object.GetComponent<PlayerBase>().player_id == shooter_id)
            {
                return;
            }
            else
            {
                collision_object.GetComponent<Health>().TakeDamage(bullet_damage);
            }
        }

        this.gameObject.SetActive(false);
    }
}
