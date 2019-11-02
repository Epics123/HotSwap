Shader "Custom/Blur"
{
	Properties
	{
		_BlurRadius("Blur Radius", Range(0.0, 20.0)) = 1
		_Intensity("Blur Intensity", Range(0.0,  1.0)) = 0.01
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
				Name "HORIZONTAL_BLUR"

				CGPROGRAM

				#pragma vertex vert

				#pragma fragment frag

				#include "UnityCG.cginc"

				struct v2f
				{
					float4 vertex : SV_POSITION;
					float4 uvgrab : TEXCOORD0;
				};

				float _BlurRadius;
				float4 _Intensity;
				sampler2D _GrabTexture;
				float4 _GrabTexture_TexelSize;

				v2f vert(appdata_base IN)
				{;
					v2f OUT;

					OUT.vertex = UnityObjectToClipPos(IN.vertex);

					#if UNITY_UV_STARTS_AT_TOP
						float scale = 1.0;
					#else
						float scale = 1.0
					#endif

					OUT.uvgrab.xy = (float2(OUT.vertex.x, OUT.vertex.y * scale) + OUT.vertex.w) * 0.5;
					OUT.uvgrab.zw = OUT.uvgrab.zw;

					return OUT;
				}

				half4 frag(v2f IN) : COLOR
				{
					half4 texcol = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(IN.uvgrab));
					half4 texsum = half4(0, 0, 0, 0);

					#define GRABPIXEL(weight, kernalx) tex2Dproj(_GrabTexure, UNITY_PROJ_COORD(float4(IN.uvgrab.x + _GrabTexture_TexelSize * kernalx * _BlurRadius, IN.uvgrab.y, IN.uvgrab.z, IN.uvgrab.w)))* weight

					texsum += GRABPIXEL(0.05, -4.0);

					texcol = lerp(texcol, texsum, _Intensity);
					return texcol;
				}

				ENDCG
			}

			Pass
			{
				Name "OBJECT"

				CGPROGRAM

				#pragma vertex vert

				#pragma fragment frag

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD0;
				};

				float4 _Color;
				sampler2D _MainTex;

				v2f vert(appdata IN)
				{
					v2f OUT;

					OUT.pos = UnityObjectToClipPos(IN.vertex);
					OUT.uv = IN.uv;

					return OUT;
				}

				fixed4 frag(v2f IN) : SV_Target
				{
					float4 texColor = tex2D(_MainTex, IN.uv);
					return texColor * _Color;
				}

				ENDCG
			}

		}
}
