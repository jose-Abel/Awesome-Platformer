using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public float resetSpeed = 0.5f;
	public float cameraSpeed = 0.3f;

	public Bounds cameraBounds;
	private Transform target;

	private float offsetZ;
	private Vector3 lastTargetPosition;
	private Vector3 currentVelocity;

	private bool followsPlayer;

	void Awake()
	{
		BoxCollider2D myCol = GetComponent<BoxCollider2D>();

		myCol.size = new Vector2(Camera.main.aspect * 2f * Camera.main.orthographicSize, 15f);

		cameraBounds = myCol.bounds;


	}
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG).transform;

		lastTargetPosition = target.position;

		offsetZ = (transform.position - target.position).z;

		followsPlayer = true;
	}
	
	// FixedUpdate is called every 2 or 3 frames in a second
	void FixedUpdate () {
		if (followsPlayer)
		{
			Vector3 aheadTargetPos = target.position + Vector3.forward * offsetZ;

			if(aheadTargetPos.x >= transform.position.x)
			{
				Vector3 newCameraPosition = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, cameraSpeed);

				transform.position = new Vector3(newCameraPosition.x, transform.position.y, newCameraPosition.z);

				lastTargetPosition = target.position;
			}
		}
	}
}
