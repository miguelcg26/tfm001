Shader "TSDv2/TexTransparent" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_SmoothnessTex("SmoothnessTex (RGB)", 2D) = "black" {}
		_MetallicTex("MetalicTex (RGB)", 2D) = "black" {}
		_MatcapTex("MatcapTex (RGB)", 2D) = "white" {}
		[MaterialToggle(_MATCAP_ON)] _MatcapS ("Matcap map switch", Float) = 1		//4
        [NoScaleOffset]																
        _Matcap ("Matcap Map ", 2D) = "white" {}									//5
        _MatcapIn ("Matcap intensity", Float) = 1.0									//6	
		_BumpMap ("Normalmap", 2D) = "bump" {}
		[MaterialToggle(_RIM_ON)] _RimS ("Rim switch", Float) = 0 					//7
        _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)						//8	
      	_RimPow ("Rim Power", Float) = 1.0											//9
      	_RimIn ("Rim Intensity", Float) = 1.0										//10
		_EmissionMap("Emission", 2D) = "black" {}
		_EmissIn ("Emission intensity", Float) = 1.0
		_Cutoff("Alpha Cutoff", Range(0,1)) = 0.5
	}
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
    	LOD 300

		Blend SrcAlpha OneMinusSrcAlpha 
		
		Pass 
		{
			ZWrite On
			ColorMask 0
    	}

		CGPROGRAM
		#pragma surface surfTex Standard  fullforwardshadows alpha:fade
		#pragma shader_feature _MATCAP_ON
		#pragma shader_feature _RIM_ON
		#pragma target 3.0
		#include "../TSDv2.cginc"
		ENDCG
	} 
	FallBack "Diffuse"
}
