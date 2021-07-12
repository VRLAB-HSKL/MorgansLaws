using UnityEngine;

/// <summary>
/// The base class for all connectors for all kind of gates
/// </summary>
public class GateIO : MonoBehaviour
{
    /// <summary>
    /// Determines if the connector is enabled
    /// </summary>
    public bool value = true;

    /// <summary>
    /// The parent gate of the connector
    /// </summary>
    [HideInInspector]
    public Gate gate;

    /// <summary>
    /// Get the parent gate of the connector
    /// </summary>
    private void Start()
    {
        gate = GetComponentInParent<Gate>();
    }

    private void Update()
    {
        OnCircuitChanged();
    }

    /// <summary>
    /// This virtual method can be overritten by any kind of connector
    /// </summary>
    public virtual void OnCircuitChanged()
    {

    }
}
