Shader "ARQUpgradeFresnel"
{
    Properties
    {
        Vector1_1B9A4EAC("Alpha_Slider", Range(-370, 370)) = 0
        [NoScaleOffset]Texture2D_555682C7("AlbedoMap", 2D) = "white" {}
        [HDR]Color_F148A98C("Albedo Color", Color) = (1, 1, 1, 0)
        [NoScaleOffset]Texture2D_1A1AD8DB("Emission Map", 2D) = "white" {}
        Vector1_6AE94978("Show Tits", Range(0, 1)) = 0
        [HDR]Color_E64E431("TitsColor", Color) = (128, 0, 0, 0)
        Vector1_8A376464("EmissionSlider", Range(-370, 370)) = 0
        [HDR]Color_6E507C6E("MainColorEmission", Color) = (1.518592, 1.518592, 1.518592, 0)
        [HDR]Color_AB865509("ExtraColor", Color) = (1.514916, 0, 0, 0)
        Vector1_9C827CB6("Fresnel Strength", Range(0, 15)) = 0
        Vector1_76F33557("origin", Range(-370, 370)) = -1
    }
        SubShader
        {
            Tags
            {
                "RenderPipeline" = "UniversalPipeline"
                "RenderType" = "Transparent"
                "Queue" = "Transparent+0"
            }

            Pass
            {
                Name "Universal Forward"
                Tags
                {
                    "LightMode" = "UniversalForward"
                }

            // Render State
            Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
            Cull Back
            ZTest LEqual
            ZWrite On
            // ColorMask: <None>


            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // Debug
            // <None>

            // --------------------------------------------------
            // Pass

            // Pragmas
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0
            #pragma multi_compile_fog
            #pragma multi_compile_instancing

            // Keywords
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS _ADDITIONAL_OFF
            #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE
            // GraphKeywords: <None>

            // Defines
            #define _SURFACE_TYPE_TRANSPARENT 1
            #define _AlphaClip 1
            #define _NORMAL_DROPOFF_TS 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_TEXCOORD1
            #define VARYINGS_NEED_POSITION_WS 
            #define VARYINGS_NEED_NORMAL_WS
            #define VARYINGS_NEED_TANGENT_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_VIEWDIRECTION_WS
            #define VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
            #define SHADERPASS_FORWARD

            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"

            // --------------------------------------------------
            // Graph

            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
            float Vector1_1B9A4EAC;
            float4 Color_F148A98C;
            float Vector1_6AE94978;
            float4 Color_E64E431;
            float Vector1_8A376464;
            float4 Color_6E507C6E;
            float4 Color_AB865509;
            float Vector1_9C827CB6;
            float Vector1_76F33557;
            CBUFFER_END
            TEXTURE2D(Texture2D_555682C7); SAMPLER(samplerTexture2D_555682C7); float4 Texture2D_555682C7_TexelSize;
            TEXTURE2D(Texture2D_1A1AD8DB); SAMPLER(samplerTexture2D_1A1AD8DB); float4 Texture2D_1A1AD8DB_TexelSize;
            SAMPLER(_SampleTexture2D_9FA979EA_Sampler_3_Linear_Repeat);
            SAMPLER(_SampleTexture2D_F47AD8CE_Sampler_3_Linear_Repeat);

            // Graph Functions

            void Unity_Multiply_float(float4 A, float4 B, out float4 Out)
            {
                Out = A * B;
            }

            void Unity_Subtract_float(float A, float B, out float Out)
            {
                Out = A - B;
            }

            void Unity_Clamp_float(float In, float Min, float Max, out float Out)
            {
                Out = clamp(In, Min, Max);
            }

            void Unity_Divide_float(float A, float B, out float Out)
            {
                Out = A / B;
            }

            void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
            {
                Out = lerp(A, B, T);
            }

            void Unity_OneMinus_float(float In, out float Out)
            {
                Out = 1 - In;
            }

            void Unity_Multiply_float(float3 A, float3 B, out float3 Out)
            {
                Out = A * B;
            }

            void Unity_Add_float3(float3 A, float3 B, out float3 Out)
            {
                Out = A + B;
            }

            void Unity_FresnelEffect_float(float3 Normal, float3 ViewDir, float Power, out float Out)
            {
                Out = pow((1.0 - saturate(dot(normalize(Normal), normalize(ViewDir)))), Power);
            }

            void Unity_OneMinus_float3(float3 In, out float3 Out)
            {
                Out = 1 - In;
            }

            void Unity_Subtract_float3(float3 A, float3 B, out float3 Out)
            {
                Out = A - B;
            }

            void Unity_Divide_float3(float3 A, float3 B, out float3 Out)
            {
                Out = A / B;
            }

            // Graph Vertex
            // GraphVertex: <None>

            // Graph Pixel
            struct SurfaceDescriptionInputs
            {
                float3 WorldSpaceNormal;
                float3 TangentSpaceNormal;
                float3 WorldSpaceViewDirection;
                float3 ObjectSpacePosition;
                float4 uv0;
            };

            struct SurfaceDescription
            {
                float3 Albedo;
                float3 Normal;
                float3 Emission;
                float Metallic;
                float Smoothness;
                float Occlusion;
                float Alpha;
                float AlphaClipThreshold;
            };

            SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
            {
                SurfaceDescription surface = (SurfaceDescription)0;
                float4 _Property_B95B9650_Out_0 = Color_F148A98C;
                float4 _SampleTexture2D_9FA979EA_RGBA_0 = SAMPLE_TEXTURE2D(Texture2D_555682C7, samplerTexture2D_555682C7, IN.uv0.xy);
                float _SampleTexture2D_9FA979EA_R_4 = _SampleTexture2D_9FA979EA_RGBA_0.r;
                float _SampleTexture2D_9FA979EA_G_5 = _SampleTexture2D_9FA979EA_RGBA_0.g;
                float _SampleTexture2D_9FA979EA_B_6 = _SampleTexture2D_9FA979EA_RGBA_0.b;
                float _SampleTexture2D_9FA979EA_A_7 = _SampleTexture2D_9FA979EA_RGBA_0.a;
                float4 _Multiply_FD9E0353_Out_2;
                Unity_Multiply_float(_Property_B95B9650_Out_0, _SampleTexture2D_9FA979EA_RGBA_0, _Multiply_FD9E0353_Out_2);
                float4 _Property_E10D3009_Out_0 = Color_6E507C6E;
                float4 _Property_286FC3A6_Out_0 = Color_AB865509;
                float _Split_B16B0BF5_R_1 = IN.ObjectSpacePosition[0];
                float _Split_B16B0BF5_G_2 = IN.ObjectSpacePosition[1];
                float _Split_B16B0BF5_B_3 = IN.ObjectSpacePosition[2];
                float _Split_B16B0BF5_A_4 = 0;
                float _Property_38AB02FB_Out_0 = Vector1_76F33557;
                float _Subtract_115C740E_Out_2;
                Unity_Subtract_float(_Split_B16B0BF5_R_1, _Property_38AB02FB_Out_0, _Subtract_115C740E_Out_2);
                float _Vector1_7A19215E_Out_0 = 0.1;
                float _Clamp_9AFFD795_Out_3;
                Unity_Clamp_float(_Vector1_7A19215E_Out_0, 0.1, 10, _Clamp_9AFFD795_Out_3);
                float _Divide_7F0B0F2E_Out_2;
                Unity_Divide_float(_Subtract_115C740E_Out_2, _Clamp_9AFFD795_Out_3, _Divide_7F0B0F2E_Out_2);
                float _Clamp_CAD83493_Out_3;
                Unity_Clamp_float(_Divide_7F0B0F2E_Out_2, 0, 1, _Clamp_CAD83493_Out_3);
                float4 _Lerp_77AD5732_Out_3;
                Unity_Lerp_float4(_Property_E10D3009_Out_0, _Property_286FC3A6_Out_0, (_Clamp_CAD83493_Out_3.xxxx), _Lerp_77AD5732_Out_3);
                float _Split_867A667F_R_1 = IN.ObjectSpacePosition[0];
                float _Split_867A667F_G_2 = IN.ObjectSpacePosition[1];
                float _Split_867A667F_B_3 = IN.ObjectSpacePosition[2];
                float _Split_867A667F_A_4 = 0;
                float _OneMinus_F032AE90_Out_1;
                Unity_OneMinus_float(_Split_867A667F_R_1, _OneMinus_F032AE90_Out_1);
                float _Property_ACB6CBA1_Out_0 = Vector1_8A376464;
                float _Subtract_17F0861D_Out_2;
                Unity_Subtract_float(_OneMinus_F032AE90_Out_1, _Property_ACB6CBA1_Out_0, _Subtract_17F0861D_Out_2);
                float _Vector1_330FBE3B_Out_0 = 0.1;
                float _Clamp_5EEF89D7_Out_3;
                Unity_Clamp_float(_Vector1_330FBE3B_Out_0, 0.1, 10, _Clamp_5EEF89D7_Out_3);
                float _Divide_704B9063_Out_2;
                Unity_Divide_float(_Subtract_17F0861D_Out_2, _Clamp_5EEF89D7_Out_3, _Divide_704B9063_Out_2);
                float _Clamp_8EAED4C3_Out_3;
                Unity_Clamp_float(_Divide_704B9063_Out_2, 0, 1, _Clamp_8EAED4C3_Out_3);
                float4 _Lerp_9A0705AB_Out_3;
                Unity_Lerp_float4(_Lerp_77AD5732_Out_3, float4(0, 0, 0, 0), (_Clamp_8EAED4C3_Out_3.xxxx), _Lerp_9A0705AB_Out_3);
                float4 _Property_B3BA4C68_Out_0 = Color_E64E431;
                float _Property_3A07ED09_Out_0 = Vector1_6AE94978;
                float4 _SampleTexture2D_F47AD8CE_RGBA_0 = SAMPLE_TEXTURE2D(Texture2D_1A1AD8DB, samplerTexture2D_1A1AD8DB, IN.uv0.xy);
                float _SampleTexture2D_F47AD8CE_R_4 = _SampleTexture2D_F47AD8CE_RGBA_0.r;
                float _SampleTexture2D_F47AD8CE_G_5 = _SampleTexture2D_F47AD8CE_RGBA_0.g;
                float _SampleTexture2D_F47AD8CE_B_6 = _SampleTexture2D_F47AD8CE_RGBA_0.b;
                float _SampleTexture2D_F47AD8CE_A_7 = _SampleTexture2D_F47AD8CE_RGBA_0.a;
                float3 _Vector3_17F9514B_Out_0 = float3(_SampleTexture2D_F47AD8CE_R_4, _SampleTexture2D_F47AD8CE_G_5, _SampleTexture2D_F47AD8CE_B_6);
                float3 _Multiply_FFB2F1C5_Out_2;
                Unity_Multiply_float((_Property_3A07ED09_Out_0.xxx), _Vector3_17F9514B_Out_0, _Multiply_FFB2F1C5_Out_2);
                float3 _Multiply_C3F0A823_Out_2;
                Unity_Multiply_float((_Property_B3BA4C68_Out_0.xyz), _Multiply_FFB2F1C5_Out_2, _Multiply_C3F0A823_Out_2);
                float3 _Add_7271F88C_Out_2;
                Unity_Add_float3((_Lerp_9A0705AB_Out_3.xyz), _Multiply_C3F0A823_Out_2, _Add_7271F88C_Out_2);
                float _Property_E27718A5_Out_0 = Vector1_9C827CB6;
                float _FresnelEffect_ECD927F2_Out_3;
                Unity_FresnelEffect_float(IN.WorldSpaceNormal, IN.WorldSpaceViewDirection, _Property_E27718A5_Out_0, _FresnelEffect_ECD927F2_Out_3);
                float3 _OneMinus_865BB92A_Out_1;
                Unity_OneMinus_float3(IN.ObjectSpacePosition, _OneMinus_865BB92A_Out_1);
                float _Property_7EF5AB61_Out_0 = Vector1_1B9A4EAC;
                float3 _Vector3_FCB8BA64_Out_0 = float3(_Property_7EF5AB61_Out_0, 0, 0);
                float3 _Subtract_C74B9C47_Out_2;
                Unity_Subtract_float3(_OneMinus_865BB92A_Out_1, _Vector3_FCB8BA64_Out_0, _Subtract_C74B9C47_Out_2);
                float3 _Vector3_FFBCB949_Out_0 = float3(0, 0, 0);
                float3 _Divide_B3205955_Out_2;
                Unity_Divide_float3(_Subtract_C74B9C47_Out_2, _Vector3_FFBCB949_Out_0, _Divide_B3205955_Out_2);
                surface.Albedo = (_Multiply_FD9E0353_Out_2.xyz);
                surface.Normal = IN.TangentSpaceNormal;
                surface.Emission = _Add_7271F88C_Out_2;
                surface.Metallic = 0;
                surface.Smoothness = 0.5;
                surface.Occlusion = 1;
                surface.Alpha = _FresnelEffect_ECD927F2_Out_3;
                surface.AlphaClipThreshold = (_Divide_B3205955_Out_2).x;
                return surface;
            }

            // --------------------------------------------------
            // Structs and Packing

            // Generated Type: Attributes
            struct Attributes
            {
                float3 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                float4 uv0 : TEXCOORD0;
                float4 uv1 : TEXCOORD1;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : INSTANCEID_SEMANTIC;
                #endif
            };

            // Generated Type: Varyings
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS;
                float3 normalWS;
                float4 tangentWS;
                float4 texCoord0;
                float3 viewDirectionWS;
                #if defined(LIGHTMAP_ON)
                float2 lightmapUV;
                #endif
                #if !defined(LIGHTMAP_ON)
                float3 sh;
                #endif
                float4 fogFactorAndVertexLight;
                float4 shadowCoord;
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                #endif
            };

            // Generated Type: PackedVaryings
            struct PackedVaryings
            {
                float4 positionCS : SV_POSITION;
                #if defined(LIGHTMAP_ON)
                #endif
                #if !defined(LIGHTMAP_ON)
                #endif
                #if UNITY_ANY_INSTANCING_ENABLED
                uint instanceID : CUSTOM_INSTANCE_ID;
                #endif
                float3 interp00 : TEXCOORD0;
                float3 interp01 : TEXCOORD1;
                float4 interp02 : TEXCOORD2;
                float4 interp03 : TEXCOORD3;
                float3 interp04 : TEXCOORD4;
                float2 interp05 : TEXCOORD5;
                float3 interp06 : TEXCOORD6;
                float4 interp07 : TEXCOORD7;
                float4 interp08 : TEXCOORD8;
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                #endif
            };

            // Packed Type: Varyings
            PackedVaryings PackVaryings(Varyings input)
            {
                PackedVaryings output = (PackedVaryings)0;
                output.positionCS = input.positionCS;
                output.interp00.xyz = input.positionWS;
                output.interp01.xyz = input.normalWS;
                output.interp02.xyzw = input.tangentWS;
                output.interp03.xyzw = input.texCoord0;
                output.interp04.xyz = input.viewDirectionWS;
                #if defined(LIGHTMAP_ON)
                output.interp05.xy = input.lightmapUV;
                #endif
                #if !defined(LIGHTMAP_ON)
                output.interp06.xyz = input.sh;
                #endif
                output.interp07.xyzw = input.fogFactorAndVertexLight;
                output.interp08.xyzw = input.shadowCoord;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif
                return output;
            }

            // Unpacked Type: Varyings
            Varyings UnpackVaryings(PackedVaryings input)
            {
                Varyings output = (Varyings)0;
                output.positionCS = input.positionCS;
                output.positionWS = input.interp00.xyz;
                output.normalWS = input.interp01.xyz;
                output.tangentWS = input.interp02.xyzw;
                output.texCoord0 = input.interp03.xyzw;
                output.viewDirectionWS = input.interp04.xyz;
                #if defined(LIGHTMAP_ON)
                output.lightmapUV = input.interp05.xy;
                #endif
                #if !defined(LIGHTMAP_ON)
                output.sh = input.interp06.xyz;
                #endif
                output.fogFactorAndVertexLight = input.interp07.xyzw;
                output.shadowCoord = input.interp08.xyzw;
                #if UNITY_ANY_INSTANCING_ENABLED
                output.instanceID = input.instanceID;
                #endif
                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                #endif
                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                #endif
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                output.cullFace = input.cullFace;
                #endif
                return output;
            }

            // --------------------------------------------------
            // Build Graph Inputs

            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
            {
                SurfaceDescriptionInputs output;
                ZERO_INITIALIZE(SurfaceDescriptionInputs, output);

                // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
                float3 unnormalizedNormalWS = input.normalWS;
                const float renormFactor = 1.0 / length(unnormalizedNormalWS);


                output.WorldSpaceNormal = renormFactor * input.normalWS.xyz;		// we want a unit length Normal Vector node in shader graph
                output.TangentSpaceNormal = float3(0.0f, 0.0f, 1.0f);


                output.WorldSpaceViewDirection = input.viewDirectionWS; //TODO: by default normalized in HD, but not in universal
                output.ObjectSpacePosition = TransformWorldToObject(input.positionWS);
                output.uv0 = input.texCoord0;
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
            #else
            #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
            #endif
            #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

                return output;
            }


            // --------------------------------------------------
            // Main

            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/PBRForwardPass.hlsl"

            ENDHLSL
        }

        Pass
        {
            Name "ShadowCaster"
            Tags
            {
                "LightMode" = "ShadowCaster"
            }

                // Render State
                Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
                Cull Back
                ZTest LEqual
                ZWrite On
                // ColorMask: <None>


                HLSLPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                // Debug
                // <None>

                // --------------------------------------------------
                // Pass

                // Pragmas
                #pragma prefer_hlslcc gles
                #pragma exclude_renderers d3d11_9x
                #pragma target 2.0
                #pragma multi_compile_instancing

                // Keywords
                // PassKeywords: <None>
                // GraphKeywords: <None>

                // Defines
                #define _SURFACE_TYPE_TRANSPARENT 1
                #define _AlphaClip 1
                #define _NORMAL_DROPOFF_TS 1
                #define ATTRIBUTES_NEED_NORMAL
                #define ATTRIBUTES_NEED_TANGENT
                #define VARYINGS_NEED_POSITION_WS 
                #define VARYINGS_NEED_NORMAL_WS
                #define VARYINGS_NEED_VIEWDIRECTION_WS
                #define SHADERPASS_SHADOWCASTER

                // Includes
                #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
                #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"

                // --------------------------------------------------
                // Graph

                // Graph Properties
                CBUFFER_START(UnityPerMaterial)
                float Vector1_1B9A4EAC;
                float4 Color_F148A98C;
                float Vector1_6AE94978;
                float4 Color_E64E431;
                float Vector1_8A376464;
                float4 Color_6E507C6E;
                float4 Color_AB865509;
                float Vector1_9C827CB6;
                float Vector1_76F33557;
                CBUFFER_END
                TEXTURE2D(Texture2D_555682C7); SAMPLER(samplerTexture2D_555682C7); float4 Texture2D_555682C7_TexelSize;
                TEXTURE2D(Texture2D_1A1AD8DB); SAMPLER(samplerTexture2D_1A1AD8DB); float4 Texture2D_1A1AD8DB_TexelSize;

                // Graph Functions

                void Unity_FresnelEffect_float(float3 Normal, float3 ViewDir, float Power, out float Out)
                {
                    Out = pow((1.0 - saturate(dot(normalize(Normal), normalize(ViewDir)))), Power);
                }

                void Unity_OneMinus_float3(float3 In, out float3 Out)
                {
                    Out = 1 - In;
                }

                void Unity_Subtract_float3(float3 A, float3 B, out float3 Out)
                {
                    Out = A - B;
                }

                void Unity_Divide_float3(float3 A, float3 B, out float3 Out)
                {
                    Out = A / B;
                }

                // Graph Vertex
                // GraphVertex: <None>

                // Graph Pixel
                struct SurfaceDescriptionInputs
                {
                    float3 WorldSpaceNormal;
                    float3 TangentSpaceNormal;
                    float3 WorldSpaceViewDirection;
                    float3 ObjectSpacePosition;
                };

                struct SurfaceDescription
                {
                    float Alpha;
                    float AlphaClipThreshold;
                };

                SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                {
                    SurfaceDescription surface = (SurfaceDescription)0;
                    float _Property_E27718A5_Out_0 = Vector1_9C827CB6;
                    float _FresnelEffect_ECD927F2_Out_3;
                    Unity_FresnelEffect_float(IN.WorldSpaceNormal, IN.WorldSpaceViewDirection, _Property_E27718A5_Out_0, _FresnelEffect_ECD927F2_Out_3);
                    float3 _OneMinus_865BB92A_Out_1;
                    Unity_OneMinus_float3(IN.ObjectSpacePosition, _OneMinus_865BB92A_Out_1);
                    float _Property_7EF5AB61_Out_0 = Vector1_1B9A4EAC;
                    float3 _Vector3_FCB8BA64_Out_0 = float3(_Property_7EF5AB61_Out_0, 0, 0);
                    float3 _Subtract_C74B9C47_Out_2;
                    Unity_Subtract_float3(_OneMinus_865BB92A_Out_1, _Vector3_FCB8BA64_Out_0, _Subtract_C74B9C47_Out_2);
                    float3 _Vector3_FFBCB949_Out_0 = float3(0, 0, 0);
                    float3 _Divide_B3205955_Out_2;
                    Unity_Divide_float3(_Subtract_C74B9C47_Out_2, _Vector3_FFBCB949_Out_0, _Divide_B3205955_Out_2);
                    surface.Alpha = _FresnelEffect_ECD927F2_Out_3;
                    surface.AlphaClipThreshold = (_Divide_B3205955_Out_2).x;
                    return surface;
                }

                // --------------------------------------------------
                // Structs and Packing

                // Generated Type: Attributes
                struct Attributes
                {
                    float3 positionOS : POSITION;
                    float3 normalOS : NORMAL;
                    float4 tangentOS : TANGENT;
                    #if UNITY_ANY_INSTANCING_ENABLED
                    uint instanceID : INSTANCEID_SEMANTIC;
                    #endif
                };

                // Generated Type: Varyings
                struct Varyings
                {
                    float4 positionCS : SV_POSITION;
                    float3 positionWS;
                    float3 normalWS;
                    float3 viewDirectionWS;
                    #if UNITY_ANY_INSTANCING_ENABLED
                    uint instanceID : CUSTOM_INSTANCE_ID;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                    uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                    uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                    #endif
                };

                // Generated Type: PackedVaryings
                struct PackedVaryings
                {
                    float4 positionCS : SV_POSITION;
                    #if UNITY_ANY_INSTANCING_ENABLED
                    uint instanceID : CUSTOM_INSTANCE_ID;
                    #endif
                    float3 interp00 : TEXCOORD0;
                    float3 interp01 : TEXCOORD1;
                    float3 interp02 : TEXCOORD2;
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                    uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                    uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                    #endif
                };

                // Packed Type: Varyings
                PackedVaryings PackVaryings(Varyings input)
                {
                    PackedVaryings output = (PackedVaryings)0;
                    output.positionCS = input.positionCS;
                    output.interp00.xyz = input.positionWS;
                    output.interp01.xyz = input.normalWS;
                    output.interp02.xyz = input.viewDirectionWS;
                    #if UNITY_ANY_INSTANCING_ENABLED
                    output.instanceID = input.instanceID;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                    output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                    output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    output.cullFace = input.cullFace;
                    #endif
                    return output;
                }

                // Unpacked Type: Varyings
                Varyings UnpackVaryings(PackedVaryings input)
                {
                    Varyings output = (Varyings)0;
                    output.positionCS = input.positionCS;
                    output.positionWS = input.interp00.xyz;
                    output.normalWS = input.interp01.xyz;
                    output.viewDirectionWS = input.interp02.xyz;
                    #if UNITY_ANY_INSTANCING_ENABLED
                    output.instanceID = input.instanceID;
                    #endif
                    #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                    output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                    #endif
                    #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                    output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                    #endif
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    output.cullFace = input.cullFace;
                    #endif
                    return output;
                }

                // --------------------------------------------------
                // Build Graph Inputs

                SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
                {
                    SurfaceDescriptionInputs output;
                    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);

                    // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
                    float3 unnormalizedNormalWS = input.normalWS;
                    const float renormFactor = 1.0 / length(unnormalizedNormalWS);


                    output.WorldSpaceNormal = renormFactor * input.normalWS.xyz;		// we want a unit length Normal Vector node in shader graph
                    output.TangentSpaceNormal = float3(0.0f, 0.0f, 1.0f);


                    output.WorldSpaceViewDirection = input.viewDirectionWS; //TODO: by default normalized in HD, but not in universal
                    output.ObjectSpacePosition = TransformWorldToObject(input.positionWS);
                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
                #else
                #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
                #endif
                #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

                    return output;
                }


                // --------------------------------------------------
                // Main

                #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
                #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShadowCasterPass.hlsl"

                ENDHLSL
            }

            Pass
            {
                Name "DepthOnly"
                Tags
                {
                    "LightMode" = "DepthOnly"
                }

                    // Render State
                    Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
                    Cull Back
                    ZTest LEqual
                    ZWrite On
                    ColorMask 0


                    HLSLPROGRAM
                    #pragma vertex vert
                    #pragma fragment frag

                    // Debug
                    // <None>

                    // --------------------------------------------------
                    // Pass

                    // Pragmas
                    #pragma prefer_hlslcc gles
                    #pragma exclude_renderers d3d11_9x
                    #pragma target 2.0
                    #pragma multi_compile_instancing

                    // Keywords
                    // PassKeywords: <None>
                    // GraphKeywords: <None>

                    // Defines
                    #define _SURFACE_TYPE_TRANSPARENT 1
                    #define _AlphaClip 1
                    #define _NORMAL_DROPOFF_TS 1
                    #define ATTRIBUTES_NEED_NORMAL
                    #define ATTRIBUTES_NEED_TANGENT
                    #define VARYINGS_NEED_POSITION_WS 
                    #define VARYINGS_NEED_NORMAL_WS
                    #define VARYINGS_NEED_VIEWDIRECTION_WS
                    #define SHADERPASS_DEPTHONLY

                    // Includes
                    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
                    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
                    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
                    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
                    #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"

                    // --------------------------------------------------
                    // Graph

                    // Graph Properties
                    CBUFFER_START(UnityPerMaterial)
                    float Vector1_1B9A4EAC;
                    float4 Color_F148A98C;
                    float Vector1_6AE94978;
                    float4 Color_E64E431;
                    float Vector1_8A376464;
                    float4 Color_6E507C6E;
                    float4 Color_AB865509;
                    float Vector1_9C827CB6;
                    float Vector1_76F33557;
                    CBUFFER_END
                    TEXTURE2D(Texture2D_555682C7); SAMPLER(samplerTexture2D_555682C7); float4 Texture2D_555682C7_TexelSize;
                    TEXTURE2D(Texture2D_1A1AD8DB); SAMPLER(samplerTexture2D_1A1AD8DB); float4 Texture2D_1A1AD8DB_TexelSize;

                    // Graph Functions

                    void Unity_FresnelEffect_float(float3 Normal, float3 ViewDir, float Power, out float Out)
                    {
                        Out = pow((1.0 - saturate(dot(normalize(Normal), normalize(ViewDir)))), Power);
                    }

                    void Unity_OneMinus_float3(float3 In, out float3 Out)
                    {
                        Out = 1 - In;
                    }

                    void Unity_Subtract_float3(float3 A, float3 B, out float3 Out)
                    {
                        Out = A - B;
                    }

                    void Unity_Divide_float3(float3 A, float3 B, out float3 Out)
                    {
                        Out = A / B;
                    }

                    // Graph Vertex
                    // GraphVertex: <None>

                    // Graph Pixel
                    struct SurfaceDescriptionInputs
                    {
                        float3 WorldSpaceNormal;
                        float3 TangentSpaceNormal;
                        float3 WorldSpaceViewDirection;
                        float3 ObjectSpacePosition;
                    };

                    struct SurfaceDescription
                    {
                        float Alpha;
                        float AlphaClipThreshold;
                    };

                    SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                    {
                        SurfaceDescription surface = (SurfaceDescription)0;
                        float _Property_E27718A5_Out_0 = Vector1_9C827CB6;
                        float _FresnelEffect_ECD927F2_Out_3;
                        Unity_FresnelEffect_float(IN.WorldSpaceNormal, IN.WorldSpaceViewDirection, _Property_E27718A5_Out_0, _FresnelEffect_ECD927F2_Out_3);
                        float3 _OneMinus_865BB92A_Out_1;
                        Unity_OneMinus_float3(IN.ObjectSpacePosition, _OneMinus_865BB92A_Out_1);
                        float _Property_7EF5AB61_Out_0 = Vector1_1B9A4EAC;
                        float3 _Vector3_FCB8BA64_Out_0 = float3(_Property_7EF5AB61_Out_0, 0, 0);
                        float3 _Subtract_C74B9C47_Out_2;
                        Unity_Subtract_float3(_OneMinus_865BB92A_Out_1, _Vector3_FCB8BA64_Out_0, _Subtract_C74B9C47_Out_2);
                        float3 _Vector3_FFBCB949_Out_0 = float3(0, 0, 0);
                        float3 _Divide_B3205955_Out_2;
                        Unity_Divide_float3(_Subtract_C74B9C47_Out_2, _Vector3_FFBCB949_Out_0, _Divide_B3205955_Out_2);
                        surface.Alpha = _FresnelEffect_ECD927F2_Out_3;
                        surface.AlphaClipThreshold = (_Divide_B3205955_Out_2).x;
                        return surface;
                    }

                    // --------------------------------------------------
                    // Structs and Packing

                    // Generated Type: Attributes
                    struct Attributes
                    {
                        float3 positionOS : POSITION;
                        float3 normalOS : NORMAL;
                        float4 tangentOS : TANGENT;
                        #if UNITY_ANY_INSTANCING_ENABLED
                        uint instanceID : INSTANCEID_SEMANTIC;
                        #endif
                    };

                    // Generated Type: Varyings
                    struct Varyings
                    {
                        float4 positionCS : SV_POSITION;
                        float3 positionWS;
                        float3 normalWS;
                        float3 viewDirectionWS;
                        #if UNITY_ANY_INSTANCING_ENABLED
                        uint instanceID : CUSTOM_INSTANCE_ID;
                        #endif
                        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                        uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                        #endif
                        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                        uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                        #endif
                        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                        FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                        #endif
                    };

                    // Generated Type: PackedVaryings
                    struct PackedVaryings
                    {
                        float4 positionCS : SV_POSITION;
                        #if UNITY_ANY_INSTANCING_ENABLED
                        uint instanceID : CUSTOM_INSTANCE_ID;
                        #endif
                        float3 interp00 : TEXCOORD0;
                        float3 interp01 : TEXCOORD1;
                        float3 interp02 : TEXCOORD2;
                        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                        uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                        #endif
                        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                        uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                        #endif
                        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                        FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                        #endif
                    };

                    // Packed Type: Varyings
                    PackedVaryings PackVaryings(Varyings input)
                    {
                        PackedVaryings output = (PackedVaryings)0;
                        output.positionCS = input.positionCS;
                        output.interp00.xyz = input.positionWS;
                        output.interp01.xyz = input.normalWS;
                        output.interp02.xyz = input.viewDirectionWS;
                        #if UNITY_ANY_INSTANCING_ENABLED
                        output.instanceID = input.instanceID;
                        #endif
                        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                        output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                        #endif
                        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                        output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                        #endif
                        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                        output.cullFace = input.cullFace;
                        #endif
                        return output;
                    }

                    // Unpacked Type: Varyings
                    Varyings UnpackVaryings(PackedVaryings input)
                    {
                        Varyings output = (Varyings)0;
                        output.positionCS = input.positionCS;
                        output.positionWS = input.interp00.xyz;
                        output.normalWS = input.interp01.xyz;
                        output.viewDirectionWS = input.interp02.xyz;
                        #if UNITY_ANY_INSTANCING_ENABLED
                        output.instanceID = input.instanceID;
                        #endif
                        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                        output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                        #endif
                        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                        output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                        #endif
                        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                        output.cullFace = input.cullFace;
                        #endif
                        return output;
                    }

                    // --------------------------------------------------
                    // Build Graph Inputs

                    SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
                    {
                        SurfaceDescriptionInputs output;
                        ZERO_INITIALIZE(SurfaceDescriptionInputs, output);

                        // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
                        float3 unnormalizedNormalWS = input.normalWS;
                        const float renormFactor = 1.0 / length(unnormalizedNormalWS);


                        output.WorldSpaceNormal = renormFactor * input.normalWS.xyz;		// we want a unit length Normal Vector node in shader graph
                        output.TangentSpaceNormal = float3(0.0f, 0.0f, 1.0f);


                        output.WorldSpaceViewDirection = input.viewDirectionWS; //TODO: by default normalized in HD, but not in universal
                        output.ObjectSpacePosition = TransformWorldToObject(input.positionWS);
                    #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                    #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
                    #else
                    #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
                    #endif
                    #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

                        return output;
                    }


                    // --------------------------------------------------
                    // Main

                    #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
                    #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/DepthOnlyPass.hlsl"

                    ENDHLSL
                }

                Pass
                {
                    Name "Meta"
                    Tags
                    {
                        "LightMode" = "Meta"
                    }

                        // Render State
                        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
                        Cull Back
                        ZTest LEqual
                        ZWrite On
                        // ColorMask: <None>


                        HLSLPROGRAM
                        #pragma vertex vert
                        #pragma fragment frag

                        // Debug
                        // <None>

                        // --------------------------------------------------
                        // Pass

                        // Pragmas
                        #pragma prefer_hlslcc gles
                        #pragma exclude_renderers d3d11_9x
                        #pragma target 2.0

                        // Keywords
                        #pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
                        // GraphKeywords: <None>

                        // Defines
                        #define _SURFACE_TYPE_TRANSPARENT 1
                        #define _AlphaClip 1
                        #define _NORMAL_DROPOFF_TS 1
                        #define ATTRIBUTES_NEED_NORMAL
                        #define ATTRIBUTES_NEED_TANGENT
                        #define ATTRIBUTES_NEED_TEXCOORD0
                        #define ATTRIBUTES_NEED_TEXCOORD1
                        #define ATTRIBUTES_NEED_TEXCOORD2
                        #define VARYINGS_NEED_POSITION_WS 
                        #define VARYINGS_NEED_NORMAL_WS
                        #define VARYINGS_NEED_TEXCOORD0
                        #define VARYINGS_NEED_VIEWDIRECTION_WS
                        #define SHADERPASS_META

                        // Includes
                        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
                        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
                        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
                        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
                        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/MetaInput.hlsl"
                        #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"

                        // --------------------------------------------------
                        // Graph

                        // Graph Properties
                        CBUFFER_START(UnityPerMaterial)
                        float Vector1_1B9A4EAC;
                        float4 Color_F148A98C;
                        float Vector1_6AE94978;
                        float4 Color_E64E431;
                        float Vector1_8A376464;
                        float4 Color_6E507C6E;
                        float4 Color_AB865509;
                        float Vector1_9C827CB6;
                        float Vector1_76F33557;
                        CBUFFER_END
                        TEXTURE2D(Texture2D_555682C7); SAMPLER(samplerTexture2D_555682C7); float4 Texture2D_555682C7_TexelSize;
                        TEXTURE2D(Texture2D_1A1AD8DB); SAMPLER(samplerTexture2D_1A1AD8DB); float4 Texture2D_1A1AD8DB_TexelSize;
                        SAMPLER(_SampleTexture2D_9FA979EA_Sampler_3_Linear_Repeat);
                        SAMPLER(_SampleTexture2D_F47AD8CE_Sampler_3_Linear_Repeat);

                        // Graph Functions

                        void Unity_Multiply_float(float4 A, float4 B, out float4 Out)
                        {
                            Out = A * B;
                        }

                        void Unity_Subtract_float(float A, float B, out float Out)
                        {
                            Out = A - B;
                        }

                        void Unity_Clamp_float(float In, float Min, float Max, out float Out)
                        {
                            Out = clamp(In, Min, Max);
                        }

                        void Unity_Divide_float(float A, float B, out float Out)
                        {
                            Out = A / B;
                        }

                        void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
                        {
                            Out = lerp(A, B, T);
                        }

                        void Unity_OneMinus_float(float In, out float Out)
                        {
                            Out = 1 - In;
                        }

                        void Unity_Multiply_float(float3 A, float3 B, out float3 Out)
                        {
                            Out = A * B;
                        }

                        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
                        {
                            Out = A + B;
                        }

                        void Unity_FresnelEffect_float(float3 Normal, float3 ViewDir, float Power, out float Out)
                        {
                            Out = pow((1.0 - saturate(dot(normalize(Normal), normalize(ViewDir)))), Power);
                        }

                        void Unity_OneMinus_float3(float3 In, out float3 Out)
                        {
                            Out = 1 - In;
                        }

                        void Unity_Subtract_float3(float3 A, float3 B, out float3 Out)
                        {
                            Out = A - B;
                        }

                        void Unity_Divide_float3(float3 A, float3 B, out float3 Out)
                        {
                            Out = A / B;
                        }

                        // Graph Vertex
                        // GraphVertex: <None>

                        // Graph Pixel
                        struct SurfaceDescriptionInputs
                        {
                            float3 WorldSpaceNormal;
                            float3 TangentSpaceNormal;
                            float3 WorldSpaceViewDirection;
                            float3 ObjectSpacePosition;
                            float4 uv0;
                        };

                        struct SurfaceDescription
                        {
                            float3 Albedo;
                            float3 Emission;
                            float Alpha;
                            float AlphaClipThreshold;
                        };

                        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                        {
                            SurfaceDescription surface = (SurfaceDescription)0;
                            float4 _Property_B95B9650_Out_0 = Color_F148A98C;
                            float4 _SampleTexture2D_9FA979EA_RGBA_0 = SAMPLE_TEXTURE2D(Texture2D_555682C7, samplerTexture2D_555682C7, IN.uv0.xy);
                            float _SampleTexture2D_9FA979EA_R_4 = _SampleTexture2D_9FA979EA_RGBA_0.r;
                            float _SampleTexture2D_9FA979EA_G_5 = _SampleTexture2D_9FA979EA_RGBA_0.g;
                            float _SampleTexture2D_9FA979EA_B_6 = _SampleTexture2D_9FA979EA_RGBA_0.b;
                            float _SampleTexture2D_9FA979EA_A_7 = _SampleTexture2D_9FA979EA_RGBA_0.a;
                            float4 _Multiply_FD9E0353_Out_2;
                            Unity_Multiply_float(_Property_B95B9650_Out_0, _SampleTexture2D_9FA979EA_RGBA_0, _Multiply_FD9E0353_Out_2);
                            float4 _Property_E10D3009_Out_0 = Color_6E507C6E;
                            float4 _Property_286FC3A6_Out_0 = Color_AB865509;
                            float _Split_B16B0BF5_R_1 = IN.ObjectSpacePosition[0];
                            float _Split_B16B0BF5_G_2 = IN.ObjectSpacePosition[1];
                            float _Split_B16B0BF5_B_3 = IN.ObjectSpacePosition[2];
                            float _Split_B16B0BF5_A_4 = 0;
                            float _Property_38AB02FB_Out_0 = Vector1_76F33557;
                            float _Subtract_115C740E_Out_2;
                            Unity_Subtract_float(_Split_B16B0BF5_R_1, _Property_38AB02FB_Out_0, _Subtract_115C740E_Out_2);
                            float _Vector1_7A19215E_Out_0 = 0.1;
                            float _Clamp_9AFFD795_Out_3;
                            Unity_Clamp_float(_Vector1_7A19215E_Out_0, 0.1, 10, _Clamp_9AFFD795_Out_3);
                            float _Divide_7F0B0F2E_Out_2;
                            Unity_Divide_float(_Subtract_115C740E_Out_2, _Clamp_9AFFD795_Out_3, _Divide_7F0B0F2E_Out_2);
                            float _Clamp_CAD83493_Out_3;
                            Unity_Clamp_float(_Divide_7F0B0F2E_Out_2, 0, 1, _Clamp_CAD83493_Out_3);
                            float4 _Lerp_77AD5732_Out_3;
                            Unity_Lerp_float4(_Property_E10D3009_Out_0, _Property_286FC3A6_Out_0, (_Clamp_CAD83493_Out_3.xxxx), _Lerp_77AD5732_Out_3);
                            float _Split_867A667F_R_1 = IN.ObjectSpacePosition[0];
                            float _Split_867A667F_G_2 = IN.ObjectSpacePosition[1];
                            float _Split_867A667F_B_3 = IN.ObjectSpacePosition[2];
                            float _Split_867A667F_A_4 = 0;
                            float _OneMinus_F032AE90_Out_1;
                            Unity_OneMinus_float(_Split_867A667F_R_1, _OneMinus_F032AE90_Out_1);
                            float _Property_ACB6CBA1_Out_0 = Vector1_8A376464;
                            float _Subtract_17F0861D_Out_2;
                            Unity_Subtract_float(_OneMinus_F032AE90_Out_1, _Property_ACB6CBA1_Out_0, _Subtract_17F0861D_Out_2);
                            float _Vector1_330FBE3B_Out_0 = 0.1;
                            float _Clamp_5EEF89D7_Out_3;
                            Unity_Clamp_float(_Vector1_330FBE3B_Out_0, 0.1, 10, _Clamp_5EEF89D7_Out_3);
                            float _Divide_704B9063_Out_2;
                            Unity_Divide_float(_Subtract_17F0861D_Out_2, _Clamp_5EEF89D7_Out_3, _Divide_704B9063_Out_2);
                            float _Clamp_8EAED4C3_Out_3;
                            Unity_Clamp_float(_Divide_704B9063_Out_2, 0, 1, _Clamp_8EAED4C3_Out_3);
                            float4 _Lerp_9A0705AB_Out_3;
                            Unity_Lerp_float4(_Lerp_77AD5732_Out_3, float4(0, 0, 0, 0), (_Clamp_8EAED4C3_Out_3.xxxx), _Lerp_9A0705AB_Out_3);
                            float4 _Property_B3BA4C68_Out_0 = Color_E64E431;
                            float _Property_3A07ED09_Out_0 = Vector1_6AE94978;
                            float4 _SampleTexture2D_F47AD8CE_RGBA_0 = SAMPLE_TEXTURE2D(Texture2D_1A1AD8DB, samplerTexture2D_1A1AD8DB, IN.uv0.xy);
                            float _SampleTexture2D_F47AD8CE_R_4 = _SampleTexture2D_F47AD8CE_RGBA_0.r;
                            float _SampleTexture2D_F47AD8CE_G_5 = _SampleTexture2D_F47AD8CE_RGBA_0.g;
                            float _SampleTexture2D_F47AD8CE_B_6 = _SampleTexture2D_F47AD8CE_RGBA_0.b;
                            float _SampleTexture2D_F47AD8CE_A_7 = _SampleTexture2D_F47AD8CE_RGBA_0.a;
                            float3 _Vector3_17F9514B_Out_0 = float3(_SampleTexture2D_F47AD8CE_R_4, _SampleTexture2D_F47AD8CE_G_5, _SampleTexture2D_F47AD8CE_B_6);
                            float3 _Multiply_FFB2F1C5_Out_2;
                            Unity_Multiply_float((_Property_3A07ED09_Out_0.xxx), _Vector3_17F9514B_Out_0, _Multiply_FFB2F1C5_Out_2);
                            float3 _Multiply_C3F0A823_Out_2;
                            Unity_Multiply_float((_Property_B3BA4C68_Out_0.xyz), _Multiply_FFB2F1C5_Out_2, _Multiply_C3F0A823_Out_2);
                            float3 _Add_7271F88C_Out_2;
                            Unity_Add_float3((_Lerp_9A0705AB_Out_3.xyz), _Multiply_C3F0A823_Out_2, _Add_7271F88C_Out_2);
                            float _Property_E27718A5_Out_0 = Vector1_9C827CB6;
                            float _FresnelEffect_ECD927F2_Out_3;
                            Unity_FresnelEffect_float(IN.WorldSpaceNormal, IN.WorldSpaceViewDirection, _Property_E27718A5_Out_0, _FresnelEffect_ECD927F2_Out_3);
                            float3 _OneMinus_865BB92A_Out_1;
                            Unity_OneMinus_float3(IN.ObjectSpacePosition, _OneMinus_865BB92A_Out_1);
                            float _Property_7EF5AB61_Out_0 = Vector1_1B9A4EAC;
                            float3 _Vector3_FCB8BA64_Out_0 = float3(_Property_7EF5AB61_Out_0, 0, 0);
                            float3 _Subtract_C74B9C47_Out_2;
                            Unity_Subtract_float3(_OneMinus_865BB92A_Out_1, _Vector3_FCB8BA64_Out_0, _Subtract_C74B9C47_Out_2);
                            float3 _Vector3_FFBCB949_Out_0 = float3(0, 0, 0);
                            float3 _Divide_B3205955_Out_2;
                            Unity_Divide_float3(_Subtract_C74B9C47_Out_2, _Vector3_FFBCB949_Out_0, _Divide_B3205955_Out_2);
                            surface.Albedo = (_Multiply_FD9E0353_Out_2.xyz);
                            surface.Emission = _Add_7271F88C_Out_2;
                            surface.Alpha = _FresnelEffect_ECD927F2_Out_3;
                            surface.AlphaClipThreshold = (_Divide_B3205955_Out_2).x;
                            return surface;
                        }

                        // --------------------------------------------------
                        // Structs and Packing

                        // Generated Type: Attributes
                        struct Attributes
                        {
                            float3 positionOS : POSITION;
                            float3 normalOS : NORMAL;
                            float4 tangentOS : TANGENT;
                            float4 uv0 : TEXCOORD0;
                            float4 uv1 : TEXCOORD1;
                            float4 uv2 : TEXCOORD2;
                            #if UNITY_ANY_INSTANCING_ENABLED
                            uint instanceID : INSTANCEID_SEMANTIC;
                            #endif
                        };

                        // Generated Type: Varyings
                        struct Varyings
                        {
                            float4 positionCS : SV_POSITION;
                            float3 positionWS;
                            float3 normalWS;
                            float4 texCoord0;
                            float3 viewDirectionWS;
                            #if UNITY_ANY_INSTANCING_ENABLED
                            uint instanceID : CUSTOM_INSTANCE_ID;
                            #endif
                            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                            #endif
                            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                            #endif
                            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                            #endif
                        };

                        // Generated Type: PackedVaryings
                        struct PackedVaryings
                        {
                            float4 positionCS : SV_POSITION;
                            #if UNITY_ANY_INSTANCING_ENABLED
                            uint instanceID : CUSTOM_INSTANCE_ID;
                            #endif
                            float3 interp00 : TEXCOORD0;
                            float3 interp01 : TEXCOORD1;
                            float4 interp02 : TEXCOORD2;
                            float3 interp03 : TEXCOORD3;
                            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                            #endif
                            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                            #endif
                            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                            #endif
                        };

                        // Packed Type: Varyings
                        PackedVaryings PackVaryings(Varyings input)
                        {
                            PackedVaryings output = (PackedVaryings)0;
                            output.positionCS = input.positionCS;
                            output.interp00.xyz = input.positionWS;
                            output.interp01.xyz = input.normalWS;
                            output.interp02.xyzw = input.texCoord0;
                            output.interp03.xyz = input.viewDirectionWS;
                            #if UNITY_ANY_INSTANCING_ENABLED
                            output.instanceID = input.instanceID;
                            #endif
                            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                            #endif
                            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                            #endif
                            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                            output.cullFace = input.cullFace;
                            #endif
                            return output;
                        }

                        // Unpacked Type: Varyings
                        Varyings UnpackVaryings(PackedVaryings input)
                        {
                            Varyings output = (Varyings)0;
                            output.positionCS = input.positionCS;
                            output.positionWS = input.interp00.xyz;
                            output.normalWS = input.interp01.xyz;
                            output.texCoord0 = input.interp02.xyzw;
                            output.viewDirectionWS = input.interp03.xyz;
                            #if UNITY_ANY_INSTANCING_ENABLED
                            output.instanceID = input.instanceID;
                            #endif
                            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                            #endif
                            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                            #endif
                            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                            output.cullFace = input.cullFace;
                            #endif
                            return output;
                        }

                        // --------------------------------------------------
                        // Build Graph Inputs

                        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
                        {
                            SurfaceDescriptionInputs output;
                            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);

                            // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
                            float3 unnormalizedNormalWS = input.normalWS;
                            const float renormFactor = 1.0 / length(unnormalizedNormalWS);


                            output.WorldSpaceNormal = renormFactor * input.normalWS.xyz;		// we want a unit length Normal Vector node in shader graph
                            output.TangentSpaceNormal = float3(0.0f, 0.0f, 1.0f);


                            output.WorldSpaceViewDirection = input.viewDirectionWS; //TODO: by default normalized in HD, but not in universal
                            output.ObjectSpacePosition = TransformWorldToObject(input.positionWS);
                            output.uv0 = input.texCoord0;
                        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
                        #else
                        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
                        #endif
                        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

                            return output;
                        }


                        // --------------------------------------------------
                        // Main

                        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
                        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/LightingMetaPass.hlsl"

                        ENDHLSL
                    }

                    Pass
                    {
                            // Name: <None>
                            Tags
                            {
                                "LightMode" = "Universal2D"
                            }

                            // Render State
                            Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
                            Cull Back
                            ZTest LEqual
                            ZWrite Off
                            // ColorMask: <None>


                            HLSLPROGRAM
                            #pragma vertex vert
                            #pragma fragment frag

                            // Debug
                            // <None>

                            // --------------------------------------------------
                            // Pass

                            // Pragmas
                            #pragma prefer_hlslcc gles
                            #pragma exclude_renderers d3d11_9x
                            #pragma target 2.0
                            #pragma multi_compile_instancing

                            // Keywords
                            // PassKeywords: <None>
                            // GraphKeywords: <None>

                            // Defines
                            #define _SURFACE_TYPE_TRANSPARENT 1
                            #define _AlphaClip 1
                            #define _NORMAL_DROPOFF_TS 1
                            #define ATTRIBUTES_NEED_NORMAL
                            #define ATTRIBUTES_NEED_TANGENT
                            #define ATTRIBUTES_NEED_TEXCOORD0
                            #define VARYINGS_NEED_POSITION_WS 
                            #define VARYINGS_NEED_NORMAL_WS
                            #define VARYINGS_NEED_TEXCOORD0
                            #define VARYINGS_NEED_VIEWDIRECTION_WS
                            #define SHADERPASS_2D

                            // Includes
                            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
                            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
                            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
                            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
                            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"

                            // --------------------------------------------------
                            // Graph

                            // Graph Properties
                            CBUFFER_START(UnityPerMaterial)
                            float Vector1_1B9A4EAC;
                            float4 Color_F148A98C;
                            float Vector1_6AE94978;
                            float4 Color_E64E431;
                            float Vector1_8A376464;
                            float4 Color_6E507C6E;
                            float4 Color_AB865509;
                            float Vector1_9C827CB6;
                            float Vector1_76F33557;
                            CBUFFER_END
                            TEXTURE2D(Texture2D_555682C7); SAMPLER(samplerTexture2D_555682C7); float4 Texture2D_555682C7_TexelSize;
                            TEXTURE2D(Texture2D_1A1AD8DB); SAMPLER(samplerTexture2D_1A1AD8DB); float4 Texture2D_1A1AD8DB_TexelSize;
                            SAMPLER(_SampleTexture2D_9FA979EA_Sampler_3_Linear_Repeat);

                            // Graph Functions

                            void Unity_Multiply_float(float4 A, float4 B, out float4 Out)
                            {
                                Out = A * B;
                            }

                            void Unity_FresnelEffect_float(float3 Normal, float3 ViewDir, float Power, out float Out)
                            {
                                Out = pow((1.0 - saturate(dot(normalize(Normal), normalize(ViewDir)))), Power);
                            }

                            void Unity_OneMinus_float3(float3 In, out float3 Out)
                            {
                                Out = 1 - In;
                            }

                            void Unity_Subtract_float3(float3 A, float3 B, out float3 Out)
                            {
                                Out = A - B;
                            }

                            void Unity_Divide_float3(float3 A, float3 B, out float3 Out)
                            {
                                Out = A / B;
                            }

                            // Graph Vertex
                            // GraphVertex: <None>

                            // Graph Pixel
                            struct SurfaceDescriptionInputs
                            {
                                float3 WorldSpaceNormal;
                                float3 TangentSpaceNormal;
                                float3 WorldSpaceViewDirection;
                                float3 ObjectSpacePosition;
                                float4 uv0;
                            };

                            struct SurfaceDescription
                            {
                                float3 Albedo;
                                float Alpha;
                                float AlphaClipThreshold;
                            };

                            SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                            {
                                SurfaceDescription surface = (SurfaceDescription)0;
                                float4 _Property_B95B9650_Out_0 = Color_F148A98C;
                                float4 _SampleTexture2D_9FA979EA_RGBA_0 = SAMPLE_TEXTURE2D(Texture2D_555682C7, samplerTexture2D_555682C7, IN.uv0.xy);
                                float _SampleTexture2D_9FA979EA_R_4 = _SampleTexture2D_9FA979EA_RGBA_0.r;
                                float _SampleTexture2D_9FA979EA_G_5 = _SampleTexture2D_9FA979EA_RGBA_0.g;
                                float _SampleTexture2D_9FA979EA_B_6 = _SampleTexture2D_9FA979EA_RGBA_0.b;
                                float _SampleTexture2D_9FA979EA_A_7 = _SampleTexture2D_9FA979EA_RGBA_0.a;
                                float4 _Multiply_FD9E0353_Out_2;
                                Unity_Multiply_float(_Property_B95B9650_Out_0, _SampleTexture2D_9FA979EA_RGBA_0, _Multiply_FD9E0353_Out_2);
                                float _Property_E27718A5_Out_0 = Vector1_9C827CB6;
                                float _FresnelEffect_ECD927F2_Out_3;
                                Unity_FresnelEffect_float(IN.WorldSpaceNormal, IN.WorldSpaceViewDirection, _Property_E27718A5_Out_0, _FresnelEffect_ECD927F2_Out_3);
                                float3 _OneMinus_865BB92A_Out_1;
                                Unity_OneMinus_float3(IN.ObjectSpacePosition, _OneMinus_865BB92A_Out_1);
                                float _Property_7EF5AB61_Out_0 = Vector1_1B9A4EAC;
                                float3 _Vector3_FCB8BA64_Out_0 = float3(_Property_7EF5AB61_Out_0, 0, 0);
                                float3 _Subtract_C74B9C47_Out_2;
                                Unity_Subtract_float3(_OneMinus_865BB92A_Out_1, _Vector3_FCB8BA64_Out_0, _Subtract_C74B9C47_Out_2);
                                float3 _Vector3_FFBCB949_Out_0 = float3(0, 0, 0);
                                float3 _Divide_B3205955_Out_2;
                                Unity_Divide_float3(_Subtract_C74B9C47_Out_2, _Vector3_FFBCB949_Out_0, _Divide_B3205955_Out_2);
                                surface.Albedo = (_Multiply_FD9E0353_Out_2.xyz);
                                surface.Alpha = _FresnelEffect_ECD927F2_Out_3;
                                surface.AlphaClipThreshold = (_Divide_B3205955_Out_2).x;
                                return surface;
                            }

                            // --------------------------------------------------
                            // Structs and Packing

                            // Generated Type: Attributes
                            struct Attributes
                            {
                                float3 positionOS : POSITION;
                                float3 normalOS : NORMAL;
                                float4 tangentOS : TANGENT;
                                float4 uv0 : TEXCOORD0;
                                #if UNITY_ANY_INSTANCING_ENABLED
                                uint instanceID : INSTANCEID_SEMANTIC;
                                #endif
                            };

                            // Generated Type: Varyings
                            struct Varyings
                            {
                                float4 positionCS : SV_POSITION;
                                float3 positionWS;
                                float3 normalWS;
                                float4 texCoord0;
                                float3 viewDirectionWS;
                                #if UNITY_ANY_INSTANCING_ENABLED
                                uint instanceID : CUSTOM_INSTANCE_ID;
                                #endif
                                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                                uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                                #endif
                                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                                uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                                #endif
                                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                                #endif
                            };

                            // Generated Type: PackedVaryings
                            struct PackedVaryings
                            {
                                float4 positionCS : SV_POSITION;
                                #if UNITY_ANY_INSTANCING_ENABLED
                                uint instanceID : CUSTOM_INSTANCE_ID;
                                #endif
                                float3 interp00 : TEXCOORD0;
                                float3 interp01 : TEXCOORD1;
                                float4 interp02 : TEXCOORD2;
                                float3 interp03 : TEXCOORD3;
                                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                                uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
                                #endif
                                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                                uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
                                #endif
                                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                                FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
                                #endif
                            };

                            // Packed Type: Varyings
                            PackedVaryings PackVaryings(Varyings input)
                            {
                                PackedVaryings output = (PackedVaryings)0;
                                output.positionCS = input.positionCS;
                                output.interp00.xyz = input.positionWS;
                                output.interp01.xyz = input.normalWS;
                                output.interp02.xyzw = input.texCoord0;
                                output.interp03.xyz = input.viewDirectionWS;
                                #if UNITY_ANY_INSTANCING_ENABLED
                                output.instanceID = input.instanceID;
                                #endif
                                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                                output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                                #endif
                                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                                output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                                #endif
                                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                                output.cullFace = input.cullFace;
                                #endif
                                return output;
                            }

                            // Unpacked Type: Varyings
                            Varyings UnpackVaryings(PackedVaryings input)
                            {
                                Varyings output = (Varyings)0;
                                output.positionCS = input.positionCS;
                                output.positionWS = input.interp00.xyz;
                                output.normalWS = input.interp01.xyz;
                                output.texCoord0 = input.interp02.xyzw;
                                output.viewDirectionWS = input.interp03.xyz;
                                #if UNITY_ANY_INSTANCING_ENABLED
                                output.instanceID = input.instanceID;
                                #endif
                                #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
                                output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
                                #endif
                                #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
                                output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
                                #endif
                                #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                                output.cullFace = input.cullFace;
                                #endif
                                return output;
                            }

                            // --------------------------------------------------
                            // Build Graph Inputs

                            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
                            {
                                SurfaceDescriptionInputs output;
                                ZERO_INITIALIZE(SurfaceDescriptionInputs, output);

                                // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
                                float3 unnormalizedNormalWS = input.normalWS;
                                const float renormFactor = 1.0 / length(unnormalizedNormalWS);


                                output.WorldSpaceNormal = renormFactor * input.normalWS.xyz;		// we want a unit length Normal Vector node in shader graph
                                output.TangentSpaceNormal = float3(0.0f, 0.0f, 1.0f);


                                output.WorldSpaceViewDirection = input.viewDirectionWS; //TODO: by default normalized in HD, but not in universal
                                output.ObjectSpacePosition = TransformWorldToObject(input.positionWS);
                                output.uv0 = input.texCoord0;
                            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
                            #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
                            #else
                            #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
                            #endif
                            #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

                                return output;
                            }


                            // --------------------------------------------------
                            // Main

                            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
                            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/PBR2DPass.hlsl"

                            ENDHLSL
                        }

        }
            CustomEditor "UnityEditor.ShaderGraph.PBRMasterGUI"
                                FallBack "Hidden/Shader Graph/FallbackError"
}
