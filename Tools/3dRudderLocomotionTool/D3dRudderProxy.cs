using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.InputNew;
using UnityEditor.Experimental.EditorVR.Input;
using UnityEditor.Experimental.EditorVR.Utilities;

namespace UnityEditor.Experimental.EditorVR.Proxies
{
    public class D3dRudderProxy : MonoBehaviour, IProxy
    {
        internal IInputToEvents m_InputToEvents;
        public bool active { get { return m_InputToEvents.active; } }
        public event Action activeChanged
        {
            add { m_InputToEvents.activeChanged += value; }
            remove { m_InputToEvents.activeChanged -= value; }
        }

        public bool hidden { get; set; }
        public Dictionary<Transform, Transform> menuOrigins { get; set; }
        public Dictionary<Transform, Transform> alternateMenuOrigins { get; set; }
        public Dictionary<Transform, Transform> previewOrigins { get; set; }
        public TrackedObject trackedObjectInput { get; set; }

        protected Dictionary<Node, Transform> m_RayOrigins;
        public virtual Dictionary<Node, Transform> rayOrigins { get { return m_RayOrigins; } }

        public Dictionary<Transform, Transform> fieldGrabOrigins { get; set; }

        // Use this for initialization
        void Awake()
        {
            Debug.Log("Create Proxy 3dRudder");
			m_RayOrigins = new Dictionary<Node, Transform>
			{
				{ Node.Middle, transform },
			};
			menuOrigins = new Dictionary<Transform, Transform>()
			{
				{ transform, transform },
			};
			alternateMenuOrigins = new Dictionary<Transform, Transform>()
			{
				{ transform, transform },
			};
			previewOrigins = new Dictionary<Transform, Transform>
			{
				{ transform, transform },
			};
			fieldGrabOrigins = new Dictionary<Transform, Transform>
			{
				{ transform, transform },
			};
			m_InputToEvents = ObjectUtils.AddComponent<D3dRudderInputToEvents>(gameObject);
        }
    }
}