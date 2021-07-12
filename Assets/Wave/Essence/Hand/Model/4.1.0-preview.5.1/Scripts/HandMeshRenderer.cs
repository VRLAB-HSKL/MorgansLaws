// "Wave SDK 
// © 2020 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the WaveVR SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using UnityEngine;
using Wave.Native;
using Wave.Essence.Events;

namespace Wave.Essence.Hand.Model
{
	public class BoneMap
	{
		public BoneMap(HandManager.HandJoint b, string name, HandManager.HandJoint p)
		{
			BoneID = b;
			DisplayName = name;
			BoneParentID = p;
			BoneDiffFromParent = Vector3.zero;
		}

		public HandManager.HandJoint BoneID;
		public string DisplayName;
		public HandManager.HandJoint BoneParentID;
		public Vector3 BoneDiffFromParent;
	}

	[DisallowMultipleComponent]
	public class HandMeshRenderer : MonoBehaviour
	{
		private const string LOG_TAG = "Wave.Essence.Hand.Model.HandMeshRenderer";
		private const int BONE_MAX_ID = 26;
		private bool mEnabled = false;

		#region Name Definition
		// The order of joint name MUST align with runtime's definition
		private readonly string[] BNames = new string[]
		{
				"WaveBone_0",  // WVR_HandJoint_Palm = 0
				"WaveBone_1", // WVR_HandJoint_Wrist = 1
				"WaveBone_2", // WVR_HandJoint_Thumb_Joint0 = 2
				"WaveBone_3", // WVR_HandJoint_Thumb_Joint1 = 3
				"WaveBone_4", // WVR_HandJoint_Thumb_Joint2 = 4
				"WaveBone_5", // WVR_HandJoint_Thumb_Tip = 5
				"WaveBone_6", // WVR_HandJoint_Index_Joint0 = 6
				"WaveBone_7", // WVR_HandJoint_Index_Joint1 = 7
				"WaveBone_8", // WVR_HandJoint_Index_Joint2 = 8
				"WaveBone_9", // WVR_HandJoint_Index_Joint3 = 9
				"WaveBone_10", // WVR_HandJoint_Index_Tip = 10
				"WaveBone_11", // WVR_HandJoint_Middle_Joint0 = 11
				"WaveBone_12", // WVR_HandJoint_Middle_Joint1 = 12
				"WaveBone_13", // WVR_HandJoint_Middle_Joint2 = 13
				"WaveBone_14", // WVR_HandJoint_Middle_Joint3 = 14
				"WaveBone_15", // WVR_HandJoint_Middle_Tip = 15
				"WaveBone_16", // WVR_HandJoint_Ring_Joint0 = 16
				"WaveBone_17", // WVR_HandJoint_Ring_Joint1 = 17
				"WaveBone_18", // WVR_HandJoint_Ring_Joint2 = 18
				"WaveBone_19", // WVR_HandJoint_Ring_Joint3 = 19
				"WaveBone_20", // WVR_HandJoint_Ring_Tip = 20
				"WaveBone_21", // WVR_HandJoint_Pinky_Joint0 = 21
				"WaveBone_22", // WVR_HandJoint_Pinky_Joint0 = 22
				"WaveBone_23", // WVR_HandJoint_Pinky_Joint0 = 23
				"WaveBone_24", // WVR_HandJoint_Pinky_Joint0 = 24
				"WaveBone_25" // WVR_HandJoint_Pinky_Tip = 25
		};
		#endregion

