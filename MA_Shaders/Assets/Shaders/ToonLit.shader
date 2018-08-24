Shader "Toon/Lit" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_Color2 ("Noise Color", Color) = (0.5,0.5,0.5,1)
		_SliderValue ("Noise Map Treshold", Range (-1, 1)) = 0.5
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_SecondaryTex ("Secondary Texture", 2D) = "white" {}
		_NoiseTex ("Noise", 2D) = "white" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {} 
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf ToonRamp

		sampler2D _Ramp;

		// custom lighting function that uses a texture ramp based
		// on angle between light direction and normal
		#pragma lighting ToonRamp exclude_path:prepass
		inline half4 LightingToonRamp (SurfaceOutput s, half3 lightDir, half atten)
		{
			#ifndef USING_DIRECTIONAL_LIGHT
			lightDir = normalize(lightDir);
			#endif
	
			half d = dot (s.Normal, lightDir)*0.5 + 0.5;
			half3 ramp = tex2D (_Ramp, float2(d,d)).rgb;
	
			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
			c.a = 0;
			return c;
		}


		sampler2D _MainTex;
		sampler2D _SecondaryTex;
		sampler2D _NoiseTex;
		float _SliderValue;
		float4 _Color;
		float4 _Color2;

		struct Input {
			float2 uv_MainTex : TEXCOORD0;
			float2 uv_NoiseTex : TEXCOORD1;
			float2 uv_SecondaryTex : TEXCOORD2;
			float3 worldPos;
			float4 screenPos;
		};

		void surf (Input IN, inout SurfaceOutput o) {

			float2 screenUV = IN.screenPos.xyz;
			//Textures used

			half4 c = tex2D(_MainTex,		IN.uv_MainTex)		* _Color;
			half4 s = tex2D(_SecondaryTex,	IN.uv_SecondaryTex) * _Color2;
			half4 n = tex2D(_NoiseTex,		IN.uv_NoiseTex)		* _Color;

			o.Albedo = c.rgb;

			if(n.r > _SliderValue){
				o.Albedo = s.rgb;
				o.Emission = s.rgb * _Color2;
			}

			o.Alpha = n.a ;
		}

		ENDCG

			} 

			Fallback "Diffuse"
}
