// Upgrade NOTE: commented out 'float4 unity_DynamicLightmapST', a built-in variable
// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable

Shader "Transparent/Refractive"
{
  Properties
  {
    _MainTex ("Base (RGB), Alpha (A)", 2D) = "white" {}
    _BumpMap ("Normal Map (RGB)", 2D) = "bump" {}
    _Mask ("Specularity (R), Shininess (G), Refraction (B)", 2D) = "black" {}
    _Color ("Color Tint", Color) = (1,1,1,1)
    _Specular ("Specular Color", Color) = (0,0,0,0)
    _Focus ("Focus", Range(-100, 100)) = -100
    _Shininess ("Shininess", Range(0.01, 1)) = 0.2
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "QUEUE" = "Transparent+1"
      "RenderType" = "Transparent"
    }
    LOD 500
    Pass // ind: 1, name: 
    {
      Tags
      { 
      }
      ZClip Off
      ZWrite Off
      Cull Off
      Stencil
      { 
        Ref 0
        ReadMask 0
        WriteMask 0
        Pass Keep
        Fail Keep
        ZFail Keep
        PassFront Keep
        FailFront Keep
        ZFailFront Keep
        PassBack Keep
        FailBack Keep
        ZFailBack Keep
      } 
      // m_ProgramMask = 0
      
    } // end phase
    Pass // ind: 2, name: FORWARD
    {
      Name "FORWARD"
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "FORWARDBASE"
        "QUEUE" = "Transparent+1"
        "RenderType" = "Transparent"
      }
      LOD 500
      ZClip Off
      ZWrite Off
      Cull Off
      Blend SrcAlpha OneMinusSrcAlpha
      ColorMask RGB
      // m_ProgramMask = 6
      CGPROGRAM
      #pragma multi_compile DIRECTIONAL
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      #define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4 unity_SHAr;
      //uniform float4 unity_SHAg;
      //uniform float4 unity_SHAb;
      //uniform float4 unity_SHBr;
      //uniform float4 unity_SHBg;
      //uniform float4 unity_SHBb;
      //uniform float4 unity_SHC;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4 unity_WorldTransformParams;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _MainTex_ST;
      //uniform float3 _WorldSpaceCameraPos;
      //uniform float4 _WorldSpaceLightPos0;
      uniform float4 _LightColor0;
      uniform float4 _Color;
      uniform float4 _Specular;
      uniform float4 _GrabTexture_TexelSize;
      uniform float _Focus;
      uniform float _Shininess;
      uniform sampler2D _MainTex;
      uniform sampler2D _BumpMap;
      uniform sampler2D _Mask;
      uniform sampler2D _GrabTexture;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float4 tangent :TANGENT0;
          float3 normal :NORMAL0;
          float4 texcoord :TEXCOORD0;
          float4 color :COLOR0;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord :TEXCOORD0;
          float4 texcoord1 :TEXCOORD1;
          float4 texcoord2 :TEXCOORD2;
          float4 texcoord3 :TEXCOORD3;
          float4 color :COLOR0;
          float4 texcoord4 :TEXCOORD4;
          float4 texcoord5 :TEXCOORD5;
          float3 texcoord6 :TEXCOORD6;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 texcoord :TEXCOORD0;
          float4 texcoord1 :TEXCOORD1;
          float4 texcoord2 :TEXCOORD2;
          float4 texcoord3 :TEXCOORD3;
          float4 color :COLOR0;
          float4 texcoord5 :TEXCOORD5;
          float3 texcoord6 :TEXCOORD6;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat16_0;
      float4 u_xlat1;
      float4 u_xlat2;
      float4 u_xlat3;
      float3 u_xlat16_4;
      float3 u_xlat16_5;
      float u_xlat18;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          u_xlat0 = (in_v.vertex.yyyy * conv_mxt4x4_1(unity_ObjectToWorld));
          u_xlat0 = ((conv_mxt4x4_0(unity_ObjectToWorld) * in_v.vertex.xxxx) + u_xlat0);
          u_xlat0 = ((conv_mxt4x4_2(unity_ObjectToWorld) * in_v.vertex.zzzz) + u_xlat0);
          u_xlat1 = (u_xlat0 + conv_mxt4x4_3(unity_ObjectToWorld));
          u_xlat0.xyz = ((conv_mxt4x4_3(unity_ObjectToWorld).xyz * in_v.vertex.www) + u_xlat0.xyz);
          u_xlat1 = mul(unity_MatrixVP, u_xlat1);
          out_v.vertex = u_xlat1;
          out_v.texcoord.xy = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          out_v.texcoord1.w = u_xlat0.x;
          u_xlat2.xyz = (in_v.tangent.yyy * conv_mxt4x4_1(unity_ObjectToWorld).yzx);
          u_xlat2.xyz = ((conv_mxt4x4_0(unity_ObjectToWorld).yzx * in_v.tangent.xxx) + u_xlat2.xyz);
          u_xlat2.xyz = ((conv_mxt4x4_2(unity_ObjectToWorld).yzx * in_v.tangent.zzz) + u_xlat2.xyz);
          u_xlat0.x = dot(u_xlat2.xyz, u_xlat2.xyz);
          u_xlat0.x = rsqrt(u_xlat0.x);
          u_xlat2.xyz = (u_xlat0.xxx * u_xlat2.xyz);
          out_v.texcoord1.x = u_xlat2.z;
          u_xlat0.x = (in_v.tangent.w * unity_WorldTransformParams.w);
          u_xlat3.x = dot(in_v.normal.xyz, conv_mxt4x4_0(unity_WorldToObject).xyz);
          u_xlat3.y = dot(in_v.normal.xyz, conv_mxt4x4_1(unity_WorldToObject).xyz);
          u_xlat3.z = dot(in_v.normal.xyz, conv_mxt4x4_2(unity_WorldToObject).xyz);
          u_xlat3.xyz = normalize(u_xlat3.xyz);
          u_xlat16_4.xyz = (u_xlat2.xyz * u_xlat3.zxy);
          u_xlat16_4.xyz = ((u_xlat3.yzx * u_xlat2.yzx) + (-u_xlat16_4.xyz));
          u_xlat16_4.xyz = (u_xlat0.xxx * u_xlat16_4.xyz);
          out_v.texcoord1.y = u_xlat16_4.x;
          out_v.texcoord1.z = u_xlat3.x;
          out_v.texcoord2.x = u_xlat2.x;
          out_v.texcoord3.x = u_xlat2.y;
          out_v.texcoord2.w = u_xlat0.y;
          out_v.texcoord3.w = u_xlat0.z;
          out_v.texcoord2.y = u_xlat16_4.y;
          out_v.texcoord3.y = u_xlat16_4.z;
          out_v.texcoord2.z = u_xlat3.y;
          out_v.texcoord3.z = u_xlat3.z;
          out_v.color = in_v.color;
          out_v.texcoord4 = u_xlat1;
          u_xlat0.xy = (u_xlat1.ww + u_xlat1.xy);
          out_v.texcoord5.zw = u_xlat1.zw;
          out_v.texcoord5.xy = (u_xlat0.xy * float2(0.5, 0.5));
          u_xlat16_4.x = (u_xlat3.y * u_xlat3.y);
          u_xlat16_4.x = ((u_xlat3.x * u_xlat3.x) + (-u_xlat16_4.x));
          u_xlat16_0 = (u_xlat3.yzzx * u_xlat3.xyzz);
          u_xlat16_5.x = dot(unity_SHBr, u_xlat16_0);
          u_xlat16_5.y = dot(unity_SHBg, u_xlat16_0);
          u_xlat16_5.z = dot(unity_SHBb, u_xlat16_0);
          u_xlat16_4.xyz = ((unity_SHC.xyz * u_xlat16_4.xxx) + u_xlat16_5.xyz);
          u_xlat3.w = 1;
          u_xlat16_5.x = dot(unity_SHAr, u_xlat3);
          u_xlat16_5.y = dot(unity_SHAg, u_xlat3);
          u_xlat16_5.z = dot(unity_SHAb, u_xlat3);
          u_xlat16_4.xyz = (u_xlat16_4.xyz + u_xlat16_5.xyz);
          u_xlat16_4.xyz = max(u_xlat16_4.xyz, float3(0, 0, 0));
          u_xlat1.xyz = log2(u_xlat16_4.xyz);
          u_xlat1.xyz = (u_xlat1.xyz * float3(0.416666657, 0.416666657, 0.416666657));
          u_xlat1.xyz = exp2(u_xlat1.xyz);
          u_xlat1.xyz = ((u_xlat1.xyz * float3(1.05499995, 1.05499995, 1.05499995)) + float3(-0.0549999997, (-0.0549999997), (-0.0549999997)));
          u_xlat1.xyz = max(u_xlat1.xyz, float3(0, 0, 0));
          out_v.texcoord6.xyz = u_xlat1.xyz;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float3 u_xlat0_d;
      float4 u_xlat16_0_d;
      float3 u_xlat10_0;
      float3 u_xlat1_d;
      float3 u_xlat16_1;
      float4 u_xlat10_1;
      float4 u_xlat16_2;
      float2 u_xlat3_d;
      float3 u_xlat10_3;
      float u_xlat16_4_d;
      float u_xlat16_7;
      float u_xlat15;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d.x = in_f.texcoord1.w;
          u_xlat0_d.y = in_f.texcoord2.w;
          u_xlat0_d.z = in_f.texcoord3.w;
          u_xlat0_d.xyz = ((-u_xlat0_d.xyz) + _WorldSpaceCameraPos.xyz);
          u_xlat0_d.xyz = normalize(u_xlat0_d.xyz);
          u_xlat10_1.xyz = tex2D(_BumpMap, in_f.texcoord.xy).xyz;
          u_xlat16_2.xyz = ((u_xlat10_1.xyz * float3(2, 2, 2)) + float3(-1, (-1), (-1)));
          u_xlat1_d.x = dot(in_f.texcoord1.xyz, u_xlat16_2.xyz);
          u_xlat1_d.y = dot(in_f.texcoord2.xyz, u_xlat16_2.xyz);
          u_xlat1_d.z = dot(in_f.texcoord3.xyz, u_xlat16_2.xyz);
          u_xlat16_2.xy = (u_xlat16_2.xy * _GrabTexture_TexelSize.xy);
          u_xlat16_2.xy = (u_xlat16_2.xy * float2(_Focus, _Focus));
          u_xlat3_d.xy = ((u_xlat16_2.xy * in_f.texcoord5.zz) + in_f.texcoord5.xy);
          u_xlat3_d.xy = (u_xlat3_d.xy / in_f.texcoord5.ww);
          u_xlat10_3.xyz = tex2D(_GrabTexture, u_xlat3_d.xy).xyz;
          u_xlat16_2.x = dot(u_xlat1_d.xyz, u_xlat1_d.xyz);
          u_xlat16_2.x = rsqrt(u_xlat16_2.x);
          u_xlat16_2.xyz = (u_xlat1_d.xyz * u_xlat16_2.xxx);
          u_xlat16_2.w = dot(_WorldSpaceLightPos0.xyz, u_xlat16_2.xyz);
          u_xlat16_4_d = (u_xlat16_2.w + u_xlat16_2.w);
          u_xlat16_2.xyz = ((u_xlat16_2.xyz * (-float3(u_xlat16_4_d, u_xlat16_4_d, u_xlat16_4_d))) + _WorldSpaceLightPos0.xyz);
          u_xlat16_2.x = dot((-u_xlat0_d.xyz), u_xlat16_2.xyz);
          u_xlat16_2.xw = max(u_xlat16_2.xw, float2(0, 0));
          u_xlat16_2.x = log2(u_xlat16_2.x);
          u_xlat10_0.xyz = tex2D(_Mask, in_f.texcoord.xy).xyz;
          u_xlat16_7 = (u_xlat10_0.y * _Shininess);
          u_xlat16_7 = ((u_xlat16_7 * 250) + 4);
          u_xlat16_2.x = (u_xlat16_2.x * u_xlat16_7);
          u_xlat16_2.x = exp2(u_xlat16_2.x);
          u_xlat16_2.x = (u_xlat10_0.x * u_xlat16_2.x);
          u_xlat16_2.xyz = (u_xlat16_2.xxx * _Specular.xyz);
          u_xlat10_1 = tex2D(_MainTex, in_f.texcoord.xy);
          u_xlat16_0_d.xyw = (u_xlat10_1.xyz * in_f.color.xyz);
          u_xlat16_1.xyz = ((_Color.xyz * u_xlat10_3.xyz) + (-u_xlat16_0_d.xyw));
          u_xlat16_0_d.xyz = ((u_xlat10_0.zzz * u_xlat16_1.xyz) + u_xlat16_0_d.xyw);
          u_xlat16_2.xyz = ((u_xlat16_0_d.xyz * u_xlat16_2.www) + u_xlat16_2.xyz);
          u_xlat16_2.xyz = (u_xlat16_2.xyz * _LightColor0.xyz);
          u_xlat16_2.xyz = (u_xlat16_2.xyz + u_xlat16_2.xyz);
          out_f.color.xyz = ((u_xlat16_0_d.xyz * in_f.texcoord6.xyz) + u_xlat16_2.xyz);
          u_xlat16_0_d.x = (in_f.color.w * _Color.w);
          u_xlat16_0_d.x = (u_xlat10_1.w * u_xlat16_0_d.x);
          out_f.color.w = u_xlat16_0_d.x;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 3, name: FORWARD
    {
      Name "FORWARD"
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "FORWARDADD"
        "QUEUE" = "Transparent+1"
        "RenderType" = "Transparent"
      }
      LOD 500
      ZClip Off
      ZWrite Off
      Cull Off
      Blend SrcAlpha One
      ColorMask RGB
      // m_ProgramMask = 6
      CGPROGRAM
      #pragma multi_compile POINT
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      #define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4 unity_WorldTransformParams;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _MainTex_ST;
      //uniform float3 _WorldSpaceCameraPos;
      //uniform float4 _WorldSpaceLightPos0;
      uniform float4 _LightColor0;
      uniform float4x4 unity_WorldToLight;
      uniform float4 _Color;
      uniform float4 _Specular;
      uniform float4 _GrabTexture_TexelSize;
      uniform float _Focus;
      uniform float _Shininess;
      uniform sampler2D _MainTex;
      uniform sampler2D _BumpMap;
      uniform sampler2D _Mask;
      uniform sampler2D _GrabTexture;
      uniform sampler2D _LightTexture0;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float4 tangent :TANGENT0;
          float3 normal :NORMAL0;
          float4 texcoord :TEXCOORD0;
          float4 color :COLOR0;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord :TEXCOORD0;
          float3 texcoord1 :TEXCOORD1;
          float3 texcoord2 :TEXCOORD2;
          float3 texcoord3 :TEXCOORD3;
          float3 texcoord4 :TEXCOORD4;
          float4 color :COLOR0;
          float4 texcoord5 :TEXCOORD5;
          float4 texcoord6 :TEXCOORD6;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 texcoord :TEXCOORD0;
          float3 texcoord1 :TEXCOORD1;
          float3 texcoord2 :TEXCOORD2;
          float3 texcoord3 :TEXCOORD3;
          float3 texcoord4 :TEXCOORD4;
          float4 color :COLOR0;
          float4 texcoord6 :TEXCOORD6;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      float3 u_xlat2;
      float3 u_xlat16_3;
      float u_xlat13;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          u_xlat0 = (in_v.vertex.yyyy * conv_mxt4x4_1(unity_ObjectToWorld));
          u_xlat0 = ((conv_mxt4x4_0(unity_ObjectToWorld) * in_v.vertex.xxxx) + u_xlat0);
          u_xlat0 = ((conv_mxt4x4_2(unity_ObjectToWorld) * in_v.vertex.zzzz) + u_xlat0);
          u_xlat1 = (u_xlat0 + conv_mxt4x4_3(unity_ObjectToWorld));
          out_v.texcoord4.xyz = ((conv_mxt4x4_3(unity_ObjectToWorld).xyz * in_v.vertex.www) + u_xlat0.xyz);
          u_xlat0 = mul(unity_MatrixVP, u_xlat1);
          out_v.vertex = u_xlat0;
          out_v.texcoord.xy = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          u_xlat1.y = dot(in_v.normal.xyz, conv_mxt4x4_0(unity_WorldToObject).xyz);
          u_xlat1.z = dot(in_v.normal.xyz, conv_mxt4x4_1(unity_WorldToObject).xyz);
          u_xlat1.x = dot(in_v.normal.xyz, conv_mxt4x4_2(unity_WorldToObject).xyz);
          u_xlat1.xyz = normalize(u_xlat1.xyz);
          u_xlat2.xyz = (in_v.tangent.yyy * conv_mxt4x4_1(unity_ObjectToWorld).yzx);
          u_xlat2.xyz = ((conv_mxt4x4_0(unity_ObjectToWorld).yzx * in_v.tangent.xxx) + u_xlat2.xyz);
          u_xlat2.xyz = ((conv_mxt4x4_2(unity_ObjectToWorld).yzx * in_v.tangent.zzz) + u_xlat2.xyz);
          u_xlat2.xyz = normalize(u_xlat2.xyz);
          u_xlat16_3.xyz = (u_xlat1.xyz * u_xlat2.xyz);
          u_xlat16_3.xyz = ((u_xlat1.zxy * u_xlat2.yzx) + (-u_xlat16_3.xyz));
          u_xlat13 = (in_v.tangent.w * unity_WorldTransformParams.w);
          u_xlat16_3.xyz = (float3(u_xlat13, u_xlat13, u_xlat13) * u_xlat16_3.xyz);
          out_v.texcoord1.y = u_xlat16_3.x;
          out_v.texcoord1.x = u_xlat2.z;
          out_v.texcoord1.z = u_xlat1.y;
          out_v.texcoord2.x = u_xlat2.x;
          out_v.texcoord3.x = u_xlat2.y;
          out_v.texcoord2.z = u_xlat1.z;
          out_v.texcoord3.z = u_xlat1.x;
          out_v.texcoord2.y = u_xlat16_3.y;
          out_v.texcoord3.y = u_xlat16_3.z;
          out_v.color = in_v.color;
          out_v.texcoord5 = u_xlat0;
          u_xlat0.xy = (u_xlat0.ww + u_xlat0.xy);
          out_v.texcoord6.zw = u_xlat0.zw;
          out_v.texcoord6.xy = (u_xlat0.xy * float2(0.5, 0.5));
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float3 u_xlat0_d;
      float3 u_xlat16_0;
      float3 u_xlat10_0;
      float4 u_xlat16_1;
      float3 u_xlat16_2;
      float4 u_xlat10_2;
      float3 u_xlat3;
      float4 u_xlat16_3_d;
      float3 u_xlat10_3;
      float u_xlat16_5;
      float u_xlat12;
      float u_xlat16_13;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat10_0.xyz = tex2D(_BumpMap, in_f.texcoord.xy).xyz;
          u_xlat16_1.xyz = ((u_xlat10_0.xyz * float3(2, 2, 2)) + float3(-1, (-1), (-1)));
          u_xlat16_2.x = dot(in_f.texcoord1.xyz, u_xlat16_1.xyz);
          u_xlat16_2.y = dot(in_f.texcoord2.xyz, u_xlat16_1.xyz);
          u_xlat16_2.z = dot(in_f.texcoord3.xyz, u_xlat16_1.xyz);
          u_xlat16_1.xy = (u_xlat16_1.xy * _GrabTexture_TexelSize.xy);
          u_xlat16_1.xy = (u_xlat16_1.xy * float2(_Focus, _Focus));
          u_xlat0_d.xy = ((u_xlat16_1.xy * in_f.texcoord6.zz) + in_f.texcoord6.xy);
          u_xlat0_d.xy = (u_xlat0_d.xy / in_f.texcoord6.ww);
          u_xlat10_0.xyz = tex2D(_GrabTexture, u_xlat0_d.xy).xyz;
          u_xlat16_1.x = dot(u_xlat16_2.xyz, u_xlat16_2.xyz);
          u_xlat16_1.x = rsqrt(u_xlat16_1.x);
          u_xlat16_1.xyz = (u_xlat16_1.xxx * u_xlat16_2.xyz);
          u_xlat3.xyz = ((-in_f.texcoord4.xyz) + _WorldSpaceLightPos0.xyz);
          u_xlat3.xyz = normalize(u_xlat3.xyz);
          u_xlat16_1.w = dot(u_xlat3.xyz, u_xlat16_1.xyz);
          u_xlat16_2.x = (u_xlat16_1.w + u_xlat16_1.w);
          u_xlat16_1.xyz = ((u_xlat16_1.xyz * (-u_xlat16_2.xxx)) + u_xlat3.xyz);
          u_xlat3.xyz = ((-in_f.texcoord4.xyz) + _WorldSpaceCameraPos.xyz);
          u_xlat3.xyz = normalize(u_xlat3.xyz);
          u_xlat16_1.x = dot((-u_xlat3.xyz), u_xlat16_1.xyz);
          u_xlat16_1.xw = max(u_xlat16_1.xw, float2(0, 0));
          u_xlat16_1.x = log2(u_xlat16_1.x);
          u_xlat10_3.xyz = tex2D(_Mask, in_f.texcoord.xy).xyz;
          u_xlat16_5 = (u_xlat10_3.y * _Shininess);
          u_xlat16_5 = ((u_xlat16_5 * 250) + 4);
          u_xlat16_1.x = (u_xlat16_1.x * u_xlat16_5);
          u_xlat16_1.x = exp2(u_xlat16_1.x);
          u_xlat16_1.x = (u_xlat10_3.x * u_xlat16_1.x);
          u_xlat16_1.xyz = (u_xlat16_1.xxx * _Specular.xyz);
          u_xlat10_2 = tex2D(_MainTex, in_f.texcoord.xy);
          u_xlat16_3_d.xyw = (u_xlat10_2.xyz * in_f.color.xyz);
          u_xlat16_0.xyz = ((_Color.xyz * u_xlat10_0.xyz) + (-u_xlat16_3_d.xyw));
          u_xlat16_0.xyz = ((u_xlat10_3.zzz * u_xlat16_0.xyz) + u_xlat16_3_d.xyw);
          u_xlat16_1.xyz = ((u_xlat16_0.xyz * u_xlat16_1.www) + u_xlat16_1.xyz);
          u_xlat16_1.xyz = (u_xlat16_1.xyz * _LightColor0.xyz);
          u_xlat0_d.xyz = (in_f.texcoord4.yyy * conv_mxt4x4_1(unity_WorldToLight).xyz);
          u_xlat0_d.xyz = ((conv_mxt4x4_0(unity_WorldToLight).xyz * in_f.texcoord4.xxx) + u_xlat0_d.xyz);
          u_xlat0_d.xyz = ((conv_mxt4x4_2(unity_WorldToLight).xyz * in_f.texcoord4.zzz) + u_xlat0_d.xyz);
          u_xlat0_d.xyz = (u_xlat0_d.xyz + conv_mxt4x4_3(unity_WorldToLight).xyz);
          u_xlat0_d.x = dot(u_xlat0_d.xyz, u_xlat0_d.xyz);
          u_xlat10_0.x = tex2D(_LightTexture0, u_xlat0_d.xx).w;
          u_xlat16_13 = (u_xlat10_0.x + u_xlat10_0.x);
          out_f.color.xyz = (float3(u_xlat16_13, u_xlat16_13, u_xlat16_13) * u_xlat16_1.xyz);
          u_xlat16_0.x = (in_f.color.w * _Color.w);
          u_xlat16_0.x = (u_xlat10_2.w * u_xlat16_0.x);
          out_f.color.w = u_xlat16_0.x;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 4, name: META
    {
      Name "META"
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "META"
        "QUEUE" = "Transparent+1"
        "RenderType" = "Transparent"
      }
      LOD 500
      ZClip Off
      ZWrite Off
      Cull Off
      Blend SrcAlpha OneMinusSrcAlpha
      ColorMask RGB
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4 unity_WorldTransformParams;
      //uniform float4x4 unity_MatrixVP;
      // uniform float4 unity_LightmapST;
      // uniform float4 unity_DynamicLightmapST;
      uniform float4 unity_MetaVertexControl;
      uniform float4 _MainTex_ST;
      uniform float4 _Color;
      uniform float4 _GrabTexture_TexelSize;
      uniform float _Focus;
      uniform float4 unity_MetaFragmentControl;
      uniform float unity_OneOverOutputBoost;
      uniform float unity_MaxOutputValue;
      uniform sampler2D _MainTex;
      uniform sampler2D _BumpMap;
      uniform sampler2D _Mask;
      uniform sampler2D _GrabTexture;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float4 tangent :TANGENT0;
          float3 normal :NORMAL0;
          float4 texcoord :TEXCOORD0;
          float4 texcoord1 :TEXCOORD1;
          float4 texcoord2 :TEXCOORD2;
          float4 color :COLOR0;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord :TEXCOORD0;
          float3 texcoord1 :TEXCOORD1;
          float3 texcoord2 :TEXCOORD2;
          float3 texcoord3 :TEXCOORD3;
          float4 color :COLOR0;
          float4 texcoord4 :TEXCOORD4;
          float4 texcoord5 :TEXCOORD5;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 texcoord :TEXCOORD0;
          float4 color :COLOR0;
          float4 texcoord5 :TEXCOORD5;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      int u_xlatb0;
      float4 u_xlat1;
      float3 u_xlat16_2;
      float u_xlat9;
      int u_xlatb9;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          #ifdef UNITY_ADRENO_ES3
          u_xlatb0 = (0<in_v.vertex.z);
          #else
          u_xlatb0 = (0<in_v.vertex.z);
          #endif
          u_xlat0.z = (u_xlatb0)?(9.99999975E-05):(float(0));
          u_xlat0.xy = ((in_v.texcoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
          u_xlat0.xyz = (unity_MetaVertexControl.x)?(u_xlat0.xyz):(in_v.vertex.xyz);
          #ifdef UNITY_ADRENO_ES3
          u_xlatb9 = (0<u_xlat0.z);
          #else
          u_xlatb9 = (0<u_xlat0.z);
          #endif
          u_xlat1.z = (u_xlatb9)?(9.99999975E-05):(float(0));
          u_xlat1.xy = ((in_v.texcoord2.xy * unity_DynamicLightmapST.xy) + unity_DynamicLightmapST.zw);
          u_xlat0.xyz = (unity_MetaVertexControl.y)?(u_xlat1.xyz):(u_xlat0.xyz);
          out_v.vertex = UnityObjectToClipPos(u_xlat0);
          out_v.texcoord.xy = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          u_xlat0.y = dot(in_v.normal.xyz, conv_mxt4x4_0(unity_WorldToObject).xyz);
          u_xlat0.z = dot(in_v.normal.xyz, conv_mxt4x4_1(unity_WorldToObject).xyz);
          u_xlat0.x = dot(in_v.normal.xyz, conv_mxt4x4_2(unity_WorldToObject).xyz);
          u_xlat0.xyz = normalize(u_xlat0.xyz);
          u_xlat1.xyz = (in_v.tangent.yyy * conv_mxt4x4_1(unity_ObjectToWorld).yzx);
          u_xlat1.xyz = ((conv_mxt4x4_0(unity_ObjectToWorld).yzx * in_v.tangent.xxx) + u_xlat1.xyz);
          u_xlat1.xyz = ((conv_mxt4x4_2(unity_ObjectToWorld).yzx * in_v.tangent.zzz) + u_xlat1.xyz);
          u_xlat1.xyz = normalize(u_xlat1.xyz);
          u_xlat16_2.xyz = (u_xlat0.xyz * u_xlat1.xyz);
          u_xlat16_2.xyz = ((u_xlat0.zxy * u_xlat1.yzx) + (-u_xlat16_2.xyz));
          u_xlat9 = (in_v.tangent.w * unity_WorldTransformParams.w);
          u_xlat16_2.xyz = (float3(u_xlat9, u_xlat9, u_xlat9) * u_xlat16_2.xyz);
          out_v.texcoord1.y = u_xlat16_2.x;
          out_v.texcoord1.x = u_xlat1.z;
          out_v.texcoord1.z = u_xlat0.y;
          out_v.texcoord2.x = u_xlat1.x;
          out_v.texcoord3.x = u_xlat1.y;
          out_v.texcoord2.z = u_xlat0.z;
          out_v.texcoord3.z = u_xlat0.x;
          out_v.texcoord2.y = u_xlat16_2.y;
          out_v.texcoord3.y = u_xlat16_2.z;
          out_v.color = in_v.color;
          u_xlat0 = UnityObjectToClipPos(in_v.vertex);
          out_v.texcoord4 = u_xlat0;
          u_xlat0.xy = (u_xlat0.ww + u_xlat0.xy);
          out_v.texcoord5.zw = u_xlat0.zw;
          out_v.texcoord5.xy = (u_xlat0.xy * float2(0.5, 0.5));
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float3 u_xlat0_d;
      float4 u_xlat16_0;
      float3 u_xlat10_0;
      float2 u_xlat16_1;
      float3 u_xlat16_2_d;
      float3 u_xlat10_2;
      float u_xlat9_d;
      float u_xlat10_9;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat10_0.xy = tex2D(_BumpMap, in_f.texcoord.xy).xy;
          u_xlat16_1.xy = ((u_xlat10_0.xy * float2(2, 2)) + float2(-1, (-1)));
          u_xlat16_1.xy = (u_xlat16_1.xy * _GrabTexture_TexelSize.xy);
          u_xlat16_1.xy = (u_xlat16_1.xy * float2(_Focus, _Focus));
          u_xlat0_d.xy = ((u_xlat16_1.xy * in_f.texcoord5.zz) + in_f.texcoord5.xy);
          u_xlat0_d.xy = (u_xlat0_d.xy / in_f.texcoord5.ww);
          u_xlat10_0.xyz = tex2D(_GrabTexture, u_xlat0_d.xy).xyz;
          u_xlat10_2.xyz = tex2D(_MainTex, in_f.texcoord.xy).xyz;
          u_xlat16_2_d.xyz = (u_xlat10_2.xyz * in_f.color.xyz);
          u_xlat16_0.xyz = ((_Color.xyz * u_xlat10_0.xyz) + (-u_xlat16_2_d.xyz));
          u_xlat10_9 = tex2D(_Mask, in_f.texcoord.xy).z;
          u_xlat16_0.xyz = ((float3(u_xlat10_9, u_xlat10_9, u_xlat10_9) * u_xlat16_0.xyz) + u_xlat16_2_d.xyz);
          u_xlat16_0.xyz = log2(u_xlat16_0.xyz);
          u_xlat9_d = unity_OneOverOutputBoost;
          #ifdef UNITY_ADRENO_ES3
          u_xlat9_d = min(max(u_xlat9_d, 0), 1);
          #else
          u_xlat9_d = clamp(u_xlat9_d, 0, 1);
          #endif
          u_xlat0_d.xyz = (u_xlat16_0.xyz * float3(u_xlat9_d, u_xlat9_d, u_xlat9_d));
          u_xlat0_d.xyz = exp2(u_xlat0_d.xyz);
          u_xlat0_d.xyz = min(u_xlat0_d.xyz, float3(float3(unity_MaxOutputValue, unity_MaxOutputValue, unity_MaxOutputValue)));
          u_xlat16_0.xyz = (unity_MetaFragmentControl.x)?(u_xlat0_d.xyz):(float3(0, 0, 0));
          u_xlat16_0.w = (unity_MetaFragmentControl.x)?(1):(0);
          out_f.color = (unity_MetaFragmentControl.y)?(float4(0, 0, 0, 0.0235294122)):(u_xlat16_0);
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "QUEUE" = "Transparent+1"
      "RenderType" = "Transparent"
    }
    LOD 400
    Pass // ind: 1, name: FORWARD
    {
      Name "FORWARD"
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "FORWARDBASE"
        "QUEUE" = "Transparent+1"
        "RenderType" = "Transparent"
      }
      LOD 400
      ZClip Off
      ZWrite Off
      Cull Off
      Blend SrcAlpha OneMinusSrcAlpha
      ColorMask RGB
      // m_ProgramMask = 6
      CGPROGRAM
      #pragma multi_compile DIRECTIONAL
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4 unity_SHAr;
      //uniform float4 unity_SHAg;
      //uniform float4 unity_SHAb;
      //uniform float4 unity_SHBr;
      //uniform float4 unity_SHBg;
      //uniform float4 unity_SHBb;
      //uniform float4 unity_SHC;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4 unity_WorldTransformParams;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _MainTex_ST;
      //uniform float3 _WorldSpaceCameraPos;
      //uniform float4 _WorldSpaceLightPos0;
      uniform float4 _LightColor0;
      uniform sampler2D _MainTex;
      uniform sampler2D _BumpMap;
      uniform sampler2D _Mask;
      uniform float4 _Color;
      uniform float4 _Specular;
      uniform float _Shininess;
      struct appdata_t
      {
          float4 tangent :TANGENT;
          float4 vertex :POSITION;
          float4 color :COLOR;
          float3 normal :NORMAL;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_TEXCOORD1 :TEXCOORD1;
          float4 xlv_TEXCOORD2 :TEXCOORD2;
          float4 xlv_TEXCOORD3 :TEXCOORD3;
          float4 xlv_COLOR0 :COLOR0;
          float3 xlv_TEXCOORD4 :TEXCOORD4;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_TEXCOORD1 :TEXCOORD1;
          float4 xlv_TEXCOORD2 :TEXCOORD2;
          float4 xlv_TEXCOORD3 :TEXCOORD3;
          float4 xlv_COLOR0 :COLOR0;
          float3 xlv_TEXCOORD4 :TEXCOORD4;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float3 shlight_1;
          float tangentSign_2;
          float3 worldTangent_3;
          float3 worldNormal_4;
          float3 tmpvar_5;
          float4 tmpvar_6;
          tmpvar_6.w = 1;
          tmpvar_6.xyz = in_v.vertex.xyz;
          float3 tmpvar_7;
          tmpvar_7 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
          float3x3 tmpvar_8;
          tmpvar_8[0] = conv_mxt4x4_0(unity_WorldToObject).xyz;
          tmpvar_8[1] = conv_mxt4x4_1(unity_WorldToObject).xyz;
          tmpvar_8[2] = conv_mxt4x4_2(unity_WorldToObject).xyz;
          float3 tmpvar_9;
          tmpvar_9 = normalize(mul(in_v.normal, tmpvar_8));
          worldNormal_4 = tmpvar_9;
          float3x3 tmpvar_10;
          tmpvar_10[0] = conv_mxt4x4_0(unity_ObjectToWorld).xyz;
          tmpvar_10[1] = conv_mxt4x4_1(unity_ObjectToWorld).xyz;
          tmpvar_10[2] = conv_mxt4x4_2(unity_ObjectToWorld).xyz;
          float3 tmpvar_11;
          tmpvar_11 = normalize(mul(tmpvar_10, in_v.tangent.xyz));
          worldTangent_3 = tmpvar_11;
          float tmpvar_12;
          tmpvar_12 = (in_v.tangent.w * unity_WorldTransformParams.w);
          tangentSign_2 = tmpvar_12;
          float3 tmpvar_13;
          tmpvar_13 = (((worldNormal_4.yzx * worldTangent_3.zxy) - (worldNormal_4.zxy * worldTangent_3.yzx)) * tangentSign_2);
          float4 tmpvar_14;
          tmpvar_14.x = worldTangent_3.x;
          tmpvar_14.y = tmpvar_13.x;
          tmpvar_14.z = worldNormal_4.x;
          tmpvar_14.w = tmpvar_7.x;
          float4 tmpvar_15;
          tmpvar_15.x = worldTangent_3.y;
          tmpvar_15.y = tmpvar_13.y;
          tmpvar_15.z = worldNormal_4.y;
          tmpvar_15.w = tmpvar_7.y;
          float4 tmpvar_16;
          tmpvar_16.x = worldTangent_3.z;
          tmpvar_16.y = tmpvar_13.z;
          tmpvar_16.z = worldNormal_4.z;
          tmpvar_16.w = tmpvar_7.z;
          float4 tmpvar_17;
          tmpvar_17.w = 1;
          tmpvar_17.xyz = float3(worldNormal_4);
          float4 normal_18;
          normal_18 = tmpvar_17;
          float3 res_19;
          float3 x_20;
          x_20.x = dot(unity_SHAr, normal_18);
          x_20.y = dot(unity_SHAg, normal_18);
          x_20.z = dot(unity_SHAb, normal_18);
          float3 x1_21;
          float4 tmpvar_22;
          tmpvar_22 = (normal_18.xyzz * normal_18.yzzx);
          x1_21.x = dot(unity_SHBr, tmpvar_22);
          x1_21.y = dot(unity_SHBg, tmpvar_22);
          x1_21.z = dot(unity_SHBb, tmpvar_22);
          res_19 = (x_20 + (x1_21 + (unity_SHC.xyz * ((normal_18.x * normal_18.x) - (normal_18.y * normal_18.y)))));
          float3 tmpvar_23;
          float _tmp_dvx_0 = max(((1.055 * pow(max(res_19, float3(0, 0, 0)), float3(0.4166667, 0.4166667, 0.4166667))) - 0.055), float3(0, 0, 0));
          tmpvar_23 = float3(_tmp_dvx_0, _tmp_dvx_0, _tmp_dvx_0);
          res_19 = tmpvar_23;
          shlight_1 = tmpvar_23;
          tmpvar_5 = shlight_1;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_6));
          out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          out_v.xlv_TEXCOORD1 = tmpvar_14;
          out_v.xlv_TEXCOORD2 = tmpvar_15;
          out_v.xlv_TEXCOORD3 = tmpvar_16;
          out_v.xlv_COLOR0 = in_v.color;
          out_v.xlv_TEXCOORD4 = tmpvar_5;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float3 worldN_1;
          float4 c_2;
          float3 worldViewDir_3;
          float3 lightDir_4;
          float4 tmpvar_5;
          float3 tmpvar_6;
          tmpvar_6.x = in_f.xlv_TEXCOORD1.w;
          tmpvar_6.y = in_f.xlv_TEXCOORD2.w;
          tmpvar_6.z = in_f.xlv_TEXCOORD3.w;
          float3 tmpvar_7;
          tmpvar_7 = _WorldSpaceLightPos0.xyz;
          lightDir_4 = tmpvar_7;
          float3 tmpvar_8;
          tmpvar_8 = normalize((_WorldSpaceCameraPos - tmpvar_6));
          worldViewDir_3 = tmpvar_8;
          tmpvar_5 = in_f.xlv_COLOR0;
          float3 tmpvar_9;
          float3 tmpvar_10;
          float tmpvar_11;
          float tmpvar_12;
          float4 col_13;
          float3 mask_14;
          float3 nm_15;
          float4 tex_16;
          float4 tmpvar_17;
          tmpvar_17 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          tex_16 = tmpvar_17;
          float3 tmpvar_18;
          tmpvar_18 = ((tex2D(_BumpMap, in_f.xlv_TEXCOORD0).xyz * 2) - 1);
          nm_15 = tmpvar_18;
          float3 tmpvar_19;
          tmpvar_19 = tex2D(_Mask, in_f.xlv_TEXCOORD0).xyz;
          mask_14 = tmpvar_19;
          col_13.xyz = (tmpvar_5.xyz * tex_16.xyz);
          float3 tmpvar_20;
          float _tmp_dvx_1 = (mask_14.z * 0.5);
          tmpvar_20 = float3(_tmp_dvx_1, _tmp_dvx_1, _tmp_dvx_1);
          float3 tmpvar_21;
          tmpvar_21 = lerp(col_13.xyz, _Color.xyz, tmpvar_20);
          col_13.xyz = float3(tmpvar_21);
          col_13.w = ((tmpvar_5.w * _Color.w) * tex_16.w);
          tmpvar_9 = col_13.xyz;
          tmpvar_10 = nm_15;
          tmpvar_11 = (_Shininess * mask_14.y);
          tmpvar_12 = col_13.w;
          c_2.w = 0;
          float tmpvar_22;
          tmpvar_22 = dot(in_f.xlv_TEXCOORD1.xyz, tmpvar_10);
          worldN_1.x = tmpvar_22;
          float tmpvar_23;
          tmpvar_23 = dot(in_f.xlv_TEXCOORD2.xyz, tmpvar_10);
          worldN_1.y = tmpvar_23;
          float tmpvar_24;
          tmpvar_24 = dot(in_f.xlv_TEXCOORD3.xyz, tmpvar_10);
          worldN_1.z = tmpvar_24;
          c_2.xyz = (tmpvar_9 * in_f.xlv_TEXCOORD4);
          float3 lightDir_25;
          lightDir_25 = lightDir_4;
          float3 viewDir_26;
          viewDir_26 = worldViewDir_3;
          float4 c_27;
          float shininess_28;
          float3 nNormal_29;
          float3 tmpvar_30;
          tmpvar_30 = normalize(worldN_1);
          nNormal_29 = tmpvar_30;
          float tmpvar_31;
          tmpvar_31 = ((tmpvar_11 * 250) + 4);
          shininess_28 = tmpvar_31;
          c_27.xyz = (((tmpvar_9 * max(0, dot(nNormal_29, lightDir_25))) + (_Specular.xyz * (pow(max(0, dot((-viewDir_26), (lightDir_25 - (2 * (dot(nNormal_29, lightDir_25) * nNormal_29))))), shininess_28) * mask_14.x))) * _LightColor0.xyz);
          c_27.xyz = (c_27.xyz * 2);
          c_27.w = tmpvar_12;
          c_2 = (c_2 + c_27);
          out_f.color = c_2;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 2, name: FORWARD
    {
      Name "FORWARD"
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "FORWARDADD"
        "QUEUE" = "Transparent+1"
        "RenderType" = "Transparent"
      }
      LOD 400
      ZClip Off
      ZWrite Off
      Cull Off
      Blend SrcAlpha One
      ColorMask RGB
      // m_ProgramMask = 6
      CGPROGRAM
      #pragma multi_compile POINT
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4 unity_WorldTransformParams;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _MainTex_ST;
      //uniform float3 _WorldSpaceCameraPos;
      //uniform float4 _WorldSpaceLightPos0;
      uniform float4 _LightColor0;
      uniform sampler2D _LightTexture0;
      uniform float4x4 unity_WorldToLight;
      uniform sampler2D _MainTex;
      uniform sampler2D _BumpMap;
      uniform sampler2D _Mask;
      uniform float4 _Color;
      uniform float4 _Specular;
      uniform float _Shininess;
      struct appdata_t
      {
          float4 tangent :TANGENT;
          float4 vertex :POSITION;
          float4 color :COLOR;
          float3 normal :NORMAL;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float3 xlv_TEXCOORD2 :TEXCOORD2;
          float3 xlv_TEXCOORD3 :TEXCOORD3;
          float3 xlv_TEXCOORD4 :TEXCOORD4;
          float4 xlv_COLOR0 :COLOR0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float3 xlv_TEXCOORD2 :TEXCOORD2;
          float3 xlv_TEXCOORD3 :TEXCOORD3;
          float3 xlv_TEXCOORD4 :TEXCOORD4;
          float4 xlv_COLOR0 :COLOR0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float tangentSign_1;
          float3 worldTangent_2;
          float3 worldNormal_3;
          float4 tmpvar_4;
          tmpvar_4.w = 1;
          tmpvar_4.xyz = in_v.vertex.xyz;
          float3x3 tmpvar_5;
          tmpvar_5[0] = conv_mxt4x4_0(unity_WorldToObject).xyz;
          tmpvar_5[1] = conv_mxt4x4_1(unity_WorldToObject).xyz;
          tmpvar_5[2] = conv_mxt4x4_2(unity_WorldToObject).xyz;
          float3 tmpvar_6;
          tmpvar_6 = normalize(mul(in_v.normal, tmpvar_5));
          worldNormal_3 = tmpvar_6;
          float3x3 tmpvar_7;
          tmpvar_7[0] = conv_mxt4x4_0(unity_ObjectToWorld).xyz;
          tmpvar_7[1] = conv_mxt4x4_1(unity_ObjectToWorld).xyz;
          tmpvar_7[2] = conv_mxt4x4_2(unity_ObjectToWorld).xyz;
          float3 tmpvar_8;
          tmpvar_8 = normalize(mul(tmpvar_7, in_v.tangent.xyz));
          worldTangent_2 = tmpvar_8;
          float tmpvar_9;
          tmpvar_9 = (in_v.tangent.w * unity_WorldTransformParams.w);
          tangentSign_1 = tmpvar_9;
          float3 tmpvar_10;
          tmpvar_10 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
          float3 tmpvar_11;
          tmpvar_11.x = worldTangent_2.x;
          tmpvar_11.y = tmpvar_10.x;
          tmpvar_11.z = worldNormal_3.x;
          float3 tmpvar_12;
          tmpvar_12.x = worldTangent_2.y;
          tmpvar_12.y = tmpvar_10.y;
          tmpvar_12.z = worldNormal_3.y;
          float3 tmpvar_13;
          tmpvar_13.x = worldTangent_2.z;
          tmpvar_13.y = tmpvar_10.z;
          tmpvar_13.z = worldNormal_3.z;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_4));
          out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          out_v.xlv_TEXCOORD1 = tmpvar_11;
          out_v.xlv_TEXCOORD2 = tmpvar_12;
          out_v.xlv_TEXCOORD3 = tmpvar_13;
          out_v.xlv_TEXCOORD4 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
          out_v.xlv_COLOR0 = in_v.color;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float3 worldN_1;
          float4 c_2;
          float3 lightCoord_3;
          float3 worldViewDir_4;
          float3 lightDir_5;
          float4 tmpvar_6;
          float3 tmpvar_7;
          tmpvar_7 = normalize((_WorldSpaceLightPos0.xyz - in_f.xlv_TEXCOORD4));
          lightDir_5 = tmpvar_7;
          float3 tmpvar_8;
          tmpvar_8 = normalize((_WorldSpaceCameraPos - in_f.xlv_TEXCOORD4));
          worldViewDir_4 = tmpvar_8;
          tmpvar_6 = in_f.xlv_COLOR0;
          float3 tmpvar_9;
          float3 tmpvar_10;
          float tmpvar_11;
          float tmpvar_12;
          float4 col_13;
          float3 mask_14;
          float3 nm_15;
          float4 tex_16;
          float4 tmpvar_17;
          tmpvar_17 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          tex_16 = tmpvar_17;
          float3 tmpvar_18;
          tmpvar_18 = ((tex2D(_BumpMap, in_f.xlv_TEXCOORD0).xyz * 2) - 1);
          nm_15 = tmpvar_18;
          float3 tmpvar_19;
          tmpvar_19 = tex2D(_Mask, in_f.xlv_TEXCOORD0).xyz;
          mask_14 = tmpvar_19;
          col_13.xyz = (tmpvar_6.xyz * tex_16.xyz);
          float3 tmpvar_20;
          float _tmp_dvx_2 = (mask_14.z * 0.5);
          tmpvar_20 = float3(_tmp_dvx_2, _tmp_dvx_2, _tmp_dvx_2);
          float3 tmpvar_21;
          tmpvar_21 = lerp(col_13.xyz, _Color.xyz, tmpvar_20);
          col_13.xyz = float3(tmpvar_21);
          col_13.w = ((tmpvar_6.w * _Color.w) * tex_16.w);
          tmpvar_9 = col_13.xyz;
          tmpvar_10 = nm_15;
          tmpvar_11 = (_Shininess * mask_14.y);
          tmpvar_12 = col_13.w;
          float4 tmpvar_22;
          tmpvar_22.w = 1;
          tmpvar_22.xyz = in_f.xlv_TEXCOORD4;
          lightCoord_3 = mul(unity_WorldToLight, tmpvar_22).xyz;
          float tmpvar_23;
          tmpvar_23 = dot(lightCoord_3, lightCoord_3);
          float tmpvar_24;
          tmpvar_24 = tex2D(_LightTexture0, float2(tmpvar_23, tmpvar_23)).w;
          worldN_1.x = dot(in_f.xlv_TEXCOORD1, tmpvar_10);
          worldN_1.y = dot(in_f.xlv_TEXCOORD2, tmpvar_10);
          worldN_1.z = dot(in_f.xlv_TEXCOORD3, tmpvar_10);
          float3 lightDir_25;
          lightDir_25 = lightDir_5;
          float3 viewDir_26;
          viewDir_26 = worldViewDir_4;
          float atten_27;
          atten_27 = tmpvar_24;
          float4 c_28;
          float shininess_29;
          float3 nNormal_30;
          float3 tmpvar_31;
          tmpvar_31 = normalize(worldN_1);
          nNormal_30 = tmpvar_31;
          float tmpvar_32;
          tmpvar_32 = ((tmpvar_11 * 250) + 4);
          shininess_29 = tmpvar_32;
          float3 tmpvar_33;
          tmpvar_33 = normalize(lightDir_25);
          lightDir_25 = tmpvar_33;
          c_28.xyz = (((tmpvar_9 * max(0, dot(nNormal_30, tmpvar_33))) + (_Specular.xyz * (pow(max(0, dot((-viewDir_26), (tmpvar_33 - (2 * (dot(nNormal_30, tmpvar_33) * nNormal_30))))), shininess_29) * mask_14.x))) * _LightColor0.xyz);
          c_28.xyz = (c_28.xyz * (atten_27 * 2));
          c_28.w = tmpvar_12;
          c_2 = c_28;
          out_f.color = c_2;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 3, name: META
    {
      Name "META"
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "META"
        "QUEUE" = "Transparent+1"
        "RenderType" = "Transparent"
      }
      LOD 400
      ZClip Off
      ZWrite Off
      Cull Off
      Blend SrcAlpha OneMinusSrcAlpha
      ColorMask RGB
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4 unity_WorldTransformParams;
      //uniform float4x4 unity_MatrixVP;
      // uniform float4 unity_LightmapST;
      // uniform float4 unity_DynamicLightmapST;
      uniform float4 unity_MetaVertexControl;
      uniform float4 _MainTex_ST;
      uniform sampler2D _MainTex;
      uniform sampler2D _Mask;
      uniform float4 _Color;
      uniform float4 unity_MetaFragmentControl;
      uniform float unity_OneOverOutputBoost;
      uniform float unity_MaxOutputValue;
      uniform float unity_UseLinearSpace;
      struct appdata_t
      {
          float4 tangent :TANGENT;
          float4 vertex :POSITION;
          float4 color :COLOR;
          float3 normal :NORMAL;
          float4 texcoord :TEXCOORD0;
          float4 texcoord1 :TEXCOORD1;
          float4 texcoord2 :TEXCOORD2;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float3 xlv_TEXCOORD2 :TEXCOORD2;
          float3 xlv_TEXCOORD3 :TEXCOORD3;
          float4 xlv_COLOR0 :COLOR0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_COLOR0 :COLOR0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float4 tmpvar_1;
          tmpvar_1 = in_v.color;
          float tangentSign_2;
          float3 worldTangent_3;
          float3 worldNormal_4;
          float4 vertex_5;
          vertex_5 = in_v.vertex;
          if(unity_MetaVertexControl.x)
          {
              vertex_5.xy = ((in_v.texcoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
              float tmpvar_6;
              if((in_v.vertex.z>0))
              {
                  tmpvar_6 = 0.0001;
              }
              else
              {
                  tmpvar_6 = 0;
              }
              vertex_5.z = tmpvar_6;
          }
          if(unity_MetaVertexControl.y)
          {
              vertex_5.xy = ((in_v.texcoord2.xy * unity_DynamicLightmapST.xy) + unity_DynamicLightmapST.zw);
              float tmpvar_7;
              if((vertex_5.z>0))
              {
                  tmpvar_7 = 0.0001;
              }
              else
              {
                  tmpvar_7 = 0;
              }
              vertex_5.z = tmpvar_7;
          }
          float4 tmpvar_8;
          tmpvar_8.w = 1;
          tmpvar_8.xyz = vertex_5.xyz;
          float3x3 tmpvar_9;
          tmpvar_9[0] = conv_mxt4x4_0(unity_WorldToObject).xyz;
          tmpvar_9[1] = conv_mxt4x4_1(unity_WorldToObject).xyz;
          tmpvar_9[2] = conv_mxt4x4_2(unity_WorldToObject).xyz;
          float3 tmpvar_10;
          tmpvar_10 = normalize(mul(in_v.normal, tmpvar_9));
          worldNormal_4 = tmpvar_10;
          float3x3 tmpvar_11;
          tmpvar_11[0] = conv_mxt4x4_0(unity_ObjectToWorld).xyz;
          tmpvar_11[1] = conv_mxt4x4_1(unity_ObjectToWorld).xyz;
          tmpvar_11[2] = conv_mxt4x4_2(unity_ObjectToWorld).xyz;
          float3 tmpvar_12;
          tmpvar_12 = normalize(mul(tmpvar_11, in_v.tangent.xyz));
          worldTangent_3 = tmpvar_12;
          float tmpvar_13;
          tmpvar_13 = (in_v.tangent.w * unity_WorldTransformParams.w);
          tangentSign_2 = tmpvar_13;
          float3 tmpvar_14;
          tmpvar_14 = (((worldNormal_4.yzx * worldTangent_3.zxy) - (worldNormal_4.zxy * worldTangent_3.yzx)) * tangentSign_2);
          float3 tmpvar_15;
          tmpvar_15.x = worldTangent_3.x;
          tmpvar_15.y = tmpvar_14.x;
          tmpvar_15.z = worldNormal_4.x;
          float3 tmpvar_16;
          tmpvar_16.x = worldTangent_3.y;
          tmpvar_16.y = tmpvar_14.y;
          tmpvar_16.z = worldNormal_4.y;
          float3 tmpvar_17;
          tmpvar_17.x = worldTangent_3.z;
          tmpvar_17.y = tmpvar_14.z;
          tmpvar_17.z = worldNormal_4.z;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_8));
          out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          out_v.xlv_TEXCOORD1 = tmpvar_15;
          out_v.xlv_TEXCOORD2 = tmpvar_16;
          out_v.xlv_TEXCOORD3 = tmpvar_17;
          out_v.xlv_COLOR0 = tmpvar_1;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 tmpvar_1;
          float3 tmpvar_2;
          float4 tmpvar_3;
          tmpvar_3 = in_f.xlv_COLOR0;
          float3 tmpvar_4;
          float4 col_5;
          float3 mask_6;
          float4 tex_7;
          float4 tmpvar_8;
          tmpvar_8 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          tex_7 = tmpvar_8;
          float3 tmpvar_9;
          tmpvar_9 = tex2D(_Mask, in_f.xlv_TEXCOORD0).xyz;
          mask_6 = tmpvar_9;
          col_5.xyz = (tmpvar_3.xyz * tex_7.xyz);
          float3 tmpvar_10;
          float _tmp_dvx_3 = (mask_6.z * 0.5);
          tmpvar_10 = float3(_tmp_dvx_3, _tmp_dvx_3, _tmp_dvx_3);
          float3 tmpvar_11;
          tmpvar_11 = lerp(col_5.xyz, _Color.xyz, tmpvar_10);
          col_5.xyz = float3(tmpvar_11);
          col_5.w = ((tmpvar_3.w * _Color.w) * tex_7.w);
          tmpvar_4 = col_5.xyz;
          tmpvar_2 = tmpvar_4;
          float4 res_12;
          res_12 = float4(0, 0, 0, 0);
          if(unity_MetaFragmentControl.x)
          {
              float4 tmpvar_13;
              tmpvar_13.w = 1;
              tmpvar_13.xyz = float3(tmpvar_2);
              res_12.w = tmpvar_13.w;
              float3 tmpvar_14;
              float _tmp_dvx_4 = clamp(unity_OneOverOutputBoost, 0, 1);
              tmpvar_14 = clamp(pow(tmpvar_2, float3(_tmp_dvx_4, _tmp_dvx_4, _tmp_dvx_4)), float3(0, 0, 0), float3(unity_MaxOutputValue, unity_MaxOutputValue, unity_MaxOutputValue));
              res_12.xyz = float3(tmpvar_14);
          }
          if(unity_MetaFragmentControl.y)
          {
              float3 emission_15;
              if(int(unity_UseLinearSpace))
              {
                  emission_15 = float3(0, 0, 0);
              }
              else
              {
                  emission_15 = float3(0, 0, 0);
              }
              float4 tmpvar_16;
              float alpha_17;
              float3 tmpvar_18;
              tmpvar_18 = (emission_15 * 0.01030928);
              alpha_17 = (ceil((max(max(tmpvar_18.x, tmpvar_18.y), max(tmpvar_18.z, 0.02)) * 255)) / 255);
              float tmpvar_19;
              tmpvar_19 = max(alpha_17, 0.02);
              alpha_17 = tmpvar_19;
              float4 tmpvar_20;
              tmpvar_20.xyz = float3((tmpvar_18 / tmpvar_19));
              tmpvar_20.w = tmpvar_19;
              tmpvar_16 = tmpvar_20;
              res_12 = tmpvar_16;
          }
          tmpvar_1 = res_12;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "QUEUE" = "Transparent+1"
      "RenderType" = "Transparent"
    }
    LOD 300
    Pass // ind: 1, name: FORWARD
    {
      Name "FORWARD"
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "FORWARDBASE"
        "QUEUE" = "Transparent+1"
        "RenderType" = "Transparent"
      }
      LOD 300
      ZClip Off
      ZWrite Off
      Cull Off
      Blend SrcAlpha OneMinusSrcAlpha
      ColorMask RGB
      // m_ProgramMask = 6
      CGPROGRAM
      #pragma multi_compile DIRECTIONAL
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4 unity_SHAr;
      //uniform float4 unity_SHAg;
      //uniform float4 unity_SHAb;
      //uniform float4 unity_SHBr;
      //uniform float4 unity_SHBg;
      //uniform float4 unity_SHBb;
      //uniform float4 unity_SHC;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _MainTex_ST;
      //uniform float4 _WorldSpaceLightPos0;
      uniform float4 _LightColor0;
      uniform sampler2D _MainTex;
      uniform sampler2D _Mask;
      uniform float4 _Color;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 color :COLOR;
          float3 normal :NORMAL;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float3 xlv_TEXCOORD2 :TEXCOORD2;
          float4 xlv_COLOR0 :COLOR0;
          float3 xlv_TEXCOORD3 :TEXCOORD3;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float4 xlv_COLOR0 :COLOR0;
          float3 xlv_TEXCOORD3 :TEXCOORD3;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float3 worldNormal_1;
          float3 tmpvar_2;
          float4 tmpvar_3;
          tmpvar_3.w = 1;
          tmpvar_3.xyz = in_v.vertex.xyz;
          float3x3 tmpvar_4;
          tmpvar_4[0] = conv_mxt4x4_0(unity_WorldToObject).xyz;
          tmpvar_4[1] = conv_mxt4x4_1(unity_WorldToObject).xyz;
          tmpvar_4[2] = conv_mxt4x4_2(unity_WorldToObject).xyz;
          float3 tmpvar_5;
          tmpvar_5 = normalize(mul(in_v.normal, tmpvar_4));
          worldNormal_1 = tmpvar_5;
          tmpvar_2 = worldNormal_1;
          float3 normal_6;
          normal_6 = worldNormal_1;
          float4 tmpvar_7;
          tmpvar_7.w = 1;
          tmpvar_7.xyz = float3(normal_6);
          float3 res_8;
          float3 x_9;
          x_9.x = dot(unity_SHAr, tmpvar_7);
          x_9.y = dot(unity_SHAg, tmpvar_7);
          x_9.z = dot(unity_SHAb, tmpvar_7);
          float3 x1_10;
          float4 tmpvar_11;
          tmpvar_11 = (normal_6.xyzz * normal_6.yzzx);
          x1_10.x = dot(unity_SHBr, tmpvar_11);
          x1_10.y = dot(unity_SHBg, tmpvar_11);
          x1_10.z = dot(unity_SHBb, tmpvar_11);
          res_8 = (x_9 + (x1_10 + (unity_SHC.xyz * ((normal_6.x * normal_6.x) - (normal_6.y * normal_6.y)))));
          float3 tmpvar_12;
          float _tmp_dvx_5 = max(((1.055 * pow(max(res_8, float3(0, 0, 0)), float3(0.4166667, 0.4166667, 0.4166667))) - 0.055), float3(0, 0, 0));
          tmpvar_12 = float3(_tmp_dvx_5, _tmp_dvx_5, _tmp_dvx_5);
          res_8 = tmpvar_12;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_3));
          out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          out_v.xlv_TEXCOORD1 = tmpvar_2;
          out_v.xlv_TEXCOORD2 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
          out_v.xlv_COLOR0 = in_v.color;
          out_v.xlv_TEXCOORD3 = max(float3(0, 0, 0), tmpvar_12);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float3 tmpvar_1;
          float3 tmpvar_2;
          float3 tmpvar_3;
          float3 lightDir_4;
          float4 tmpvar_5;
          float3 tmpvar_6;
          tmpvar_6 = _WorldSpaceLightPos0.xyz;
          lightDir_4 = tmpvar_6;
          tmpvar_5 = in_f.xlv_COLOR0;
          tmpvar_3 = in_f.xlv_TEXCOORD1;
          float3 tmpvar_7;
          float tmpvar_8;
          float4 col_9;
          float3 mask_10;
          float4 tex_11;
          float4 tmpvar_12;
          tmpvar_12 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          tex_11 = tmpvar_12;
          float3 tmpvar_13;
          tmpvar_13 = tex2D(_Mask, in_f.xlv_TEXCOORD0).xyz;
          mask_10 = tmpvar_13;
          col_9.xyz = (tmpvar_5.xyz * tex_11.xyz);
          float3 tmpvar_14;
          float _tmp_dvx_6 = (mask_10.z * 0.5);
          tmpvar_14 = float3(_tmp_dvx_6, _tmp_dvx_6, _tmp_dvx_6);
          float3 tmpvar_15;
          tmpvar_15 = lerp(col_9.xyz, _Color.xyz, tmpvar_14);
          col_9.xyz = float3(tmpvar_15);
          col_9.w = ((tmpvar_5.w * _Color.w) * tex_11.w);
          tmpvar_7 = col_9.xyz;
          tmpvar_8 = col_9.w;
          tmpvar_1 = _LightColor0.xyz;
          tmpvar_2 = lightDir_4;
          float4 c_16;
          float4 c_17;
          float diff_18;
          float tmpvar_19;
          tmpvar_19 = max(0, dot(tmpvar_3, tmpvar_2));
          diff_18 = tmpvar_19;
          c_17.xyz = float3(((tmpvar_7 * tmpvar_1) * diff_18));
          c_17.w = tmpvar_8;
          c_16.w = c_17.w;
          c_16.xyz = (c_17.xyz + (tmpvar_7 * in_f.xlv_TEXCOORD3));
          out_f.color = c_16;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 2, name: FORWARD
    {
      Name "FORWARD"
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "FORWARDADD"
        "QUEUE" = "Transparent+1"
        "RenderType" = "Transparent"
      }
      LOD 300
      ZClip Off
      ZWrite Off
      Cull Off
      Blend SrcAlpha One
      ColorMask RGB
      // m_ProgramMask = 6
      CGPROGRAM
      #pragma multi_compile POINT
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _MainTex_ST;
      //uniform float4 _WorldSpaceLightPos0;
      uniform float4 _LightColor0;
      uniform sampler2D _LightTexture0;
      uniform float4x4 unity_WorldToLight;
      uniform sampler2D _MainTex;
      uniform sampler2D _Mask;
      uniform float4 _Color;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 color :COLOR;
          float3 normal :NORMAL;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float3 xlv_TEXCOORD2 :TEXCOORD2;
          float4 xlv_COLOR0 :COLOR0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float3 xlv_TEXCOORD2 :TEXCOORD2;
          float4 xlv_COLOR0 :COLOR0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float3 worldNormal_1;
          float3 tmpvar_2;
          float4 tmpvar_3;
          tmpvar_3.w = 1;
          tmpvar_3.xyz = in_v.vertex.xyz;
          float3x3 tmpvar_4;
          tmpvar_4[0] = conv_mxt4x4_0(unity_WorldToObject).xyz;
          tmpvar_4[1] = conv_mxt4x4_1(unity_WorldToObject).xyz;
          tmpvar_4[2] = conv_mxt4x4_2(unity_WorldToObject).xyz;
          float3 tmpvar_5;
          tmpvar_5 = normalize(mul(in_v.normal, tmpvar_4));
          worldNormal_1 = tmpvar_5;
          tmpvar_2 = worldNormal_1;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_3));
          out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          out_v.xlv_TEXCOORD1 = tmpvar_2;
          out_v.xlv_TEXCOORD2 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
          out_v.xlv_COLOR0 = in_v.color;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float3 tmpvar_1;
          float3 tmpvar_2;
          float3 lightCoord_3;
          float3 tmpvar_4;
          float3 lightDir_5;
          float4 tmpvar_6;
          float3 tmpvar_7;
          tmpvar_7 = normalize((_WorldSpaceLightPos0.xyz - in_f.xlv_TEXCOORD2));
          lightDir_5 = tmpvar_7;
          tmpvar_6 = in_f.xlv_COLOR0;
          tmpvar_4 = in_f.xlv_TEXCOORD1;
          float3 tmpvar_8;
          float tmpvar_9;
          float4 col_10;
          float3 mask_11;
          float4 tex_12;
          float4 tmpvar_13;
          tmpvar_13 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          tex_12 = tmpvar_13;
          float3 tmpvar_14;
          tmpvar_14 = tex2D(_Mask, in_f.xlv_TEXCOORD0).xyz;
          mask_11 = tmpvar_14;
          col_10.xyz = (tmpvar_6.xyz * tex_12.xyz);
          float3 tmpvar_15;
          float _tmp_dvx_7 = (mask_11.z * 0.5);
          tmpvar_15 = float3(_tmp_dvx_7, _tmp_dvx_7, _tmp_dvx_7);
          float3 tmpvar_16;
          tmpvar_16 = lerp(col_10.xyz, _Color.xyz, tmpvar_15);
          col_10.xyz = float3(tmpvar_16);
          col_10.w = ((tmpvar_6.w * _Color.w) * tex_12.w);
          tmpvar_8 = col_10.xyz;
          tmpvar_9 = col_10.w;
          float4 tmpvar_17;
          tmpvar_17.w = 1;
          tmpvar_17.xyz = in_f.xlv_TEXCOORD2;
          lightCoord_3 = mul(unity_WorldToLight, tmpvar_17).xyz;
          float tmpvar_18;
          tmpvar_18 = dot(lightCoord_3, lightCoord_3);
          float tmpvar_19;
          tmpvar_19 = tex2D(_LightTexture0, float2(tmpvar_18, tmpvar_18)).w;
          tmpvar_1 = _LightColor0.xyz;
          tmpvar_2 = lightDir_5;
          tmpvar_1 = (tmpvar_1 * tmpvar_19);
          float4 c_20;
          float4 c_21;
          float diff_22;
          float tmpvar_23;
          tmpvar_23 = max(0, dot(tmpvar_4, tmpvar_2));
          diff_22 = tmpvar_23;
          c_21.xyz = float3(((tmpvar_8 * tmpvar_1) * diff_22));
          c_21.w = tmpvar_9;
          c_20.w = c_21.w;
          c_20.xyz = c_21.xyz;
          out_f.color = c_20;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 3, name: META
    {
      Name "META"
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "META"
        "QUEUE" = "Transparent+1"
        "RenderType" = "Transparent"
      }
      LOD 300
      ZClip Off
      ZWrite Off
      Cull Off
      Blend SrcAlpha OneMinusSrcAlpha
      ColorMask RGB
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      // uniform float4 unity_LightmapST;
      // uniform float4 unity_DynamicLightmapST;
      uniform float4 unity_MetaVertexControl;
      uniform float4 _MainTex_ST;
      uniform sampler2D _MainTex;
      uniform sampler2D _Mask;
      uniform float4 _Color;
      uniform float4 unity_MetaFragmentControl;
      uniform float unity_OneOverOutputBoost;
      uniform float unity_MaxOutputValue;
      uniform float unity_UseLinearSpace;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 color :COLOR;
          float4 texcoord :TEXCOORD0;
          float4 texcoord1 :TEXCOORD1;
          float4 texcoord2 :TEXCOORD2;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float4 xlv_COLOR0 :COLOR0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_COLOR0 :COLOR0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float4 tmpvar_1;
          tmpvar_1 = in_v.color;
          float4 vertex_2;
          vertex_2 = in_v.vertex;
          if(unity_MetaVertexControl.x)
          {
              vertex_2.xy = ((in_v.texcoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
              float tmpvar_3;
              if((in_v.vertex.z>0))
              {
                  tmpvar_3 = 0.0001;
              }
              else
              {
                  tmpvar_3 = 0;
              }
              vertex_2.z = tmpvar_3;
          }
          if(unity_MetaVertexControl.y)
          {
              vertex_2.xy = ((in_v.texcoord2.xy * unity_DynamicLightmapST.xy) + unity_DynamicLightmapST.zw);
              float tmpvar_4;
              if((vertex_2.z>0))
              {
                  tmpvar_4 = 0.0001;
              }
              else
              {
                  tmpvar_4 = 0;
              }
              vertex_2.z = tmpvar_4;
          }
          float4 tmpvar_5;
          tmpvar_5.w = 1;
          tmpvar_5.xyz = vertex_2.xyz;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_5));
          out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          out_v.xlv_TEXCOORD1 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
          out_v.xlv_COLOR0 = tmpvar_1;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 tmpvar_1;
          float3 tmpvar_2;
          float4 tmpvar_3;
          tmpvar_3 = in_f.xlv_COLOR0;
          float3 tmpvar_4;
          float4 col_5;
          float3 mask_6;
          float4 tex_7;
          float4 tmpvar_8;
          tmpvar_8 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          tex_7 = tmpvar_8;
          float3 tmpvar_9;
          tmpvar_9 = tex2D(_Mask, in_f.xlv_TEXCOORD0).xyz;
          mask_6 = tmpvar_9;
          col_5.xyz = (tmpvar_3.xyz * tex_7.xyz);
          float3 tmpvar_10;
          float _tmp_dvx_8 = (mask_6.z * 0.5);
          tmpvar_10 = float3(_tmp_dvx_8, _tmp_dvx_8, _tmp_dvx_8);
          float3 tmpvar_11;
          tmpvar_11 = lerp(col_5.xyz, _Color.xyz, tmpvar_10);
          col_5.xyz = float3(tmpvar_11);
          col_5.w = ((tmpvar_3.w * _Color.w) * tex_7.w);
          tmpvar_4 = col_5.xyz;
          tmpvar_2 = tmpvar_4;
          float4 res_12;
          res_12 = float4(0, 0, 0, 0);
          if(unity_MetaFragmentControl.x)
          {
              float4 tmpvar_13;
              tmpvar_13.w = 1;
              tmpvar_13.xyz = float3(tmpvar_2);
              res_12.w = tmpvar_13.w;
              float3 tmpvar_14;
              float _tmp_dvx_9 = clamp(unity_OneOverOutputBoost, 0, 1);
              tmpvar_14 = clamp(pow(tmpvar_2, float3(_tmp_dvx_9, _tmp_dvx_9, _tmp_dvx_9)), float3(0, 0, 0), float3(unity_MaxOutputValue, unity_MaxOutputValue, unity_MaxOutputValue));
              res_12.xyz = float3(tmpvar_14);
          }
          if(unity_MetaFragmentControl.y)
          {
              float3 emission_15;
              if(int(unity_UseLinearSpace))
              {
                  emission_15 = float3(0, 0, 0);
              }
              else
              {
                  emission_15 = float3(0, 0, 0);
              }
              float4 tmpvar_16;
              float alpha_17;
              float3 tmpvar_18;
              tmpvar_18 = (emission_15 * 0.01030928);
              alpha_17 = (ceil((max(max(tmpvar_18.x, tmpvar_18.y), max(tmpvar_18.z, 0.02)) * 255)) / 255);
              float tmpvar_19;
              tmpvar_19 = max(alpha_17, 0.02);
              alpha_17 = tmpvar_19;
              float4 tmpvar_20;
              tmpvar_20.xyz = float3((tmpvar_18 / tmpvar_19));
              tmpvar_20.w = tmpvar_19;
              tmpvar_16 = tmpvar_20;
              res_12 = tmpvar_16;
          }
          tmpvar_1 = res_12;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "QUEUE" = "Transparent+1"
      "RenderType" = "Transparent"
    }
    LOD 100
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "QUEUE" = "Transparent+1"
        "RenderType" = "Transparent"
      }
      LOD 100
      ZClip Off
      ZWrite Off
      Cull Off
      Fog
      { 
        Mode  Off
      } 
      Blend SrcAlpha OneMinusSrcAlpha
      ColorMask RGB
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _MainTex_ST;
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 color :COLOR;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float4 xlv_COLOR0 :COLOR0;
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float4 xlv_COLOR0 :COLOR0;
          float2 xlv_TEXCOORD0 :TEXCOORD0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float4 tmpvar_1;
          float4 tmpvar_2;
          tmpvar_2 = clamp(in_v.color, 0, 1);
          tmpvar_1 = tmpvar_2;
          float4 tmpvar_3;
          tmpvar_3.w = 1;
          tmpvar_3.xyz = in_v.vertex.xyz;
          out_v.xlv_COLOR0 = tmpvar_1;
          out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_3));
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 col_1;
          col_1 = (tex2D(_MainTex, in_f.xlv_TEXCOORD0) * in_f.xlv_COLOR0);
          if((col_1.w<=0.01))
          {
              discard;
          }
          out_f.color = col_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
