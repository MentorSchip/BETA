Shader "Holistic/Waves" 
{
    Properties 
	{
        _WaterTex("Water", 2D) = "white" {}
	    _FoamTex("Foam", 2D) = "white" {}
        _Tint("Colour Tint", Color) = (1,1,1,1)
	    _Brightness("Brightness", Range(0,2)) = 1
        _Freq("Frequency", Range(0,50)) = 3
        _Speed("Speed",Range(0,100)) = 10
        _Amp("Amplitude",Range(0,1)) = 0.5
	    _ScrollX("X scroll speed", Range(-10, 10)) = 0
		_ScrollY("Y scroll speed", Range(-10, 10)) = 0
	    _TexDelta("Scroll Speed Differential", Range(-4, 4)) = 1
		_percentPrimary("Primary Percent", Range(0, 1)) = 0.5
    }
	
    SubShader 
	{
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert 
      
        struct Input 
	    {
            float2 uv_WaterTex;
            float3 vertColor;
        };
      
        sampler2D _WaterTex;
	    sampler2D _FoamTex;
	    float4 _Tint;
	    half _Brightness;
        half _Freq;
        half _Speed;
        half _Amp;
	    half _ScrollX;
	    half _ScrollY;
	    half _TexDelta;
		half _percentPrimary;

        struct appdata 
	    {
            float4 vertex: POSITION;
            float3 normal: NORMAL;
            float4 texcoord: TEXCOORD0;
            float4 texcoord1: TEXCOORD1;
            float4 texcoord2: TEXCOORD2;
        };
      
        void vert (inout appdata v, out Input o) 
	    {
            UNITY_INITIALIZE_OUTPUT(Input,o);
            float t = _Time * _Speed;
            float waveHeight = sin(t + v.vertex.x * _Freq) * _Amp + sin(t*2 + v.vertex.x * _Freq*2) * _Amp;
            v.vertex.y = v.vertex.y + waveHeight;
            v.normal = normalize(float3(v.normal.x + waveHeight, v.normal.y, v.normal.z));
            o.vertColor = waveHeight + _Brightness * _Tint;
        }

        void surf (Input IN, inout SurfaceOutput o) 
	    {      	  
		    _ScrollX *= _Time;
			_ScrollY *= _Time;
			float3 primary = (tex2D(_WaterTex, IN.uv_WaterTex + float2(_ScrollX, _ScrollY))).rgb;
			float3 secondary = (tex2D(_FoamTex, IN.uv_WaterTex + float2(_ScrollX * _TexDelta, _ScrollY * _TexDelta))).rgb;
			//float4 c = tex2D(_WaterTex, IN.uv_WaterTex);
			o.Albedo = ((primary * _percentPrimary + secondary * (1 - _percentPrimary))/2.0) * IN.vertColor.rgb;
        }
        ENDCG
    } 
    Fallback "Diffuse"
  }