Shader "Custom/Distort"
{
	Properties
	{
		_DistortColor("Distort Color", Color) = (1,1,1,1)
		_BumpAmt("Distortion", Range(0,128)) = 10
		_DistortTex("Distort Texture (RGB)", 2D) = "white" {}
		_BumpMap("Normal Map", 2D) = "bump" {}
	}

		SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
		}

		GrabPass{}

		Pass
		{
			Name "DISTORT"

			CGPROGRAM


			#pragma vertex vert

			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 uvgrab : TEXCOORD0;
				float2 uvbump : TEXCOORD1;
				float uvmain : TEXCOORD2;
			};

			float _BumpAmt;
			float4 _BumpMap_ST;
			float4 _DistortTex_ST;
			fixed4 _DistortColor;
			sampler2D _GrabTexture;
			float4 _GrabTexture_TexelSize;
			sampler2D _BumpMap;
			sampler2D _DistortTex;

			v2f vert(appdata IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);

				#if UNITY_UV_STARTS_AT_TOP
					float scale = -1.0;
				#else
					float scale = 1.0;
				#endif

				OUT.uvgrab.xy = (float2(OUT.vertex.x, OUT.vertex.y * scale) + OUT.vertex.w) * 0.5;
				OUT.uvgrab.zw = OUT.vertex.zw;

				OUT.uvbump = TRANSFORM_TEX(IN.texcoord, _BumpMap);
				OUT.uvmain = TRANSFORM_TEX(IN.texcoord, _DistortTex);

				return OUT;
			}

			half4 frag(v2f IN) : COLOR
			{
				half2 bump = UnpackNormal(tex2D(_BumpMap, IN.uvbump)).rg;
				float2 offset = bump * _BumpAmt * _GrabTexture_TexelSize.xy;
				IN.uvgrab.xy = offset * IN.uvgrab.z + IN.uvgrab.xy;

				half4 col = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(IN.uvgrab));
				half4 tint = tex2D(_DistortTex, IN.uvmain) * _DistortColor;

				return col * tint;
			}

			ENDCG
		}
	}
}
