using System;
using UnityEngine;
using System.Linq;

/// <summary>
/// The sockets connects the logic gate with the cables connected cables.
/// </summary>
public class Socket : Gate
{
    /// <summary>
    /// Locks the gate inside the socket
    /// </summary>
    public bool locked = false;

    /// <summary>
    /// Material of the socket, if the gates male is true
    /// </summary>
    public Material onMaterial;
    /// <summary>
    /// Material of the socket, if the gates male is false
    /// </summary>
    public Material offMaterial;

    /// <summary>
    /// The connected gate
    /// </summary>
    [HideInInspector]
    public Gate gate;

    /// <summary>
    /// Sends the value of its female connectors foward the inputs of the gates
    /// and sets its own male output to the value of its gate output.
    /// </summary>
    public override void OnCircuitChanged()
    {
        if (gate != null)
        {
            // use smallest number of inputs of either the socket or gate
            int countIn = Math.Min(Inputs.Length, gate.Inputs.Length);

            // update the inputs of the gate
            for (int i = 0; i < countIn; i++)
            {
                gate.Inputs[i].value = Inputs[i].value;
                gate.Inputs[i].OnCircuitChanged();
            }

            // use smallest number of outputs of either the socket or gate
            int countOut = Math.Min(Outputs.Length, gate.Outputs.Length);

            // update the output of the socket
            for (int i = 0; i < countOut; i++)
            {
                Outputs[i].value = gate.Outputs[i].value;
                Outputs[i].OnCircuitChanged();
            }

            // disables draggable when the lock is enabled
            gate.GetComponent<Draggable>().enabled = !locked;
        }
        else
        {
            // if no gate is connected, disable the output of the socket
            foreach (GateOutput Out in Outputs)
            {
                Out.value = false;
                Out.OnCircuitChanged();
            }
        }

        // change material depending on values of the outputs
        GetComponent<Renderer>().material = Outputs.All(Output => Output.value) ? onMaterial : offMaterial;
    }
}
