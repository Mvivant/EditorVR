﻿#if UNITY_EDITOR
using System;
using UnityEngine;

namespace UnityEditor.Experimental.EditorVR
{
	/// <summary>
	/// Gives decorated class the ability to use snapping
	/// </summary>
	public interface IUsesSnapping
	{
	}

	public static class IUsesSnappingMethods
	{
		internal delegate bool ManipulatorSnappingDelegate(Transform rayOrigin, GameObject[] objects, ref Vector3 position, ref Quaternion rotation, Vector3 delta);
		internal delegate bool DirectSnappingDelegate(Transform rayOrigin, GameObject go, ref Vector3 position, ref Quaternion rotation, Vector3 targetPosition, Quaternion targetRotation);

		internal static ManipulatorSnappingDelegate manipulatorSnapping { get; set; }
		internal static DirectSnappingDelegate directSnapping { get; set; }
		internal static Action<Transform> clearSnappingState { get; set; }

		/// <summary>
		/// Perform manipulator snapping: Translate a position vector using deltas while also respecting snapping
		/// </summary>
		/// <param name="rayOrigin">The ray doing the translating</param>
		/// <param name="objects">The objects being translated (used to determine bounds; Transforms do not get modified)</param>
		/// <param name="position">The position being modified by delta. This will be set with a snapped position if possible</param>
		/// <param name="rotation">The rotation to be modified if rotation snapping is enabled</param>
		/// <param name="delta">The position delta to apply</param>
		/// <returns>Whether the position was set to a snapped position</returns>
		public static bool ManipulatorSnapping(this IUsesSnapping usesSnaping, Transform rayOrigin, GameObject[] objects, ref Vector3 position, ref Quaternion rotation, Vector3 delta)
		{
			return manipulatorSnapping(rayOrigin, objects, ref position, ref rotation, delta);
		}

		/// <summary>
		/// Perform direct snapping: Transform a position/rotation directly while also respecting snapping
		/// </summary>
		/// <param name="rayOrigin">The ray doing the transforming</param>
		/// <param name="go">The object being transformed (used to determine bounds; Transforms do not get modified)</param>
		/// <param name="position">The position being transformed. This will be set to a snapped position if possible</param>
		/// <param name="rotation">The rotation being transformed. This will only be modified if rotation snapping is enabled</param>
		/// <param name="targetPosition">The input position provided by direct transformation</param>
		/// <param name="targetRotation">The input rotation provided by direct transformation</param>
		/// <returns></returns>
		public static bool DirectSnapping(this IUsesSnapping usesSnaping, Transform rayOrigin, GameObject go, ref Vector3 position, ref Quaternion rotation, Vector3 targetPosition, Quaternion targetRotation)
		{
			return directSnapping(rayOrigin, go, ref position, ref rotation, targetPosition, targetRotation);
		}

		/// <summary>
		/// Clear state information for a given ray
		/// </summary>
		/// <param name="rayOrigin">The ray whose state to clear</param>
		public static void ClearSnappingState(this IUsesSnapping usesSnaping, Transform rayOrigin)
		{
			clearSnappingState(rayOrigin);
		}
	}
}
#endif