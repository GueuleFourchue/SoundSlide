Shader "Unlit/Skybox"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Scale("Scale",float) = 0.
		_Color("Color",Color)=(0,0,0)
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
			
			#include "UnityCG.cginc"
			#include "Utils.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 vertexWorld : TEXCOORD2;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
				float3 view : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float3 colorShader,_Color;
			float _Scale;
			
			v2f vert (appdata v)
			{
				v2f o;
				v.vertex = mul(UNITY_MATRIX_M, v.vertex);
				o.view = normalize(v.vertex - _WorldSpaceCameraPos);
				o.vertexWorld = v.vertex;
				o.vertex = mul(UNITY_MATRIX_VP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.normal = normalize(v.normal);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 color = tex2D(_MainTex, i.uv);
				float2 uv1 = tan(length(i.uv)*_Scale);
				float2 uv2 = tan((dot(i.view, i.normal)*_Scale));
				float2 uv = lerp(uv2, uv1, abs(dot(i.view, float3(0,0,1))));
				uv.x = atan2(i.vertexWorld.y, i.vertexWorld.x);
				uv.y += abs(sin(uv.x+ _Time.y));
				float lod = 8.*length(i.uv);
				uv = floor(uv*lod)/lod;
				color.rgb = hsv2rgb(float3(fmod(uv.y+_Time.y, 1.), 1., 1.)) * _Color;

				return color;
			}
			ENDCG
		}
	}
}
