using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    public float origin_height;

    [SerializeField]
    protected float target_height;
    [SerializeField]
    protected float move_speed;

    protected Vector3 initial_position;
    protected Vector3 target_position;

    protected virtual void OnEnable()
    {
        //this.transform.up += (Vector3.up * origin_height);
        initial_position = this.transform.position;
        target_position = new Vector3(initial_position.x, target_height, initial_position.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (this.gameObject.activeInHierarchy)
        {
            if(this.transform.position.y != target_height)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, target_position, move_speed*Time.deltaTime);
            }
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        this.gameObject.SetActive(false);
    }
}
