#if UNITY_EDITOR
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputNew;

namespace UnityEditor.Experimental.EditorVR.Tools
{
    sealed class Locomotion3dRudderTool : MonoBehaviour, ITool, ILocomotor, IStandardActionMap
    {
        public Transform cameraRig { private get; set; }

        [SerializeField]
        private ActionMap m_BlinkActionMap;
        public ActionMap actionMap { get { return m_BlinkActionMap; } }

        public Vector3 SpeedTranslation = new Vector3(10, 10, 10);
        public float SpeedRotation = 50;

        public void ProcessInput(ActionMapInput input, ConsumeControlDelegate consumeControl)
        {
            Debug.Log("process input 3dRudder");

            var D3dRudderInput = (Locomotion3dRudder)input;
            // Translation
            Vector3 translation = new Vector3(D3dRudderInput.right.value, D3dRudderInput.up.value, D3dRudderInput.forward.value);
            cameraRig.Translate(Vector3.Scale(translation, SpeedTranslation * Time.unscaledDeltaTime));
            // Rotation
            cameraRig.Rotate(0, D3dRudderInput.rotation.value * SpeedRotation * Time.unscaledDeltaTime, 0);
        }
    }
}
#endif
