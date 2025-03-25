using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

/// <summary>
/// A collecting game object
/// </summary>
public class Collector : MonoBehaviour
{
    #region Fields

    // targeting support
    SortedList<Target> targets = new SortedList<Target>();
    Target targetPickup = null;

    // movement support
    const float BaseImpulseForceMagnitude = 2.0f;
    const float ImpulseForceIncrement = 0.3f;

    // saved for efficiency
    Rigidbody2D rb2d;

    #endregion

    #region Methods

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        // center collector in screen
        Vector3 position = transform.position;
        position.x = 0;
        position.y = 0;
        position.z = 0;
        transform.position = position;

        // save reference for efficiency
        rb2d = GetComponent<Rigidbody2D>();

        // add as listener for pickup spawned event

        EventManager.AddListener(OnPickupSpawned); //melis
    }

    private void OnPickupSpawned(GameObject pickUpObj)
    {
        if (pickUpObj == null)
        {
            Debug.Log("Received null GameObject in OnPickupSpawned");
            return;
        }

        Target newTarget = new Target(pickUpObj, pickUpObj.transform.position);

        targets.AddAndSort(newTarget);

        SetTarget();
    }


    /// <summary>
    /// Called when another object is within a trigger collider
    /// attached to this object
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == targetPickup.GameObject)
        {
            targets.RemoveAt(targets.Count - 1);

            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].UpdateDistance(transform.position);
            }


            if (targets.Count > 0)
            {
                SetTarget();
            }
            else
            {
                targetPickup = null;
            }
        }
    }


    /// <summary>
    /// Sets the target pickup to the provided pickup
    /// </summary>
    /// <param name="pickup">Pickup.</param>
    void SetTarget()
    {
        if (targets.Count > 0)
        {
            targetPickup = targets.Last();
            GoToTargetPickup();
        }
    }

    /// <summary>
    /// Starts the teddy bear moving toward the target pickup
    /// </summary>
    void GoToTargetPickup()
    {
        Vector2 direction = new Vector2(
            targetPickup.gameObject.transform.position.x - transform.position.x,
            targetPickup.gameObject.transform.position.y - transform.position.y);
        direction.Normalize();
        rb2d.velocity = Vector2.zero;
        
        float myforce = ImpulseForceIncrement * targets.Count + BaseImpulseForceMagnitude;
        rb2d.AddForce(direction * myforce,
            ForceMode2D.Impulse);
    }

    #endregion
}