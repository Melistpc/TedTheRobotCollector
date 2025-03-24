using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A target for the collector
/// </summary>
public class Target : IComparable
{
    #region Fields

    public GameObject gameObject;
    float distance;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructs a target with the given game object and the
    /// current position of the collector
    /// </summary>
    /// <param name="gameObject">target game object</param>
    /// <param name="position">collector position</param>
    public Target(GameObject gameObject, Vector3 position)
    {
        this.gameObject = gameObject;
        UpdateDistance(position);
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the target game object
    /// </summary>
    /// <value>target game object</value>
    public GameObject GameObject
    {
        get { return gameObject; }
    }

    /// <summary>
    /// Gets the distance for the target
    /// </summary>
    /// <value>distance</value>
    public float Distance
    {
        get { return distance; }
    }

    #endregion

    #region Public methods

    /// <summary>
    /// Updates the distance from the target game object to
    /// the given position
    /// </summary>
    /// <param name="position">position for distance calculation</param>
    public void UpdateDistance(Vector3 position)
    {
        distance = Vector3.Distance(gameObject.transform.position,
            position);
    }


    public int CompareTo(object obj)
    {
        Target other = obj as Target;
        
        if (other.Distance < Distance)
        {
            return -1;
        }
        else if (other.Distance > Distance)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// Converts the target to a string
    /// </summary>
    /// <returns>the string for the target</returns>
    public override string ToString()
    {
        return distance.ToString();
    }

    #endregion
}