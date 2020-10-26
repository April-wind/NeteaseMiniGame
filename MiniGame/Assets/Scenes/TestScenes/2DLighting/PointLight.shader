Shader "Light/PointLight"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MainColor("MainColor",Color) = (1,1,1,1)
        _LightPos("Light Pos",Vector) = (0.5,0.5,0.4,0.6)
        _LightIntensity("Light Intensity",Range(0,1)) = 0.6
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float2 _MainTex_TexelSize;
            float4 _MainColor;
            float _LightIntensity;
            float4 _LightPos;
            //uniform float4 _LightParm;
            
            //uniform float4 lightPos;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //lightPos = UnityObjectToClipPos(float(_LightParm.xyz,1));
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float dis = distance(i.uv,_LightPos.xy);
                
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) ;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                float radius = _LightPos.z; 
                float lightIntensity = saturate(lerp(0,_LightPos.w,(radius - dis)/(radius + dis)));
                col = col * _MainColor;
                col.a = lightIntensity;
                return col;
            }
            ENDCG
        }
    }
}
