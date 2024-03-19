Shader "Heatmap/RayMarching"
{
    Properties
    {
        _DataTex ("Data Texture (Generated)", 3D) = "" {}
        _MaxHeat("Max Heat", Float) = 1.0
        _GradientTex("Gradient Texture", 2D) = "" {}
        _CutoffPercentage("Cutoff Percentage", Float) = 1.0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100
        Cull Front
        ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma multi_compile __ USE_SCENE_DEPTH

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"


            //Settings
            #define MAX_STEPS 512


            //Constants
            #define M_SQRT3 1.7320508075688772935274463415059
            #define FLT_MAX 3.402823466e+38


            //Properties
            sampler3D _DataTex;
            float _MaxHeat;
            sampler2D _GradientTex;
            float _CutoffPercentage;
            sampler2D _CameraDepthTexture;


            //Structs
            struct appdata
            {
                float4 vertex : POSITION;
            };


            struct v2f
            {
                float4 vertex : SV_POSITION; //needed by default
                float3 vertexPosOS : TEXCOORD1; //vertex position on object space
                float3 cameraPosOS : TEXCOORD2; //camera position in object space
                float4 vertexPosSS : TEXCOORD3; //vertex position on screen space
            };


            //space transformation

            //Transform from world space into object space.
            float3 TransformWorldToObject(float3 position)
            {
                return mul(unity_WorldToObject, float4(position, 1.0)).xyz;
            }


            //Transform from object space into view space.
            float3 TransformObjectToView(float3 position)
            {
                return mul(UNITY_MATRIX_MV, float4(position, 1.0)).xyz;
            }

            
            //Transform from view space into object space.
            float3 TransformViewToObject(float3 position)
            {
                return mul(transpose(UNITY_MATRIX_IT_MV), float4(position, 1.0)).xyz;
            }


            //Gets the heat at the specified position
            float getHeat(float3 pos)
            {
                pos += float3(0.5f, 0.5f, 0.5);
                return tex3Dlod(_DataTex, float4(pos, 0.0f));
            }


            //Get the colour from a 1D Gradient stored in a texture.
            float4 getGradientColour(float t)
            {
                return tex2Dlod(_GradientTex, float4(t, 0.0f, 0.0f, 0.0f));
            }


            //Find ray intersection points with axis aligned bounding box
            float2 intersectAABB(float3 rayOrigin, float3 rayDir, float3 boundsMin, float3 boundsMax)
            {
                float3 tMin = (boundsMin - rayOrigin) / rayDir;
                float3 tMax = (boundsMax - rayOrigin) / rayDir;
                float3 t1 = min(tMin, tMax);
                float3 t2 = max(tMin, tMax);
                float tNear = max(max(t1.x, t1.y), t1.z);
                float tFar = min(min(t2.x, t2.y), t2.z);
                return float2(tNear, tFar);
            };


            //vertex shader
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.vertexPosOS = v.vertex;
                o.cameraPosOS = TransformWorldToObject(_WorldSpaceCameraPos);
                o.vertexPosSS = ComputeScreenPos(o.vertex);

                return o;
            }


            //convert a depth value into object space
            float sceneDepthOS(float3 depthRay, float3 cameraPos, float depth)
            {
                depthRay = TransformObjectToView(depthRay + cameraPos);
                depthRay /= depthRay.z;
                depthRay *= depth;
                depthRay = TransformViewToObject(depthRay);
                depthRay -= cameraPos;
                return length(depthRay);
            }


            float raymarch(float3 rayOrigin, float3 rayDir, float maxDepth)
            {
                float2 intersects = intersectAABB(rayOrigin, rayDir, float3(-0.5f, -0.5f, -0.5f), float3(0.5f, 0.5f, 0.5));
                float rayStart = max(intersects.x, 0.0f); //start at intersect or camera
                float rayEnd = min(intersects.y, maxDepth); //end at intersect or maxDepth
                float stepSize = M_SQRT3 / MAX_STEPS; //worst case from corner to corner

                float maxHeat = 0.0f;
                for (float i = rayEnd; i >= rayStart; i -= stepSize)
                {
                    float heat = getHeat(rayOrigin + rayDir * i);
                    maxHeat = max(maxHeat, heat);
                }

                return maxHeat;
            }

            
            //fragment shader
            fixed4 frag(v2f i) : SV_Target
            {
                float3 cameraPos = i.cameraPosOS;
                float3 rayDir = normalize(i.vertexPosOS - cameraPos);

                float2 screenSpaceUV = i.vertexPosSS.xy / i.vertexPosSS.w;
                float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, screenSpaceUV);
                depth = LinearEyeDepth(depth);
#if USE_SCENE_DEPTH
                float sceneDepth = sceneDepthOS(rayDir, cameraPos, depth);
#else
                float sceneDepth = FLT_MAX;
#endif
                float maxHeat = raymarch(cameraPos, rayDir, sceneDepth); //sample the data texture
                maxHeat = maxHeat / (_MaxHeat * _CutoffPercentage); //normalize value
                fixed4 col = getGradientColour(maxHeat);
                return col;
            }

            ENDCG
        }
    }
}
