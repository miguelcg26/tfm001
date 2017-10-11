sampler2D _MainTex;

		struct Input 
		{
			float2 uv_MainTex;
			float2 uv_BumpMap;
	 		half3 viewDir;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		sampler2D _BumpMap;
		sampler2D _EmissionMap;
		half _EmissIn;

		#if _MATCAP_ON
		   	half _MatcapIn;
		   	sampler2D _Matcap;
		#endif
		
		#if _RIM_ON
		fixed4 _RimColor;
    	fixed _RimPow;
    	fixed _RimIn;
    	#endif   


		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			o.Normal = normalize(o.Normal);

			#if _MATCAP_ON
			float3 r = reflect(-IN.viewDir, o.Normal);
			r = mul((float3x3)UNITY_MATRIX_MV, r);
            r.z += 1;
            float m = 2 * length(r);
			float2 uv2 = r.xy / m + 0.5;
	        fixed3 matcap = tex2D(_Matcap, uv2);
	        o.Albedo *=  matcap * _MatcapIn;
	        #endif
			#if _RIM_ON
        	fixed rim = 1.0 - saturate(dot (IN.viewDir, o.Normal));
        	o.Albedo += _RimColor.rgb * pow (rim, _RimPow) * _RimIn;
        	#endif
			o.Emission = tex2D (_EmissionMap, IN.uv_MainTex) * _EmissIn;
		}

		sampler2D _SmoothnessTex;
		sampler2D _MetallicTex;
		sampler2D _MatcapTex;

		void surfTex (Input IN, inout SurfaceOutputStandard o) 
		{
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			fixed4 metallicArea = tex2D ( _MetallicTex, IN.uv_MainTex );
			o.Metallic = metallicArea.xyz *  _Metallic;
			fixed4 smoothnessArea = tex2D ( _SmoothnessTex, IN.uv_MainTex );
			o.Smoothness = smoothnessArea.xyz * _Glossiness;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			o.Normal = normalize(o.Normal);

			#if _MATCAP_ON
			float3 r = reflect(-IN.viewDir, o.Normal);
			r = mul((float3x3)UNITY_MATRIX_MV, r);
            r.z += 1;
            float m = 2 * length(r);
			float2 uv2 = r.xy / m + 0.5;
	        fixed3 matcap = tex2D(_Matcap, uv2);
			fixed4 matcapArea = tex2D ( _MatcapTex, IN.uv_MainTex );
			matcap = matcap * matcapArea.xyz;
	        o.Albedo *=  matcap * _MatcapIn;
	        #endif
			#if _RIM_ON
        	fixed rim = 1.0 - saturate(dot (IN.viewDir, o.Normal));
        	o.Albedo += _RimColor.rgb * pow (rim, _RimPow) * _RimIn;
        	#endif
			o.Emission = tex2D (_EmissionMap, IN.uv_MainTex) * _EmissIn;
		}