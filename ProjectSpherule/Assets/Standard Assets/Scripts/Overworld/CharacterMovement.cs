using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

    public float movementSpeed;
    private Vector2 movementVector;
    private Animator animator;
    private int direction;
    private Rigidbody2D rigidbody;

    const int STATE_IDLE = 0;
    const int STATE_WALK_NORTH = 1;
    const int STATE_WALK_SOUTH = 2;
    const int STATE_WALK_EAST = 3;
    const int STATE_WALK_WEST = 4;
    const int STATE_WALK_DIAGONALNORTH = 5;
    const int STATE_WALK_DIAGONALSOUTH= 6;

    private const string DIRECTION_LEFT = "left";
    private const string DIRECTION_RIGHT = "right";

    private string _currentDirection = DIRECTION_LEFT;
    private int _currentAnimationState = STATE_IDLE;

    void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        rigidbody.velocity = Vector2.zero;
        movementVector = Vector2.zero;
        if(!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            ChangeState(STATE_IDLE);
        }

        if (Input.GetKey(KeyCode.D))
        {
            ChangeDirection(DIRECTION_RIGHT);
            if(Input.GetKey(KeyCode.W))
            {
                ChangeState(STATE_WALK_DIAGONALNORTH);
            }
            else if(Input.GetKey(KeyCode.S))
            {
                ChangeState(STATE_WALK_DIAGONALSOUTH);
            }
            else
            {
                ChangeState(STATE_WALK_EAST);
            }
            
            movementVector.x = movementSpeed;

        }

        if (Input.GetKey(KeyCode.A))
        {
            ChangeDirection(DIRECTION_LEFT);
            if (Input.GetKey(KeyCode.W))
            {
                ChangeState(STATE_WALK_DIAGONALNORTH);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                ChangeState(STATE_WALK_DIAGONALSOUTH);
            }
            else
            {
                ChangeState(STATE_WALK_WEST);
            }
            movementVector.x = -movementSpeed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.D))
            {
                ChangeDirection(DIRECTION_RIGHT);
                ChangeState(STATE_WALK_DIAGONALNORTH);
            }
            else if(Input.GetKey(KeyCode.A))
            {
                ChangeDirection(DIRECTION_LEFT);
                ChangeState(STATE_WALK_DIAGONALNORTH);
            }
            else
            {
                ChangeState(STATE_WALK_NORTH);
            }
            movementVector.y = movementSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.D))
            {
                ChangeDirection(DIRECTION_RIGHT);
                ChangeState(STATE_WALK_DIAGONALSOUTH);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                ChangeDirection(DIRECTION_LEFT);
                ChangeState(STATE_WALK_DIAGONALSOUTH);
            }
            else
            {
                ChangeState(STATE_WALK_SOUTH);
            }
            movementVector.y = -movementSpeed;
        }

        rigidbody.AddForce(movementVector);

    }

    void ChangeState(int state)
    {

        if (_currentAnimationState == state)
            return;

        switch (state)
        {

            case STATE_WALK_NORTH:
                animator.SetInteger("movement", STATE_WALK_NORTH);
                break;

            case STATE_WALK_SOUTH:
                animator.SetInteger("movement", STATE_WALK_SOUTH);
                break;

            case STATE_WALK_EAST:
                animator.SetInteger("movement", STATE_WALK_EAST);
                break;

            case STATE_WALK_WEST:
                animator.SetInteger("movement", STATE_WALK_WEST);
                break;

            case STATE_WALK_DIAGONALNORTH:
                animator.SetInteger("movement", STATE_WALK_DIAGONALNORTH);
                break;

            case STATE_WALK_DIAGONALSOUTH:
                animator.SetInteger("movement", STATE_WALK_DIAGONALSOUTH);
                break;

            case STATE_IDLE:
                animator.SetInteger("movement", STATE_IDLE);
                break;
            

        }

        _currentAnimationState = state;
    }

    //--------------------------------------
    // Flip player sprite for left/right walking
    //--------------------------------------
    void ChangeDirection(string direction)
    {

        if (_currentDirection != direction)
        {
            if (direction == DIRECTION_RIGHT)
            {
                transform.Rotate(0, 180, 0);
                _currentDirection = DIRECTION_RIGHT;
            }
            else if (direction == DIRECTION_LEFT)
            {
                transform.Rotate(0, -180, 0);
                _currentDirection = DIRECTION_LEFT;
            }
        }

    }
}
