using UnityEngine;
using System.Collections;

public class MouseOrbit : MonoBehaviour {
	
	public Transform target;
	public float xSpeed = 120.0f;
	public float ySpeed = 120.0f;
	
	float x = 0.0f;
	float y = 0.0f;
	
	// Use this for initialization
	void Start () 
	{

	}
	
	void LateUpdate () 
	{
		if (target && Input.GetMouseButton(0) ) 
		{
			float dx = x - Input.mousePosition.x;
			float dy = y - Input.mousePosition.y;

			
			transform.RotateAround( target.transform.position, Vector3.up, -dx*xSpeed );
			transform.RotateAround( target.transform.position, transform.right, dy*ySpeed );

		}
		x = Input.mousePosition.x;
		y = Input.mousePosition.y;
	}
	

}