using UnityEngine;
using System.Collections;

public class LiftTrigger : MonoBehaviour 
{
	public float timeToDoorsClose = 2f;
	public float timeToLiftStart = 3f;
	public float timeToEndLevel = 6f;
	public float liftSpeed = 3f;

	private GameObject player;
	private Animator playerAnim;
	private HashIDs hash;
	private CameraMovement camMovement;
	private SceneFadeInOut sceneFadeInOut;
	private LiftDoorsTracking liftDoorsTracking;

	private bool playerInLift;
	private float timer;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag(Tags.player);
		playerAnim = player.GetComponent<Animator>();
		hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
		camMovement = Camera.main.gameObject.GetComponent<CameraMovement>();
		sceneFadeInOut = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<SceneFadeInOut>();
		liftDoorsTracking = GetComponent<LiftDoorsTracking>();
	}

	void OnTriggerEnter( Collider other )
	{
		if( other.gameObject == player )
		{
			playerInLift = true;
		}
	}

	void OnTriggerExit( Collider other )
	{
		if( other.gameObject == player )
		{
			playerInLift = false;
			timer = 0f;
		}
	}


	void Update()
	{
		if( playerInLift )
			LiftActivation();

		if( timer < timeToDoorsClose )
		{
			liftDoorsTracking.DoorFollowing();
		}
		else
		{
			liftDoorsTracking.CloseDoors();
		}
	}

	void LiftActivation()
	{
		timer += Time.deltaTime;

		if( timer >= timeToLiftStart )
		{
			playerAnim.SetFloat( hash.speedFloat, 0f);
			camMovement.enabled = false;
			player.transform.parent = transform;

			transform.Translate( Vector3.up * liftSpeed * Time.deltaTime );
			if( !audio.isPlaying )
				audio.Play ();

			if( timer >= timeToEndLevel )
				sceneFadeInOut.EndScene();
		}
	}

}
