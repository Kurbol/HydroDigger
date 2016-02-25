using System.Collections;
using UnityEngine;

public class LiquidBehavior : HydroBehavior
{
    private const float colorFadeRate = 0.2F;

    private float colorFadePercent;

    public override void InitializeState()
    {
        gameObject.layer = LayerMask.NameToLayer("Metaball");
        Rigidbody.gravityScale = HydroManager.Liquid.Physics.GravityScale;
        Rigidbody.angularDrag = HydroManager.Liquid.Physics.AngularDrag;
        Rigidbody.mass = HydroManager.Liquid.Physics.Mass;

        colorFadePercent = 0;
        StopCoroutine("FadeInColor");
        StartCoroutine("FadeInColor");
    }

    protected override void UpdatePhysicsBehavior()
    {
        if (Rigidbody == null)
            return;

        if (Rigidbody.velocity.magnitude > HydroManager.Liquid.Physics.MaximumVelocity)
            Rigidbody.velocity = Rigidbody.velocity.normalized * HydroManager.Liquid.Physics.MaximumVelocity;
    }

    protected override void UpdateGraphicsBehavior()
    {
        if (SpriteRenderer == null || colorFadePercent < 1)
            return;

        SpriteRenderer.color = HydroManager.Liquid.Color;
    }

    private IEnumerator FadeInColor()
    {
        Color startingColor = HydroManager.Liquid.Color;
        startingColor.a = 0.35F;

        while (colorFadePercent < 1)
        {
            SpriteRenderer.color = Color.Lerp(startingColor, HydroManager.Liquid.Color, Mathf.Clamp01(colorFadePercent));
            colorFadePercent += colorFadeRate * Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
    }
}