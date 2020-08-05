using UnityEngine;

public class RewindBlock : MonoBehaviour
{
    public GameObject m_Fog;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().m_IsRewind = true;
            m_Fog.GetComponent<ParticleSystemRenderer>().sortingOrder=-2;
            var col= m_Fog.GetComponent<ParticleSystem>().colorOverLifetime;
            col.enabled = true;

            this.GetComponent<Renderer>().sortingOrder = 1;

        }
    }
}
