﻿Shader "_Custom/CC_04_Lighting Shader 1"
{
	Properties
	{
		_Tint ("Tint", Color) = (1,1,1,1)
		_MainTex ("Albedo", 2D) = "white" {}
		_SpecularTint ("Specular", Color) = (0.5, 0.5, 0.5)
		_Smoothness ("Smoothness", Range(0, 1)) = 0.5
	}

	SubShader
	{
		Pass
		{
			Tags
			{
			"LightMode" = "ForwardBase"
			}

			CGPROGRAM
				#pragma vertex MyVertexProgram
				#pragma fragment MyFragmentProgram

				#include "UnityStandardBRDF.cginc"
				#include "UnityStandardUtils.cginc"

				float4 _Tint;
				sampler2D _MainTex;
				float4 _MainTex_ST;
				float4 _SpecularTint;
				float _Smoothness;

				struct Interpolators
				{
					float4 position : SV_POSITION;
					float2 uv : TEXCOORD0;
					float3 normal : TEXCOORD1;
					float3 worldPos : TEXCOORD2;
				};

				struct VertexData {
					float4 position : POSITION;
					float3 normal : NORMAL;
					float2 uv : TEXCOORD0;
				};

				//VERTEX - SHAPES
				Interpolators MyVertexProgram (VertexData v) {
					Interpolators i;
					i.position = UnityObjectToClipPos(v.position);
					i.worldPos = mul(unity_ObjectToWorld, v.position);
					i.normal = UnityObjectToWorldNormal(v.normal);
					i.uv = TRANSFORM_TEX(v.uv, _MainTex);

					return i;
				}

				//FRAGMENT - COLORS
				float4 MyFragmentProgram (Interpolators i) : SV_TARGET {
					i.normal = normalize(i.normal);
					float3 lightDir = _WorldSpaceLightPos0.xyz;
					float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
					float3 lightColor = _LightColor0.rgb;
					float3 albedo = tex2D(_MainTex, i.uv).rgb * _Tint.rgb;
					float oneMinusReflectivity;
					albedo = EnergyConservationBetweenDiffuseAndSpecular( albedo, _SpecularTint.rgb, oneMinusReflectivity);
					float3 diffuse = albedo * lightColor * DotClamped(lightDir, i.normal);
					float3 halfVector = normalize(lightDir + viewDir);
					float3 specular = _SpecularTint.rgb * lightColor * pow(DotClamped(halfVector, i.normal),
					_Smoothness * 100
				);

				return float4(diffuse + specular, 1);
				}
			ENDCG
		}
	}
}