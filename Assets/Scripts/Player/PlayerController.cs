using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Stack<Vector3> m_RecordedPositions;

    private Animator m_PlayerAnim;
    private SpriteRenderer m_PlayerSR;
    private Vector3 m_Pos;

    private GameManager m_GameManager;

    private bool m_IsMoving;
    private bool m_IsLookingLeft;

    private bool m_IsDead;

    private bool m_IsTouchUp;
    private bool m_IsTouchDown;
    private bool m_IsTouchRight;
    private bool m_IsTouchLeft;

    private bool m_IsLookingUp, m_IsLookingDown, m_isLookingSide;

    public LayerMask m_ObjectLayer;
    public Transform m_Up, m_Down, m_Right, m_Left;
    public int m_MaxDistance;
    public bool m_IsRewind;
    public float m_MoveSpeedy;
    public bool m_End;

    void Start()
    {
        m_GameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        m_PlayerAnim = GetComponent<Animator>();
        m_PlayerSR = GetComponent<SpriteRenderer>();
        m_RecordedPositions = new Stack<Vector3>();

        Record();
    }

    void Update()
    {
        if(!m_IsDead)
        {
            CheckTouch();

            if(!m_IsRewind)
            {
                if(!m_IsMoving)
                    Control();
                else
                    Move();
            }
            else
                Rewind();
        }

        PlayerAnimations();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall") && !m_IsRewind)
        {
            m_IsMoving = false;
            Record();

        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            m_IsDead = true;
            m_IsLookingUp = false;
            m_IsLookingDown = false;
            m_isLookingSide = false;
            m_IsMoving = false;
        }
    }

    private void CheckTouch()
    {
        m_IsTouchUp = Physics.CheckSphere(m_Up.position, 0.1f, m_ObjectLayer, QueryTriggerInteraction.Ignore);
        m_IsTouchDown = Physics.CheckSphere(m_Down.position, 0.1f, m_ObjectLayer, QueryTriggerInteraction.Ignore);
        m_IsTouchLeft = Physics.CheckSphere(m_Left.position, 0.1f, m_ObjectLayer, QueryTriggerInteraction.Ignore);
        m_IsTouchRight = Physics.CheckSphere(m_Right.position, 0.1f, m_ObjectLayer, QueryTriggerInteraction.Ignore);
    }

    private void ChangeDirection(Vector2 pos)
    {
        if(pos.y > 0)
        {
            m_IsLookingUp = true;
            m_IsLookingDown = false;
            m_isLookingSide = false;
        }
        else if(pos.y < 0)
        {
            m_IsLookingUp = false;
            m_IsLookingDown = true;
            m_isLookingSide = false;
        }
        else if(pos.x != 0)
        {
            m_IsLookingUp = false;
            m_IsLookingDown = false;
            m_isLookingSide = true;
        }
    }

    private void ChangeDirectionRewind(Vector2 pos)
    {
        if(pos.y > 0)
            m_PlayerAnim.Play("Player_Walk_Up");
        else if(pos.y < 0)
            m_PlayerAnim.Play("Player_Walk_Down");
        else
            m_PlayerAnim.Play("Player_Walk_Side");

        if(pos.x > 0 && m_IsLookingLeft)
            Rotate();
        else if(pos.x < 0 && !m_IsLookingLeft)
            Rotate();
    }

    private void Control()
    {
        if(Input.GetButton("right") && !m_IsTouchRight)
        {
            m_Pos = transform.position + Vector3.right;
            Moved();
        }
        else if(Input.GetButton("left") && !m_IsTouchLeft)
        {
            m_Pos = transform.position + Vector3.left;
            Moved();
        }

        if(Input.GetButton("up") && !m_IsTouchUp)
        {
            m_Pos = transform.position + Vector3.up;
            Moved();
        }
        else if(Input.GetButton("down") && !m_IsTouchDown)
        {
            m_Pos = transform.position + Vector3.down;
            Moved();
        }

        ChangeDirection(m_Pos - transform.position);
    }

    private void Move()
    {
        var X = m_Pos.x - transform.position.x;

        if(X > 0 && m_IsLookingLeft)
            Rotate();
        else if(X < 0 && !m_IsLookingLeft)
            Rotate();

        transform.position = Vector3.MoveTowards(transform.position, m_Pos, m_MoveSpeedy * Time.deltaTime);
        
        if(transform.position == m_Pos)
        {
            m_IsMoving = false;
        }
    }

    private void Rotate()
    {
        m_IsLookingLeft = !m_IsLookingLeft;
        m_PlayerSR.flipX = !m_PlayerSR.flipX;
    }

    public void Record()
    {
        m_RecordedPositions.Push(transform.position);
    }

    public void Rewind()
    {
        ChangeDirectionRewind(transform.position - m_RecordedPositions.Peek());

        transform.position = Vector3.MoveTowards(transform.position, m_RecordedPositions.Peek(), 3 * Time.deltaTime);

        if(transform.position == m_RecordedPositions.Peek())
            m_RecordedPositions.Pop();

        if(m_RecordedPositions.Count == 0)
        {
            m_IsRewind = false;
            m_IsMoving = false;
            PlayerAnimations();
            m_GameManager.Victory();
        }
    }

    private void PlayerAnimations()
    {
        m_PlayerAnim.SetBool("IsWalking", m_IsMoving);
        m_PlayerAnim.SetBool("IsSide", m_isLookingSide);
        m_PlayerAnim.SetBool("IsUp", m_IsLookingUp);
        m_PlayerAnim.SetBool("IsDown", m_IsLookingDown);
        m_PlayerAnim.SetBool("IsDead", m_IsDead);
    }

    private void Moved()
    {
        m_IsMoving = true;
        Record();
        m_GameManager.UseMovement();
    }

    public void OnDead()
    {
        gameObject.SetActive(false);
        m_IsDead = false;
        m_End = true;
        m_GameManager.GameOver();

    }
}
