using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2D_slope : MonoBehaviour
{
    public float moveSpeed = 3f;
    public int isGrounded = 0;
    public int reachground = 0;
	Rigidbody2D rb;
	public bool IsHitLeftObstacle { get; private set; }
	public bool IsHitRightObstacle { get; private set; }
    /*Vector3 movement_tmp;
    Vector3 movement;*/
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()   
    {
        alignSurface();
        Jump();
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        print(Input.GetAxis("Horizontal"));
        if (reachground == 1) {
            //movement = movement_tmp;
            reachground = 0;
        }
        if (isGrounded != 0)
        {
            transform.position += movement * Time.deltaTime * moveSpeed / 1;
        }
        else
        {
            if(movement[0]>0){
                rb.velocity = new Vector2(transform.right.x * 3f, rb.velocity.y);
            }
            else if (movement[0]<0)
                rb.velocity = new Vector2(transform.right.x * -3f, rb.velocity.y);
            transform.position += movement * Time.deltaTime * moveSpeed;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded <= 1)
        {
            // transform.position += new vector3(0f,1f,0f);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
            // transform.position += new vector3(0f, 1f, 0f);
            // print(transform.position);
            // transform.position = transform.position + new Vector3(0f, 10f, 0f);
            isGrounded += 1;
        }
        /*else
            movement_tmp = movement/10000;*/
    }

	RaycastHit2D getGroundSurface()
	{
		RaycastHit2D raycasthit2d = new RaycastHit2D();
		raycasthit2d.normal = Vector3.up; //懸浮在空中時，法面朝正上方
		Vector3 dir = -transform.up; //地板偵測射線的方向
        print(-transform.up);
        print(transform.position);
		RaycastHit2D[] raycasthit2ds = Physics2D.RaycastAll(transform.position, dir,10);
		foreach (var i in raycasthit2ds) if (i.transform != transform) raycasthit2d = i;
		return raycasthit2d;
	}

    void alignSurface()
	{
		if (isGrounded != 0 && rb.velocity.y > 0) return;
		Vector3 normal = getGroundSurface().normal;

		Vector3 newDir = Vector3.RotateTowards(transform.up, normal, 0.15f, 0.0F);
		transform.rotation = Quaternion.FromToRotation(Vector3.up, newDir);
	}

    void OnCollisionStay2D(Collision2D other)
	{
		if (isGrounded!=0) return;
		if (other.transform.position.x < transform.position.x) IsHitLeftObstacle = true;
		IsHitRightObstacle = true;
	}

	void OnCollisionExit2D(Collision2D other)
	{
		IsHitRightObstacle = IsHitLeftObstacle = false;
	}
}
