using UnityEngine;

/// <summary>
/// Handles the collsions between sockets and gates.
/// Gates will animate to the right position inside the socket ring 
/// and the lasers of both the socket and the gates are enabled, if the associated in and ouput connectors are enabled
/// </summary>
public class SocketCollision : MonoBehaviour
{
    /// <summary>
    /// Moves the socket to the right position inside the socket
    /// and enables / disables the laserrs of both the socket and gates depending of thei current values
    /// </summary>
    /// <param name="other">Current collider</param>
    private void OnTriggerStay(Collider other)
    {
        Socket socket = GetComponent<Socket>();
        Gate gate = other.gameObject.GetComponent<Gate>();
        GateControl gateControl = other.GetComponent<GateControl>();

        // check if the collider is a gate and position it, if its not grabbed by the played
        if (gate != null && gateControl != null && !gateControl.isGrabbed && gate == socket.gate)
        {
            // animate the gate to the right position inside the socket
            float stepSize = 2 * Time.deltaTime;

            Vector3 sourcePosition = gate.transform.position;
            Vector3 targetPosition = transform.position;
            gate.transform.position = Vector3.MoveTowards(sourcePosition, targetPosition, stepSize);

            Quaternion sourceRotation = gate.transform.rotation;
            Quaternion targetRotation = transform.parent.rotation;
            gate.transform.rotation = Quaternion.Slerp(sourceRotation, targetRotation, stepSize);

            // enables / disables the socket laser to the gate
            for (int i = 0; i < socket.LaserOutputs.Length; i++)
                if (i < socket.gate.LaserInputs.Length)
                    socket.LaserOutputs[i].value = socket.Inputs[i].value;

            // enables / disables the gate laser to the socket
            for (int i = 0; i < socket.gate.LaserOutputs.Length; i++)
                if (i < socket.LaserInputs.Length)
                    socket.gate.LaserOutputs[i].value = socket.gate.Outputs[i].value;
        }
    }

    /// <summary>
    /// Connects the Socket to the gate, turns off the physics of the gate and enable the lasers
    /// </summary>
    /// <param name="other">The collider that just entered</param>
    private void OnTriggerEnter(Collider other)
    {
        Socket socket = GetComponent<Socket>();
        Gate gate = other.GetComponent<Gate>();

        if (gate != null && socket.gate == null)
        {
            socket.gate = gate;
            socket.OnCircuitChanged();

            // turn off the physics of the connected gate
            Rigidbody gateRigidbody = socket.gate.GetComponent<Rigidbody>();
            gateRigidbody.useGravity = false;
            gateRigidbody.isKinematic = true;

            // enable the socket laser aiming to the gates laser inputs
            for (int i = 0; i < socket.LaserOutputs.Length; i++)
                if (i < socket.gate.LaserInputs.Length)
                    socket.LaserOutputs[i].Enable(socket.gate.LaserInputs[i]);

            // enable the gates laser aiming to the sockets laser inputs
            for (int i = 0; i < socket.gate.LaserOutputs.Length; i++)
                if (i < socket.LaserInputs.Length)
                    socket.gate.LaserOutputs[i].Enable(socket.LaserInputs[i]);
        }
    }

    /// <summary>
    /// Disconnects the socket ot the gate, turns on the physcis of the gates and disables the lasers
    /// </summary>
    /// <param name="other">The collider that just leaved</param>
    private void OnTriggerExit(Collider other)
    {
        Socket socket = GetComponent<Socket>();
        Gate gate = other.GetComponent<Gate>();

        if (!socket.locked && gate == socket.gate && socket.gate != null)
        {
            // disable the socket laser aiming to the gates laser inputs
            foreach (LaserOutput LaserOut in socket.LaserOutputs)
                LaserOut.Disable();
            // disnable the gates laser aiming to the sockets laser inputs
            foreach (LaserOutput LaserOut in socket.gate.LaserOutputs)
                LaserOut.Disable();

            // disabled the iput connectors of the previously connected gate
            foreach (GateInput In in socket.gate.Inputs)
                In.value = false;

            socket.gate = null;
            socket.OnCircuitChanged();

            // turns on the physics of the previously connected gate
            Rigidbody gateRigidbody = other.GetComponent<Rigidbody>();
            gateRigidbody.useGravity = true;
            gateRigidbody.isKinematic = false;
        }
    }
}
