using System;
using UnityEngine;
using UnityEditor.VR;
using UnityEngine.VR.Tools;
using UnityEngine.InputNew;
using UnityEngine.VR.Utilities;
using UnityEngine.InputNew;

[MainMenuItem("GreyBoxingTool","Primitive","Create primitives in the scene")]
public class GreyBoxingTool: MonoBehaviour, ITool, IStandardActionMap, IRay, IInstantiateUI, ICustomActionMap
{
	private GameObject m_CurrentGameObject = null;

	private Vector3 m_PointA = Vector3.zero;
	private Vector3 m_PointB = Vector3.zero;

	private PrimitiveCreationStates m_State = PrimitiveCreationStates.PointA;

	[SerializeField]
	private Canvas CanvasPrefab;
	private Canvas m_ToolCanvas;
	private bool m_ToolCanvasSpawned = false;
	private GreyBoxMenu m_GreyBoxMenuScript;

	private GameObject m_DrawSphere;

	public float m_DrawDistance = 0.01f;
	private const float kMaxDrawDistance = 20.0f;
	private float m_ScrollSpeed = 5.0f;

	public Standard standardInput
	{
		get; set;
	}

	public ActionMap actionMap
	{
		get
		{
			return m_GreyBoxActionMap;
		}
	}
	[SerializeField]
	private ActionMap m_GreyBoxActionMap;

	public ActionMapInput actionMapInput
	{
		get
		{
			return m_GreyBoxInput;
		}
		set
		{
			m_GreyBoxInput = (GreyBoxInput)value;
		}
	}
	private GreyBoxInput m_GreyBoxInput;

	public Transform rayOrigin
	{
		get; set;
	}

	public Func<GameObject,GameObject> instantiateUI
	{
		private get; set;
	}
	
	private enum PrimitiveCreationStates
	{
		PointA,
		Freeform,
	}

	void Update()
	{
		if(!m_ToolCanvasSpawned && standardInput.action.wasJustPressed)
		{
			if(m_ToolCanvas == null)
			{
				var go = instantiateUI(CanvasPrefab.gameObject);
				m_ToolCanvas = go.GetComponent<Canvas>();
				m_ToolCanvasSpawned = true;
			}
			m_ToolCanvas.transform.position = rayOrigin.position + rayOrigin.forward * 20.0f;
			m_ToolCanvas.transform.rotation = Quaternion.LookRotation(m_ToolCanvas.transform.position - VRView.viewerCamera.transform.position);

			m_GreyBoxMenuScript = m_ToolCanvas.gameObject.GetComponent<GreyBoxMenu>();
			m_DrawSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			m_DrawSphere.transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);
			m_DrawSphere.GetComponent<Collider>().enabled = false;
			return;
		}
		
		if(m_DrawSphere)
		{
			float temp = m_GreyBoxInput.scroll.rawValue;
			if(m_DrawDistance <= kMaxDrawDistance)
			{
				m_DrawDistance += temp * m_ScrollSpeed * Time.unscaledDeltaTime;
				m_DrawDistance = Mathf.Clamp(m_DrawDistance,0.0f,kMaxDrawDistance);
				m_GreyBoxMenuScript.m_DrawDistanceSlider.value = m_DrawDistance / kMaxDrawDistance;
                m_GreyBoxMenuScript.m_CurrentValueText.text = m_DrawDistance.ToString("N2");
			}

			m_DrawSphere.transform.position = rayOrigin.position + rayOrigin.forward * m_DrawDistance;

			if(m_DrawDistance > 2.0f)
				m_DrawSphere.transform.localScale = new Vector3(0.05f,0.05f,0.05f) * m_DrawDistance * 0.35f;
			else
				m_DrawSphere.transform.localScale = new Vector3(0.015f,0.015f,0.015f);
		}

		switch(m_State)
		{
			case PrimitiveCreationStates.PointA:
			{
				if(standardInput.action.wasJustPressed)
				{
					m_CurrentGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
					m_CurrentGameObject.transform.localScale = new Vector3(0.0025f,0.0025f,0.0025f);

					m_PointA = rayOrigin.position + rayOrigin.forward * m_DrawDistance;
					m_CurrentGameObject.transform.position = m_PointA;

					m_State = PrimitiveCreationStates.Freeform;
				}
				break;
			}
			case PrimitiveCreationStates.Freeform:
			{
				m_PointB = rayOrigin.position + rayOrigin.forward * m_DrawDistance;
				m_CurrentGameObject.transform.position = (m_PointA + m_PointB) * 0.5f;
				Vector3 maxCorner = Vector3.Max(m_PointA,m_PointB);
				Vector3 minCorner = Vector3.Min(m_PointA,m_PointB);
				m_CurrentGameObject.transform.localScale = (maxCorner - minCorner);

				if(standardInput.action.wasJustReleased)
					m_State = PrimitiveCreationStates.PointA;

				break;
			}
		}
	}

	void OnDestroy()
	{
		if(m_DrawSphere)
			U.Object.Destroy(m_DrawSphere);

		if(m_ToolCanvas != null)
			U.Object.Destroy(m_ToolCanvas.gameObject);
	}
}