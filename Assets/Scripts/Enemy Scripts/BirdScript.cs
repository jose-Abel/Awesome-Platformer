using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour {

	private Rigidbody2D myBody;
	private Animator anim;

	private Vector3 moveDirection = Vector3.left;
	private Vector3 originPosition;
	private Vector3 movePosition;

	public GameObject birdEgg;
	public LayerMask playerLayer;
	private bool attacked;

	private bool canMove;

	private float speed = 2.5f;

	void Awake()
	{
		myBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}
	// Use this for initialization
	void Start () {
		//originPosition is declaring how long the bird will fly to the right
		originPosition = transform.position;
		originPosition.x += 6f;

		//originPosition is declaring how long the bird will fly to the left
		movePosition = transform.position;
		movePosition.x -= 6f;

		canMove = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		MoveTheBird();
		DropTheEgg();
	}

	void MoveTheBird()
	{
		if (canMove)
		{
			
			transform.Translate(moveDirection * speed * Time.smoothDeltaTime);

			//if position of the bird in the map is bigger than the original position + 6f then make the bird goes the left and changing the sprite to face left
			if (transform.position.x >= originPosition.x)
			{
				moveDirection = Vector3.left;
				ChangeDirection(0.5f);
			}
			//else if position of the bird in the map is less than the original position - 6f then make the bird goes the right and changing the sprite to face left
			else if (transform.position.x <= movePosition.x)
			{
				moveDirection = Vector3.right;
				ChangeDirection(-0.5f);
			}
		}
	}

	void ChangeDirection(float direction)
	{
		Vector3 tempScale = transform.localScale;
		tempScale.x = direction;
		transform.localScale = tempScale;
	}

	void DropTheEgg()
	{
		if (!attacked)
		{
			if(Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer))
			{
				Instantiate(birdEgg, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);

				attacked = true;

				anim.Play("BirdFly");
			}
		}
	}

	IEnumerator BirdDead()
	{
		yield return new WaitForSeconds(3f);
		gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if(target.tag == MyTags.BULLET_TAG)
		{
			anim.Play("BirdDead");

			//makes the bird pass through other colliders
			GetComponent<BoxCollider2D>().isTrigger = true;

			//Rigid Body dynamic removes the gravity from the game object
			myBody.bodyType = RigidbodyType2D.Dynamic;

			canMove = false;

			StartCoroutine(BirdDead());
		}
	}
}
