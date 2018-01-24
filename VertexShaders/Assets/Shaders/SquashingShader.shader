// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/Squashing Shader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_SquishAmount("Squish Amount", Float) = 1.0
		_SquishLimit("Squish Limit", Float) = 0.0
		_Squishiness("Squishiness", Float) = 0.25
		_SquishinessScalar("Squishiness Scalar", Float) = 8
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows
		#pragma vertex vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		half _SquishAmount;
		half _SquishLimit;
		half _Squishiness;
		half _SquishinessScalar;

		void vert(inout appdata_full v)
		{
			//v.vertex.xyz += v.normal * 0;
			float3 worldPos = mul(unity_ObjectToWorld, v.vertex);

			//alin kohta maailmassa, jolloin "squish" lähtee leviämään
			//float squish = -min(worldPos.y, _SquishLimit) * _SquishAmount;
			float squish = 0;

			if (worldPos.y < _SquishLimit){
				squish = 1;
			}
			else if(worldPos.y < _SquishLimit + _Squishiness){
				squish = 1 - ((worldPos.y - _SquishLimit) / (_SquishLimit + _Squishiness));
			}

			squish = pow(squish, _SquishinessScalar);
			float3 normal = mul(unity_ObjectToWorld, v.normal);

			normal.y = 0;
			if (!(normal.x == 0 && normal.z == 0)) {
				normal = normalize(normal);
			}
			
			v.vertex.xyz += normal *squish;
			v.vertex.y += squish;

			//float difference = max(worldPos.y, _SquishLimit);
			//v.vertex.y = difference * mul(unity_ObjectToWorld, float3(0, 1, 0));

			//worldPos.y = max(worldPos.y, _SquishLimit);
			//v.vertex.xyz = mul(unity_WorldToObject, worldPos);
		}

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {

			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;

			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
