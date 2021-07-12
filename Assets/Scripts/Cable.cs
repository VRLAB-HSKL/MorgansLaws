using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Cable : MonoBehaviour
{
    public CableIO[] InOutputs { get; private set; }

    private void Awake()
    {
        InOutputs = GetComponentsInChildren<CableIO>();
    }

    public void OnCircuitChanged()
    {
        CableIO Input = null, Output = null;

        if (InOutputs[0].InOutput?.GetType() == typeof(GateOutput))
        {
            Input = InOutputs[0];
            Output = InOutputs[1];
        }
        else if (InOutputs[1].InOutput?.GetType() == typeof(GateOutput))
        {
            Input = InOutputs[1];
            Output = InOutputs[0];
        }

        if (Input != null && Output != null)
        {
            Input.value = Input.InOutput != null && Input.InOutput.value;
            Output.value = Input.value;
        }
        else
        {
            foreach (var InOutput in InOutputs)
                InOutput.value = false;
        }
    }
}
