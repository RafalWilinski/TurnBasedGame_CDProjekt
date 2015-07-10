﻿////////////////////////////////////////////////////////////////////////////////////
//  CAMERA FILTER PACK - by VETASOFT 2014 //////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[AddComponentMenu ("Camera Filter Pack/Edge/Edge_filter")]
public class CameraFilterPack_Edge_Edge_filter : MonoBehaviour {
	#region Variables
	public Shader SCShader;
	private float TimeX = 1.0f;
	private Vector4 ScreenResolution;
	private Material SCMaterial;
	[Range(0, 10)]
	public float RedAmplifier = 0.0f;
	[Range(0, 10)]
	public float GreenAmplifier = 2.0f;
	[Range(0, 10)]
	public float BlueAmplifier = 0.0f;

	public static float ChangeRedAmplifier;
	public static float ChangeGreenAmplifier;
	public static float ChangeBlueAmplifier;

	#endregion
	
	#region Properties
	Material material
	{
		get
		{
			if(SCMaterial == null)
			{
				SCMaterial = new Material(SCShader);
				SCMaterial.hideFlags = HideFlags.HideAndDontSave;	
			}
			return SCMaterial;
		}
	}
	#endregion
	void Start () 
	{
		ChangeRedAmplifier 	= RedAmplifier;
		ChangeGreenAmplifier= GreenAmplifier;
		ChangeBlueAmplifier	= BlueAmplifier;

		SCShader = Shader.Find("CameraFilterPack/Edge_Edge_filter");

		if(!SystemInfo.supportsImageEffects)
		{
			enabled = false;
			return;
		}
	}
	
	void OnRenderImage (RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if(SCShader != null)
		{
			TimeX+=Time.deltaTime;
			if (TimeX>100)  TimeX=0;
			material.SetFloat("_TimeX", TimeX);
			material.SetFloat("_RedAmplifier", RedAmplifier);
			material.SetFloat("_GreenAmplifier", GreenAmplifier);
			material.SetFloat("_BlueAmplifier", BlueAmplifier);
			material.SetVector("_ScreenResolution",new Vector2(Screen.width,Screen.height));
			Graphics.Blit(sourceTexture, destTexture, material);
		}
		else
		{
			Graphics.Blit(sourceTexture, destTexture);	
		}
		
		
	}
		void OnValidate()
{
	ChangeRedAmplifier=RedAmplifier;
		ChangeGreenAmplifier=GreenAmplifier;
		ChangeBlueAmplifier=BlueAmplifier;
	
}
	// Update is called once per frame
	void Update () 
	{
		if (Application.isPlaying)
		{
			RedAmplifier	= ChangeRedAmplifier;
			GreenAmplifier 	= ChangeGreenAmplifier;
			BlueAmplifier 	= ChangeBlueAmplifier;
		}
		#if UNITY_EDITOR
		if (Application.isPlaying!=true)
		{
			SCShader = Shader.Find("CameraFilterPack/Edge_Edge_filter");

		}
		#endif

	}
	
	void OnDisable ()
	{
		if(SCMaterial)
		{
			DestroyImmediate(SCMaterial);	
		}
		
	}
	
	
}