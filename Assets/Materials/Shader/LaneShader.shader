
Shader "Unlit/FadeShader"
{
	Properties
	{
		[HDR]_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Near("Near", float) = 5.0
		_Far("Far", float) = 250
		_Smooth("Smooth", float) = 0.5
	}
		SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}
		Blend SrcAlpha OneMinusSrcAlpha

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
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 worldPos : TEXCOORD0;
			};

			fixed4 _Color;
			float _Near;
			float _Far;
			float _Smooth;


			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = _Color;

				float dist = distance(_WorldSpaceCameraPos, i.worldPos);

				float fadeFactor = 1.0;

				fadeFactor *= smoothstep(_Near - _Smooth, _Near, dist);
				fadeFactor *= 1.0 - smoothstep(_Far - _Smooth, _Far, dist);

				col.a *= fadeFactor;

				return col;
			}
			ENDCG
		}
	}
}
