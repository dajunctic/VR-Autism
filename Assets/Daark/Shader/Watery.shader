Shader "Daark/WaveWaterShader"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {} // Texture bề mặt nước
        _NormalMap ("Normal Map", 2D) = "bump" {} // Normal Map cho sóng gợn
        _WaveSpeed ("Wave Speed", Float) = 1.0 // Tốc độ sóng
        _WaveAmplitude ("Wave Amplitude", Float) = 0.1 // Biên độ sóng
        _Glossiness ("Smoothness", Range(0.0, 1.0)) = 0.5 // Độ bóng
        _Metallic ("Metallic", Range(0.0, 1.0)) = 0.0 // Kim loại
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
        sampler2D _NormalMap;
        float _WaveSpeed;
        float _WaveAmplitude;
        float _Glossiness;
        float _Metallic;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Ánh xạ texture chính
            float2 waveUV = IN.uv_MainTex;

            // Hiệu ứng sóng: làm biến đổi tọa độ UV dựa trên thời gian
            waveUV.y += _Time.y * _WaveSpeed;

            fixed4 baseColor = tex2D(_MainTex, waveUV);
            o.Albedo = baseColor.rgb;

            // Ánh xạ Normal Map với hiệu ứng sóng
            fixed3 normalTex = UnpackNormal(tex2D(_NormalMap, waveUV));
            o.Normal = normalTex;

            // Cài đặt ánh sáng và độ bóng
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
        }
        ENDCG
    }

    FallBack "Diffuse"
}
