using UnityEngine;

/// <summary>
/// Gates resembles everything that can be put inside the circuit
/// </summary>
public class Gate : MonoBehaviour
{
	/// <summary>
	/// All input (female) connectors of the gate
	/// </summary>
	public GateInput[] Inputs { get; private set; }
	/// <summary>
	/// All output (male) connectors of the gate
	/// </summary>
	public GateOutput[] Outputs { get; private set; }

	/// <summary>
	/// All inputs for the laser inside the socket ring
	/// </summary>
	public LaserInput[] LaserInputs { get; private set; }
	/// <summary>
	/// All outputs for the laser inside the socket ring
	/// </summary>
	public LaserOutput[] LaserOutputs { get; private set; }

	/// <summary>
	/// Set the in and output connectors and laser in and outputs
	/// </summary>
	private void Awake()
	{
		Inputs = GetComponentsInChildren<GateInput>();
		Outputs = GetComponentsInChildren<GateOutput>();

		LaserInputs = GetComponentsInChildren<LaserInput>();
		LaserOutputs = GetComponentsInChildren<LaserOutput>();
	}

	private void Update()
	{
		OnCircuitChanged();
	}

	// the default behaviour if no inherit overrides this method is to update the outputs
	public virtual void OnCircuitChanged()
	{
		if (Outputs == null)
			return;

		foreach (GateOutput Out in Outputs)
			Out.OnCircuitChanged();
	}
}
