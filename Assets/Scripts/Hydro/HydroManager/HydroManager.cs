using UnityEngine;

public class HydroManager : Singleton<HydroManager>
{
    [SerializeField]
    private bool showGizmos;

    [SerializeField]
    private Liquid liquidProperties;
    public static Liquid Liquid
    {
        get
        {
            return Instance.liquidProperties;
        }
    }
}