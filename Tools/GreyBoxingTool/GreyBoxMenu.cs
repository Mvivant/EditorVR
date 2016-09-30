using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR.Tools;
using UnityEngine.InputNew;
using System.Collections;

public class GreyBoxMenu : MonoBehaviour
{
	//public float m_DrawDistance = 0.0f;
	//private const float kMaxDrawDistance = 20.0f;

	[SerializeField]
	public Slider m_DrawDistanceSlider;
	[SerializeField]
	public Text m_CurrentValueText;



	public void Start()
	{
		//m_DrawDistanceSlider.onValueChanged.AddListener( delegate { ValueChangeCheck(); } );
	}

	void Update()
	{
		//float temp = m_GreyBoxInput.scroll.rawValue;
  //      if(m_DrawDistance < kMaxDrawDistance)
		//{
		//	m_DrawDistance += temp * Time.unscaledDeltaTime;
		//	m_CurrentValueText.text = m_DrawDistance.ToString("N2");
		//}
	}

	//public void ValueChangeCheck()
	//{
	//	m_DrawDistance = m_DrawDistanceSlider.value * kMaxDrawDistance;
	//	m_CurrentValueText.text = m_DrawDistance.ToString("N2");
 //   }
}