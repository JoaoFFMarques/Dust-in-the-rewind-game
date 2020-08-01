using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private List<Vector3> m_RecordedPositions;


    private Rigidbody m_PlayerRB;
    private Animator m_PlayerAnim;
    private Vector3 m_Movement;
    public float m_MoveSpeedy;

    public bool m_IsRewind;
    private bool m_IsLookingLeft;
    

    void Start()
    {
        m_PlayerAnim = GetComponent<Animator>();
        m_PlayerRB = GetComponent<Rigidbody>();

        m_RecordedPositions = new List<Vector3>();
        Record();
    }

    void Update()
    {
        if((transform.position.x> m_RecordedPositions[m_RecordedPositions.Count-1].x+1 ||
            transform.position.x < m_RecordedPositions[m_RecordedPositions.Count-1].x - 1 ||
            transform.position.y > m_RecordedPositions[m_RecordedPositions.Count-1].y + 1 ||
            transform.position.y < m_RecordedPositions[m_RecordedPositions.Count-1].y - 1) && !m_IsRewind)
        {
            Record();
        }

        m_PlayerAnim.SetBool("IsWalking", m_Movement.x != 0 || m_Movement.y != 0);

    }
   
    private void FixedUpdate()
    {
        if(!m_IsRewind)
            Move();
        else
            Rewind();
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

    public void Record()
    {
        m_RecordedPositions.Add(transform.position);
    }

    public void Rewind()
    {
        
        Time.timeScale = 4;
        transform.position = Vector3.MoveTowards(transform.position, m_RecordedPositions[m_RecordedPositions.Count - 1], 1 * Time.deltaTime);
       
        if(transform.position == m_RecordedPositions[m_RecordedPositions.Count - 1])
            m_RecordedPositions.RemoveAt(m_RecordedPositions.Count - 1);

        if(m_RecordedPositions.Count - 1 == 0)
        {
            m_IsRewind = false;
            Time.timeScale = 1;
        }
    }
}
