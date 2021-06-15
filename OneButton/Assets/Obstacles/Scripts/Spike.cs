using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : ObstacleBase
{
    [SerializeField]
    private int spike_damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        GameObject collision_object = collision.gameObject;
        if (collision_object.CompareTag("Player"))
        {
            collision_object.GetComponent<Health>().TakeDamage(spike_damage);

        }

        base.OnCollisionEnter(collision);
    }
}
