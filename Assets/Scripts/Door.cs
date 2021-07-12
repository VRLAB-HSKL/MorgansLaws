using UnityEngine;
using System.Linq;

/// <summary>
/// Doors separate the stages inside a scene. 
/// There are three with an input connector to the left, right or both sides of the door. 
/// The goal of each stage is to open the doors by setting the value of the inputs to true.
/// </summary>
public class Door : Gate
{
    /// <summary>
    /// The plane to move down- and upwards depending on the value of the doors inputs 
    /// </summary>
    public GameObject DoorPlane;
    /// <summary>
    /// The speed of the moving door plane
    /// </summary>
    public float DoorSpeed = 1.5f;

    /// <summary>
    /// The display on top of the door to show if the door is open or closed
    /// </summary>
    public GameObject Display;
    /// <summary>
    /// The material for the display, when the value of the doors inputs are true
    /// </summary>
    public Material DisplayOnMaterial;
    /// <summary>
    /// The material for the display, when the value of the doors inputs are false
    /// </summary>
    public Material DisplayOffMaterial;
    /// <summary>
    /// The horizontal speed of the moving display material
    /// </summary>
    public float displaySpeedX = 0.27f;
    /// <summary>
    /// The vertical speed of the moving display material
    /// </summary>
    public float displaySpeedY = 0.0f;

    /// <summary>
    /// The current x position of the moving display material
    /// </summary>
    private float displayX;
    /// <summary>
    /// The current y position of the moving display material
    /// </summary>
    private float displayY;

    /// <summary>
    /// The door can be blocked, even if all inputs are enabled
    /// This happens, when the player moved to the next stage
    /// </summary>
    [HideInInspector]
    public bool blocked;

    /// <summary>
    /// Get the starting positions of the moving display material
    /// </summary>
    private void Start()
    {
        displayX = Display.GetComponent<Renderer>().material.mainTextureOffset.x;
        displayY = Display.GetComponent<Renderer>().material.mainTextureOffset.y;
    }

    /// <summary>
    /// Replace the material of the display, depending its inputs are all enabled
    /// </summary>
    public override void OnCircuitChanged()
    {
        if (DisplayOnMaterial != null && DisplayOffMaterial != null)
            Display.GetComponent<Renderer>().material = Inputs.All(Input => Input.value) ? DisplayOnMaterial : DisplayOffMaterial;
    }

    /// <summary>
    /// Change the material of the display and move the door plane up and down, depending on the inputs of the door
    /// </summary>
    private void Update()
    {
        OnCircuitChanged();

        // animate the door plane
        if (DoorPlane != null && Inputs?.Length > 0)
        {
            float zPos = !blocked && Inputs.All(Input => Input.value) ? -0.0333f : 0.0f;
            float stepSize = DoorSpeed * Time.deltaTime;

            Vector3 sourcePosition = DoorPlane.transform.localPosition;
            Vector3 targetPosition = new Vector3(0.0f, 0.0f, zPos);
            DoorPlane.transform.localPosition = Vector3.Lerp(sourcePosition, targetPosition, stepSize);

            // animate the display material
            if (DisplayOnMaterial != null && DisplayOffMaterial != null)
            {
                displayX += displaySpeedX * Time.deltaTime;
                displayY += displaySpeedY * Time.deltaTime;
                Display.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(displayX, displayY));
            }
        }
    }
}