#if UNITY_EDITOR
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputNew;

namespace UnityEditor.Experimental.EditorVR.Tools
{
    sealed class Locomotion3dRudderTool : MonoBehaviour, ITool, ILocomotor, ICustomActionMap/*, IUsesProxyType*/
    {
        public Transform cameraRig { private get; set; }

        [SerializeField]
        private ActionMap m_ActionMap;
        public ActionMap actionMap { get { return m_ActionMap; } }

        //public Type proxyType { private get; set; }

        public Vector3 SpeedTranslation = new Vector3(10, 10, 10);
        public float SpeedRotation = 50;

        private void Start()
        {
            Debug.Log("start 3dRudder tools");
        }
        public void ProcessInput(ActionMapInput input, ConsumeControlDelegate consumeControl)
        {           
            var D3dRudderInput = (Locomotion3dRudder)input;
            if (D3dRudderInput != null)
            {
				//Debug.Log("input 3dRudder " + D3dRudderInput.rotation.value + " " + D3dRudderInput.forward.value);
				// Translation
				Vector3 translation = new Vector3(D3dRudderInput.right.value, D3dRudderInput.up.value, D3dRudderInput.forward.value);
                cameraRig.Translate(Vector3.Scale(translation, SpeedTranslation * Time.unscaledDeltaTime));
                // Rotation
                cameraRig.Rotate(0, D3dRudderInput.rotation.value * SpeedRotation * Time.unscaledDeltaTime, 0);				
            }
        }
    }
}
#endif
