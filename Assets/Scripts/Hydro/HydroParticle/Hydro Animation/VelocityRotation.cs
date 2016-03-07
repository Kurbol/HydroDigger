using System.Collections;
using UnityEngine;

public class VelocityRotation : MonoBehaviour
{
    [SerializeField]
    private Transform transformToRotate;
    private Transform TransformToRotate
    {
        get
        {
            if (transformToRotate == null)
                transformToRotate = gameObject.GetSafeComponent<Transform>();

            return transformToRotate;
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

    private void Start()
    {
        StartCoroutine(UpdateRotation());
    }

    private IEnumerator UpdateRotation()
    {
        while (true)
        {
            yield return null;

            if (TransformToRotate == null || RigidBody.velocity == Vector2.zero)
            {
                continue;
            }

            TransformToRotate.rotation = Quaternion.LookRotation(Vector3.forward, RigidBody.velocity);
        }
    }
}