using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour {

	private Animator anim;
	private bool animation_Started;
	private bool animation_Finished;

	private int jumpTimes;
	private bool jumpLeft = true;

	private string coroutine_Name = "FrogJump";

	void Awake()
	{
		anim = GetComponent<Animator>();
	}
	// Use this for initialization
	void Start () {
		StartCoroutine(coroutine_Name);
	}
	
	// LateUpdate is called at the end of the frame
	void LateUpdate () {
		if(animation_Finished && animation_Started)
		{
			animation_Started = false;

			//move the parent to the child position
			transform.parent.position = transform.position;
			
			//changing child position to 0,0,0 base on its parent
			transform.localPosition = Vector3.zero;
		}
		
	}

	IEnumerator FrogJump()
	{
		yield return new WaitForSeconds(Random.Range(1f, 4f));

		animation_Started = true;
		animation_Finished = false;

		jumpTimes++;

		if (jumpLeft)
		{
			anim.Play("FrogJumpLeft");
		}
		else
		{
			anim.Play("FrogJumpRight");
		}
		StartCoroutine(coroutine_Name);

	}

	void AnimationFinished()
	{
		animation_Finished = true;
		anim.Play("FrogIdleLeft");

		if(jumpLeft)
		{
			anim.Play("FrogIdleLeft");
		}
		else
		{
			anim.Play("FrogIdleRight");
		}

		if(jumpTimes == 3)
		{
			jumpTimes = 0;

			Vector3 tempScale = transform.localScale;

			tempScale.x *= -1;

			transform.localScale = tempScale;

			jumpLeft = !jumpLeft;
		}
	}
}
