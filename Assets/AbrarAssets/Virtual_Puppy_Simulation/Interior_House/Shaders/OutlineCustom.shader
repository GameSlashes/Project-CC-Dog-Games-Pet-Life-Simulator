// Upgrade NOTE: commented out 'float4x4 _World2Object', a built-in variable
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Outline" {
Properties {
 _MainTex ("MainTex", 2D) = "white" { }
 _Outline ("_Outline", Range(0,0.1)) = 0
 _OutlineColor ("Color", Color) = (1,1,1,1)
}
SubShader { 
 Pass {
  Tags { "RenderType"="Opaque" }
  Cull Front
  GpuProgramID 40272
CGPROGRAM
//#pragma target 4.0

#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"
#define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
#define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
#define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)


#define CODE_BLOCK_VERTEX
//uniform float4x4 UNITY_MATRIX_MVP;
//uniform float4x4 UNITY_MATRIX_MV;
//uniform float4x4 UNITY_MATRIX_P;
uniform float _Outline;
uniform float4 _OutlineColor;
struct appdata_t
{
    float4 vertex :POSITION;
    float3 normal :NORMAL;
};

struct OUT_Data_Vert
{
    float4 vertex :SV_POSITION;
};

struct v2f
{
    float4 vertex :Position;
};

struct OUT_Data_Frag
{
    float4 color :SV_Target0;
};

OUT_Data_Vert vert(appdata_t in_v)
{
    OUT_Data_Vert out_v;
    float3 normal_1;
    float4 tmpvar_2;
    tmpvar_2 = UnityObjectToClipPos(in_v.vertex);
    float3x3 tmpvar_3;
    tmpvar_3[0] = conv_mxt4x4_0(UNITY_MATRIX_MV).xyz;
    tmpvar_3[1] = conv_mxt4x4_1(UNITY_MATRIX_MV).xyz;
    tmpvar_3[2] = conv_mxt4x4_2(UNITY_MATRIX_MV).xyz;
    float3 tmpvar_4;
    tmpvar_4 = mul(tmpvar_3, in_v.normal);
    normal_1.z = tmpvar_4.z;
    normal_1.x = (tmpvar_4.x * conv_mxt4x4_0(UNITY_MATRIX_P).x).x;
    normal_1.y = (tmpvar_4.y * conv_mxt4x4_1(UNITY_MATRIX_P).y).x;
    tmpvar_2.xy = float2((tmpvar_2.xy + (normal_1.xy * _Outline)));
    out_v.vertex = tmpvar_2;
    return out_v;
}

#define CODE_BLOCK_FRAGMENT
OUT_Data_Frag frag(v2f in_f)
{
    OUT_Data_Frag out_f;
    float4 tmpvar_1;
    tmpvar_1 = _OutlineColor;
    out_f.color = tmpvar_1;
    return out_f;
}


ENDCG

}
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardBase" "SHADOWSUPPORT"="true" }
  GpuProgramID 107925
CGPROGRAM
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
//uniform float4x4 UNITY_MATRIX_MVP;
//uniform float4x4 unity_ObjectToWorld;
// uniform float4x4 _World2Object;
uniform float4 _MainTex_ST;
//uniform float4 _WorldSpaceLightPos0;
uniform float4 _LightColor0;
uniform sampler2D _MainTex;
struct appdata_t
{
    float4 vertex :POSITION;
    float3 normal :NORMAL;
    float4 texcoord :TEXCOORD0;
};

struct OUT_Data_Vert
{
    float2 xlv_TEXCOORD0 :TEXCOORD0;
    float3 xlv_TEXCOORD1 :TEXCOORD1;
    float3 xlv_TEXCOORD2 :TEXCOORD2;
    float3 xlv_TEXCOORD3 :TEXCOORD3;
    float4 vertex :SV_POSITION;
};

struct v2f
{
    float2 xlv_TEXCOORD0 :TEXCOORD0;
    float3 xlv_TEXCOORD1 :TEXCOORD1;
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
    float4 v_3;
    v_3.x = conv_mxt4x4_0(unity_WorldToObject).x;
    v_3.y = conv_mxt4x4_1(unity_WorldToObject).x;
    v_3.z = conv_mxt4x4_2(unity_WorldToObject).x;
    v_3.w = conv_mxt4x4_3(unity_WorldToObject).x;
    float4 v_4;
    v_4.x = conv_mxt4x4_0(unity_WorldToObject).y.x;
    v_4.y = conv_mxt4x4_1(unity_WorldToObject).y.x;
    v_4.z = conv_mxt4x4_2(unity_WorldToObject).y.x;
    v_4.w = conv_mxt4x4_3(unity_WorldToObject).y.x;
    float4 v_5;
    v_5.x = conv_mxt4x4_0(unity_WorldToObject).z.x;
    v_5.y = conv_mxt4x4_1(unity_WorldToObject).z.x;
    v_5.z = conv_mxt4x4_2(unity_WorldToObject).z.x;
    v_5.w = conv_mxt4x4_3(unity_WorldToObject).z.x;
    float3 tmpvar_6;
    tmpvar_6 = normalize((((v_3.xyz * in_v.normal.x) + (v_4.xyz * in_v.normal.y)) + (v_5.xyz * in_v.normal.z)));
    worldNormal_1 = tmpvar_6;
    tmpvar_2 = worldNormal_1;
    float3 normal_7;
    normal_7 = worldNormal_1;
    float4 tmpvar_8;
    tmpvar_8.w = 1;
    tmpvar_8.xyz = float3(normal_7);
    float3 res_9;
    float3 x_10;
    x_10.x = dot(unity_SHAr, tmpvar_8);
    x_10.y = dot(unity_SHAg, tmpvar_8);
    x_10.z = dot(unity_SHAb, tmpvar_8);
    float3 x1_11;
    float4 tmpvar_12;
    tmpvar_12 = (normal_7.xyzz * normal_7.yzzx);
    x1_11.x = dot(unity_SHBr, tmpvar_12);
    x1_11.y = dot(unity_SHBg, tmpvar_12);
    x1_11.z = dot(unity_SHBb, tmpvar_12);
    res_9 = (x_10 + (x1_11 + (unity_SHC.xyz * ((normal_7.x * normal_7.x) - (normal_7.y * normal_7.y)))));
    res_9 = max(((1.055 * pow(max(res_9, float3(0, 0, 0)), float3(0.4166667, 0.4166667, 0.4166667))) - 0.055), float3(0, 0, 0));
    out_v.vertex = UnityObjectToClipPos(in_v.vertex);
    out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
    out_v.xlv_TEXCOORD1 = tmpvar_2;
    out_v.xlv_TEXCOORD2 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
    out_v.xlv_TEXCOORD3 = max(float3(0, 0, 0), res_9);
    return out_v;
}

#define CODE_BLOCK_FRAGMENT
OUT_Data_Frag frag(v2f in_f)
{
    OUT_Data_Frag out_f;
    float3 tmpvar_1;
    float3 tmpvar_2;
    float4 c_3;
    float3 tmpvar_4;
    float3 lightDir_5;
    float3 tmpvar_6;
    tmpvar_6 = _WorldSpaceLightPos0.xyz;
    lightDir_5 = tmpvar_6;
    tmpvar_4 = in_f.xlv_TEXCOORD1;
    float4 tmpvar_7;
    tmpvar_7 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
    tmpvar_1 = _LightColor0.xyz;
    tmpvar_2 = lightDir_5;
    float4 c_8;
    float4 c_9;
    float diff_10;
    float tmpvar_11;
    tmpvar_11 = max(0, dot(tmpvar_4, tmpvar_2));
    diff_10 = tmpvar_11;
    c_9.xyz = float3(((tmpvar_7.xyz * tmpvar_1) * diff_10));
    c_9.w = 0;
    c_8.w = c_9.w;
    c_8.xyz = float3((c_9.xyz + (tmpvar_7.xyz * in_f.xlv_TEXCOORD3)));
    c_3.xyz = float3(c_8.xyz);
    c_3.w = 1;
    out_f.color = c_3;
    return out_f;
}


ENDCG

}
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardAdd" }
  ZWrite Off
  Blend One One
  GpuProgramID 176714
