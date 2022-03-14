using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;

namespace Kalagaan
{
	[InitializeOnLoad]
	public class LaunchHornetRifleSplash
	{
		static LaunchHornetRifleSplash()
		{
			string key = "HornetRifleSplash";
			if( !EditorPrefs.HasKey(key) )			
			{
				EditorPrefs.SetBool( key, true );
				HornetRifleSplash.Init ();
			}
			//EditorPrefs.DeleteKey (key);
		}

	}


	public class HornetRifleSplash : EditorWindow {
		[MenuItem ("Window/Kalagaan/Hornet rifle splash")]
		public static void Init () {
			// Get existing open window or if none, make a new one:
			HornetRifleSplash window = (HornetRifleSplash)EditorWindow.GetWindow (typeof (HornetRifleSplash));
			Vector2 size = new Vector2 ( 500,650 );
			window.minSize = size;
			window.maxSize = size;
			window.Show();

		}

		List<Texture2D> textures = new List<Texture2D>();
		Color orange = new Color (241f / 255f, 172f / 255f, 38f / 255f);
		Texture2D bg = null;

		void OnGUI () {

			if( bg == null )				
				bg = Resources.Load<Texture2D>( "background_splash");
			else
				GUI. DrawTexture ( new Rect(0,0,500,650), bg );


			if ( textures.Count == 0)
			{
				//Debug.Log("Load texture");
				//textures.Add( Resources.Load<Texture2D>( "pose1") );
				textures.Add( Resources.Load<Texture2D>( "Hornet_rifle_icon") );
				textures.Add( Resources.Load<Texture2D>( "VertExmotion_icon") );
				textures.Add( Resources.Load<Texture2D>( "polymorpher_icon") );
				textures.Add( Resources.Load<Texture2D>( "matcapFX_icon") );

			}

			//GUI.backgroundColor = orange;


			CenterText ( "Thanks for having downloaded the Hornet Rifle", EditorStyles.boldLabel );

			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			GUILayout.Label ( textures[0], GUILayout.Height( 130 ) );
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();

			GUILayout.Space (10);

			CenterText( "If you like it,", EditorStyles.label );
			CenterLink( "\nVote and support me on the asset store :)\n", "com.unity3d.kharma:content/37309" );

			GUILayout.Space (40);

			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			GUI.color = orange;
			GUILayout.Label ( "Have a look on my other assets :", EditorStyles.boldLabel );
			GUI.color = Color.white;
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();

			GUILayout.Space (20);

			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();


			GUILayout.BeginVertical ();

			GUILayout.Button ( "VertExmotion", EditorStyles.boldLabel );
			GUILayout.Label ( textures[1], GUILayout.Height( 130 ) );
			GUILayout.Label ( "Softbody system" );
			GUILayout.Space (10);

			if (GUILayout.Button ("VertExmotion BASIC"))
				Application.OpenURL ("com.unity3d.kharma:content/23930");

			if (GUILayout.Button ("VertExmotion PRO"))
				Application.OpenURL ("com.unity3d.kharma:content/25127");


			if (GUILayout.Button ("Demos"))
				Application.OpenURL ("www.kalagaan.com/VertExmotion");




			GUILayout.EndVertical ();

			GUILayout.FlexibleSpace ();

			GUILayout.BeginVertical ();
			GUILayout.Button ( "PolyMorpher", EditorStyles.boldLabel );
			GUILayout.Label ( textures[2], GUILayout.Height( 130 ) );

			GUILayout.Label ( "Morphing system" );
			GUILayout.Space (10);


			if (GUILayout.Button ("PolyMorpher"))
				Application.OpenURL ("com.unity3d.kharma:content/3428");
			
			
			if (GUILayout.Button ("Demos"))
				Application.OpenURL ("www.kalagaan.com/polymorpher");

			GUILayout.EndVertical ();

			GUILayout.FlexibleSpace ();






			GUILayout.BeginVertical ();

			GUILayout.Button ( "MatcapFX", EditorStyles.boldLabel );
			GUILayout.Label ( textures[3], GUILayout.Height( 130 ) );

			GUILayout.Label ( "Matcap shader" );
			GUILayout.Space (10);

			if( GUILayout.Button ( "Download for FREE!" ) )
				Application.OpenURL ("com.unity3d.kharma:content/4814");

			GUILayout.EndVertical ();


			GUILayout.FlexibleSpace ();

			GUILayout.EndHorizontal ();
		}


		void OnDestroy()
		{
			for(int i=0; i<textures.Count; ++i)
				Resources.UnloadAsset ( textures[i] );
			Resources.UnloadAsset ( bg );
		}


		void CenterText( string text, GUIStyle style )
		{
			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			GUILayout.Label ( text, style );
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();
		}

		void CenterLink( string text, string URL )
		{
			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			if( GUILayout.Button ( text ) )
				Application.OpenURL (URL);
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();
		}
	}
}
