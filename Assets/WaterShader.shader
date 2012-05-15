  Shader "Example/WaterShader" {
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _BumpMap ("Texture", 2D) = "" {}
    }
    SubShader {
      
      CGPROGRAM
      #pragma surface surf SimpleSpecular alpha

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

      struct Input {
          float2 uv_MainTex;
          float2 uv_BumpMap;
      };
      sampler2D _BumpMap;
      sampler2D _MainTex;
      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
          o.Specular = tex2D (_MainTex, IN.uv_MainTex).a;
          o.Alpha = tex2D (_MainTex, IN.uv_MainTex).a;
          o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
      }
      ENDCG
    }
    Fallback "Diffuse"
  }