using UnityEngine;

/// <summary>
/// The connectors and start / end point of a cable
/// if used ast start point, the metrial of the cable is set here.
/// </summary>
public class CableIO : MonoBehaviour
{
    /// <summary>
    /// value of the connector
    /// </summary>
    public bool value;

    /// <summary>
    /// Other connector of the cable.
    /// This could either be the start (female) or end (male) point of the cable
    /// </summary>
    public GateIO InOutput;

    /// <summary>
    /// The material of the cable if the value of the start point is true
    /// </summary>
    public Material cableOnMaterial;
    /// <summary>
    /// The material of the cable if the value of the start point is false
    /// </summary>
    public Material cableOffMaterial;

    /// <summary>
    /// The parent cable of the connector
    /// </summary>
    [HideInInspector]
    public Cable cable;

    /// <summary>
    /// Gets the parent cable
    /// </summary>
    private void Start()
    {
        cable = GetComponentInParent<Cable>();
    }

    private void Update()
    {
        OnCircuitChanged();
    }

    /// <summary>
    /// When disconecting the cable, it sets the connected inputs value to false
    /// and updates the cable
    /// </summary>
    /// <param name="other">The collider that was leaved</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<GateIO>() == InOutput)
        {
            // disable the previously connected input
            if (InOutput?.GetType() == typeof(GateInput))
                InOutput.value = false;

            InOutput = null;

            // updte the cable
            GetComponentInParent<Cable>().OnCircuitChanged();
        }
    }

    /// <summary>
    /// Update the cable connector and snap it to the attached connector
    /// </summary>
    /// <param name="other">The collider that was entered</param>
    void OnTriggerStay(Collider other)
    {
        GateIO collider = other.GetComponent<GateIO>();

        if (collider != null)
        {
            // Replace the current connector with new one, if the the collider hits two connectors
            if (InOutput != null && !InOutput.Equals(collider))
                OnTriggerExit(InOutput.gameObject.GetComponent<Collider>());

            InOutput = collider;

            OnCircuitChanged();

            // snap to the position and rotation of the new connector
            if (other.GetComponentInParent<Socket>() != null || 
                other.GetComponentInParent<Door>() != null || 
                other.GetComponentInParent<Power>() != null)
            {
                this.transform.position = other.gameObject.transform.position;
                this.transform.localRotation = other.gameObject.transform.rotation;
                this.transform.Rotate(new Vector3(0, 180, 0));
                this.transform.Translate(new Vector3(0, 0, -0.002f));
            }
        }
    }

    /// <summary>
    /// Update the connector depending if its an output or input
    /// </summary>
    public virtual void OnCircuitChanged()
    {
        // Update the material of the cable
        CableComponent cableComponent = GetComponent<CableComponent>();
        if (cableComponent != null)
        {
            if (value)
            {
                cableComponent.setMaterial(cableOnMaterial);
            }
            else
            {
                cableComponent.setMaterial(cableOffMaterial);
            }
        }
        
        // if an output is connected, update the cable
        if (InOutput?.GetType() == typeof(GateOutput))
        {
            GetComponentInParent<Cable>().OnCircuitChanged();
        }
        // if an input is connected, update the connector
        else if (InOutput?.GetType() == typeof(GateInput))
        {
            if (InOutput == null || InOutput.value == value)
                return;

            InOutput.value = value;
            InOutput.OnCircuitChanged();
        }
    }
}