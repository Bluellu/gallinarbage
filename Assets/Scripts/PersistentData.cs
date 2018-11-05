using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* A container for the final score to be passed into the next screen. */
public class PersistentData : MonoBehaviour {
    private int metersUp;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
        metersUp = 0;
	}
	
    public void SetDistance(int newDistance)
    {
        metersUp = newDistance;
    }

    public int GetDistance()
    {
        return metersUp;
    }
}
