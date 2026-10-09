Shader "Playable/VertexColor"
{
    Properties { _Color ("Tint", Color) = (1,1,1,1) _Highlight ("White highlight", Float) = 0 }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        Pass
        {
            Cull Off
            ZWrite On
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            struct appdata { float4 vertex : POSITION; fixed4 color : COLOR; };
            struct v2f { float4 position : SV_POSITION; fixed4 color : COLOR; };
            fixed4 _Color;
            fixed _Highlight;
            v2f vert(appdata v)
            {
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                #ifndef UNITY_COLORSPACE_GAMMA
                o.color.rgb = GammaToLinearSpace(o.color.rgb);
                #endif
                o.color.rgb = lerp(o.color.rgb, fixed3(1,1,1), _Highlight);
                // Alpha zero marks editor-baked symbols that keep their own color.
                o.color.rgb *= lerp(fixed3(1,1,1), _Color.rgb, step(0.001, v.color.a));
                o.color.a = _Color.a;
                return o;
            }
            fixed4 frag(v2f i) : SV_Target { return i.color; }
            ENDCG
        }
    }
}
