using HTC.UnityPlugin.Utility;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Resets the positions and rotations of all child GameObjects inside the Gameobject
/// </summary>
public class ResetStage : MonoBehaviour
{
    /// <summary>
    /// Cache for the original positoins
    /// </summary>
    private static List<Draggable> draggablesCache = new List<Draggable>();
    /// <summary>
    /// Saved RigidPoses of the child GameObjects
    /// </summary>
    private Dictionary<int, RigidPose> poseTable = new Dictionary<int, RigidPose>();

    /// <summary>
    /// Caches the transforms of the child GameObjects
    /// </summary>
    private void Awake()
    {
        draggablesCache.Clear();

        // get and save the RigidPoses of the children
        GetComponentsInChildren(draggablesCache);
        for (int i = 0, imax = draggablesCache.Count; i < imax; ++i)
        {
            Transform dt = draggablesCache[i].transform;
            poseTable[dt.GetInstanceID()] = new RigidPose(dt);
        }

        draggablesCache.Clear();
    }

    /// <summary>
    /// Resets the postions and rotations
    /// </summary>
    public void ResetPositions()
    {
        draggablesCache.Clear();

        GetComponentsInChildren(draggablesCache);
        for (int i = 0, imax = draggablesCache.Count; i < imax; ++i)
        {
            Transform movedTranform = draggablesCache[i].transform;
            RigidPose pose;

            // reset the RigidPose of the children to their original position and rotation
            if (poseTable.TryGetValue(movedTranform.GetInstanceID(), out pose))
            {
                movedTranform.position = pose.pos;
                movedTranform.rotation = pose.rot;

                // sets the velocities of the children to zero
                Rigidbody movedRigidBody = movedTranform.GetComponent<Rigidbody>();
                if (movedRigidBody != null)
                {
                    movedRigidBody.velocity = Vector3.zero;
                    movedRigidBody.angularVelocity = Vector3.zero;
                }
            }
        }

        draggablesCache.Clear();
    }
}