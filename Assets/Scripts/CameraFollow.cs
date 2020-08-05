using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public float m_xSmooth = 8f;
	public Vector2 m_maxXAndY;
	public Vector2 m_minXAndY;

	private Transform m_Player;
	private bool m_Track;

	public GameManager m_Manager;

    private void Update()
    {
		/*
		if(m_Manager.m_GameStarts && !m_Track)
		{
			m_Player = GameObject.FindGameObjectWithTag("Player").transform;
			m_Track = true;
		}
		*/

		if(m_Track)
			TrackPlayer();
		else
			UntrackPlayer();
	}

    private void TrackPlayer()
	{		
		float targetX;
		float targetY;

		targetX = Mathf.Lerp(transform.position.x, m_Player.position.x, m_xSmooth * Time.deltaTime);

		targetX = Mathf.Clamp(targetX, m_minXAndY.x, m_maxXAndY.x);

		targetY = Mathf.Lerp(transform.position.y, m_Player.position.y, m_xSmooth * Time.deltaTime);

		targetY = Mathf.Clamp(targetY, m_minXAndY.y, m_maxXAndY.y);

		transform.position = new Vector3(targetX, targetY, transform.position.z);		
	}

	private void UntrackPlayer()
    {
		transform.position = transform.position;
    }
	
}

