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
     
      CGPROGRAM
// Upgrade NOTE: excluded shader from OpenGL ES 2.0 because it does not contain a surface program or both vertex and fragment programs.
#pragma exclude_renderers gles
      
      #pragma surface surf SimpleSpecular vertex:vert alpha

      	
      float4 _Point1;
      float _Time1;
      float3 normal;

      struct v2f {
      	float4 pos : SV_POSITION;
      	float3 color : COLOR0;
      };
      struct Input {
          float2 uv_MainTex;
          float2 uv_BumpMap;
          float3 worldPos;
          float3 worldNormal;INTERNAL_DATA
      };
      sampler2D _BumpMap;
      sampler2D _MainTex;      


		float gety(float2 xz, float3 p){
			float2 dif;
			dif.xy = xz - p.xy;
      		float dist = sqrt(pow(dif.x,2)+pow(dif.y,2)); 
      		return (p.z + 20*cos(dist/7-(_Time1/500)));
		}
		
		float3 getnorm(float3 loc,float3 p){
				float3 temp1;
      			float3 temp2;
      			temp1 =loc + (1,0,0);
      			temp1.y=gety(temp1.xy,p);
      			temp2 =loc + (0,0,1);
      			temp2.y=gety(temp2.xz,p);
				return cross(temp1-loc, temp2-loc);
	  }

      	void vert (inout appdata_full v){
      		v2f newvert;
      		float2 dif;
      		
      		newvert.pos = v.vertex;
      		if(_Point1.w==1){
      			v.vertex.y = gety(v.vertex.xz,_Point1.xyz);
      			v.normal = getnorm(v.vertex,_Point1.xyz);
      		}
      		else{
      			v.vertex.y = _Point1.z;
      		}
      	}
      		

 	  half4 LightingSimpleSpecular (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
          half3 h = normalize (lightDir + viewDir);

          half diff = max (0, dot (s.Normal, lightDir));

          float nh = max (0, dot (s.Normal, h));
          float spec = pow (nh, 48.0);

          half4 c;
          c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * (atten * 2);
          c.a = s.Alpha;
          return c;
      }

	  

      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
          o.Specular = tex2D (_MainTex, IN.uv_MainTex).a;
          o.Alpha = tex2D (_MainTex, IN.uv_MainTex).a;
          
      }

      ENDCG
    }
    Fallback "Specular"
  }