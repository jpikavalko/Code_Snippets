// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Transparent"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_Amount("Amount", Range(0, 1)) = 0.5
	}

		SubShader
	{
		Tags
	{
		"Queue" = "Transparent"
		"PreviewType" = "Plane"
	}

		Pass
	{
		Blend SrcAlpha OneMinusSrcAlpha
		//Blend One One

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

		struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float4 vertex : SV_POSITION;
		float2 uv : TEXCOORD0;
	};

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = v.uv;
		return o;
	}

	sampler2D _MainTex;
	float _Amount;
	float4 _Color;

	float4 frag(v2f i) : SV_Target
	{
		float4 color = tex2D(_MainTex, i.uv);
		return color * color.a  *_Amount * _Color;
	}
		ENDCG
	}
	}
}
