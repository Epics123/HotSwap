Shader "Custom/OutlineBlur"
{
	Properties
	{
		_BlurRadius("Blur Radius", Range(0.0,20.0)) = 1
		_Intensity("Blur Intensity", Range(0.0,1.0)) = 0.01
		_OutlineWidth("Outline Width", Range(1.0,  10.0)) = 1.1
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
			Name "OUTLINEHORIZONTALBLUR"
			ZWrite Off

			CGPROGRAM//Allows talk between two languages: shader lab and nvidia C for graphics.

			#pragma vertex vert//Define for the building function.

			#pragma fragment frag//Define for coloring function.

			#include "UnityCG.cginc"//Built in shader functions.

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 uvgrab : TEXCOORD0;
			};

			float _BlurRadius;
			float _Intensity;
			sampler2D _GrabTexture;
			float4 _GrabTexture_TexelSize;
			float _OutlineWidth;
			

			v2f vert(appdata_base IN)
			{
				IN.vertex.xyz *= _OutlineWidth + 0.1;
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);

				#if UNITY_UV_STARTS_AT_TOP
					float scale = -1.0;
				#else
					float scale = 1.0;
				#endif

				OUT.uvgrab.xy = (float2(OUT.vertex.x, OUT.vertex.y * scale) + OUT.vertex.w) * 0.5;
				OUT.uvgrab.zw = OUT.vertex.zw;

				return OUT;
			}

			half4 frag(v2f IN) : COLOR
			{
				half4 texcol = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(IN.uvgrab));
				half4 texsum = half4(0, 0, 0, 0);

				#define GRABPIXEL(weight, kernelx) tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(float4(IN.uvgrab.x + _GrabTexture_TexelSize.x * kernelx * _BlurRadius, IN.uvgrab.y,  IN.uvgrab.z,  IN.uvgrab.w))) * weight

				texsum += GRABPIXEL(0.05, -4.0);
				texsum += GRABPIXEL(0.09, -3.0);
				texsum += GRABPIXEL(0.12, -2.0);
				texsum += GRABPIXEL(0.15, -1.0);
				texsum += GRABPIXEL(0.18, 0.0);
				texsum += GRABPIXEL(0.15, 1.0);
				texsum += GRABPIXEL(0.12, 2.0);
				texsum += GRABPIXEL(0.09, 3.0);
				texsum += GRABPIXEL(0.05, 4.0);

				texcol = lerp(texcol, texsum, _Intensity);
				return texcol;
			}

			ENDCG
		}

		GrabPass{}

		Pass
		{
			Name "OUTLINEVERTICALBLUR"
			ZWrite Off

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
			float _Intensity;
			sampler2D _GrabTexture;
			float4 _GrabTexture_TexelSize;
			float _OutlineWidth;

			v2f vert(appdata_base IN)
			{
				IN.vertex.xyz *= _OutlineWidth + 0.1;
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);

				#if UNITY_UV_STARTS_AT_TOP
					float scale = -1.0;
				#else
					float scale = 1.0;
				#endif

				OUT.uvgrab.xy = (float2(OUT.vertex.x, OUT.vertex.y * scale) + OUT.vertex.w) * 0.5;
				OUT.uvgrab.zw = OUT.vertex.zw;

				return OUT;
			}

			half4 frag(v2f IN) : COLOR
			{
				half4 texcol = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(IN.uvgrab));
				half4 texsum = half4(0, 0, 0, 0);

				#define GRABPIXEL(weight, kernely) tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(float4(IN.uvgrab.x, IN.uvgrab.y + _GrabTexture_TexelSize.y * kernely * _BlurRadius,  IN.uvgrab.z,  IN.uvgrab.w))) * weight

				texsum += GRABPIXEL(0.05, -4.0);
				texsum += GRABPIXEL(0.09, -3.0);
				texsum += GRABPIXEL(0.12, -2.0);
				texsum += GRABPIXEL(0.15, -1.0);
				texsum += GRABPIXEL(0.18, 0.0);
				texsum += GRABPIXEL(0.15, 1.0);
				texsum += GRABPIXEL(0.12, 2.0);
				texsum += GRABPIXEL(0.09, 3.0);
				texsum += GRABPIXEL(0.05, 4.0);

				texcol = lerp(texcol, texsum, _Intensity);
				return texcol;
			}

			ENDCG
		}	
	}
}
