using System.Collections;
using UnityEngine;

public abstract class HydroBehavior : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidBody;
    public Rigidbody2D Rigidbody
    {
        get
        {
            if (rigidBody == null)
            {
                rigidBody = gameObject.GetSafeComponent<Rigidbody2D>();
            }

            return rigidBody;
        }
    }

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer
    {
        get
        {
            if (spriteRenderer == null)
            {
                spriteRenderer = gameObject.GetSafeComponent<SpriteRenderer>();
            }

            return spriteRenderer;
        }
    }

    public abstract void InitializeState();

    public void StartBehavior()
    {
        StartCoroutine("RunPhysicsBehavior");
        StartCoroutine("RunGraphicsBehavior");
    }

    public void StopBehavior()
    {
        StopCoroutine("RunPhysicsBehavior");
        StopCoroutine("RunGraphicsBehavior");
    }

    private IEnumerator RunPhysicsBehavior()
    {
        while (true)
        {
            UpdatePhysicsBehavior();

            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator RunGraphicsBehavior()
    {
        while (true)
        {
            UpdateGraphicsBehavior();

            yield return new WaitForEndOfFrame();
        }
    }

    protected abstract void UpdatePhysicsBehavior();

    protected abstract void UpdateGraphicsBehavior();
}