		public BoneMap[] boneMap = new BoneMap[]
		{
			new BoneMap(HandManager.HandJoint.Palm, "Palm", HandManager.HandJoint.Palm),  // 0
			new BoneMap(HandManager.HandJoint.Wrist, "Wrist", HandManager.HandJoint.Wrist),// 1
			new BoneMap(HandManager.HandJoint.Thumb_Joint0, "Thumb root", HandManager.HandJoint.Wrist), // 2
			new BoneMap(HandManager.HandJoint.Thumb_Joint1, "Thumb joint1", HandManager.HandJoint.Thumb_Joint0), // 3
			new BoneMap(HandManager.HandJoint.Thumb_Joint2, "Thumb joint2", HandManager.HandJoint.Thumb_Joint1), // 4
			new BoneMap(HandManager.HandJoint.Thumb_Tip, "Thumb tip", HandManager.HandJoint.Thumb_Joint2), // 5
			new BoneMap(HandManager.HandJoint.Index_Joint0, "Index root", HandManager.HandJoint.Wrist),  // 6
			new BoneMap(HandManager.HandJoint.Index_Joint1, "Index joint1", HandManager.HandJoint.Index_Joint0), // 7
			new BoneMap(HandManager.HandJoint.Index_Joint2, "Index joint2", HandManager.HandJoint.Index_Joint1), // 8
			new BoneMap(HandManager.HandJoint.Index_Joint3, "Index joint3", HandManager.HandJoint.Index_Joint2), // 9
			new BoneMap(HandManager.HandJoint.Index_Tip, "Index tip", HandManager.HandJoint.Index_Joint3), // 10
			new BoneMap(HandManager.HandJoint.Middle_Joint0, "Middle root", HandManager.HandJoint.Wrist), // 11
			new BoneMap(HandManager.HandJoint.Middle_Joint1, "Middle joint1", HandManager.HandJoint.Middle_Joint0), // 12
			new BoneMap(HandManager.HandJoint.Middle_Joint2, "Middle joint2", HandManager.HandJoint.Middle_Joint1), // 13
			new BoneMap(HandManager.HandJoint.Middle_Joint3, "Middle joint3", HandManager.HandJoint.Middle_Joint2), // 14
			new BoneMap(HandManager.HandJoint.Middle_Tip, "Middle tip", HandManager.HandJoint.Middle_Joint3), // 15
			new BoneMap(HandManager.HandJoint.Ring_Joint0, "Ring root", HandManager.HandJoint.Wrist), // 16
			new BoneMap(HandManager.HandJoint.Ring_Joint1, "Ring joint1", HandManager.HandJoint.Ring_Joint0), // 17
			new BoneMap(HandManager.HandJoint.Ring_Joint2, "Ring joint2", HandManager.HandJoint.Ring_Joint1), // 18
			new BoneMap(HandManager.HandJoint.Ring_Joint3, "Ring joint3", HandManager.HandJoint.Ring_Joint2), // 19
			new BoneMap(HandManager.HandJoint.Ring_Tip, "Ring tip", HandManager.HandJoint.Ring_Joint3), // 20
			new BoneMap(HandManager.HandJoint.Pinky_Joint0, "Pinky root", HandManager.HandJoint.Wrist), // 21
			new BoneMap(HandManager.HandJoint.Pinky_Joint1, "Pinky joint1", HandManager.HandJoint.Pinky_Joint0), // 22
			new BoneMap(HandManager.HandJoint.Pinky_Joint2, "Pinky joint2", HandManager.HandJoint.Pinky_Joint1), // 23
			new BoneMap(HandManager.HandJoint.Pinky_Joint3, "Pinky joint3", HandManager.HandJoint.Pinky_Joint2), // 24
			new BoneMap(HandManager.HandJoint.Pinky_Tip, "Pinky tip", HandManager.HandJoint.Pinky_Joint3), // 25
		};

		//public enum PreferTrackerType
		//{
		//	HandManagerDefined,
		//	Natural,
		//	Electronic
		//}

		private const float minAlpha = 0.2f;

