Shader "Custom/Outline"
{
    Properties
    {
        _MainTex("MainText", 2D) = "white" {}
        _Color("Outline Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Cull Off //Don't ignore any pixel
        Blend One OneMinusSrcAlpha

        Pass
        {

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;

            struct MeshData
            {
                float4 vertex : POSITION;
                float4 color: COLOR;
                float4 uv0 : TEXCOORD0;
            };

            struct Interpolator
            {
                float4 pos: SV_POSITION;
                half2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            Interpolator vert(MeshData v)
            {
                Interpolator o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv0;
                o.color = v.color;
                return o;
            }

            fixed4 _Color;
            float4 _MainTex_TexelSize;

            fixed4 frag(Interpolator i) : COLOR
            {
                half4 c = tex2D(_MainTex, i.uv);
                c.rgb *= c.a;

                half4 outlineC = _Color;
                outlineC.a *= ceil(c.a);
                outlineC.rbg *= outlineC.a;

                fixed upAlpha = tex2D(_MainTex, i.uv + fixed2(0, _MainTex_TexelSize.y)).a;
                fixed downAlpha = tex2D(_MainTex, i.uv - fixed2(0, _MainTex_TexelSize.y)).a;
                fixed rightAlpha = tex2D(_MainTex, i.uv + fixed2( _MainTex_TexelSize.x, 0)).a;
                fixed leftAlpha = tex2D(_MainTex, i.uv - fixed2( _MainTex_TexelSize.x, 0)).a;

                return lerp(outlineC, c, ceil(upAlpha * downAlpha * rightAlpha * leftAlpha));
            }

            ENDCG
        }
    }
}
