using UnityEngine;
using System.Collections;

public class LaserSwitchDeactivation : MonoBehaviour 
{
	public GameObject laser;
	public Material unlockedMat;

	private GameObject player;

	// Use this for initialization
	void Awake () 
	{
		player = GameObject.FindGameObjectWithTag(Tags.player);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerStay( Collider other ) 
	{
		if( other.gameObject == player )
		{
			if( Input.GetButton("Switch") )
			{
				LaserDeactivation( );
			}
		}
	}

	void LaserDeactivation( )
	{
		laser.SetActive (false);
		audio.Play();

		Renderer screen = transform.Find ("prop_switchUnit_screen_001").renderer;
		screen.material = unlockedMat;
	}
}
