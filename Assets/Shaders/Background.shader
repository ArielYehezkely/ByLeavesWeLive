Shader "Unlit/Background"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags{ "Queue" = "Background-1" "RenderType" = "Background" "PreviewType" = "Skybox" }
		Cull Off ZWrite Off

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

		void vert(float4 pos : POSITION, out float4 outUV : TEXCOORD0, out float4 outPos : SV_POSITION)
	{
		outPos = UnityObjectToClipPos(pos);
		outUV = ComputeScreenPos(outPos);
	}

	sampler2D _MainTex;

	fixed4 frag(float4 uv : TEXCOORD0) : SV_Target
	{
		fixed4 col = tex2Dproj(_MainTex, uv);
	return col;
	}
		ENDCG
	}
	}
}
