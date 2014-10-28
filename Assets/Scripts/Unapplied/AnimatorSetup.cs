using UnityEngine;
using System.Collections;

public class AnimatorSetup
{
	public float speedDampTime = 0.1f;
	public float angularSpeedDampTime = 0.7f;
	public float angeResponseTime = 0.6f;


	private Animator anim;
	private HashIDs hash;

	public AnimatorSetup( Animator animator, HashIDs hashIDs )
	{
		anim = animator;
		hash = hashIDs;
	}

	public void Setup( float speed, float angle)
	{
		float angularSpeed = angle / angeResponseTime;

		anim.SetFloat ( hash.speedFloat, speed, speedDampTime, Time.deltaTime );
		anim.SetFloat( hash.angularSpeedFloat, angularSpeed, angularSpeedDampTime, Time.deltaTime);
	}
}
