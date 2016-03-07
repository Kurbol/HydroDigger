using System.Collections;
using UnityEngine;

public class LiquidBehavior : HydroBehavior
{
    private const float colorFadeRate = 0.2F;

    private float colorFadePercent;

    public override void InitializeState()
    {
        gameObject.layer = LayerMask.NameToLayer("Metaball");
        Physics = HydroManager.Liquid.Physics;

        colorFadePercent = 0;
        StopCoroutine("FadeInColor");
        StartCoroutine("FadeInColor");

        StopCoroutine("UpdatePhysics");
        StartCoroutine("UpdatePhysics");
    }

    protected override void UpdatePhysicsBehavior()
    {
        return;
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

    private IEnumerator UpdatePhysics()
    {
        while (true)
        {
            Coordinate coordinate = MapManager.Map.GetCoordinateFromPosition(transform.position);
            SoilType soilType = MapManager.Map[coordinate.X, coordinate.Y];

            if (soilType == SoilType.None)
                Physics = HydroManager.Liquid.Physics;
            else
                Physics = soilType.SoilTypeMetadata().FluidPhysics;

            yield return new WaitForFixedUpdate();
        }
    }
}