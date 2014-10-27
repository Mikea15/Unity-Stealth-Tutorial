using UnityEngine;
using System.Collections;

public class DoorAnimation : MonoBehaviour 
{
	public bool requireKey;
	public AudioClip doorSwitchClip;
	public AudioClip accessDeniedClip;

	private Animator anim;
	private HashIDs hash;
	private GameObject player;
	private PlayerInventory playerInventory;

	private int countColliders;

	void Awake()
	{
		anim = GetComponent<Animator>();
		hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
		player = GameObject.FindGameObjectWithTag(Tags.player);
		playerInventory = player.GetComponent<PlayerInventory>();
	}

	void Update( )
	{
		anim.SetBool( hash.openBool, countColliders > 0 );

		if( anim.IsInTransition(0) && !audio.isPlaying )
		{
			audio.clip = doorSwitchClip;
			audio.Play();
		}
	}

	void OnTriggerEnter(Collider other )
	{
		if( other.gameObject == player )
		{
			if( requireKey )
			{
				if( playerInventory.hasKey )
					countColliders++;
				else
				{
					audio.clip = accessDeniedClip;
					audio.Play();
				}
			}
			else
			{
				countColliders++;
			}
		}
		else if( other.gameObject.tag == Tags.enemy )
		{
			if( other is CapsuleCollider )
			{
				countColliders++;
			}
		}
	}

	void OnTriggerExit( Collider other )
	{
		if( other.gameObject == player || (other.gameObject.tag == Tags.enemy && other is CapsuleCollider ) )
			countColliders = Mathf.Max(0, countColliders-1);
	}

}
