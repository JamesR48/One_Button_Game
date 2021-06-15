using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : PlayerBase
{
    #region serializable variables

    [SerializeField]
    private GameObject turret = null; //Check if character has a turret (if it's a castle basically)

    #endregion

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (!on_wall)
        {
            SeekTarget(turret);
        }

        if (current_bullet_amount > 0)
        {
            Shoot();

            Move(turret);
        }

        Reload(Time.deltaTime);

    }

    protected override void OnCollisionEnter(Collision collision)
    {
        FacingBounds(collision, this.gameObject, turret);
    }
}
