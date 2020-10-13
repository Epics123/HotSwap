Shader "Unlit/HologramShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1, 0, 0, 1)
		_Bias("Bias", Float) = 0
		_ScanningFrequency("Scanning Frequency", Float) = 100
		_ScanningSpeed("Scanning Speed", Float) = 100
		_OutlineColor("Outline", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel Snap", float) = 0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off


        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile _ PIXELSNAP_ON

            #include "UnityCG.cginc"

			float _Bias;
			float _ScanningFrequency;
			float _ScanningSpeed;

            struct appdata
            {
                float4 vertex : POSITION;
				float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
				float4 objVertex : TEXCOORD1;
            };


			fixed4 _Color;
			fixed4 _OutlineColor;
            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
				o.objVertex = mul(unity_ObjectToWorld, v.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv = v.uv;
                UNITY_TRANSFER_FOG(o,o.vertex);
				o.objVertex = UnityPixelSnap(o.objVertex);
				
                return o;
            }

			float4 _MainTex_TexelSize;

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

				//Set the color to represent a sin graph that changes over time
				col = col * max(0, sin(i.vertex.y * _ScanningFrequency + _Time.x * _ScanningSpeed) + _Bias);
				//col *= 0 + max(0, cos(i.objVertex.x * _ScanningFrequency + _Time.x * _ScanningSpeed) + 0.9);
				//col *= 0.5 + max(0, cos(i.objVertex.z * _ScanningFrequency + _Time.x * _ScanningSpeed) + 0.9);

				//Fix the black lines at the end
				fixed4 up = tex2D(_MainTex, i.uv + fixed2(0, _MainTex_TexelSize.y));
				fixed4 down = tex2D(_MainTex, i.uv - fixed2(0, _MainTex_TexelSize.y));
				fixed4 left = tex2D(_MainTex, i.uv - fixed2(_MainTex_TexelSize.x, 0));
				fixed4 right = tex2D(_MainTex, i.uv + fixed2(_MainTex_TexelSize.x, 0));
				if (up.a * down.a * left.a * right.a == 0) {
					col.rgb = _OutlineColor.rgb;
				}
				col.rgb *= col.a;

                return col;
            }
            ENDCG
        }
    }
}
