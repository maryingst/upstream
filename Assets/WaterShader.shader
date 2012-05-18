  Shader "Example/WaterShader" {
  Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _BumpMap ("Texture", 2D) = "" {}
      _Point1 ("Point1", Vector) = (1,1,1,0)
      _Point2 ("Point2", Vector) = (1,1,1,0)
      _Point3 ("Point3", Vector) = (1,1,1,0)
      _Point4 ("Point4", Vector) = (1,1,1,0)
      _Point5 ("Point5", Vector) = (1,1,1,0)
      _Time1 ("Time1",float) = 0
      _Time2 ("Time2",float) = 0
      _Time3 ("Time3",float) = 0
      _Time4 ("Time4",float) = 0
      _Time5 ("Time5",float) = 0
      
    }
SubShader {
    Pass {

CGPROGRAM
// Upgrade NOTE: excluded shader from Xbox360; has structs without semantics (struct v2f members norm)
#pragma exclude_renderers xbox360
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

struct v2f {
    float4 pos : SV_POSITION;
    float2  uv : TEXCOORD0;
    float3 varyingNormalDirection;
    float4 eyedir;
};

float4 _Point1;
float4 _Point2;
float4 _Point3;
float4 _Point4;
float4 _Point5;
float _Time1;
float _Time2;
float _Time3;
float _Time4;
float _Time5;
float4 _MainTex_ST;
float4 _BumpMap_ST;

uniform sampler2D _BumpMap;
uniform sampler2D _MainTex;

		float gety(float2 xz, float3 p,float time){
			float2 dif;
			dif.xy = xz - p.xz;
      		float dist = sqrt(pow(dif.x,2)+pow(dif.y,2)); 
      		return (p.y - 20*cos(dist/7-(time/500))-20);
		}
		
		float3 getnorm(float3 loc){
				float3 temp1;
      			float3 temp2;
      			float3 temploc = loc;
      			temp1 =loc + float3(0.01,0,0);
      			temp2 =loc + float3(0,0,0.01);
      			if(_Point1.w==1){
    				temp1.y= gety(temp1.xz,_Point1.xyz,_Time1);
    				temp2.y= gety(temp2.xz,_Point1.xyz,_Time1);
    				if(_Point2.w==1){
    					temp1.y+= gety(temp1.xz,_Point2.xyz,_Time2);
    					temp2.y+= gety(temp2.xz,_Point2.xyz,_Time2);
    				}
    			}
    				
    			return cross((temp1-loc), (temp2-loc));
	  }

v2f vert (appdata_base v)
{
    v2f o;
    float temp = 1;//gety(v.vertex.xz,_Point1.xyz);
    float4 tempv =v.vertex;
    if(_Point1.w==1){
    	tempv.y= gety(v.vertex.xz,_Point1.xyz,_Time1);
    	if(_Point2.w==1){
    		tempv.y+= gety(v.vertex.xz,_Point2.xyz,_Time2);
    	}
    }
    o.pos = mul(UNITY_MATRIX_MVP,tempv);
    o.uv =  TRANSFORM_TEX (v.texcoord, _MainTex);
    o.varyingNormalDirection = mul(UNITY_MATRIX_MVP, float4(normalize(getnorm(tempv)),1)).xyz;
    o.eyedir = mul(UNITY_MATRIX_MV,float4(tempv.xyz,1));
    return o;
}

half4 frag (v2f i) : COLOR
{
	float3 normalDirection = i.varyingNormalDirection + tex2D(_BumpMap, i.uv).xyz;
	
	
    float3 spec = max(0.0, dot(normalize(normalDirection),normalize(i.eyedir.xyz)));
    spec = tex2D(_MainTex,i.uv).rgb *spec+tex2D(_MainTex,i.uv).rgb/2;
    if(spec.r>1 || spec.g>1 || spec.b>1)
    	spec = spec-tex2D(_MainTex,i.uv).rgb/2;
    return float4(spec,0.5);
}
ENDCG

    }
}
Fallback "VertexLit"
} 