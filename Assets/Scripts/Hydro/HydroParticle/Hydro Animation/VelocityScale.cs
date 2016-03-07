using System.Collections;
using UnityEngine;

public class VelocityScale : MonoBehaviour
{
    [SerializeField]
    private Transform transformToScale;
    private Transform TransformToScale
    {
        get
        {
            if (transformToScale == null)
                transformToScale = gameObject.GetSafeComponent<Transform>();

            return transformToScale;
        }
    }

    [SerializeField]
    private Rigidbody2D rigidBody;
    private Rigidbody2D RigidBody
    {
        get
        {
            if (rigidBody == null)
                rigidBody = gameObject.GetSafeComponent<Rigidbody2D>();

            return rigidBody;
        }
    }

    [SerializeField]
    private Vector2 baseScale = Vector2.one;

    [SerializeField]
    private float scaleMultiplier = 0.03F;

    [SerializeField]
    private float velocityThreshold = 0.5F;

    private void Start()
    {
        StartCoroutine(UpdateVelocityScale());
    }

    private IEnumerator UpdateVelocityScale()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            if (TransformToScale == null)
            {
                continue;
            }
            else if (RigidBody.velocity.magnitude < velocityThreshold)
            {
                TransformToScale.localScale = Vector3.one;
                continue;
            }

            Vector2 scale = baseScale;
            float scaleModifier = Mathf.Min(Mathf.Abs(RigidBody.velocity.y) * scaleMultiplier, 0.5F);
            scale.x -= scaleModifier;
            scale.y += scaleModifier;

            TransformToScale.localScale = scale;
        }
    }
}