using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour 
{
	public float maximumDamage = 120f;
	public float minimumDamage = 45f;

	public AudioClip shotClip;
	public float flashIntensity = 3f;
	public float fadeSpeed = 10f;

	private Animator anim;
	private HashIDs hash;
	private LineRenderer laserShotLine;
	private Light laserShotLight;
	private SphereCollider col;
	private Transform player;
	private PlayerHealth playerHealth;
	private bool shooting;
	private float scaledDamage;

	void Awake() 
	{
		anim = GetComponent<Animator>();
		laserShotLine = GetComponentInChildren<LineRenderer>();
		laserShotLight = laserShotLine.gameObject.light;
		col = GetComponent<SphereCollider>();
		player = GameObject.FindGameObjectWithTag(Tags.player).transform;
		playerHealth = player.GetComponent<PlayerHealth>();
		hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();

		laserShotLine.enabled = false;
		laserShotLight.intensity = 0f;
		scaledDamage = maximumDamage - minimumDamage;
	}

	void Update()
	{
		float shot = anim.GetFloat( hash.shotFloat );
		if( shot > 0.5f && !shooting )
		{
			Shoot ();
		}

		if( shot < 0.5f )
		{
			shooting = false;
			laserShotLine.enabled = false;
		}

		laserShotLight.intensity = Mathf.Lerp( laserShotLight.intensity, 0f, fadeSpeed * Time.deltaTime );
	}

	void OnAnimatorIK( int layerIndex )
	{
		float aimWeight = anim.GetFloat(hash.aimWeightFloat);
		anim.SetIKPosition(AvatarIKGoal.RightHand, player.position + Vector3.up * 1.5f);
		anim.SetIKPositionWeight(AvatarIKGoal.RightHand, aimWeight);
	}

	void Shoot()
	{
		shooting = true;

		float fractionalDistance = ( col.radius - Vector3.Distance ( transform.position, player.position ) ) / col.radius;
		float damage = scaledDamage * fractionalDistance + minimumDamage;

		playerHealth.TakeDamage(damage);
		ShotEffects();

	}

	void ShotEffects()
	{
		laserShotLine.SetPosition(0, laserShotLine.transform.position);
		laserShotLine.SetPosition(1, player.position + Vector3.up * 1.5f);
		laserShotLine.enabled = true;
		laserShotLight.intensity = flashIntensity;
		AudioSource.PlayClipAtPoint( shotClip, laserShotLight.transform.position );

	}

}















