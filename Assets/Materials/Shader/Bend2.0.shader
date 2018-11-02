Shader "Custom/Bend2.0"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		[HDR] _Color("Color", color) = (1,1,1,1)
		_CurvDeg ("Curvature", float) = 0.0
		_ZOffset ("Offset", float) = 0.0
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			Pass
			{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			uniform float _CurvDeg;
			uniform float _ZOffset;

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
			
			v2f vert (appdata v)
			{
				float4 vv = mul(unity_ObjectToWorld, v.vertex);
				vv.xyz -= _WorldSpaceCameraPos.xyz;

				//vv = float4(0.0f, (vv.z*vv.z) * _CurvDeg, 0.0f, 0.0f);

				vv = float4(0.0f, (vv.z * vv.z ) * _CurvDeg, 0.0f, 0.0f);

				v.vertex += mul(unity_WorldToObject, vv);

				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
