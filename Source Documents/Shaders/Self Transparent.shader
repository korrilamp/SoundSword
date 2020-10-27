 Shader "Custom/Self Transparent"
 {
     Properties
     {
         _Color ("Color", Color) = (1,1,1,1)
         _MainTex ("Albedo (RGB)", 2D) = "white" {}
         _Glossiness ("Smoothness", Range(0,1)) = 0.5
         _Metallic ("Metallic", Range(0,1)) = 0.0
     }
 
     CGINCLUDE
 
     // Use shader model 3.0 target, to get nicer looking lighting
     #pragma target 3.0
 
     sampler2D _MainTex;
 
     struct Input
     {
         float2 uv_MainTex;
     };
 
     half _Glossiness;
     half _Metallic;
     fixed4 _Color;
 
     // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
     // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
     // #pragma instancing_options assumeuniformscaling
     UNITY_INSTANCING_BUFFER_START(Props)
         // put more per-instance properties here
     UNITY_INSTANCING_BUFFER_END(Props)
 
     void vert (inout appdata_full v)
     {
         float3 op = mul (unity_WorldToObject, float4 (_WorldSpaceCameraPos.xyz, 1)).xyz;
         float3 viewDir = v.vertex.xyz - op;
         v.normal.xyz *= dot (viewDir, v.normal.xyz) > 0.0 ? -1.0 : 1.0;
     }
 
     void surf (Input IN, inout SurfaceOutputStandard o)
     {
         // Albedo comes from a texture tinted by color
         fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
         o.Albedo = c.rgb;
         // Metallic and smoothness come from slider variables
         o.Metallic = _Metallic;
         o.Smoothness = _Glossiness;
         o.Alpha = c.a;
     }
 
     ENDCG
 
     SubShader
     {
         Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }
         LOD 200
 
         ZWrite Off
         Cull Front
         // Blend One One
         // BlendOp Sub
 
         //Optional to stop Z-fighting
         //Offset -1,-10
 
         CGPROGRAM
         #pragma surface surf Standard alpha:fade noshadow
         ENDCG
 
         // ZWrite Off
         // Cull Back
 
         // CGPROGRAM
         // #pragma surface surf Standard alpha:fade noshadow
         // ENDCG
     }
     FallBack "Diffuse"
 }