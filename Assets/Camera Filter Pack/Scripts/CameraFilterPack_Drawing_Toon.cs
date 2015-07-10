﻿////////////////////////////////////////////////////////////////////////////////////
//  CAMERA FILTER PACK - by VETASOFT 2014 //////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[AddComponentMenu ("Camera Filter Pack/Drawing/Toon")]
public class CameraFilterPack_Drawing_Toon : MonoBehaviour {
	#region Variables
	public Shader SCShader;
	private Material SCMaterial;
	private float TimeX = 1.0f;
	[Range(0, 2)]
	public float Threshold = 1f;
	[Range(0, 8)]
	public float DotSize = 1f;

	public static float ChangeThreshold;
	public static float ChangeDotSize;

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
		ChangeThreshold = Threshold;
		ChangeDotSize = DotSize;

		SCShader = Shader.Find("CameraFilterPack/Drawing_Toon");

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
			material.SetFloat("_Distortion", Threshold);
			material.SetFloat("_DotSize", DotSize);
			Graphics.Blit(sourceTexture, destTexture, material);
		}
		else
		{
			Graphics.Blit(sourceTexture, destTexture);	
		}
		
		
	}

	void OnValidate()
{
		ChangeThreshold=Threshold;
		ChangeDotSize=DotSize;
		
}
	// Update is called once per frame
	void Update () 
	{
		if (Application.isPlaying)
		{
			Threshold = ChangeThreshold;
			DotSize = ChangeDotSize;
		}
		#if UNITY_EDITOR
		if (Application.isPlaying!=true)
		{
			SCShader = Shader.Find("CameraFilterPack/Drawing_Toon");

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