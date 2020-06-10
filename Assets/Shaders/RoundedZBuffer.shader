Shader "RoundedZBuffer"
{
    Properties
    {
        Color_BDC0FAE1("Color", Color) = (1, 1, 1, 0)
        Color_FA90A21F("CornerColor", Color) = (1, 0, 0, 0)
        Vector1_9D39BDF("MainAlpha", Range(0, 1)) = 1
        Vector1_4383654A("AlphaCorner", Range(0, 1)) = 1
        Vector1_7F5D304A("tolchina", Range(0, 1)) = 0.07
        Vector1_54D95D8A("ramkaW", Float) = 0
        Vector1_AAFAFC9F("ramkaH", Float) = 0
        Vector1_9FA8D3C1("mainW", Float) = 0
        Vector1_860B56A3("mainH", Float) = 0
        Vector1_7BA8B457("mainx", Float) = 0
        Vector1_8CDD2817("mainy", Float) = 0
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
        float4 Color_BDC0FAE1;
        float4 Color_FA90A21F;
        float Vector1_9D39BDF;
        float Vector1_4383654A;
        float Vector1_7F5D304A;
        float Vector1_54D95D8A;
        float Vector1_AAFAFC9F;
        float Vector1_9FA8D3C1;
        float Vector1_860B56A3;
        float Vector1_7BA8B457;
        float Vector1_8CDD2817;
        CBUFFER_END

            // Graph Functions

            void Unity_RoundedRectangle_float(float2 UV, float Width, float Height, float Radius, out float Out)
            {
                Radius = max(min(min(abs(Radius * 2), abs(Width)), abs(Height)), 1e-5);
                float2 uv = abs(UV * 2 - 1) - float2(Width, Height) + Radius;
                float d = length(max(0, uv)) / Radius;
                Out = saturate((1 - d) / fwidth(d));
            }

            void Unity_Subtract_float(float A, float B, out float Out)
            {
                Out = A - B;
            }

            void Unity_Multiply_float(float A, float B, out float Out)
            {
                Out = A * B;
            }

            void Unity_Multiply_float(float4 A, float4 B, out float4 Out)
            {
                Out = A * B;
            }

            void Unity_Add_float4(float4 A, float4 B, out float4 Out)
            {
                Out = A + B;
            }

            void Unity_Add_float(float A, float B, out float Out)
            {
                Out = A + B;
            }

            void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
            {
                Out = lerp(A, B, T);
            }

            // Graph Vertex
            // GraphVertex: <None>

            // Graph Pixel
            struct SurfaceDescriptionInputs
            {
                float3 TangentSpaceNormal;
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
                float _Property_CEBD485E_Out_0 = Vector1_54D95D8A;
                float _Property_A34CD787_Out_0 = Vector1_AAFAFC9F;
                float _Property_9B360D27_Out_0 = Vector1_7F5D304A;
                float _RoundedRectangle_534F79F8_Out_4;
                Unity_RoundedRectangle_float(IN.uv0.xy, _Property_CEBD485E_Out_0, _Property_A34CD787_Out_0, _Property_9B360D27_Out_0, _RoundedRectangle_534F79F8_Out_4);
                float _Property_550565FC_Out_0 = Vector1_9FA8D3C1;
                float _Property_2036F0EB_Out_0 = Vector1_860B56A3;
                float _RoundedRectangle_5A575F89_Out_4;
                Unity_RoundedRectangle_float(IN.uv0.xy, _Property_550565FC_Out_0, _Property_2036F0EB_Out_0, _Property_9B360D27_Out_0, _RoundedRectangle_5A575F89_Out_4);
                float _Subtract_39783738_Out_2;
                Unity_Subtract_float(_RoundedRectangle_534F79F8_Out_4, _RoundedRectangle_5A575F89_Out_4, _Subtract_39783738_Out_2);
                float _Property_BA712D58_Out_0 = Vector1_4383654A;
                float _Multiply_34380CB7_Out_2;
                Unity_Multiply_float(_Subtract_39783738_Out_2, _Property_BA712D58_Out_0, _Multiply_34380CB7_Out_2);
                float4 _Property_E7A7710_Out_0 = Color_FA90A21F;
                float4 _Multiply_48315B84_Out_2;
                Unity_Multiply_float((_Multiply_34380CB7_Out_2.xxxx), _Property_E7A7710_Out_0, _Multiply_48315B84_Out_2);
                float _Property_AEECA291_Out_0 = Vector1_7BA8B457;
                float _Property_A6B94C27_Out_0 = Vector1_8CDD2817;
                float _RoundedRectangle_DFC683B1_Out_4;
                Unity_RoundedRectangle_float(IN.uv0.xy, _Property_AEECA291_Out_0, _Property_A6B94C27_Out_0, _Property_9B360D27_Out_0, _RoundedRectangle_DFC683B1_Out_4);
                float _Property_E981CBD7_Out_0 = Vector1_9D39BDF;
                float _Multiply_3DFF6866_Out_2;
                Unity_Multiply_float(_RoundedRectangle_DFC683B1_Out_4, _Property_E981CBD7_Out_0, _Multiply_3DFF6866_Out_2);
                float4 _Property_1B13237F_Out_0 = Color_BDC0FAE1;
                float4 _Multiply_4871739B_Out_2;
                Unity_Multiply_float((_Multiply_3DFF6866_Out_2.xxxx), _Property_1B13237F_Out_0, _Multiply_4871739B_Out_2);
                float4 _Add_F2E7BCB_Out_2;
                Unity_Add_float4(_Multiply_48315B84_Out_2, _Multiply_4871739B_Out_2, _Add_F2E7BCB_Out_2);
                float4 Color_64C9B2C7 = IsGammaSpace() ? float4(0, 0, 0, 1) : float4(SRGBToLinear(float3(0, 0, 0)), 1);
                float4 Color_CB92DE71 = IsGammaSpace() ? float4(1, 1, 1, 1) : float4(SRGBToLinear(float3(1, 1, 1)), 1);
                float _Add_75CBB4CB_Out_2;
                Unity_Add_float(_Multiply_34380CB7_Out_2, _Multiply_3DFF6866_Out_2, _Add_75CBB4CB_Out_2);
                float4 _Lerp_696F929F_Out_3;
                Unity_Lerp_float4(Color_64C9B2C7, Color_CB92DE71, (_Add_75CBB4CB_Out_2.xxxx), _Lerp_696F929F_Out_3);
                surface.Albedo = (_Add_F2E7BCB_Out_2.xyz);
                surface.Normal = IN.TangentSpaceNormal;
                surface.Emission = IsGammaSpace() ? float3(0, 0, 0) : SRGBToLinear(float3(0, 0, 0));
                surface.Metallic = 0;
                surface.Smoothness = 0;
                surface.Occlusion = 1;
                surface.Alpha = (_Lerp_696F929F_Out_3).x;
                surface.AlphaClipThreshold = 0;
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



                output.TangentSpaceNormal = float3(0.0f, 0.0f, 1.0f);


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
                #define _NORMAL_DROPOFF_TS 1
                #define ATTRIBUTES_NEED_NORMAL
                #define ATTRIBUTES_NEED_TANGENT
                #define ATTRIBUTES_NEED_TEXCOORD0
                #define VARYINGS_NEED_TEXCOORD0
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
                float4 Color_BDC0FAE1;
                float4 Color_FA90A21F;
                float Vector1_9D39BDF;
                float Vector1_4383654A;
                float Vector1_7F5D304A;
                float Vector1_54D95D8A;
                float Vector1_AAFAFC9F;
                float Vector1_9FA8D3C1;
                float Vector1_860B56A3;
                float Vector1_7BA8B457;
                float Vector1_8CDD2817;
                CBUFFER_END

                    // Graph Functions

                    void Unity_RoundedRectangle_float(float2 UV, float Width, float Height, float Radius, out float Out)
                    {
                        Radius = max(min(min(abs(Radius * 2), abs(Width)), abs(Height)), 1e-5);
                        float2 uv = abs(UV * 2 - 1) - float2(Width, Height) + Radius;
                        float d = length(max(0, uv)) / Radius;
                        Out = saturate((1 - d) / fwidth(d));
                    }

                    void Unity_Subtract_float(float A, float B, out float Out)
                    {
                        Out = A - B;
                    }

                    void Unity_Multiply_float(float A, float B, out float Out)
                    {
                        Out = A * B;
                    }

                    void Unity_Add_float(float A, float B, out float Out)
                    {
                        Out = A + B;
                    }

                    void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
                    {
                        Out = lerp(A, B, T);
                    }

                    // Graph Vertex
                    // GraphVertex: <None>

                    // Graph Pixel
                    struct SurfaceDescriptionInputs
                    {
                        float3 TangentSpaceNormal;
                        float4 uv0;
                    };

                    struct SurfaceDescription
                    {
                        float Alpha;
                        float AlphaClipThreshold;
                    };

                    SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                    {
                        SurfaceDescription surface = (SurfaceDescription)0;
                        float4 Color_64C9B2C7 = IsGammaSpace() ? float4(0, 0, 0, 1) : float4(SRGBToLinear(float3(0, 0, 0)), 1);
                        float4 Color_CB92DE71 = IsGammaSpace() ? float4(1, 1, 1, 1) : float4(SRGBToLinear(float3(1, 1, 1)), 1);
                        float _Property_CEBD485E_Out_0 = Vector1_54D95D8A;
                        float _Property_A34CD787_Out_0 = Vector1_AAFAFC9F;
                        float _Property_9B360D27_Out_0 = Vector1_7F5D304A;
                        float _RoundedRectangle_534F79F8_Out_4;
                        Unity_RoundedRectangle_float(IN.uv0.xy, _Property_CEBD485E_Out_0, _Property_A34CD787_Out_0, _Property_9B360D27_Out_0, _RoundedRectangle_534F79F8_Out_4);
                        float _Property_550565FC_Out_0 = Vector1_9FA8D3C1;
                        float _Property_2036F0EB_Out_0 = Vector1_860B56A3;
                        float _RoundedRectangle_5A575F89_Out_4;
                        Unity_RoundedRectangle_float(IN.uv0.xy, _Property_550565FC_Out_0, _Property_2036F0EB_Out_0, _Property_9B360D27_Out_0, _RoundedRectangle_5A575F89_Out_4);
                        float _Subtract_39783738_Out_2;
                        Unity_Subtract_float(_RoundedRectangle_534F79F8_Out_4, _RoundedRectangle_5A575F89_Out_4, _Subtract_39783738_Out_2);
                        float _Property_BA712D58_Out_0 = Vector1_4383654A;
                        float _Multiply_34380CB7_Out_2;
                        Unity_Multiply_float(_Subtract_39783738_Out_2, _Property_BA712D58_Out_0, _Multiply_34380CB7_Out_2);
                        float _Property_AEECA291_Out_0 = Vector1_7BA8B457;
                        float _Property_A6B94C27_Out_0 = Vector1_8CDD2817;
                        float _RoundedRectangle_DFC683B1_Out_4;
                        Unity_RoundedRectangle_float(IN.uv0.xy, _Property_AEECA291_Out_0, _Property_A6B94C27_Out_0, _Property_9B360D27_Out_0, _RoundedRectangle_DFC683B1_Out_4);
                        float _Property_E981CBD7_Out_0 = Vector1_9D39BDF;
                        float _Multiply_3DFF6866_Out_2;
                        Unity_Multiply_float(_RoundedRectangle_DFC683B1_Out_4, _Property_E981CBD7_Out_0, _Multiply_3DFF6866_Out_2);
                        float _Add_75CBB4CB_Out_2;
                        Unity_Add_float(_Multiply_34380CB7_Out_2, _Multiply_3DFF6866_Out_2, _Add_75CBB4CB_Out_2);
                        float4 _Lerp_696F929F_Out_3;
                        Unity_Lerp_float4(Color_64C9B2C7, Color_CB92DE71, (_Add_75CBB4CB_Out_2.xxxx), _Lerp_696F929F_Out_3);
                        surface.Alpha = (_Lerp_696F929F_Out_3).x;
                        surface.AlphaClipThreshold = 0;
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
                        float4 texCoord0;
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
                        float4 interp00 : TEXCOORD0;
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
                        output.interp00.xyzw = input.texCoord0;
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
                        output.texCoord0 = input.interp00.xyzw;
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



                        output.TangentSpaceNormal = float3(0.0f, 0.0f, 1.0f);


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
                        #define _NORMAL_DROPOFF_TS 1
                        #define ATTRIBUTES_NEED_NORMAL
                        #define ATTRIBUTES_NEED_TANGENT
                        #define ATTRIBUTES_NEED_TEXCOORD0
                        #define VARYINGS_NEED_TEXCOORD0
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
                        float4 Color_BDC0FAE1;
                        float4 Color_FA90A21F;
                        float Vector1_9D39BDF;
                        float Vector1_4383654A;
                        float Vector1_7F5D304A;
                        float Vector1_54D95D8A;
                        float Vector1_AAFAFC9F;
                        float Vector1_9FA8D3C1;
                        float Vector1_860B56A3;
                        float Vector1_7BA8B457;
                        float Vector1_8CDD2817;
                        CBUFFER_END

                            // Graph Functions

                            void Unity_RoundedRectangle_float(float2 UV, float Width, float Height, float Radius, out float Out)
                            {
                                Radius = max(min(min(abs(Radius * 2), abs(Width)), abs(Height)), 1e-5);
                                float2 uv = abs(UV * 2 - 1) - float2(Width, Height) + Radius;
                                float d = length(max(0, uv)) / Radius;
                                Out = saturate((1 - d) / fwidth(d));
                            }

                            void Unity_Subtract_float(float A, float B, out float Out)
                            {
                                Out = A - B;
                            }

                            void Unity_Multiply_float(float A, float B, out float Out)
                            {
                                Out = A * B;
                            }

                            void Unity_Add_float(float A, float B, out float Out)
                            {
                                Out = A + B;
                            }

                            void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
                            {
                                Out = lerp(A, B, T);
                            }

                            // Graph Vertex
                            // GraphVertex: <None>

                            // Graph Pixel
                            struct SurfaceDescriptionInputs
                            {
                                float3 TangentSpaceNormal;
                                float4 uv0;
                            };

                            struct SurfaceDescription
                            {
                                float Alpha;
                                float AlphaClipThreshold;
                            };

                            SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
                            {
                                SurfaceDescription surface = (SurfaceDescription)0;
                                float4 Color_64C9B2C7 = IsGammaSpace() ? float4(0, 0, 0, 1) : float4(SRGBToLinear(float3(0, 0, 0)), 1);
                                float4 Color_CB92DE71 = IsGammaSpace() ? float4(1, 1, 1, 1) : float4(SRGBToLinear(float3(1, 1, 1)), 1);
                                float _Property_CEBD485E_Out_0 = Vector1_54D95D8A;
                                float _Property_A34CD787_Out_0 = Vector1_AAFAFC9F;
                                float _Property_9B360D27_Out_0 = Vector1_7F5D304A;
                                float _RoundedRectangle_534F79F8_Out_4;
                                Unity_RoundedRectangle_float(IN.uv0.xy, _Property_CEBD485E_Out_0, _Property_A34CD787_Out_0, _Property_9B360D27_Out_0, _RoundedRectangle_534F79F8_Out_4);
                                float _Property_550565FC_Out_0 = Vector1_9FA8D3C1;
                                float _Property_2036F0EB_Out_0 = Vector1_860B56A3;
                                float _RoundedRectangle_5A575F89_Out_4;
                                Unity_RoundedRectangle_float(IN.uv0.xy, _Property_550565FC_Out_0, _Property_2036F0EB_Out_0, _Property_9B360D27_Out_0, _RoundedRectangle_5A575F89_Out_4);
                                float _Subtract_39783738_Out_2;
                                Unity_Subtract_float(_RoundedRectangle_534F79F8_Out_4, _RoundedRectangle_5A575F89_Out_4, _Subtract_39783738_Out_2);
                                float _Property_BA712D58_Out_0 = Vector1_4383654A;
                                float _Multiply_34380CB7_Out_2;
                                Unity_Multiply_float(_Subtract_39783738_Out_2, _Property_BA712D58_Out_0, _Multiply_34380CB7_Out_2);
                                float _Property_AEECA291_Out_0 = Vector1_7BA8B457;
                                float _Property_A6B94C27_Out_0 = Vector1_8CDD2817;
                                float _RoundedRectangle_DFC683B1_Out_4;
                                Unity_RoundedRectangle_float(IN.uv0.xy, _Property_AEECA291_Out_0, _Property_A6B94C27_Out_0, _Property_9B360D27_Out_0, _RoundedRectangle_DFC683B1_Out_4);
                                float _Property_E981CBD7_Out_0 = Vector1_9D39BDF;
                                float _Multiply_3DFF6866_Out_2;
                                Unity_Multiply_float(_RoundedRectangle_DFC683B1_Out_4, _Property_E981CBD7_Out_0, _Multiply_3DFF6866_Out_2);
                                float _Add_75CBB4CB_Out_2;
                                Unity_Add_float(_Multiply_34380CB7_Out_2, _Multiply_3DFF6866_Out_2, _Add_75CBB4CB_Out_2);
                                float4 _Lerp_696F929F_Out_3;
                                Unity_Lerp_float4(Color_64C9B2C7, Color_CB92DE71, (_Add_75CBB4CB_Out_2.xxxx), _Lerp_696F929F_Out_3);
                                surface.Alpha = (_Lerp_696F929F_Out_3).x;
                                surface.AlphaClipThreshold = 0;
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
                                float4 texCoord0;
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
                                float4 interp00 : TEXCOORD0;
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
                                output.interp00.xyzw = input.texCoord0;
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
                                output.texCoord0 = input.interp00.xyzw;
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



                                output.TangentSpaceNormal = float3(0.0f, 0.0f, 1.0f);


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
                                #define _NORMAL_DROPOFF_TS 1
                                #define ATTRIBUTES_NEED_NORMAL
                                #define ATTRIBUTES_NEED_TANGENT
                                #define ATTRIBUTES_NEED_TEXCOORD0
                                #define ATTRIBUTES_NEED_TEXCOORD1
                                #define ATTRIBUTES_NEED_TEXCOORD2
                                #define VARYINGS_NEED_TEXCOORD0
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
                                float4 Color_BDC0FAE1;
                                float4 Color_FA90A21F;
                                float Vector1_9D39BDF;
                                float Vector1_4383654A;
                                float Vector1_7F5D304A;
                                float Vector1_54D95D8A;
                                float Vector1_AAFAFC9F;
                                float Vector1_9FA8D3C1;
                                float Vector1_860B56A3;
                                float Vector1_7BA8B457;
                                float Vector1_8CDD2817;
                                CBUFFER_END

                                    // Graph Functions

                                    void Unity_RoundedRectangle_float(float2 UV, float Width, float Height, float Radius, out float Out)
                                    {
                                        Radius = max(min(min(abs(Radius * 2), abs(Width)), abs(Height)), 1e-5);
                                        float2 uv = abs(UV * 2 - 1) - float2(Width, Height) + Radius;
                                        float d = length(max(0, uv)) / Radius;
                                        Out = saturate((1 - d) / fwidth(d));
                                    }

                                    void Unity_Subtract_float(float A, float B, out float Out)
                                    {
                                        Out = A - B;
                                    }

                                    void Unity_Multiply_float(float A, float B, out float Out)
                                    {
                                        Out = A * B;
                                    }

                                    void Unity_Multiply_float(float4 A, float4 B, out float4 Out)
                                    {
                                        Out = A * B;
                                    }

                                    void Unity_Add_float4(float4 A, float4 B, out float4 Out)
                                    {
                                        Out = A + B;
                                    }

                                    void Unity_Add_float(float A, float B, out float Out)
                                    {
                                        Out = A + B;
                                    }

                                    void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
                                    {
                                        Out = lerp(A, B, T);
                                    }

                                    // Graph Vertex
                                    // GraphVertex: <None>

                                    // Graph Pixel
                                    struct SurfaceDescriptionInputs
                                    {
                                        float3 TangentSpaceNormal;
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
                                        float _Property_CEBD485E_Out_0 = Vector1_54D95D8A;
                                        float _Property_A34CD787_Out_0 = Vector1_AAFAFC9F;
                                        float _Property_9B360D27_Out_0 = Vector1_7F5D304A;
                                        float _RoundedRectangle_534F79F8_Out_4;
                                        Unity_RoundedRectangle_float(IN.uv0.xy, _Property_CEBD485E_Out_0, _Property_A34CD787_Out_0, _Property_9B360D27_Out_0, _RoundedRectangle_534F79F8_Out_4);
                                        float _Property_550565FC_Out_0 = Vector1_9FA8D3C1;
                                        float _Property_2036F0EB_Out_0 = Vector1_860B56A3;
                                        float _RoundedRectangle_5A575F89_Out_4;
                                        Unity_RoundedRectangle_float(IN.uv0.xy, _Property_550565FC_Out_0, _Property_2036F0EB_Out_0, _Property_9B360D27_Out_0, _RoundedRectangle_5A575F89_Out_4);
                                        float _Subtract_39783738_Out_2;
                                        Unity_Subtract_float(_RoundedRectangle_534F79F8_Out_4, _RoundedRectangle_5A575F89_Out_4, _Subtract_39783738_Out_2);
                                        float _Property_BA712D58_Out_0 = Vector1_4383654A;
                                        float _Multiply_34380CB7_Out_2;
                                        Unity_Multiply_float(_Subtract_39783738_Out_2, _Property_BA712D58_Out_0, _Multiply_34380CB7_Out_2);
                                        float4 _Property_E7A7710_Out_0 = Color_FA90A21F;
                                        float4 _Multiply_48315B84_Out_2;
                                        Unity_Multiply_float((_Multiply_34380CB7_Out_2.xxxx), _Property_E7A7710_Out_0, _Multiply_48315B84_Out_2);
                                        float _Property_AEECA291_Out_0 = Vector1_7BA8B457;
                                        float _Property_A6B94C27_Out_0 = Vector1_8CDD2817;
                                        float _RoundedRectangle_DFC683B1_Out_4;
                                        Unity_RoundedRectangle_float(IN.uv0.xy, _Property_AEECA291_Out_0, _Property_A6B94C27_Out_0, _Property_9B360D27_Out_0, _RoundedRectangle_DFC683B1_Out_4);
                                        float _Property_E981CBD7_Out_0 = Vector1_9D39BDF;
                                        float _Multiply_3DFF6866_Out_2;
                                        Unity_Multiply_float(_RoundedRectangle_DFC683B1_Out_4, _Property_E981CBD7_Out_0, _Multiply_3DFF6866_Out_2);
                                        float4 _Property_1B13237F_Out_0 = Color_BDC0FAE1;
                                        float4 _Multiply_4871739B_Out_2;
                                        Unity_Multiply_float((_Multiply_3DFF6866_Out_2.xxxx), _Property_1B13237F_Out_0, _Multiply_4871739B_Out_2);
                                        float4 _Add_F2E7BCB_Out_2;
                                        Unity_Add_float4(_Multiply_48315B84_Out_2, _Multiply_4871739B_Out_2, _Add_F2E7BCB_Out_2);
                                        float4 Color_64C9B2C7 = IsGammaSpace() ? float4(0, 0, 0, 1) : float4(SRGBToLinear(float3(0, 0, 0)), 1);
                                        float4 Color_CB92DE71 = IsGammaSpace() ? float4(1, 1, 1, 1) : float4(SRGBToLinear(float3(1, 1, 1)), 1);
                                        float _Add_75CBB4CB_Out_2;
                                        Unity_Add_float(_Multiply_34380CB7_Out_2, _Multiply_3DFF6866_Out_2, _Add_75CBB4CB_Out_2);
                                        float4 _Lerp_696F929F_Out_3;
                                        Unity_Lerp_float4(Color_64C9B2C7, Color_CB92DE71, (_Add_75CBB4CB_Out_2.xxxx), _Lerp_696F929F_Out_3);
                                        surface.Albedo = (_Add_F2E7BCB_Out_2.xyz);
                                        surface.Emission = IsGammaSpace() ? float3(0, 0, 0) : SRGBToLinear(float3(0, 0, 0));
                                        surface.Alpha = (_Lerp_696F929F_Out_3).x;
                                        surface.AlphaClipThreshold = 0;
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
                                        float4 texCoord0;
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
                                        float4 interp00 : TEXCOORD0;
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
                                        output.interp00.xyzw = input.texCoord0;
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
                                        output.texCoord0 = input.interp00.xyzw;
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



                                        output.TangentSpaceNormal = float3(0.0f, 0.0f, 1.0f);


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
                                        #define _NORMAL_DROPOFF_TS 1
                                        #define ATTRIBUTES_NEED_NORMAL
                                        #define ATTRIBUTES_NEED_TANGENT
                                        #define ATTRIBUTES_NEED_TEXCOORD0
                                        #define VARYINGS_NEED_TEXCOORD0
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
                                        float4 Color_BDC0FAE1;
                                        float4 Color_FA90A21F;
                                        float Vector1_9D39BDF;
                                        float Vector1_4383654A;
                                        float Vector1_7F5D304A;
                                        float Vector1_54D95D8A;
                                        float Vector1_AAFAFC9F;
                                        float Vector1_9FA8D3C1;
                                        float Vector1_860B56A3;
                                        float Vector1_7BA8B457;
                                        float Vector1_8CDD2817;
                                        CBUFFER_END

                                            // Graph Functions

                                            void Unity_RoundedRectangle_float(float2 UV, float Width, float Height, float Radius, out float Out)
                                            {
                                                Radius = max(min(min(abs(Radius * 2), abs(Width)), abs(Height)), 1e-5);
                                                float2 uv = abs(UV * 2 - 1) - float2(Width, Height) + Radius;
                                                float d = length(max(0, uv)) / Radius;
                                                Out = saturate((1 - d) / fwidth(d));
                                            }

                                            void Unity_Subtract_float(float A, float B, out float Out)
                                            {
                                                Out = A - B;
                                            }

                                            void Unity_Multiply_float(float A, float B, out float Out)
                                            {
                                                Out = A * B;
                                            }

                                            void Unity_Multiply_float(float4 A, float4 B, out float4 Out)
                                            {
                                                Out = A * B;
                                            }

                                            void Unity_Add_float4(float4 A, float4 B, out float4 Out)
                                            {
                                                Out = A + B;
                                            }

                                            void Unity_Add_float(float A, float B, out float Out)
                                            {
                                                Out = A + B;
                                            }

                                            void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
                                            {
                                                Out = lerp(A, B, T);
                                            }

                                            // Graph Vertex
                                            // GraphVertex: <None>

                                            // Graph Pixel
                                            struct SurfaceDescriptionInputs
                                            {
                                                float3 TangentSpaceNormal;
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
                                                float _Property_CEBD485E_Out_0 = Vector1_54D95D8A;
                                                float _Property_A34CD787_Out_0 = Vector1_AAFAFC9F;
                                                float _Property_9B360D27_Out_0 = Vector1_7F5D304A;
                                                float _RoundedRectangle_534F79F8_Out_4;
                                                Unity_RoundedRectangle_float(IN.uv0.xy, _Property_CEBD485E_Out_0, _Property_A34CD787_Out_0, _Property_9B360D27_Out_0, _RoundedRectangle_534F79F8_Out_4);
                                                float _Property_550565FC_Out_0 = Vector1_9FA8D3C1;
                                                float _Property_2036F0EB_Out_0 = Vector1_860B56A3;
                                                float _RoundedRectangle_5A575F89_Out_4;
                                                Unity_RoundedRectangle_float(IN.uv0.xy, _Property_550565FC_Out_0, _Property_2036F0EB_Out_0, _Property_9B360D27_Out_0, _RoundedRectangle_5A575F89_Out_4);
                                                float _Subtract_39783738_Out_2;
                                                Unity_Subtract_float(_RoundedRectangle_534F79F8_Out_4, _RoundedRectangle_5A575F89_Out_4, _Subtract_39783738_Out_2);
                                                float _Property_BA712D58_Out_0 = Vector1_4383654A;
                                                float _Multiply_34380CB7_Out_2;
                                                Unity_Multiply_float(_Subtract_39783738_Out_2, _Property_BA712D58_Out_0, _Multiply_34380CB7_Out_2);
                                                float4 _Property_E7A7710_Out_0 = Color_FA90A21F;
                                                float4 _Multiply_48315B84_Out_2;
                                                Unity_Multiply_float((_Multiply_34380CB7_Out_2.xxxx), _Property_E7A7710_Out_0, _Multiply_48315B84_Out_2);
                                                float _Property_AEECA291_Out_0 = Vector1_7BA8B457;
                                                float _Property_A6B94C27_Out_0 = Vector1_8CDD2817;
                                                float _RoundedRectangle_DFC683B1_Out_4;
                                                Unity_RoundedRectangle_float(IN.uv0.xy, _Property_AEECA291_Out_0, _Property_A6B94C27_Out_0, _Property_9B360D27_Out_0, _RoundedRectangle_DFC683B1_Out_4);
                                                float _Property_E981CBD7_Out_0 = Vector1_9D39BDF;
                                                float _Multiply_3DFF6866_Out_2;
                                                Unity_Multiply_float(_RoundedRectangle_DFC683B1_Out_4, _Property_E981CBD7_Out_0, _Multiply_3DFF6866_Out_2);
                                                float4 _Property_1B13237F_Out_0 = Color_BDC0FAE1;
                                                float4 _Multiply_4871739B_Out_2;
                                                Unity_Multiply_float((_Multiply_3DFF6866_Out_2.xxxx), _Property_1B13237F_Out_0, _Multiply_4871739B_Out_2);
                                                float4 _Add_F2E7BCB_Out_2;
                                                Unity_Add_float4(_Multiply_48315B84_Out_2, _Multiply_4871739B_Out_2, _Add_F2E7BCB_Out_2);
                                                float4 Color_64C9B2C7 = IsGammaSpace() ? float4(0, 0, 0, 1) : float4(SRGBToLinear(float3(0, 0, 0)), 1);
                                                float4 Color_CB92DE71 = IsGammaSpace() ? float4(1, 1, 1, 1) : float4(SRGBToLinear(float3(1, 1, 1)), 1);
                                                float _Add_75CBB4CB_Out_2;
                                                Unity_Add_float(_Multiply_34380CB7_Out_2, _Multiply_3DFF6866_Out_2, _Add_75CBB4CB_Out_2);
                                                float4 _Lerp_696F929F_Out_3;
                                                Unity_Lerp_float4(Color_64C9B2C7, Color_CB92DE71, (_Add_75CBB4CB_Out_2.xxxx), _Lerp_696F929F_Out_3);
                                                surface.Albedo = (_Add_F2E7BCB_Out_2.xyz);
                                                surface.Alpha = (_Lerp_696F929F_Out_3).x;
                                                surface.AlphaClipThreshold = 0;
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
                                                float4 texCoord0;
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
                                                float4 interp00 : TEXCOORD0;
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
                                                output.interp00.xyzw = input.texCoord0;
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
                                                output.texCoord0 = input.interp00.xyzw;
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



                                                output.TangentSpaceNormal = float3(0.0f, 0.0f, 1.0f);


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
