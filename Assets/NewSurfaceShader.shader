Shader "Custom/NewSurfaceShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_PaintMap("PaintMap",2D) = "white"{}
		_PaintMap_up("PaintMap_up",2D) = "white"{}
		_Thickness("Thickness",float) = 0
		_Tessellation("Tessellation",Range(1,32)) = 1
		_PaintColor("PaintColor",Color) = (0.8,0.8,0.8,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert addshadow tessellate:tessFixed

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		sampler2D _PaintMap;
		sampler2D _PaintMap_up;
		float _Thickness;
		float _Tessellation;
		fixed4 _PaintColor;

		float4 tessFixed()
		{
			return _Tessellation;
		}

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_PaintMap;
			float2 uv_PaintMap_up;
        };


        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

		void vert(inout appdata_full v)
		{
			float p = tex2Dlod(_PaintMap, v.texcoord).r;
			float tr = tex2Dlod(_PaintMap_up, v.texcoord).r;
			float3 worldPos = mul(unity_ObjectToWorld, v.vertex.xyz);
			worldPos.y += _Thickness * p * tr;
			v.vertex.xyz = mul(unity_WorldToObject, worldPos.xyz);
		}

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float4 c = tex2D(_MainTex, IN.uv_MainTex);
			float4 p = tex2D(_PaintMap, IN.uv_PaintMap);
			o.Albedo = c.rgb*(1 - (1 - p.rgb)*(1 - _PaintColor));
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