CGPROGRAM
//#pragma target 4.0

#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"
#define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
#define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
#define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
#define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)


#define CODE_BLOCK_VERTEX
//uniform float4x4 UNITY_MATRIX_MVP;
//uniform float4x4 unity_ObjectToWorld;
// uniform float4x4 _World2Object;
uniform float4 _MainTex_ST;
//uniform float4 _WorldSpaceLightPos0;
uniform float4 _LightColor0;
uniform sampler2D _LightTexture0;
uniform float4x4 unity_WorldToLight;
uniform sampler2D _MainTex;
struct appdata_t
{
    float4 vertex :POSITION;
    float3 normal :NORMAL;
    float4 texcoord :TEXCOORD0;
};

struct OUT_Data_Vert
{
    float2 xlv_TEXCOORD0 :TEXCOORD0;
    float3 xlv_TEXCOORD1 :TEXCOORD1;
    float3 xlv_TEXCOORD2 :TEXCOORD2;
    float4 vertex :SV_POSITION;
};

struct v2f
{
    float2 xlv_TEXCOORD0 :TEXCOORD0;
    float3 xlv_TEXCOORD1 :TEXCOORD1;
    float3 xlv_TEXCOORD2 :TEXCOORD2;
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
    float4 v_3;
    v_3.x = conv_mxt4x4_0(unity_WorldToObject).x;
    v_3.y = conv_mxt4x4_1(unity_WorldToObject).x;
    v_3.z = conv_mxt4x4_2(unity_WorldToObject).x;
    v_3.w = conv_mxt4x4_3(unity_WorldToObject).x;
    float4 v_4;
    v_4.x = conv_mxt4x4_0(unity_WorldToObject).y.x;
    v_4.y = conv_mxt4x4_1(unity_WorldToObject).y.x;
    v_4.z = conv_mxt4x4_2(unity_WorldToObject).y.x;
    v_4.w = conv_mxt4x4_3(unity_WorldToObject).y.x;
    float4 v_5;
    v_5.x = conv_mxt4x4_0(unity_WorldToObject).z.x;
    v_5.y = conv_mxt4x4_1(unity_WorldToObject).z.x;
    v_5.z = conv_mxt4x4_2(unity_WorldToObject).z.x;
    v_5.w = conv_mxt4x4_3(unity_WorldToObject).z.x;
    float3 tmpvar_6;
    tmpvar_6 = normalize((((v_3.xyz * in_v.normal.x) + (v_4.xyz * in_v.normal.y)) + (v_5.xyz * in_v.normal.z)));
    worldNormal_1 = tmpvar_6;
    tmpvar_2 = worldNormal_1;
    out_v.vertex = UnityObjectToClipPos(in_v.vertex);
    out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
    out_v.xlv_TEXCOORD1 = tmpvar_2;
    out_v.xlv_TEXCOORD2 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
    return out_v;
}

