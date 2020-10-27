Shader "GreyscaleTransition" {
     Properties {
     
         _MainTex ("Base (RGB)", 2D) = "white" {}
         _BumpMap ("Bump (RGB)", 2D) = "black" {}
         _Brightness ("Brightness (Desaturated)", Range(-1,1)) = 0 
         _Fade ("Fade ",Range(0,1)) = 0
         _Alpha ("Alpha (Desaturated)", Range(0,1)) = 1
     }
     SubShader {
         Tags { 
             "RenderType"="Transparent"    
             "IgnoreProjector"="False"
             "Queue" = "Transparent" 
             
             }
         LOD 200
         Cull Off
         ZWrite on
         Blend SrcAlpha OneMinusSrcAlpha
         CGPROGRAM
         #pragma surface surf Lambert
 
         sampler2D _MainTex;
         sampler2D _BumpMap;
         fixed _Brightness;
         fixed _Fade;
         fixed _Alpha;
         
         struct Input {
             float2 uv_MainTex;
             float2 uv_BumpMap;
         };
 
         void surf (Input IN, inout SurfaceOutput o) {
             half4 c = tex2D (_MainTex, IN.uv_MainTex);
             fixed3 desat = dot(c.rgb, float3(0.3, 0.59, 0.11)) + _Brightness;
             fixed3 colour = c.rgb;
             o.Albedo = lerp(colour,desat,_Fade);
             o.Alpha =  lerp(c.a,_Alpha,_Fade);
             o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
             
         }
         ENDCG
     } 
     FallBack "Diffuse"
 }
 