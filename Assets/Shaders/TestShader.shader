Shader "Custom/TestShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)

		[Toggle] _Rim ("Rim lighting", Float) = 0
		_RimColor ("Rim color", Color) = (1,1,1,1)
		_RimPower ("Rim falloff", Range(0,5)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM

		#pragma surface surf Lambert

		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
		};

		fixed4 _Color;

		fixed4 _RimColor;
		fixed _RimPower;
		float _Rim;

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
			if(_Rim) o.Emission = _RimColor.rgb * pow(rim, _RimPower);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
