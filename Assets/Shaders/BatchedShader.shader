// Upgrade NOTE: upgraded instancing buffer 'IronEqualBatchedShader' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "IronEqual/BatchedShader"
{
	Properties
	{
		[PerRendererData]_Metallic("Metallic", Range( 0 , 1)) = 0
		[PerRendererData]_Smoothness("Smoothness", Range( 0 , 1)) = 0.5
		[PerRendererData]_Albedo("Albedo", Color) = (0,0,0,0)
		[HDR][PerRendererData]_Emission("Emission", Color) = (0,0,0,0)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			half filler;
		};

		UNITY_INSTANCING_BUFFER_START(IronEqualBatchedShader)
			UNITY_DEFINE_INSTANCED_PROP(float4, _Emission)
#define _Emission_arr IronEqualBatchedShader
			UNITY_DEFINE_INSTANCED_PROP(float4, _Albedo)
#define _Albedo_arr IronEqualBatchedShader
			UNITY_DEFINE_INSTANCED_PROP(float, _Smoothness)
#define _Smoothness_arr IronEqualBatchedShader
			UNITY_DEFINE_INSTANCED_PROP(float, _Metallic)
#define _Metallic_arr IronEqualBatchedShader
		UNITY_INSTANCING_BUFFER_END(IronEqualBatchedShader)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 _Albedo_Instance = UNITY_ACCESS_INSTANCED_PROP(_Albedo_arr, _Albedo);
			o.Albedo = _Albedo_Instance.rgb;
			float4 _Emission_Instance = UNITY_ACCESS_INSTANCED_PROP(_Emission_arr, _Emission);
			o.Emission = _Emission_Instance.rgb;
			float _Metallic_Instance = UNITY_ACCESS_INSTANCED_PROP(_Metallic_arr, _Metallic);
			o.Metallic = _Metallic_Instance;
			float _Smoothness_Instance = UNITY_ACCESS_INSTANCED_PROP(_Smoothness_arr, _Smoothness);
			o.Smoothness = _Smoothness_Instance;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16100
-2553;68;2546;1298;1273;649;1;True;False
Node;AmplifyShaderEditor.ColorNode;4;-301,-220;Float;False;InstancedProperty;_Albedo;Albedo;2;1;[PerRendererData];Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;7;-355,299;Float;False;InstancedProperty;_Smoothness;Smoothness;1;1;[PerRendererData];Create;True;0;0;False;0;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-355,176;Float;False;InstancedProperty;_Metallic;Metallic;0;1;[PerRendererData];Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;5;-311,-32;Float;False;InstancedProperty;_Emission;Emission;3;2;[HDR];[PerRendererData];Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;3;103,-25;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;IronEqual/BatchedShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;4;0
WireConnection;3;2;5;0
WireConnection;3;3;6;0
WireConnection;3;4;7;0
ASEEND*/
//CHKSM=B299CE4AB670337E4F9749BA53152D09C460DBA6