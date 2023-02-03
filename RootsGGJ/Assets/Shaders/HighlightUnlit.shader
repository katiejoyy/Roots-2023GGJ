Shader "Unlit/HighlightUnlit"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_HighlightColor("HighlightColor", Color) = (1,1,1,0.2)
		_HighlightIntensity("HighlightIntensity", Range(0, 1)) = 0.25
		_HighlightedEnabled("HighlightedEnabled", Range(0,1)) = 0.0
		_Color("Color", Color) = (1,1,1,1)
	}

	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			half _HighlightedEnabled;
			fixed4 _HighlightColor;
			half _HighlightIntensity;
			fixed4 _Color;

			half highlightAmount;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				highlightAmount = _HighlightedEnabled * _HighlightIntensity;
				fixed4 colour_out = tex2D(_MainTex, i.uv) * (_Color + (highlightAmount * _HighlightColor));
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, colour_out);
				return colour_out;
			}
			ENDCG
		}
	}
}
