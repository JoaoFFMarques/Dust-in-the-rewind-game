using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DropShadow : MonoBehaviour
{
    public Vector2 m_ShadowOffset;
    public Material m_ShadowMaterial;

    SpriteRenderer m_SpriteRenderer;
    GameObject m_ShadowGameobject;

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        m_ShadowGameobject = new GameObject("Shadow");

        SpriteRenderer shadowSpriteRenderer = m_ShadowGameobject.AddComponent<SpriteRenderer>();

        shadowSpriteRenderer.sprite = m_SpriteRenderer.sprite;
        shadowSpriteRenderer.material = m_ShadowMaterial;

        shadowSpriteRenderer.sortingLayerName = m_SpriteRenderer.sortingLayerName;
        shadowSpriteRenderer.sortingOrder = m_SpriteRenderer.sortingOrder - 1;
    }

    void LateUpdate()
    {
        m_ShadowGameobject.transform.localPosition = transform.localPosition + (Vector3)ShadowOffset;
        m_ShadowGameobject.transform.localRotation = transform.localRotation;
    }
}