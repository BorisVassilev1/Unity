Shader "hidden/preview"
{
	Properties
	{
				[NonModifiableTextureData] [NoScaleOffset] Texture_507A46B3("Texture", 2D) = "white" {}
				Float_D2D9489B("FresnelPower", Float) = 10
				[NonModifiableTextureData] [NoScaleOffset] Texture_E2350D28("BaseColor", 2D) = "white" {}
				[NonModifiableTextureData] [NoScaleOffset] _Texture2D_3BDAFFBE_Tex("Texture", 2D) = "white" {}
				[NonModifiableTextureData] [NoScaleOffset] _Texture2D_43A49A96_Tex("Texture", 2D) = "white" {}
	}
	CGINCLUDE
	#include "UnityCG.cginc"
			void Unity_OneMinus_float(float In, out float Out)
			{
			    Out = 1 - In;
			}
			void Unity_Power_float(float A, float B, out float Out)
			{
			    Out = pow(A, B);
			}
			void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
			{
			    Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
			}
			void Unity_Multiply_float(float A, float B, out float Out)
			{
			    Out = A * B;
			}
			void Unity_Hue_Normalized_float(float3 In, float Offset, out float3 Out)
			{
			    // RGB to HSV
			    float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
			    float4 P = lerp(float4(In.bg, K.wz), float4(In.gb, K.xy), step(In.b, In.g));
			    float4 Q = lerp(float4(P.xyw, In.r), float4(In.r, P.yzx), step(P.x, In.r));
			    float D = Q.x - min(Q.w, Q.y);
			    float E = 1e-10;
			    float3 hsv = float3(abs(Q.z + (Q.w - Q.y)/(6.0 * D + E)), D / (Q.x + E), Q.x);
			    float hue = hsv.x + Offset;
			    hsv.x = (hue < 0)
			            ? hue + 1
			            : (hue > 1)
			                ? hue - 1
			                : hue;
			    // HSV to RGB
			    float4 K2 = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
			    float3 P2 = abs(frac(hsv.xxx + K2.xyz) * 6.0 - K2.www);
			    Out = hsv.z * lerp(K2.xxx, saturate(P2 - K2.xxx), hsv.y);
			}
			void Unity_Power_float(float3 A, float3 B, out float3 Out)
			{
			    Out = pow(A, B);
			}
			void Unity_Divide_float(float3 A, float3 B, out float3 Out)
			{
			    Out = A / B;
			}
			void Unity_Reciprocal_float(float In, out float Out)
			{
			    Out = 1.0/In;
			}
			void Unity_Multiply_float(float3 A, float3 B, out float3 Out)
			{
			    Out = A * B;
			}
			void Unity_NormalCreate_float(Texture2D Texture, SamplerState Sampler, float2 UV, float Offset, float Strength, out float3 Out)
			{
			    Offset = pow(Offset, 3) * 0.1;
			    float2 offsetU = float2(UV.x + Offset, UV.y);
			    float2 offsetV = float2(UV.x, UV.y + Offset);
			    float normalSample = Texture.Sample(Sampler, UV);
			    float uSample = Texture.Sample(Sampler, offsetU);
			    float vSample = Texture.Sample(Sampler, offsetV);
			    float3 va = float3(1, 0, (uSample - normalSample) * Strength);
			    float3 vb = float3(0, 1, (vSample - normalSample) * Strength);
			    Out = normalize(cross(va, vb));
			}
			void Unity_FresnelEffect_float(float3 Normal, float3 ViewDir, float Power, out float Out)
			{
			    Out = pow((1.0 - saturate(dot(normalize(Normal), normalize(ViewDir)))), Power);
			}
			void Unity_Add_float(float A, float B, out float Out)
			{
			    Out = A + B;
			}
			void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA)
			{
			    RGBA = float4(R, G, B, A);
			}
			void Unity_Multiply_float(float4 A, float4 B, out float4 Out)
			{
			    Out = A * B;
			}
			void Unity_ColorspaceConversion_HSV_RGB_float(float3 In, out float3 Out)
			{
			    float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
			    float3 P = abs(frac(In.xxx + K.xyz) * 6.0 - K.www);
			    Out = In.z * lerp(K.xxx, saturate(P - K.xxx), In.y);
			}
			void Unity_Subtract_float(float A, float B, out float Out)
			{
			    Out = A - B;
			}
			void Unity_Add_float(float3 A, float3 B, out float3 Out)
			{
			    Out = A + B;
			}
			struct GraphVertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 texcoord0 : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			struct SurfaceInputs{
				float3 ObjectSpaceNormal;
				float3 ObjectSpaceTangent;
				float3 ObjectSpaceBiTangent;
				float3 TangentSpaceViewDirection;
				half4 uv0;
			};
			struct SurfaceDescription{
				float4 PreviewOutput;
			};
			float Float_9FB7287C;
			UNITY_DECLARE_TEX2D(Texture_507A46B3);
			float Float_D2D9489B;
			UNITY_DECLARE_TEX2D(Texture_E2350D28);
			UNITY_DECLARE_TEX2D(_Texture2D_3BDAFFBE_Tex);
			float4 _Texture2D_3BDAFFBE_UV;
			UNITY_DECLARE_TEX2D(_Texture2D_43A49A96_Tex);
			float4 _Texture2D_43A49A96_UV;
			float Vector1_A9FE7B6C;
			float4 _Texture2D_63B21599_UV;
			float _Power_8AAB91A9_B;
			float4 _Remap_4DA15C85_InMinMax;
			float4 _Remap_4DA15C85_OutMinMax;
			float Vector1_91963E7E;
			float _Multiply_9DFBFF7B_B;
			float4 _Hue_6D737DC1_In;
			float Vector1_1781D992;
			float4 Vector3_88660F88;
			float4 _NormalCreate_8B6271FA_UV;
			float _NormalCreate_8B6271FA_Offset;
			float _NormalCreate_8B6271FA_Strength;
			float4 _Texture2D_CE7FDB22_UV;
			float _Add_955670F1_B;
			float _Combine_76C873_G;
			float _Combine_76C873_B;
			float _Combine_76C873_A;
			float _Multiply_77943E40_B;
			float Vector1_FA7B64E2;
			float _Subtract_4053AE6C_A;
			float _Combine_EBDC57C5_G;
			float _Combine_EBDC57C5_B;
			float _Combine_EBDC57C5_A;
			float4 _Remap_49700E8_InMinMax;
			float4 _Remap_49700E8_OutMinMax;
			GraphVertexInput PopulateVertexData(GraphVertexInput v){
				return v;
			}
			SurfaceDescription PopulateSurfaceData(SurfaceInputs IN) {
				SurfaceDescription surface = (SurfaceDescription)0;
				float3 ObjectSpaceNormal = IN.ObjectSpaceNormal;
				float3 ObjectSpaceTangent = IN.ObjectSpaceTangent;
				float3 ObjectSpaceBiTangent = IN.ObjectSpaceBiTangent;
				float3 TangentSpaceViewDirection = IN.TangentSpaceViewDirection;
				half4 uv0 = IN.uv0;
				float4 _Texture2D_3BDAFFBE_RGBA = UNITY_SAMPLE_TEX2D(_Texture2D_3BDAFFBE_Tex,uv0.xy);
				float _Texture2D_3BDAFFBE_R = _Texture2D_3BDAFFBE_RGBA.r;
				float _Texture2D_3BDAFFBE_G = _Texture2D_3BDAFFBE_RGBA.g;
				float _Texture2D_3BDAFFBE_B = _Texture2D_3BDAFFBE_RGBA.b;
				float _Texture2D_3BDAFFBE_A = _Texture2D_3BDAFFBE_RGBA.a;
				if (Float_9FB7287C == 0) { surface.PreviewOutput = half4(_Texture2D_3BDAFFBE_RGBA.x, _Texture2D_3BDAFFBE_RGBA.y, _Texture2D_3BDAFFBE_RGBA.z, 1.0); return surface; }
				float4 _Texture2D_43A49A96_RGBA = UNITY_SAMPLE_TEX2D(_Texture2D_43A49A96_Tex,uv0.xy);
				float _Texture2D_43A49A96_R = _Texture2D_43A49A96_RGBA.r;
				float _Texture2D_43A49A96_G = _Texture2D_43A49A96_RGBA.g;
				float _Texture2D_43A49A96_B = _Texture2D_43A49A96_RGBA.b;
				float _Texture2D_43A49A96_A = _Texture2D_43A49A96_RGBA.a;
				if (Float_9FB7287C == 1) { surface.PreviewOutput = half4(_Texture2D_43A49A96_RGBA.x, _Texture2D_43A49A96_RGBA.y, _Texture2D_43A49A96_RGBA.z, 1.0); return surface; }
				float4 _Texture2D_63B21599_RGBA = UNITY_SAMPLE_TEX2D(Texture_507A46B3,uv0.xy);
				float _Texture2D_63B21599_R = _Texture2D_63B21599_RGBA.r;
				float _Texture2D_63B21599_G = _Texture2D_63B21599_RGBA.g;
				float _Texture2D_63B21599_B = _Texture2D_63B21599_RGBA.b;
				float _Texture2D_63B21599_A = _Texture2D_63B21599_RGBA.a;
				if (Float_9FB7287C == 2) { surface.PreviewOutput = half4(_Texture2D_63B21599_RGBA.x, _Texture2D_63B21599_RGBA.y, _Texture2D_63B21599_RGBA.z, 1.0); return surface; }
				float _OneMinus_D38B6E7C_Out;
				Unity_OneMinus_float(_Texture2D_63B21599_R, _OneMinus_D38B6E7C_Out);
				if (Float_9FB7287C == 3) { surface.PreviewOutput = half4(_OneMinus_D38B6E7C_Out, _OneMinus_D38B6E7C_Out, _OneMinus_D38B6E7C_Out, 1.0); return surface; }
				float _Power_8AAB91A9_Out;
				Unity_Power_float(_OneMinus_D38B6E7C_Out, _Power_8AAB91A9_B, _Power_8AAB91A9_Out);
				if (Float_9FB7287C == 4) { surface.PreviewOutput = half4(_Power_8AAB91A9_Out, _Power_8AAB91A9_Out, _Power_8AAB91A9_Out, 1.0); return surface; }
				float _Remap_4DA15C85_Out;
				Unity_Remap_float(_Power_8AAB91A9_Out, _Remap_4DA15C85_InMinMax, _Remap_4DA15C85_OutMinMax, _Remap_4DA15C85_Out);
				if (Float_9FB7287C == 5) { surface.PreviewOutput = half4(_Remap_4DA15C85_Out, _Remap_4DA15C85_Out, _Remap_4DA15C85_Out, 1.0); return surface; }
				float _Multiply_9DFBFF7B_Out;
				Unity_Multiply_float(_Time.y, _Multiply_9DFBFF7B_B, _Multiply_9DFBFF7B_Out);
				if (Float_9FB7287C == 6) { surface.PreviewOutput = half4(_Multiply_9DFBFF7B_Out, _Multiply_9DFBFF7B_Out, _Multiply_9DFBFF7B_Out, 1.0); return surface; }
				float3 _Hue_6D737DC1_Out;
				Unity_Hue_Normalized_float(_Hue_6D737DC1_In, _Multiply_9DFBFF7B_Out, _Hue_6D737DC1_Out);
				if (Float_9FB7287C == 7) { surface.PreviewOutput = half4(_Hue_6D737DC1_Out.x, _Hue_6D737DC1_Out.y, _Hue_6D737DC1_Out.z, 1.0); return surface; }
				float3 _Power_C4E20D3B_Out;
				Unity_Power_float(_Hue_6D737DC1_Out, (Vector1_1781D992.xxx), _Power_C4E20D3B_Out);
				if (Float_9FB7287C == 8) { surface.PreviewOutput = half4(_Power_C4E20D3B_Out.x, _Power_C4E20D3B_Out.y, _Power_C4E20D3B_Out.z, 1.0); return surface; }
				float3 _Divide_B0B110CE_Out;
				Unity_Divide_float(_Power_C4E20D3B_Out, Vector3_88660F88, _Divide_B0B110CE_Out);
				if (Float_9FB7287C == 9) { surface.PreviewOutput = half4(_Divide_B0B110CE_Out.x, _Divide_B0B110CE_Out.y, _Divide_B0B110CE_Out.z, 1.0); return surface; }
				float _Reciprocal_93F3D817_Out;
				Unity_Reciprocal_float(Vector1_1781D992, _Reciprocal_93F3D817_Out);
				if (Float_9FB7287C == 10) { surface.PreviewOutput = half4(_Reciprocal_93F3D817_Out, _Reciprocal_93F3D817_Out, _Reciprocal_93F3D817_Out, 1.0); return surface; }
				float3 _Power_E77DC98F_Out;
				Unity_Power_float(_Divide_B0B110CE_Out, (_Reciprocal_93F3D817_Out.xxx), _Power_E77DC98F_Out);
				if (Float_9FB7287C == 11) { surface.PreviewOutput = half4(_Power_E77DC98F_Out.x, _Power_E77DC98F_Out.y, _Power_E77DC98F_Out.z, 1.0); return surface; }
				float3 _Multiply_731F83FC_Out;
				Unity_Multiply_float(_Power_E77DC98F_Out, (Vector1_91963E7E.xxx), _Multiply_731F83FC_Out);
				if (Float_9FB7287C == 12) { surface.PreviewOutput = half4(_Multiply_731F83FC_Out.x, _Multiply_731F83FC_Out.y, _Multiply_731F83FC_Out.z, 1.0); return surface; }
				float3 _NormalCreate_8B6271FA_Out;
				Unity_NormalCreate_float(Texture_507A46B3, samplerTexture_507A46B3, uv0.xy, _NormalCreate_8B6271FA_Offset, _NormalCreate_8B6271FA_Strength, _NormalCreate_8B6271FA_Out);
				if (Float_9FB7287C == 13) { surface.PreviewOutput = half4(_NormalCreate_8B6271FA_Out.x, _NormalCreate_8B6271FA_Out.y, _NormalCreate_8B6271FA_Out.z, 1.0); return surface; }
				if (Float_9FB7287C == 14) { surface.PreviewOutput = half4(TangentSpaceViewDirection.x, TangentSpaceViewDirection.y, TangentSpaceViewDirection.z, 1.0); return surface; }
				float _Property_DEDFE7DB_Out = Float_D2D9489B;
				float _FresnelEffect_F1C111ED_Out;
				Unity_FresnelEffect_float(_NormalCreate_8B6271FA_Out, TangentSpaceViewDirection, _Property_DEDFE7DB_Out, _FresnelEffect_F1C111ED_Out);
				if (Float_9FB7287C == 15) { surface.PreviewOutput = half4(_FresnelEffect_F1C111ED_Out, _FresnelEffect_F1C111ED_Out, _FresnelEffect_F1C111ED_Out, 1.0); return surface; }
				float4 _Texture2D_CE7FDB22_RGBA = UNITY_SAMPLE_TEX2D(Texture_507A46B3,uv0.xy);
				float _Texture2D_CE7FDB22_R = _Texture2D_CE7FDB22_RGBA.r;
				float _Texture2D_CE7FDB22_G = _Texture2D_CE7FDB22_RGBA.g;
				float _Texture2D_CE7FDB22_B = _Texture2D_CE7FDB22_RGBA.b;
				float _Texture2D_CE7FDB22_A = _Texture2D_CE7FDB22_RGBA.a;
				if (Float_9FB7287C == 16) { surface.PreviewOutput = half4(_Texture2D_CE7FDB22_RGBA.x, _Texture2D_CE7FDB22_RGBA.y, _Texture2D_CE7FDB22_RGBA.z, 1.0); return surface; }
				float _Add_955670F1_Out;
				Unity_Add_float(_SinTime.w, _Add_955670F1_B, _Add_955670F1_Out);
				if (Float_9FB7287C == 17) { surface.PreviewOutput = half4(_Add_955670F1_Out, _Add_955670F1_Out, _Add_955670F1_Out, 1.0); return surface; }
				float4 _Combine_76C873_RGBA;
				Unity_Combine_float(_Add_955670F1_Out, _Combine_76C873_G, _Combine_76C873_B, _Combine_76C873_A, _Combine_76C873_RGBA);
				if (Float_9FB7287C == 18) { surface.PreviewOutput = half4(_Combine_76C873_RGBA.x, _Combine_76C873_RGBA.y, _Combine_76C873_RGBA.z, 1.0); return surface; }
				float4 _Multiply_89C71AC1_Out;
				Unity_Multiply_float(_Texture2D_CE7FDB22_RGBA, _Combine_76C873_RGBA, _Multiply_89C71AC1_Out);
				if (Float_9FB7287C == 19) { surface.PreviewOutput = half4(_Multiply_89C71AC1_Out.x, _Multiply_89C71AC1_Out.y, _Multiply_89C71AC1_Out.z, 1.0); return surface; }
				float3 _ColorspaceConversion_3570283C_Out;
				Unity_ColorspaceConversion_HSV_RGB_float((_Multiply_89C71AC1_Out.xyz), _ColorspaceConversion_3570283C_Out);
				if (Float_9FB7287C == 20) { surface.PreviewOutput = half4(_ColorspaceConversion_3570283C_Out.x, _ColorspaceConversion_3570283C_Out.y, _ColorspaceConversion_3570283C_Out.z, 1.0); return surface; }
				float _Split_81BA869A_R = _ColorspaceConversion_3570283C_Out[0];
				float _Split_81BA869A_G = _ColorspaceConversion_3570283C_Out[1];
				float _Split_81BA869A_B = _ColorspaceConversion_3570283C_Out[2];
				float _Split_81BA869A_A = 0;
				float _Multiply_9164EC34_Out;
				Unity_Multiply_float(_Split_81BA869A_R, _Split_81BA869A_G, _Multiply_9164EC34_Out);
				if (Float_9FB7287C == 21) { surface.PreviewOutput = half4(_Multiply_9164EC34_Out, _Multiply_9164EC34_Out, _Multiply_9164EC34_Out, 1.0); return surface; }
				float _Multiply_841C266E_Out;
				Unity_Multiply_float(_Multiply_9164EC34_Out, _Split_81BA869A_B, _Multiply_841C266E_Out);
				if (Float_9FB7287C == 22) { surface.PreviewOutput = half4(_Multiply_841C266E_Out, _Multiply_841C266E_Out, _Multiply_841C266E_Out, 1.0); return surface; }
				float _Multiply_77943E40_Out;
				Unity_Multiply_float(_Multiply_841C266E_Out, _Multiply_77943E40_B, _Multiply_77943E40_Out);
				if (Float_9FB7287C == 23) { surface.PreviewOutput = half4(_Multiply_77943E40_Out, _Multiply_77943E40_Out, _Multiply_77943E40_Out, 1.0); return surface; }
				float _Multiply_CC88C9FE_Out;
				Unity_Multiply_float(_FresnelEffect_F1C111ED_Out, Vector1_FA7B64E2, _Multiply_CC88C9FE_Out);
				if (Float_9FB7287C == 24) { surface.PreviewOutput = half4(_Multiply_CC88C9FE_Out, _Multiply_CC88C9FE_Out, _Multiply_CC88C9FE_Out, 1.0); return surface; }
				float _OneMinus_F47415CA_Out;
				Unity_OneMinus_float(_Multiply_77943E40_Out, _OneMinus_F47415CA_Out);
				if (Float_9FB7287C == 25) { surface.PreviewOutput = half4(_OneMinus_F47415CA_Out, _OneMinus_F47415CA_Out, _OneMinus_F47415CA_Out, 1.0); return surface; }
				float _Subtract_4053AE6C_Out;
				Unity_Subtract_float(_Subtract_4053AE6C_A, _CosTime.w, _Subtract_4053AE6C_Out);
				if (Float_9FB7287C == 26) { surface.PreviewOutput = half4(_Subtract_4053AE6C_Out, _Subtract_4053AE6C_Out, _Subtract_4053AE6C_Out, 1.0); return surface; }
				float4 _Combine_EBDC57C5_RGBA;
				Unity_Combine_float(_Subtract_4053AE6C_Out, _Combine_EBDC57C5_G, _Combine_EBDC57C5_B, _Combine_EBDC57C5_A, _Combine_EBDC57C5_RGBA);
				if (Float_9FB7287C == 27) { surface.PreviewOutput = half4(_Combine_EBDC57C5_RGBA.x, _Combine_EBDC57C5_RGBA.y, _Combine_EBDC57C5_RGBA.z, 1.0); return surface; }
				float3 _ColorspaceConversion_C8F0BBD5_Out;
				Unity_ColorspaceConversion_HSV_RGB_float((_Combine_EBDC57C5_RGBA.xyz), _ColorspaceConversion_C8F0BBD5_Out);
				if (Float_9FB7287C == 28) { surface.PreviewOutput = half4(_ColorspaceConversion_C8F0BBD5_Out.x, _ColorspaceConversion_C8F0BBD5_Out.y, _ColorspaceConversion_C8F0BBD5_Out.z, 1.0); return surface; }
				float3 _Multiply_E8E6BAEA_Out;
				Unity_Multiply_float((_OneMinus_F47415CA_Out.xxx), _ColorspaceConversion_C8F0BBD5_Out, _Multiply_E8E6BAEA_Out);
				if (Float_9FB7287C == 29) { surface.PreviewOutput = half4(_Multiply_E8E6BAEA_Out.x, _Multiply_E8E6BAEA_Out.y, _Multiply_E8E6BAEA_Out.z, 1.0); return surface; }
				float3 _Multiply_156218A9_Out;
				Unity_Multiply_float((_Multiply_CC88C9FE_Out.xxx), _Multiply_E8E6BAEA_Out, _Multiply_156218A9_Out);
				if (Float_9FB7287C == 30) { surface.PreviewOutput = half4(_Multiply_156218A9_Out.x, _Multiply_156218A9_Out.y, _Multiply_156218A9_Out.z, 1.0); return surface; }
				float3 _Multiply_F77E48F9_Out;
				Unity_Multiply_float(_ColorspaceConversion_3570283C_Out, (Vector1_A9FE7B6C.xxx), _Multiply_F77E48F9_Out);
				if (Float_9FB7287C == 31) { surface.PreviewOutput = half4(_Multiply_F77E48F9_Out.x, _Multiply_F77E48F9_Out.y, _Multiply_F77E48F9_Out.z, 1.0); return surface; }
				float3 _Multiply_5F49F55E_Out;
				Unity_Multiply_float(_Multiply_F77E48F9_Out, (_Texture2D_3BDAFFBE_RGBA.xyz), _Multiply_5F49F55E_Out);
				if (Float_9FB7287C == 32) { surface.PreviewOutput = half4(_Multiply_5F49F55E_Out.x, _Multiply_5F49F55E_Out.y, _Multiply_5F49F55E_Out.z, 1.0); return surface; }
				float3 _Add_A93EC3AF_Out;
				Unity_Add_float(_Multiply_156218A9_Out, _Multiply_5F49F55E_Out, _Add_A93EC3AF_Out);
				if (Float_9FB7287C == 33) { surface.PreviewOutput = half4(_Add_A93EC3AF_Out.x, _Add_A93EC3AF_Out.y, _Add_A93EC3AF_Out.z, 1.0); return surface; }
				float _Remap_49700E8_Out;
				Unity_Remap_float(_SinTime.w, _Remap_49700E8_InMinMax, _Remap_49700E8_OutMinMax, _Remap_49700E8_Out);
				if (Float_9FB7287C == 34) { surface.PreviewOutput = half4(_Remap_49700E8_Out, _Remap_49700E8_Out, _Remap_49700E8_Out, 1.0); return surface; }
				float3 _Multiply_D447D798_Out;
				Unity_Multiply_float(_Multiply_731F83FC_Out, (_Remap_49700E8_Out.xxx), _Multiply_D447D798_Out);
				if (Float_9FB7287C == 35) { surface.PreviewOutput = half4(_Multiply_D447D798_Out.x, _Multiply_D447D798_Out.y, _Multiply_D447D798_Out.z, 1.0); return surface; }
				float3 _Multiply_3760727D_Out;
				Unity_Multiply_float((_Power_8AAB91A9_Out.xxx), _Multiply_D447D798_Out, _Multiply_3760727D_Out);
				if (Float_9FB7287C == 36) { surface.PreviewOutput = half4(_Multiply_3760727D_Out.x, _Multiply_3760727D_Out.y, _Multiply_3760727D_Out.z, 1.0); return surface; }
				return surface;
			}
	ENDCG
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
	        struct GraphVertexOutput
	        {
	            float4 position : POSITION;
	            float3 WorldSpaceNormal : TEXCOORD0;
	float3 WorldSpaceTangent : TEXCOORD1;
	float3 WorldSpaceBiTangent : TEXCOORD2;
	float3 WorldSpaceViewDirection : TEXCOORD3;
	half4 uv0 : TEXCOORD4;
	        };
	        GraphVertexOutput vert (GraphVertexInput v)
	        {
	            v = PopulateVertexData(v);
	            GraphVertexOutput o;
	            o.position = UnityObjectToClipPos(v.vertex);
	            o.WorldSpaceNormal = mul(v.normal,(float3x3)unity_WorldToObject);
	o.WorldSpaceTangent = mul((float3x3)unity_ObjectToWorld,v.tangent);
	o.WorldSpaceBiTangent = normalize(cross(o.WorldSpaceNormal, o.WorldSpaceTangent.xyz) * v.tangent.w);
	o.WorldSpaceViewDirection = mul((float3x3)unity_ObjectToWorld,ObjSpaceViewDir(v.vertex));
	o.uv0 = v.texcoord0;
	            return o;
	        }
	        fixed4 frag (GraphVertexOutput IN) : SV_Target
	        {
	            float3 WorldSpaceNormal = normalize(IN.WorldSpaceNormal);
	float3 WorldSpaceTangent = IN.WorldSpaceTangent;
	float3 WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
	float3 WorldSpaceViewDirection = normalize(IN.WorldSpaceViewDirection);
	float3x3 tangentSpaceTransform = float3x3(WorldSpaceTangent,WorldSpaceBiTangent,WorldSpaceNormal);
	float3 ObjectSpaceNormal = mul(WorldSpaceNormal,(float3x3)unity_ObjectToWorld);
	float3 ObjectSpaceTangent = mul((float3x3)unity_WorldToObject,WorldSpaceTangent);
	float3 ObjectSpaceBiTangent = mul((float3x3)unity_WorldToObject,WorldSpaceBiTangent);
	float3 TangentSpaceViewDirection = mul(WorldSpaceViewDirection,(float3x3)tangentSpaceTransform);
	float4 uv0  = IN.uv0;
	            SurfaceInputs surfaceInput = (SurfaceInputs)0;;
	            surfaceInput.ObjectSpaceNormal = ObjectSpaceNormal;
	surfaceInput.ObjectSpaceTangent = ObjectSpaceTangent;
	surfaceInput.ObjectSpaceBiTangent = ObjectSpaceBiTangent;
	surfaceInput.TangentSpaceViewDirection = TangentSpaceViewDirection;
	surfaceInput.uv0 = uv0;
	            SurfaceDescription surf = PopulateSurfaceData(surfaceInput);
	            return surf.PreviewOutput;
	        }
	        ENDCG
	    }
	}
}
