Shader "Unlit/FlashPlayer"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)
		_StepMin("StepMin",Range(0,1)) = 0.5
		_StepMax("StepMax",float) = 1
	}
	SubShader
	{
		Tags { "Queue"="Transparent""RenderType"="Transparent" "IgnoreProjector"="True" }
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 100
		Cull Off

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
			float _StepMin,_StepMax;
			float3 _Color;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2 pos = (i.uv)*2-1;
				if(length(pos)>1)discard;
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				col.rgb = _Color;
				col.a *= smoothstep(_StepMin,_StepMax,length(pos));
				// apply fog
				return col;
			}
			ENDCG
		}
	}
}
