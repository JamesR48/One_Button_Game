using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerBase : MonoBehaviour
{
    public int player_id;

    #region serializable variables

    [SerializeField]
    protected KeyCode key; //Keyboard key to use for controlling

    //MOVEMENT
    [SerializeField]
    protected GameObject lookat_target; //enemy character to look-at based on distance
    [SerializeField]
    protected float rotation_speed = 5f;
    [SerializeField]
    protected float impulse_force = 15f;

    //SHOOTING
    [SerializeField]
    protected GameObject shooting_point;

    [SerializeField]
    protected int max_bullet_amount;

    #endregion

    #region protected variables

    protected bool on_wall = false;

    [SerializeField]
    protected int current_bullet_amount;

    float reload_counter = 0.0f;

    #endregion

    #region components

    Rigidbody player_rigidbody;
    ObjectPooler player_bullets_pool;

    #endregion

    #region MonoBehaviour callbacks

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player_rigidbody = GetComponent<Rigidbody>();

        player_bullets_pool = GetComponent<ObjectPooler>();
        player_bullets_pool.amount = max_bullet_amount;

        current_bullet_amount = max_bullet_amount;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!on_wall)
        {
            SeekTarget(this.gameObject);
        }

        if(current_bullet_amount > 0)
        {
            Shoot();

            Move(this.gameObject);
        }

        Reload(Time.deltaTime);

    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        FacingBounds(collision, this.gameObject, null);
    }

    #endregion

    protected void FacingBounds(Collision collision, GameObject facing_object, GameObject turret_object)
    {
        if (collision.gameObject.CompareTag("Bound"))
        {
            player_rigidbody.velocity = Vector3.zero;
            player_rigidbody.angularVelocity = Vector3.zero;
            player_rigidbody.Sleep();

            if (turret_object != null)
            {
                facing_object.transform.rotation = Quaternion.LookRotation(-collision.GetContact(0).normal);
                turret_object.transform.rotation = Quaternion.LookRotation(-collision.GetContact(0).normal);
            }
            else
            {
                facing_object.transform.rotation = Quaternion.LookRotation(-collision.GetContact(0).normal);
            }

            on_wall = true;
        }
    }

    protected void Move(GameObject rotating_object)
    {

        //if (Input.GetKeyDown(key))
        if (Keyboard.current.FindKeyOnCurrentKeyboardLayout(key.ToString()).wasPressedThisFrame)
        {
            player_rigidbody.velocity = Vector3.zero;
            player_rigidbody.angularVelocity = Vector3.zero;

            player_rigidbody.AddForce((-rotating_object.transform.forward) * impulse_force, ForceMode.Impulse);

            on_wall = false;
        }
    }

    protected void SeekTarget(GameObject rotating_object)
    {

        rotating_object.transform.LookAt(new Vector3(lookat_target.transform.position.x, 0f, lookat_target.transform.position.z));

    }

    protected void Shoot()
    {
        if (!on_wall && Keyboard.current.FindKeyOnCurrentKeyboardLayout(key.ToString()).wasPressedThisFrame)
        {

            GameObject bullet_instance = player_bullets_pool.GetPooledObject();
            if (bullet_instance != null)
            {
                bullet_instance.transform.position = shooting_point.transform.position + shooting_point.transform.forward;
                bullet_instance.transform.rotation = Quaternion.LookRotation(shooting_point.transform.forward);
                bullet_instance.GetComponent<MeshRenderer>().material = this.GetComponent<MeshRenderer>().material;
                bullet_instance.GetComponent<Bullet>().shooter_id = player_id;
                bullet_instance.SetActive(true);

                Camera.main.GetComponent<CameraShake>().should_shake = true;

                current_bullet_amount = Mathf.Clamp(current_bullet_amount-1, 0, max_bullet_amount);
            }

        }
    }

    protected void Reload(float delta)
    {
        if( (current_bullet_amount < max_bullet_amount) && on_wall)
        {
            reload_counter = Mathf.Clamp(reload_counter + delta, 0.0f, (float)max_bullet_amount);
            current_bullet_amount = (int)reload_counter;
        }
        else if ((current_bullet_amount < max_bullet_amount) && !on_wall)
        {
            reload_counter = (float)current_bullet_amount;
        }
        else if ((current_bullet_amount == max_bullet_amount))
        {
            reload_counter = 0.0f;
        }
    }

}
