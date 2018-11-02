// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/PokeballRain"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)
		_Height ("Height", Float) = 20.
		_Speed ("Speed", Float) = .2
		_Scale ("Scale", Float) = .2
		_Trail ("Trail", Float) = 1.
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		Cull Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			#include "Utils.cginc"

			// attributes (infos from vertices)
			struct appdata
			{
				float4 position : POSITION;
				float2 uv : TEXCOORD0;
			};

			// varying (info from vertex shader to pixel shader)
			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 position : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Height, _Speed, _Scale, _Trail;
			float3 _Color;

			// rotation matrix
			float2x2 rot (float a) {
				float c=cos(a),s=sin(a);
				return float2x2(c,-s,s,c);
			}

			float3 displace (float3 p, float ratio) {
				float d = length(p) + ratio;
				p.xz = mul(d, p.xz);
				p.yz = mul(d, p.yz);
				return p;
			}
			
			v2f vert (appdata v)
			{
				v2f o;
				// give uv to fragment shader
				o.uv = v.uv;

				// random noise
				float3 seed = v.position;
				float rng = rand(seed.xz*1.);

				// animation ratio [0 -> 1]
				float ratio = fmod(abs(_Time.y * _Speed + rng), 1.);

				// decide when ratio hits ground
				float ratioGround = 0.9;

				// billboard scale
				float scale = _Scale;
				// fade scale from animation spawn and death
				scale *= smoothstep(0.,.9, ratio);
				// random scale
				scale *= .5+.5*rng;
				
				//Deplacer
				//v.position.xyz *= 0.1 ;

				// fall animation
				float fallRatio = min(ratioGround, ratio)/ratioGround;
				v.position.y = (1.-fallRatio) * _Height;

				float unit= 0.01;
				float3 next = displace(seed, fallRatio + unit);
				float3 prev = displace(seed, fallRatio - unit);
				float3 front = normalize(next-prev);
				float3 right = cross(front, normalize(seed));

				//o.position.xyz = displace(seed.xyz, fallRatio);
				v.position.xyz += front * o.uv.y * scale + right * o.uv.x * (scale * _Trail);


				// model matrix (world space) (transform component)
				o.position = mul(UNITY_MATRIX_M, v.position);
				
				// apply view and projection matrix (screen space)
				o.position = mul(UNITY_MATRIX_VP, o.position);
				
				// uv are used to scale quad in screen space
				float2 uv = v.uv;
				uv = mul(rot(_Time.y+rng*PI),uv);
				uv.x *= _ScreenParams.y/_ScreenParams.x;

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float dist = length(i.uv);

				if (dist > 0.4) discard;
				float3 color = _Color;
				color.rgb *= 1-(length(i.uv));

				return fixed4(color, 1.);
			}
			ENDCG
		}
	}
}