		[Tooltip("Draw left hand if true, right hand otherwise")]
		public bool IsLeft = false;
		[Tooltip("Show electronic hand in controller mode")]
		public bool showElectronicHandInControllerMode = false;
		[Tooltip("Root object of skinned mesh")]
		public GameObject Hand;
		[Tooltip("Type of hand tracker")]
		public HandManager.TrackerType tracker = HandManager.TrackerType.Natural;
		private HandManager.TrackerType trackerInUsed = HandManager.TrackerType.Natural;
		private bool isStartHandTracking = false;
		private bool needRestartHandTracking = false;

		[Tooltip("Use skeleton, mesh and pose from runtime")]
		public bool useRuntimeModel = false;
		[Tooltip("Nodes of skinned mesh, must be size of 26 in same order as skeleton definition")]
		public Transform[] BonePoses = new Transform[BONE_MAX_ID];
		[Tooltip("Use hand confidence as alpha, low confidence hand becomes transparent")]
		public bool showConfidenceAsAlpha = false;
		public bool alreadyDetect = false;
		[HideInInspector]
		public bool checkInteractionMode = false;

		Quaternion rot;
		Vector3 pos;

		private Transform FindChildRecursive(Transform parent, string name)
		{
			foreach (Transform child in parent)
			{
				if (child.name.Contains(name))
					return child;

				var result = FindChildRecursive(child, name);
				if (result != null)
					return result;
			}
			return null;
		}

		public void AutoDetect()
		{
			PrintDebug("AutoDetect: IsLeft = " + IsLeft);
			var skinnedMesh = transform.GetComponentInChildren<SkinnedMeshRenderer>();
			if (skinnedMesh == null)
			{
				PrintDebug("Cannot find SkinnedMeshRenderer in " + name);
				return;
			}
			Hand = skinnedMesh.gameObject;

			for (int i = 0; i < boneMap.Length; i++)
			{
				string searchName = BNames[i];
				Transform t = FindChildRecursive(transform, searchName);

				if (t == null)
				{
					PrintDebug(boneMap[i].DisplayName + " not found!");
					continue;
				}

				PrintDebug(boneMap[i].DisplayName + " found: " + searchName);
				BonePoses[i] = t;
			}
			alreadyDetect = true;
		}

		public void ClearDetect()
		{
			for (int i = 0; i < BonePoses.Length; i++)
			{
				BonePoses[i] = null;
			}
			alreadyDetect = false;
		}

		void updateBonePose()
		{
			for (int i = 2; i < BonePoses.Length; i++) //  0 is pslm, 1 is wrist
			{
				if (BonePoses[i])
				{
					// ignore position
					//if (HandManager.Instance.GetJointPosition(boneMap[i].BoneID, ref pos, IsLeft))
					//{
					//	BonePoses[i].transform.position = pos;
					//} else
					//{
					//	Log.gpl.d(LOG_TAG, BonePoses[i].transform.name + " no pose");
					//}

					if (GetJointRotation(boneMap[i].BoneID, ref rot, IsLeft))
					{
						BonePoses[i].transform.rotation = rot;
						BonePoses[i].transform.Rotate(0, 180, 0); // hand model is based on OpenGL coordinates, need to rotate.

						//Vector3 w = BonePoses[i].transform.localEulerAngles;
						//PrintGPLDebug(BonePoses[i].transform.name + " rot 180: " + w.x + " ," + w.y + " ," + w.z);
					}
					else
					{
						// use translate to simulate rotation
						//Log.gpl.d(LOG_TAG, BonePoses[i].transform.name + " no rotation");
					}
				}
			}
		}

		private bool GetJointPosition(HandManager.HandJoint joint, ref Vector3 position, bool isLeft)
		{
			return HandManager.Instance.GetJointPosition(trackerInUsed, joint, ref position, isLeft);
		}

		private bool GetJointRotation(HandManager.HandJoint joint, ref Quaternion rotation, bool isLeft)
		{
			return HandManager.Instance.GetJointRotation(trackerInUsed, joint, ref rotation, isLeft);
		}

