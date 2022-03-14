using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(HornetRifleController))]
public class HornetRifleControllerEditor : Editor {

	public override void OnInspectorGUI () {
	
		HornetRifleController m_HornetCtrl = target as HornetRifleController;

		GUILayout.Label ("Configs", EditorStyles.boldLabel);
		for (int i=0; i<m_HornetCtrl.m_configs.Length; ++i)
			if( GUILayout.Button( m_HornetCtrl.m_configs[i].name ) )			
				m_HornetCtrl.ApplySetting( i );
			
		GUILayout.Label ("Colors", EditorStyles.boldLabel);
		for (int i=0; i<m_HornetCtrl.m_materials.Length; ++i) 
			if( GUILayout.Button( m_HornetCtrl.m_materials[i].name.Split('_')[1] ) )			
				m_HornetCtrl.SetMaterial( i );

		DrawDefaultInspector ();

	}
}
