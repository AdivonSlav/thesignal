using UnityEngine;
using System.Collections;

public class HornetRifleController : MonoBehaviour {

	[System.Serializable]
	public class RifleParts
	{
		public GameObject optical;
		public GameObject pod;
		public GameObject laser;
		public GameObject butt_stock;
		public GameObject magazine;
		public GameObject sniper_barrel;

	}

	[System.Serializable]
	public class RifleConfig
	{
		public string name = "";
		public bool optical = true;
		public bool pod = true;
		public bool laser = true;
		public bool butt_stock = true;
		public bool magazine = true;	
		public bool sniper_barrel = true;
		public float sniper_barrel_pos = 4;
	}

	public RifleParts m_parts;
	public RifleConfig[] m_configs;
	public Material[] m_materials;

	int m_colorID = 0;


	public void Start()
	{
		SetMaterial (m_colorID);
	}



	public void ApplySetting ( int id ) {
	
		if (id < 0 || id >= m_configs.Length)
			return;

		m_parts.optical.SetActive (m_configs [id].optical); 
		m_parts.sniper_barrel.SetActive (m_configs [id].sniper_barrel);
		m_parts.pod.SetActive (m_configs [id].pod);
		m_parts.laser.SetActive (m_configs [id].laser);
		m_parts.butt_stock.SetActive (m_configs [id].butt_stock);
		m_parts.magazine.SetActive (m_configs [id].magazine);

		Vector3 sbpos = m_parts.sniper_barrel.transform.localPosition;
		sbpos.z = m_configs [id].sniper_barrel_pos;
		m_parts.sniper_barrel.transform.localPosition = sbpos;
		SetMaterial (m_colorID);
	}


	public void SetMaterial( int id )
	{
		if (id < 0 || id >= m_materials.Length)
			return;

		m_colorID = id;

		Renderer[] rndr = GetComponentsInChildren<Renderer> ();

		for(int i=0; i<rndr.Length; ++i)
		{
			rndr[i].material = m_materials[id];
		}
	}

}
