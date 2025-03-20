using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
		
		EventManager.AddListener(OnPickupSpawned);//melis
        }
    public void OnPickupSpawned(GameObject arg0)
    {
	    if (arg0 == null)
	    {
		    Debug.Log("Received null GameObject in OnPickupSpawned");
		    return;
	    }

	    Target target = new Target(arg0, transform.position);

	    if (target != null)
	    {
		    targets.Add(target);
		    Debug.Log("My target spawned: "+target);
		    Debug.Log("My target count is"+targets.count);
		    
	    }
	    else
	    {
		    Debug.LogError("Attempting to add a null target.");
	    }


	    Target nextTarget = null;

	    if (targets.Count > 0)
	    {
		   // nextTarget = targets[targets.Count - 1]; melis
		   nextTarget = targets[targets.count - 1];
		    Debug.Log("My current target: " + nextTarget);
	    }
	    else
	    {
		    Debug.Log("Targets list is empty!");
		    SetTarget(targetPickup.gameObject);//melis
	    }

	    if (nextTarget != null && target.Distance < nextTarget.Distance)
	    {
		    Debug.Log("My list: " + targets);
		    Debug.Log("Target " + target + " has a shorter distance than " + nextTarget);
		

		    SetTarget(target.gameObject);
	    }
	
	 
	    SetTarget(nextTarget.gameObject);//melis
	    
	    Debug.Log("My sorted target list: " + targets);
    }





    /// <summary>
    /// Called when another object is within a trigger collider
    /// attached to this object
    /// </summary>
    /// <param name="other"></param>



 
    void OnTriggerStay2D(Collider2D other)
    {

	    if (targets.Count > 0)
	    {
		    targetPickup = targets[targets.count - 1];//melis 14.45
	    }
	    if (targetPickup != null && other.gameObject == targetPickup.GameObject)
	    {
		 
		    int targetIndex = targets.IndexOf(targetPickup);
		    if (targetIndex != -1)
		    {
			    targets.RemoveAt(targetPickup);
		    }
		    else
		    {
			    Debug.LogWarning("Target"+targetPickup+" not found in the list.");
		    }

		    //for (int i = 0; i < targets.Count; i++)
		    for (int i = 0; i < targets.count; i++)
		    
		    {
			    Debug.Log(targets.count);
			  
			
			    // Debug.Log(targets[i]);
			    targets[i].UpdateDistance(transform.position);
			   // targets.Sort();
		    }
		 
		   // targets.Sort();
		    Debug.Log("MY TARGET"+targets);
	
		    //if (targets.Count > 0)
		    if(targets.count > 0)
		    {
			  //  targetPickup = targets[targets.Count - 1]; mel
			   targetPickup = targets[targets.count - 1];
			    SetTarget(targetPickup.GameObject);
		    }
		    else
		    {
			    targetPickup = null;
			    SetTarget(null); 
		    }
	    }
    }



    
	/// <summary>
	/// Sets the target pickup to the provided pickup
	/// </summary>
	/// <param name="pickup">Pickup.</param>
	void SetTarget(GameObject pickup) 
    {
	    targetPickup=new Target(pickup,transform.position);
		 //targetPickup.gameObject = pickup; //gameobject ekledim
		// Debug.Log(targetPickup);//melis
		 GoToTargetPickup();
	}

	/// <summary>
	/// Starts the teddy bear moving toward the target pickup
	/// </summary>
	
	void GoToTargetPickup()
    {
        // calculate direction to target pickup and start moving toward it

		Vector2 direction = new Vector2(
			targetPickup.gameObject.transform.position.x - transform.position.x,//gameobj ekledim
			targetPickup.gameObject.transform.position.y - transform.position.y);
		

		direction.Normalize();
		rb2d.velocity = Vector2.zero;
		
	//	rb2d.AddForce(direction * BaseImpulseForceMagnitude,
		//	ForceMode2D.Impulse);
		
	//	change the GoToTargetPickup method to apply a force equal to the
	//BaseImpulseForceMagnitude plus the ImpulseForceIncrement times the number of pickups currently in the game.MELIS

	    float myforce=ImpulseForceIncrement*targets.Count + BaseImpulseForceMagnitude;
		rb2d.AddForce(direction *myforce,
			ForceMode2D.Impulse);
	}
	

	#endregion
}