#define CODE_BLOCK_FRAGMENT
OUT_Data_Frag frag(v2f in_f)
{
    OUT_Data_Frag out_f;
    float3 tmpvar_1;
    float3 tmpvar_2;
    float4 c_3;
    float3 tmpvar_4;
    float3 lightDir_5;
    float3 tmpvar_6;
    tmpvar_6 = normalize((_WorldSpaceLightPos0.xyz - in_f.xlv_TEXCOORD2));
    lightDir_5 = tmpvar_6;
    tmpvar_4 = in_f.xlv_TEXCOORD1;
    float4 tmpvar_7;
    tmpvar_7.w = 1;
    tmpvar_7.xyz = float3(in_f.xlv_TEXCOORD2);
    float3 tmpvar_8;
    tmpvar_8 = mul(unity_WorldToLight, tmpvar_7).xyz.xyz;
    float tmpvar_9;
    tmpvar_9 = dot(tmpvar_8, tmpvar_8);
    float tmpvar_10;
    tmpvar_10 = tex2D(_LightTexture0, float2(tmpvar_9, tmpvar_9)).w.x;
    tmpvar_1 = _LightColor0.xyz;
    tmpvar_2 = lightDir_5;
    tmpvar_1 = (tmpvar_1 * tmpvar_10);
    float4 c_11;
    float4 c_12;
    float diff_13;
    float tmpvar_14;
    tmpvar_14 = max(0, dot(tmpvar_4, tmpvar_2));
    diff_13 = tmpvar_14;
    c_12.xyz = float3(((tex2D(_MainTex, in_f.xlv_TEXCOORD0).xyz * tmpvar_1) * diff_13));
    c_12.w = 0;
    c_11.w = c_12.w;
    c_11.xyz = float3(c_12.xyz);
    c_3.xyz = float3(c_11.xyz);
    c_3.w = 1;
    out_f.color = c_3;
    return out_f;
}


