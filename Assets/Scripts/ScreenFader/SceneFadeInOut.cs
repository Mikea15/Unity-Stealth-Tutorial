using UnityEngine;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour 
{
	public float fadeSpeed = 1.5f;

	private bool sceneStarting = true;

	void Awake( )
	{

	}

	void Update()
	{

	}

	public void StartScene()
	{
		sceneStarting = true;
	}

	public void EndScene()
	{
		sceneStarting = false;
	}

}
