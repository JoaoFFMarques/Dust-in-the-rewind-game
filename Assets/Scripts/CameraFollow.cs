using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public float m_xSmooth = 8f;
	public Vector2 m_maxXAndY;
	public Vector2 m_minXAndY;

	public Transform Player;

    private void Update()
    {
		TrackPlayer();
	}
    private void TrackPlayer()
	{
		float targetX;
		float targetY = transform.position.y;

		targetX = Mathf.Lerp(transform.position.x, Player.position.x, m_xSmooth * Time.deltaTime);

		targetX = Mathf.Clamp(targetX, m_minXAndY.x, m_maxXAndY.x);

		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
	
}

