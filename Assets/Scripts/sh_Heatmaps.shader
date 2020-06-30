Shader "Maps/Heatmap" {
	Properties{
		_HeatTex("Gradient for Heatmap", 2D) = "white" {}
		_BaseTex("Texture for Background", 2D) = "white" {}
			 
	}

	SubShader{
		Tags{ "Queue" = "Transparent" "DisableBatching"="True"}
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			ZWrite Off 
			Cull Off
			CGPROGRAM
			#pragma vertex vert             
			#pragma fragment frag
			#include "UnityCG.cginc"

			#define STEPS 64
			#define STEP_SIZE 0.01
			#define TEST_RADIUS 20

			//Defining the heatpoints
			struct heatData {
				float3 pos : POSITION;
				float rad : RADIUS;
				float intensity : INTENSITY;
			};

			//Reading Screenspace
			struct vertInput {
				float4 pos : POSITION;
			};

			//Finialized Color
			struct vertOutput {
				float4 pos : POSITION;
				fixed3 wPos : TEXCOORD0;
				fixed3 worldPos : TEXCOORD1;
			};

			vertOutput vert(vertInput input) {
				vertOutput o;
				o.pos = UnityObjectToClipPos(input.pos);
				o.worldPos = mul(unity_ObjectToWorld, input.pos).xyz;
				return o;
			}
            
			StructuredBuffer<heatData> _HeatData;
			int _Count;

			sampler1D _HeatTex;
//
			half4 frag(vertOutput output) : COLOR {
				//fill color to surface
				
				// Loops over all the points
				half h = 0;

				for (int i = 0; i < _Count; i++)
				{
					// Calculates the contribution of each point
					half di = distance(output.worldPos, _HeatData[i].pos);
					half ri = _HeatData[i].rad;
					half hi = 1 - saturate(di / ri);

					h += hi * _HeatData[i].intensity;
				}

				// Converts (0-1) according to the heat texture
				h = saturate(h);
				half4 color = tex1D(_HeatTex, h);
				return color;
			}
			ENDCG
		}

	}
	Fallback "Diffuse"
}
			// bool SphereHit(float3 p, float3 center, float1 radius)
			// {
			// 	return distance(p, center) < radius;
			// };

			// float RaymarchHit(float3 position, float3 direction)
			// {
			// 	for(int i = 0; i< STEPS; i++)
			// 	{
			// 		if(SphereHit(position, float3(0,0,0), TEST_RADIUS*2))
			// 			return 1;
			// 		position += direction * STEP_SIZE;
			// 	}
			// 	return 0;
			// }