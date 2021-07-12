// "Wave SDK 
// © 2020 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the Wave SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using UnityEngine;
using UnityEngine.UI;
using Wave.Native;
using Wave.Essence.Hand;

namespace Wave.Essence.InputModule.Demo
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Text))]
	public sealed class CurrentTrackerText : MonoBehaviour
	{
		const string LOG_TAG = "Wave.Essence.InputModule.Demo.CurrentTrackerText";
		private void DEBUG(string msg)
		{
			if (Log.EnableDebugLog)
				Log.d(LOG_TAG, msg, true);
		}

		private Text m_Text = null;
		void Start()
		{
			m_Text = gameObject.GetComponent<Text>();
		}

		void Update()
		{
			if (HandManager.Instance != null && m_Text != null)
			{
				HandManager.TrackerType tracker = HandManager.TrackerType.Natural;
				if (HandManager.Instance.GetPreferTracker(ref tracker))
				{
					m_Text.text = "Current Tracker: " + tracker;
				}
			}
		}
	}
}