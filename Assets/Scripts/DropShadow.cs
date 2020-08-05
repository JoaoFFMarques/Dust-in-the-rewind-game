using UnityEngine;

public class DropShadow : MonoBehaviour
{
    public Vector2 m_ShadowOffset;
    public Vector2 m_ShadowScale;
    public Material m_ShadowMaterial;

    SpriteRenderer m_SpriteRenderer;
    SpriteRenderer m_ShadowSpriteRenderer;
    GameObject m_ShadowGameobject;

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_ShadowGameobject = new GameObject("Shadow");

        m_ShadowSpriteRenderer = m_ShadowGameobject.AddComponent<SpriteRenderer>();

        m_ShadowSpriteRenderer.sprite = m_SpriteRenderer.sprite;
        m_ShadowSpriteRenderer.material = m_ShadowMaterial;

        m_ShadowSpriteRenderer.sortingLayerName = m_SpriteRenderer.sortingLayerName;
        m_ShadowSpriteRenderer.sortingOrder = m_SpriteRenderer.sortingOrder - 1;
        m_ShadowSpriteRenderer.gameObject.transform.localScale = m_ShadowScale;
        m_ShadowGameobject.transform.SetParent(gameObject.transform);
    }

    void LateUpdate()
    {
        m_ShadowSpriteRenderer.sprite = m_SpriteRenderer.sprite;
        m_ShadowSpriteRenderer.material = m_ShadowMaterial;
        m_ShadowGameobject.transform.position = transform.position + (Vector3)m_ShadowOffset;
        m_ShadowGameobject.transform.rotation = transform.rotation;
    }
}