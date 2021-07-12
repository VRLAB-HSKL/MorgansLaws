using UnityEngine;
using System;

/// <summary>
/// If the player touches the activated portal a new scene is loaded
/// </summary>
[Obsolete("Not yet used currently for VR", true)]
public class ScenePortal : Gate
{
    protected Color onColor = Color.green;
    protected Color offColor = Color.red;

    private Renderer portalRenderer;

    private void Start()
    {
        portalRenderer = this.transform.Find("doorframe/portal_plane").GetComponent<Renderer>();
    }

    public override void OnCircuitChanged()
	{
        if (portalRenderer != null && Inputs?.Length > 0)
            portalRenderer.material.color = Inputs[0].value ? onColor : offColor;
    }
        
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.GetComponentInParent<VivePoseTracker>() != null &&
    //        Inputs != null &&
    //        Inputs.All(Input => Input.value))
    //    {
    //        GameObject stageLoader = GameObject.Find("StageLoader");
    //        if (stageLoader != null)
    //            stageLoader.GetComponent<StageLoader>()?.LoadNextStage();
    //    }
    //}
}
