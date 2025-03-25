﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// A sorted list
/// </summary>
public class SortedList<T> where T : IComparable
{
    List<T> items = new List<T>();


    // used in Add method
    List<T> tempList = new List<T>();

    #region Constructors

    /// <summary>
    /// No argument constructor
    /// </summary>
    public SortedList()
    {
        items.Sort(); //melis
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the number of items in the list
    /// </summary>
    /// <value>number of items in the list</value>
    public int Count
    {
        get { return items.Count; }
    }

    /// <summary>
    /// Gets the item in the list at the given index
    /// This property allows access using [ and ]
    /// </summary>
    /// <param name="index">index of item</param>
    /// <returns>item at the given index</returns>
    public T this[int index]
    {
        get { return items[index]; }
    }

    #endregion

    #region Methods

    public void Add(T item)
    {
        if (items == null)
        {
            items = new List<T>();
            Debug.LogWarning("Items list was null, initializing it now.");
        }

       /* if (items.Capacity == items.Count)
        {
            Expand();
        }*/

        int addLocation = 0;
      //  items.OrderBy(i => i == null).ThenBy(i => i); //https://stackoverflow.com/questions/3163657/how-can-you-do-custom-sorting-in-linq-with-null-always-on-the-end
        //   while (addLocation < items.Count && items[addLocation].CompareTo(item) < 0)
        items.Sort();
        while (addLocation < items.Count && items[addLocation].CompareTo(item) < 0)
        {
          
            addLocation++;
        }


        items.Insert(addLocation, item);
   
    }

  /*  protected void Expand()
    {
        T[] templist = new T[items.Count * ExpandMultiplyFactor]; //melis


        for (int i = 0; i < items.Count; i++)
        {
            templist[i] = items[i];
        }

        items = new List<T>(templist); //oldu mu emin değilim
    }

    private static int ExpandMultiplyFactor = 2; //melis
*/

    /// <summary>
    /// Removes the item at the given index from the list
    /// </summary>
    /// <param name="index">index</param>
    public void RemoveAt(int item)
    {
       /* if (item == null)
        {
            Debug.LogError("Attempting to remove a null item.");
            return;
        }

        int itemLocation = IndexOf(item);//Bunu remove den önce yapıp sonra itemlocation u direk atmamızı istiyor
        if (itemLocation == -1)
        {
            Debug.LogError($"Item '{item}' not found in the list.");
        }
        else
        {
            items.RemoveAt(itemLocation);
   
            //  items.Sort();//melis 11.15 SORTLAMA BURDA NULLARI BASA ALIYO

            Debug.Log($"Item '{item}' removed successfully.");
        }*/
       items.RemoveAt(item);
       
    }


    /// <summary>
    /// Determines the index of the given item using binary search
    /// </summary>
    /// <param name="item">the item to find</param>
    /// <returns>the index of the item or -1 if it's not found</returns>
    public int IndexOf(T item)
    {
        int lowerBound = 0;
        int upperBound = items.Count - 1;
    
        int location = -1;

        // loop until found value or exhausted array
        while ((location == -1) &&
               (lowerBound <= upperBound))
        {
            // find the middle
            int middleLocation = lowerBound + (upperBound - lowerBound) / 2;
            T middleValue = items[middleLocation];

            // check for match
            if (middleValue.CompareTo(item) == 0)
            {
                location = middleLocation;
            }
            else
            {
                // split data set to search appropriate side
                if (middleValue.CompareTo(item) > 0)
                {
                    upperBound = middleLocation - 1; //-1 di
                }
                else
                {
                    lowerBound = middleLocation + 1;
                }
            }
        }

        return location;
    }

    /// <summary>
    /// Sorts the list
    /// </summary>
    public void Sort()
    {
        items.Sort();
        //  items.OrderBy(i => i == null).ThenBy(i => i); 
    }

    /// <summary>
    /// Returns the contents of the list as a csv string
    /// </summary>
    /// <returns>csv string of list contents</returns>
    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < Count; i++)
        {
            stringBuilder.Append(items[i]);
            if (i < Count - 1)
            {
                stringBuilder.Append(',');
            }
        }

        return stringBuilder.ToString();
    }

    #endregion
}