		private float GetHandConfidence(bool isLeft)
		{
			return HandManager.Instance.GetHandConfidence(trackerInUsed, isLeft);
		}

		void OnApplicationPause(bool pauseStatus)
		{
			if (pauseStatus) // pause
			{
				// Pause - stop hand tracking if started
				if (isStartHandTracking) {
					PrintDebug("Pause - stop hand tracking and set needRestartHandTracking to true");
					StopHandTracking();
					needRestartHandTracking = true;
				}
			}
			else
			{
				if (needRestartHandTracking)
				{
					PrintDebug("Resume - restart hand tracking, type is " + trackerInUsed);
					StartHandTracking(trackerInUsed);
					needRestartHandTracking = false;
				}
			}
		}

		XR_InteractionMode preInteractionMode = XR_InteractionMode.Default;
		XR_InteractionMode currInteractionMode;

		bool showECHand = false;
		void Update()
		{
			bool showHand = false;

			if (Hand == null)
				return;

			if (hasIMManager)
			{
				currInteractionMode = ClientInterface.InteractionMode;

				if (currInteractionMode != preInteractionMode)
				{
					PrintDebug("Interaction mode changed to " + currInteractionMode);
					preInteractionMode = currInteractionMode;

					if (currInteractionMode == XR_InteractionMode.Gaze)
					{
						PrintDebug("Stop hand tracking if started due to Gaze mode ");
						StopHandTracking();
					}

					if (currInteractionMode == XR_InteractionMode.Hand)
					{
						if (isStartHandTracking)
						{
							if (trackerInUsed != HandManager.TrackerType.Natural)
							{
								PrintDebug("Stop hand tracking in used and restart " + trackerInUsed);
								StopHandTracking();
								StartHandTracking(HandManager.TrackerType.Natural);
							}
						} else
						{
							PrintDebug("Start Natural hand tracking in hand mode");
							StartHandTracking(HandManager.TrackerType.Natural);
						}
					}

					if (currInteractionMode == XR_InteractionMode.Controller)
					{
						// show electronic hand?
						bool isSupported = Interop.WVR_ControllerSupportElectronicHand();

						showECHand = (isSupported && showElectronicHandInControllerMode);

						PrintDebug("Device support electronic hand? " + isSupported + ", show electronic hand in controller mode? " + showElectronicHandInControllerMode);

						if (showECHand)
						{
							if (isStartHandTracking)
							{
								if (trackerInUsed != HandManager.TrackerType.Electronic)
								{
									PrintDebug("Stop hand tracking in used and restart " + trackerInUsed);
									StopHandTracking();
									StartHandTracking(HandManager.TrackerType.Electronic);
								}
							}
							else
							{
								PrintDebug("Start electronic hand tracking in controller mode");
								StartHandTracking(HandManager.TrackerType.Electronic);
							}
						} else
						{
							PrintDebug("Stop hand tracking since don't show hand in controller mode");
							StopHandTracking();
						}
					}
				}

				if (ClientInterface.InteractionMode == XR_InteractionMode.Hand)
				{
					showHand = ((HandManager.Instance != null) &&
						(HandManager.Instance.IsHandPoseValid(HandManager.TrackerType.Natural, IsLeft)) &&
						ClientInterface.IsFocused);
				}

				if (ClientInterface.InteractionMode == XR_InteractionMode.Controller)
				{
					showHand = (showECHand && (HandManager.Instance != null) &&
						(HandManager.Instance.IsHandPoseValid(HandManager.TrackerType.Electronic, IsLeft)) &&
						ClientInterface.IsFocused);
				}
			} else
			{
				showHand = ((HandManager.Instance != null) &&
							(HandManager.Instance.IsHandPoseValid(trackerInUsed, IsLeft)) &&
							ClientInterface.IsFocused);
			}

			if (HandManager.Instance != null)
			{
				PrintGPLDebug("Pose is valid: " + (HandManager.Instance.IsHandPoseValid(trackerInUsed, IsLeft)));
			}

			PrintGPLDebug("Check interaction Mode: " + hasIMManager + " trackerInUsed: " + trackerInUsed + ", showHand: " + showHand + ", has focus: " + ClientInterface.IsFocused);

			Hand.SetActive(showHand);

			if (!showHand)
				return;

			if (GetJointPosition(HandManager.HandJoint.Wrist, ref pos, IsLeft))
			{
				//Log.gpl.d(LOG_TAG, "wrist pos: " + this.transform.position.x + " ," + this.transform.position.y + " ," + this.transform.position.z);
				this.transform.localPosition = pos;
			} else
			{
				PrintGPLDebug("Invalid wrist position");
			}

			if (GetJointRotation(HandManager.HandJoint.Wrist, ref rot, IsLeft))
			{
				this.transform.rotation = rot;
				this.transform.Rotate(0, 180, 0);  // hand model is based on OpenGL coordinates, need to rotate.
			} else
			{
				PrintGPLDebug("Invalid wrist rotation");
			}

			updateBonePose();

			if (showConfidenceAsAlpha)
			{
				float conValue = GetHandConfidence(IsLeft);

				PrintGPLDebug("Confidence value: " + conValue);

				var color = Hand.GetComponent<Renderer>().material.color;
				color.a = conValue > minAlpha ? conValue : minAlpha;
				Hand.GetComponent<Renderer>().material.color = color;
			}
		}