ENDCG

}
 Pass {
  Name "PREPASS"
  Tags { "LIGHTMODE"="PrePassBase" }
  GpuProgramID 205472
CGPROGRAM
//#pragma target 4.0

#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"
#define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
#define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
#define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
#define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)


#define CODE_BLOCK_VERTEX
//uniform float4x4 UNITY_MATRIX_MVP;
//uniform float4x4 unity_ObjectToWorld;
// uniform float4x4 _World2Object;
struct appdata_t
{
    float4 vertex :POSITION;
    float3 normal :NORMAL;
};

struct OUT_Data_Vert
{
    float3 xlv_TEXCOORD0 :TEXCOORD0;
    float3 xlv_TEXCOORD1 :TEXCOORD1;
    float4 vertex :SV_POSITION;
};

struct v2f
{
    float3 xlv_TEXCOORD0 :TEXCOORD0;
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
    float4 v_3;
    v_3.x = conv_mxt4x4_0(unity_WorldToObject).x;
    v_3.y = conv_mxt4x4_1(unity_WorldToObject).x;
    v_3.z = conv_mxt4x4_2(unity_WorldToObject).x;
    v_3.w = conv_mxt4x4_3(unity_WorldToObject).x;
    float4 v_4;
    v_4.x = conv_mxt4x4_0(unity_WorldToObject).y.x;
    v_4.y = conv_mxt4x4_1(unity_WorldToObject).y.x;
    v_4.z = conv_mxt4x4_2(unity_WorldToObject).y.x;
    v_4.w = conv_mxt4x4_3(unity_WorldToObject).y.x;
    float4 v_5;
    v_5.x = conv_mxt4x4_0(unity_WorldToObject).z.x;
    v_5.y = conv_mxt4x4_1(unity_WorldToObject).z.x;
    v_5.z = conv_mxt4x4_2(unity_WorldToObject).z.x;
    v_5.w = conv_mxt4x4_3(unity_WorldToObject).z.x;
    float3 tmpvar_6;
    tmpvar_6 = normalize((((v_3.xyz * in_v.normal.x) + (v_4.xyz * in_v.normal.y)) + (v_5.xyz * in_v.normal.z)));
    worldNormal_1 = tmpvar_6;
    tmpvar_2 = worldNormal_1;
    out_v.vertex = UnityObjectToClipPos(in_v.vertex);
    out_v.xlv_TEXCOORD0 = tmpvar_2;
    out_v.xlv_TEXCOORD1 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
    return out_v;
}

#define CODE_BLOCK_FRAGMENT
OUT_Data_Frag frag(v2f in_f)
{
    OUT_Data_Frag out_f;
    float4 res_1;
    float3 tmpvar_2;
    tmpvar_2 = in_f.xlv_TEXCOORD0;
    res_1.xyz = float3(((tmpvar_2 * 0.5) + 0.5));
    res_1.w = 0;
    out_f.color = res_1;
    return out_f;
}


ENDCG

}
 Pass {
  Name "PREPASS"
  Tags { "LIGHTMODE"="PrePassFinal" }
  ZWrite Off
  GpuProgramID 318277
CGPROGRAM
//#pragma target 4.0

#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"
#define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
#define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
#define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
#define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)


#define CODE_BLOCK_VERTEX
//uniform float4 _ProjectionParams;
//uniform float4 unity_SHAr;
//uniform float4 unity_SHAg;
//uniform float4 unity_SHAb;
//uniform float4 unity_SHBr;
//uniform float4 unity_SHBg;
//uniform float4 unity_SHBb;
//uniform float4 unity_SHC;
//uniform float4x4 UNITY_MATRIX_MVP;
//uniform float4x4 unity_ObjectToWorld;
// uniform float4x4 _World2Object;
uniform float4 _MainTex_ST;
uniform sampler2D _MainTex;
uniform sampler2D _LightBuffer;
struct appdata_t
{
    float4 vertex :POSITION;
    float3 normal :NORMAL;
    float4 texcoord :TEXCOORD0;
};

struct OUT_Data_Vert
{
    float2 xlv_TEXCOORD0 :TEXCOORD0;
    float3 xlv_TEXCOORD1 :TEXCOORD1;
    float4 xlv_TEXCOORD2 :TEXCOORD2;
    float4 xlv_TEXCOORD3 :TEXCOORD3;
    float3 xlv_TEXCOORD4 :TEXCOORD4;
    float4 vertex :SV_POSITION;
};

struct v2f
{
    float2 xlv_TEXCOORD0 :TEXCOORD0;
    float4 xlv_TEXCOORD2 :TEXCOORD2;
    float3 xlv_TEXCOORD4 :TEXCOORD4;
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
    float3 tmpvar_3;
    tmpvar_1 = UnityObjectToClipPos(in_v.vertex);
    float4 o_4;
    float4 tmpvar_5;
    tmpvar_5 = (tmpvar_1 * 0.5);
    float2 tmpvar_6;
    tmpvar_6.x = tmpvar_5.x;
    tmpvar_6.y = (tmpvar_5.y * _ProjectionParams.x);
    o_4.xy = float2((tmpvar_6 + tmpvar_5.w));
    o_4.zw = tmpvar_1.zw;
    tmpvar_2.zw = float2(0, 0);
    tmpvar_2.xy = float2(0, 0);
    float4 v_7;
    v_7.x = conv_mxt4x4_0(unity_WorldToObject).x;
    v_7.y = conv_mxt4x4_1(unity_WorldToObject).x;
    v_7.z = conv_mxt4x4_2(unity_WorldToObject).x;
    v_7.w = conv_mxt4x4_3(unity_WorldToObject).x;
    float4 v_8;
    v_8.x = conv_mxt4x4_0(unity_WorldToObject).y.x;
    v_8.y = conv_mxt4x4_1(unity_WorldToObject).y.x;
    v_8.z = conv_mxt4x4_2(unity_WorldToObject).y.x;
    v_8.w = conv_mxt4x4_3(unity_WorldToObject).y.x;
    float4 v_9;
    v_9.x = conv_mxt4x4_0(unity_WorldToObject).z.x;
    v_9.y = conv_mxt4x4_1(unity_WorldToObject).z.x;
    v_9.z = conv_mxt4x4_2(unity_WorldToObject).z.x;
    v_9.w = conv_mxt4x4_3(unity_WorldToObject).z.x;
    float4 tmpvar_10;
    tmpvar_10.w = 1;
    tmpvar_10.xyz = float3(normalize((((v_7.xyz * in_v.normal.x) + (v_8.xyz * in_v.normal.y)) + (v_9.xyz * in_v.normal.z))));
    float4 normal_11;
    normal_11 = tmpvar_10;
    float3 res_12;
    float3 x_13;
    x_13.x = dot(unity_SHAr, normal_11);
    x_13.y = dot(unity_SHAg, normal_11);
    x_13.z = dot(unity_SHAb, normal_11);
    float3 x1_14;
    float4 tmpvar_15;
    tmpvar_15 = (normal_11.xyzz * normal_11.yzzx);
    x1_14.x = dot(unity_SHBr, tmpvar_15);
    x1_14.y = dot(unity_SHBg, tmpvar_15);
    x1_14.z = dot(unity_SHBb, tmpvar_15);
    res_12 = (x_13 + (x1_14 + (unity_SHC.xyz * ((normal_11.x * normal_11.x) - (normal_11.y * normal_11.y)))));
    res_12 = max(((1.055 * pow(max(res_12, float3(0, 0, 0)), float3(0.4166667, 0.4166667, 0.4166667))) - 0.055), float3(0, 0, 0));
    tmpvar_3 = res_12;
    out_v.vertex = tmpvar_1;
    out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
    out_v.xlv_TEXCOORD1 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
    out_v.xlv_TEXCOORD2 = o_4;
    out_v.xlv_TEXCOORD3 = tmpvar_2;
    out_v.xlv_TEXCOORD4 = tmpvar_3;
    return out_v;
}

#define CODE_BLOCK_FRAGMENT
OUT_Data_Frag frag(v2f in_f)
{
    OUT_Data_Frag out_f;
    float4 tmpvar_1;
    float4 c_2;
    float4 light_3;
    float4 tmpvar_4;
    tmpvar_4 = tex2D(_LightBuffer, in_f.xlv_TEXCOORD2);
    light_3 = tmpvar_4;
    light_3 = (-log2(max(light_3, float4(0.001, 0.001, 0.001, 0.001))));
    light_3.xyz = float3((light_3.xyz + in_f.xlv_TEXCOORD4));
    float4 c_5;
    c_5.xyz = float3((tex2D(_MainTex, in_f.xlv_TEXCOORD0).xyz * light_3.xyz));
    c_5.w = 0;
    c_2.xyz = float3(c_5.xyz);
    c_2.w = 1;
    tmpvar_1 = c_2;
    out_f.color = tmpvar_1;
    return out_f;
}


ENDCG

}
 Pass {
  Name "DEFERRED"
  Tags { "LIGHTMODE"="Deferred" }
  GpuProgramID 388043
CGPROGRAM
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
//uniform float4x4 UNITY_MATRIX_MVP;
//uniform float4x4 unity_ObjectToWorld;
// uniform float4x4 _World2Object;
uniform float4 _MainTex_ST;
uniform sampler2D _MainTex;
struct appdata_t
{
    float4 vertex :POSITION;
    float3 normal :NORMAL;
    float4 texcoord :TEXCOORD0;
};

struct OUT_Data_Vert
{
    float2 xlv_TEXCOORD0 :TEXCOORD0;
    float3 xlv_TEXCOORD1 :TEXCOORD1;
    float3 xlv_TEXCOORD2 :TEXCOORD2;
    float4 xlv_TEXCOORD3 :TEXCOORD3;
    float3 xlv_TEXCOORD4 :TEXCOORD4;
    float4 vertex :SV_POSITION;
};

struct v2f
{
    float2 xlv_TEXCOORD0 :TEXCOORD0;
    float3 xlv_TEXCOORD1 :TEXCOORD1;
    float3 xlv_TEXCOORD4 :TEXCOORD4;
};

struct OUT_Data_Frag
{
    float4 color :SV_Target0;
    float4 color1 :SV_Target1;
    float4 color2 :SV_Target2;
    float4 color3 :SV_Target3;
};

OUT_Data_Vert vert(appdata_t in_v)
{
    OUT_Data_Vert out_v;
    float3 worldNormal_1;
    float3 tmpvar_2;
    float4 tmpvar_3;
    float4 v_4;
    v_4.x = conv_mxt4x4_0(unity_WorldToObject).x;
    v_4.y = conv_mxt4x4_1(unity_WorldToObject).x;
    v_4.z = conv_mxt4x4_2(unity_WorldToObject).x;
    v_4.w = conv_mxt4x4_3(unity_WorldToObject).x;
    float4 v_5;
    v_5.x = conv_mxt4x4_0(unity_WorldToObject).y.x;
    v_5.y = conv_mxt4x4_1(unity_WorldToObject).y.x;
    v_5.z = conv_mxt4x4_2(unity_WorldToObject).y.x;
    v_5.w = conv_mxt4x4_3(unity_WorldToObject).y.x;
    float4 v_6;
    v_6.x = conv_mxt4x4_0(unity_WorldToObject).z.x;
    v_6.y = conv_mxt4x4_1(unity_WorldToObject).z.x;
    v_6.z = conv_mxt4x4_2(unity_WorldToObject).z.x;
    v_6.w = conv_mxt4x4_3(unity_WorldToObject).z.x;
    float3 tmpvar_7;
    tmpvar_7 = normalize((((v_4.xyz * in_v.normal.x) + (v_5.xyz * in_v.normal.y)) + (v_6.xyz * in_v.normal.z)));
    worldNormal_1 = tmpvar_7;
    tmpvar_2 = worldNormal_1;
    tmpvar_3.zw = float2(0, 0);
    tmpvar_3.xy = float2(0, 0);
    float3 normal_8;
    normal_8 = worldNormal_1;
    float4 tmpvar_9;
    tmpvar_9.w = 1;
    tmpvar_9.xyz = float3(normal_8);
    float3 res_10;
    float3 x_11;
    x_11.x = dot(unity_SHAr, tmpvar_9);
    x_11.y = dot(unity_SHAg, tmpvar_9);
    x_11.z = dot(unity_SHAb, tmpvar_9);
    float3 x1_12;
    float4 tmpvar_13;
    tmpvar_13 = (normal_8.xyzz * normal_8.yzzx);
    x1_12.x = dot(unity_SHBr, tmpvar_13);
    x1_12.y = dot(unity_SHBg, tmpvar_13);
    x1_12.z = dot(unity_SHBb, tmpvar_13);
    res_10 = (x_11 + (x1_12 + (unity_SHC.xyz * ((normal_8.x * normal_8.x) - (normal_8.y * normal_8.y)))));
    res_10 = max(((1.055 * pow(max(res_10, float3(0, 0, 0)), float3(0.4166667, 0.4166667, 0.4166667))) - 0.055), float3(0, 0, 0));
    out_v.vertex = UnityObjectToClipPos(in_v.vertex);
    out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
    out_v.xlv_TEXCOORD1 = tmpvar_2;
    out_v.xlv_TEXCOORD2 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
    out_v.xlv_TEXCOORD3 = tmpvar_3;
    out_v.xlv_TEXCOORD4 = max(float3(0, 0, 0), res_10);
    return out_v;
}

#define CODE_BLOCK_FRAGMENT
OUT_Data_Frag frag(v2f in_f)
{
    OUT_Data_Frag out_f;
    float4 outDiffuse_1;
    float4 outEmission_2;
    float3 tmpvar_3;
    tmpvar_3 = in_f.xlv_TEXCOORD1;
    float4 tmpvar_4;
    tmpvar_4 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
    float4 outDiffuseOcclusion_5;
    float4 outNormal_6;
    float4 emission_7;
    float4 tmpvar_8;
    tmpvar_8.w = 1;
    tmpvar_8.xyz = float3(tmpvar_4.xyz);
    outDiffuseOcclusion_5 = tmpvar_8;
    float4 tmpvar_9;
    tmpvar_9.w = 1;
    tmpvar_9.xyz = float3(((tmpvar_3 * 0.5) + 0.5));
    outNormal_6 = tmpvar_9;
    float4 tmpvar_10;
    tmpvar_10.w = 1;
    tmpvar_10.xyz = float3(0, 0, 0);
    emission_7 = tmpvar_10;
    emission_7.xyz = float3((emission_7.xyz + (tmpvar_4.xyz * in_f.xlv_TEXCOORD4)));
    outDiffuse_1.xyz = float3(outDiffuseOcclusion_5.xyz);
    outEmission_2.w = emission_7.w;
    outEmission_2.xyz = float3(exp2((-emission_7.xyz)));
    outDiffuse_1.w = 1;
    out_f.color = outDiffuse_1;
    out_f.color1 = float4(0, 0, 0, 0);
    out_f.color2 = outNormal_6;
    out_f.color3 = outEmission_2;
    return out_f;
}


ENDCG

}
}
Fallback "Diffuse"
}