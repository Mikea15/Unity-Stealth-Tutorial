using UnityEngine;
using System.Collections;

public class LazerBlinking : MonoBehaviour 
{

	public float onTime;
	public float offTime;

	private float timer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
		if( renderer.enabled && timer >= onTime )
		{
			SwitchBeam();
		}

		if( renderer.enabled && timer >= offTime )
		{
			SwitchBeam();
		}
	}

	void SwitchBeam()
	{
		timer = 0.0f;

		renderer.enabled = !renderer.enabled;
		light.enabled = !light.enabled;
	}
}
