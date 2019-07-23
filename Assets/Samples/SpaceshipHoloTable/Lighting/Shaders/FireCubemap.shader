Shader "CustomRenderTexture/FireCube"
{
	Properties
	{
		[NoScaleOffset] Texture_DE2106FD("Warp", 2D) = "white" {}
	[NoScaleOffset] Texture_7008A0A3("Gradient", 2D) = "white" {}
	Vector1_B70DB742("Speed", Float) = -0.2

	}
		SubShader
	{
		Pass
	{
		Lighting Off
		Blend One Zero

		CGPROGRAM
		#include "ShaderGraphLibrary/UnityCustomRenderTexture.hlsl"
		#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
		#pragma vertex CustomRenderTextureVertexShader
		#pragma fragment frag
		#pragma target 3.0

		float4 SRGBToLinear(float4 c) { return c; }
	float3 SRGBToLinear(float3 c) { return c; }

	struct SurfaceInputs
	{
		// update input values
		float3 localTexcoord;
		float3 globalTexcoord;
		uint primitiveID;
		float3 direction;
	};

	SurfaceInputs ConvertV2FToSurfaceInputs(v2f_customrendertexture IN)
	{
		SurfaceInputs o;

		o.localTexcoord = IN.localTexcoord;
		o.globalTexcoord = IN.globalTexcoord;
		o.primitiveID = IN.primitiveID;
		o.direction = IN.direction;

		return o;
	}

	TEXTURE2D(Texture_DE2106FD); SAMPLER(samplerTexture_DE2106FD);
	TEXTURE2D(Texture_7008A0A3); SAMPLER(samplerTexture_7008A0A3);
	float Vector1_B70DB742;


	void Unity_Normalize_float3(float3 In, out float3 Out)
	{
		Out = normalize(In);
	}

	void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
	{
		RGBA = float4(R, G, B, A);
		RGB = float3(R, G, B);
		RG = float2(R, G);
	}

	void Unity_Add_float2(float2 A, float2 B, out float2 Out)
	{
		Out = A + B;
	}

	void Unity_Multiply_float(float A, float B, out float Out)
	{
		Out = A * B;
	}

	void Unity_Add_float(float A, float B, out float Out)
	{
		Out = A + B;
	}

	void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
	{
		Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
	}

	void Unity_Saturate_float(float In, out float Out)
	{
		Out = saturate(In);
	}

	void Unity_Lerp_float(float A, float B, float T, out float Out)
	{
		Out = lerp(A, B, T);
	}

	struct SurfaceDescription {
		float4 Color;
	};

	SurfaceDescription PopulateSurfaceData(SurfaceInputs IN) {
		SurfaceDescription surface = (SurfaceDescription)0;
		float3 _CustomTextureUpdateData_76EB4F06_data = IN.direction;
		float3 _Normalize_5E3DDC13_Out;
		Unity_Normalize_float3(_CustomTextureUpdateData_76EB4F06_data, _Normalize_5E3DDC13_Out);
		float _Split_1A08C2A_R = _Normalize_5E3DDC13_Out[0];
		float _Split_1A08C2A_G = _Normalize_5E3DDC13_Out[1];
		float _Split_1A08C2A_B = _Normalize_5E3DDC13_Out[2];
		float _Split_1A08C2A_A = 0;
		float4 _Combine_36670950_RGBA;
		float3 _Combine_36670950_RGB;
		float2 _Combine_36670950_RG;
		Unity_Combine_float(0, _Time.y, 0, 0, _Combine_36670950_RGBA, _Combine_36670950_RGB, _Combine_36670950_RG);
		float3 _CustomTextureUpdateData_C598D595_data = IN.localTexcoord;
		float2 _Add_23D3A74F_Out;
		Unity_Add_float2(_Combine_36670950_RG, (_CustomTextureUpdateData_C598D595_data.xy), _Add_23D3A74F_Out);
		float4 _SampleTexture2D_44928D39_RGBA = SAMPLE_TEXTURE2D(Texture_DE2106FD, samplerTexture_DE2106FD, _Add_23D3A74F_Out);
		float _SampleTexture2D_44928D39_R = _SampleTexture2D_44928D39_RGBA.r;
		float _SampleTexture2D_44928D39_G = _SampleTexture2D_44928D39_RGBA.g;
		float _SampleTexture2D_44928D39_B = _SampleTexture2D_44928D39_RGBA.b;
		float _SampleTexture2D_44928D39_A = _SampleTexture2D_44928D39_RGBA.a;
		float _Property_9E71C038_Out = Vector1_B70DB742;
		float _Multiply_794EBB4D_Out;
		Unity_Multiply_float(_SampleTexture2D_44928D39_R, _Property_9E71C038_Out, _Multiply_794EBB4D_Out);

		float _Add_24C68C8E_Out;
		Unity_Add_float(_Split_1A08C2A_G, _Multiply_794EBB4D_Out, _Add_24C68C8E_Out);
		float _Remap_806149B5_Out;
		Unity_Remap_float(_Add_24C68C8E_Out, float2 (0,0.5), float2 (1,0), _Remap_806149B5_Out);
		float _Saturate_7D86D522_Out;
		Unity_Saturate_float(_Remap_806149B5_Out, _Saturate_7D86D522_Out);
		float _Lerp_47A88D7_Out;
		Unity_Lerp_float(_Saturate_7D86D522_Out, 1, 0.5, _Lerp_47A88D7_Out);
		float _Remap_D292502F_Out;
		Unity_Remap_float(_Add_24C68C8E_Out, float2 (-0.75,0), float2 (0,1), _Remap_D292502F_Out);
		float _Saturate_3BD360C0_Out;
		Unity_Saturate_float(_Remap_D292502F_Out, _Saturate_3BD360C0_Out);
		float _Multiply_11B2F691_Out;
		Unity_Multiply_float(_Lerp_47A88D7_Out, _Saturate_3BD360C0_Out, _Multiply_11B2F691_Out);

		float4 _Combine_20FA7893_RGBA;
		float3 _Combine_20FA7893_RGB;
		float2 _Combine_20FA7893_RG;
		Unity_Combine_float(_Multiply_11B2F691_Out, 0, 0, 0, _Combine_20FA7893_RGBA, _Combine_20FA7893_RGB, _Combine_20FA7893_RG);
		float4 _SampleTexture2D_85B25E49_RGBA = SAMPLE_TEXTURE2D(Texture_7008A0A3, samplerTexture_7008A0A3, _Combine_20FA7893_RG);
		float _SampleTexture2D_85B25E49_R = _SampleTexture2D_85B25E49_RGBA.r;
		float _SampleTexture2D_85B25E49_G = _SampleTexture2D_85B25E49_RGBA.g;
		float _SampleTexture2D_85B25E49_B = _SampleTexture2D_85B25E49_RGBA.b;
		float _SampleTexture2D_85B25E49_A = _SampleTexture2D_85B25E49_RGBA.a;
		surface.Color = _SampleTexture2D_85B25E49_RGBA;
		return surface;
	}



	float4 frag(v2f_customrendertexture IN) : COLOR
	{
		SurfaceInputs surfaceInput = ConvertV2FToSurfaceInputs(IN);

	SurfaceDescription surf = PopulateSurfaceData(surfaceInput);

	return surf.Color;
	}
		ENDCG
	}
	}

		FallBack "Hidden/InternalErrorShader"
}
