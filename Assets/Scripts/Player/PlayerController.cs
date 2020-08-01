using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody m_PlayerRB;
    private Animator m_PlayerAnim;
    private Vector3 m_Movement;
    public float m_MoveSpeedy;


    private bool m_IsLookingLeft;
    

    void Start()
    {
        m_PlayerAnim = GetComponent<Animator>();
        m_PlayerRB = GetComponent<Rigidbody>();       
    }

    void Update()
    {


        m_PlayerAnim.SetBool("IsWalking", m_Movement.x != 0 || m_Movement.y != 0);

    }
   
    private void FixedUpdate()
    {
        Move();          
       
    }
   
    private void Move()
    {
        m_Movement.x = Input.GetAxis("Horizontal");
        m_Movement.y = Input.GetAxis("Vertical");

        if(m_Movement.x > 0 && m_IsLookingLeft)
            Rotate();
        else if(m_Movement.x < 0 && !m_IsLookingLeft)
            Rotate();

        m_PlayerRB.MovePosition(m_PlayerRB.position + m_Movement * m_MoveSpeedy * Time.fixedDeltaTime);

    }
    private void Rotate()
    {
        float x = transform.localScale.x * -1;

        m_IsLookingLeft = !m_IsLookingLeft;

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
   
    
   
}