		private void StartHandTracking(HandManager.TrackerType newTracker)
		{
			if (!isStartHandTracking)
			{
				if (HandManager.Instance != null)
				{
					PrintDebug("Start " + newTracker + " hand tracking");
					HandManager.Instance.StartHandTracker(newTracker);
					trackerInUsed = newTracker;
					isStartHandTracking = true;
				}
			}
		}

		private void StopHandTracking()
		{
			if (isStartHandTracking)
			{
				if (HandManager.Instance != null)
				{
					PrintDebug("Stop " + trackerInUsed + " hand tracking");
					HandManager.Instance.StopHandTracker(trackerInUsed);
					isStartHandTracking = false;
				}
			}
		}

		private void PrintDebug(string msg)
		{
			Log.d(LOG_TAG, (IsLeft ? "Left" : "Right") + ", " + msg, true);
		}

		private void PrintGPLDebug(string msg)
		{
			if (Log.gpl.Print)
				Log.d(LOG_TAG, (IsLeft ? "Left" : "Right") + ", " + msg, true);
		}

		void OnEnable()
		{
			if (!mEnabled)
			{
				if (BonePoses.Length != boneMap.Length)
				{
					PrintDebug("Length of BonePoses is not equal to length of boneMap, skip!");
					return;
				}

				if (!alreadyDetect)
				{
					PrintDebug("Not detect yet, do it.");
					AutoDetect();
				}
				else
				{
					PrintDebug("Aready detect, keep the setting.");

					for (int i = 0; i < boneMap.Length; i++)
					{
						PrintDebug(boneMap[i].DisplayName + " --> " + BonePoses[i].name);
					}
				}

				// start hand tracking
				//if (!checkInteractionMode)
					StartHandTracking(tracker);

				GeneralEvent.Listen(GeneralEvent.INTERACTION_MODE_MANAGER_READY, OnInteractionModeManagerReady);

				mEnabled = true;
			}
		}

		void OnDisable()
		{
			if (mEnabled)
			{
				// stop hand tracking
				StopHandTracking();

				GeneralEvent.Remove(GeneralEvent.INTERACTION_MODE_MANAGER_READY, OnInteractionModeManagerReady);

				mEnabled = false;
			}
		}

		// Start is called before the first frame update
		void Start()
		{

		}

		private bool hasIMManager = false;
		private void OnInteractionModeManagerReady(params object[] args)
		{
			hasIMManager = true;
		}
	}
}
