 Shader "Custom/Slice" {
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _BumpMap ("Bumpmap", 2D) = "bump" {}
	  _SliceWidth ("Slice Width", Range(0, 1)) = 0.5
	  _Frequency ("Slice Frequency", Range(0, 100)) = 5
	  _Angle ("Slice Angle", Range(-1,1)) = 0.1
    }

    SubShader {
      Tags { "RenderType" = "Opaque" }
      Cull Off

      CGPROGRAM
      #pragma surface surf Lambert

      struct Input {
          float2 uv_MainTex;
          float2 uv_BumpMap;
          float3 worldPos;
      };

      sampler2D _MainTex;
      sampler2D _BumpMap;
	  half _SliceWidth;
	  half _Frequency;
	  half _Angle;

      void surf (Input IN, inout SurfaceOutput o) {
          //clip (frac((IN.worldPos.x + IN.worldPos.y + IN.worldPos.z * _Angle) * _Frequency) - _SliceWidth);
          clip (frac((IN.worldPos.y + IN.worldPos.y * _Angle) * _Frequency) - _SliceWidth);
		  o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
          o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
      }

      ENDCG

    } 

    Fallback "Diffuse"
  }