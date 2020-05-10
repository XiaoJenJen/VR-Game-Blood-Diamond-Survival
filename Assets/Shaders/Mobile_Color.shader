Shader "Custom/Mobile_Color" {
Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 150

CGPROGRAM
#pragma surface surf Lambert noforwardadd

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o) {
			
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Mobile/Diffuse"
}
