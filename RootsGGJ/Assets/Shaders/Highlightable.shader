Shader "Custom/Highlightable" 
{
	Properties 
	{
		_HighlightedEnabled ("HighlightedEnabled", Range(0,1)) = 0.0
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_HighlightColor ("HighlightColor", Color) = (1,1,1,0.2)
		_HighlightIntensity ("HighlightIntensity", Range(0, 1)) = 0.25
	}
	
	SubShader 
	{
		Tags 
		{ 
			"RenderType"="Opaque" 
		}
		
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input 
		{
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		half _HighlightedEnabled;
		fixed4 _HighlightColor;
		half _HighlightIntensity;

		half highlightAmount;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard output_surface) 
		{
			// Albedo comes from a texture tinted by color
			highlightAmount = _HighlightedEnabled * _HighlightIntensity;
			fixed4 colour_out = tex2D (_MainTex, IN.uv_MainTex) * (_Color + (highlightAmount * _HighlightColor));
			output_surface.Albedo = colour_out.rgb;
			// Metallic and smoothness come from slider variables
			output_surface.Metallic = _Metallic;
			output_surface.Smoothness = _Glossiness;
			output_surface.Alpha = colour_out.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
