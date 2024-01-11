Shader "Unlit/NewUnlitShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_LEDTex("LED Texture",2D) = "white"{}
		_TileX("TileX",float) = 1
		_TileY("TileY",float) = 1
		_Brightness("brightness",range(0,10)) = 1
	}
	SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert 
			#pragma fragment frag
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};



			struct v2f
			{
				float2 uv : TEXCOORD0;
				float3 normal : TEXCOORD3;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _LEDTex;
			float4 _MainTex_ST;
			float _Brightness;
			float _TileX;
			float _TileY;

			v2f vert(appdata v)
			{
				v2f o;
				//커브드 전광판 ㅎㅎ
				float2 value = v.uv.xy;
				float dist = distance(value.x, 0.5);
				v.vertex.xyz += v.normal * 10 * dist*dist; 

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);


				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float2 UVxy = float2(floor(i.uv.x * _TileX) / _TileX, floor(i.uv.y * _TileY) / _TileY);
				fixed4 c = tex2D(_MainTex, UVxy);

				float2 LEDxy = float2(i.uv.x * _TileX, i.uv.y * _TileY);
				float4 d = tex2D(_LEDTex, LEDxy);

				return c * d*_Brightness;
			}
			ENDCG

		}
	}
}