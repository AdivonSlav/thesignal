using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_hornetRifle : MonoBehaviour {

	public HornetRifleController m_HornetCtrl;
	public RectTransform m_buttonRef;
	public bool m_config = true;


	// Use this for initialization
	void Start () {
	
		RectTransform rt = GetComponent<RectTransform> ();

		if (m_config) {
			for (int i=0; i<m_HornetCtrl.m_configs.Length; ++i) {
				GameObject go = Instantiate (m_buttonRef.gameObject);
				go.GetComponent<RectTransform> ().SetParent (rt, false);
				go.SetActive (true);
				Button bt = go.GetComponent<Button> ();
				AddListener (bt, i);
			}
		}
		else
		{
			for (int i=0; i<m_HornetCtrl.m_materials.Length; ++i) {
				GameObject go = Instantiate (m_buttonRef.gameObject);
				go.GetComponent<RectTransform> ().SetParent (rt, false);
				go.SetActive (true);
				Button bt = go.GetComponent<Button> ();
				AddListener (bt, i);
			}
		}

	}


	void AddListener(Button b, int value) 
	{
		if( m_config )
		{
			b.onClick.AddListener(() => m_HornetCtrl.ApplySetting(value));
			b.GetComponentInChildren<Text>().text = m_HornetCtrl.m_configs[value].name;
		}
		else
		{
			b.onClick.AddListener(() => m_HornetCtrl.SetMaterial(value));
			b.GetComponentInChildren<Text>().text = m_HornetCtrl.m_materials[value].name.Split('_')[1];

		}
	}
	
}
