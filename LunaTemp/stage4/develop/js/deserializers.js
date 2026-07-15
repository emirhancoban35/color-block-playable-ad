var Deserializers = {}
Deserializers["UnityEngine.JointSpring"] = function (request, data, root) {
  var i522 = root || request.c( 'UnityEngine.JointSpring' )
  var i523 = data
  i522.spring = i523[0]
  i522.damper = i523[1]
  i522.targetPosition = i523[2]
  return i522
}

Deserializers["UnityEngine.JointMotor"] = function (request, data, root) {
  var i524 = root || request.c( 'UnityEngine.JointMotor' )
  var i525 = data
  i524.m_TargetVelocity = i525[0]
  i524.m_Force = i525[1]
  i524.m_FreeSpin = i525[2]
  return i524
}

Deserializers["UnityEngine.JointLimits"] = function (request, data, root) {
  var i526 = root || request.c( 'UnityEngine.JointLimits' )
  var i527 = data
  i526.m_Min = i527[0]
  i526.m_Max = i527[1]
  i526.m_Bounciness = i527[2]
  i526.m_BounceMinVelocity = i527[3]
  i526.m_ContactDistance = i527[4]
  i526.minBounce = i527[5]
  i526.maxBounce = i527[6]
  return i526
}

Deserializers["UnityEngine.JointDrive"] = function (request, data, root) {
  var i528 = root || request.c( 'UnityEngine.JointDrive' )
  var i529 = data
  i528.m_PositionSpring = i529[0]
  i528.m_PositionDamper = i529[1]
  i528.m_MaximumForce = i529[2]
  i528.m_UseAcceleration = i529[3]
  return i528
}

Deserializers["UnityEngine.SoftJointLimitSpring"] = function (request, data, root) {
  var i530 = root || request.c( 'UnityEngine.SoftJointLimitSpring' )
  var i531 = data
  i530.m_Spring = i531[0]
  i530.m_Damper = i531[1]
  return i530
}

Deserializers["UnityEngine.SoftJointLimit"] = function (request, data, root) {
  var i532 = root || request.c( 'UnityEngine.SoftJointLimit' )
  var i533 = data
  i532.m_Limit = i533[0]
  i532.m_Bounciness = i533[1]
  i532.m_ContactDistance = i533[2]
  return i532
}

Deserializers["UnityEngine.WheelFrictionCurve"] = function (request, data, root) {
  var i534 = root || request.c( 'UnityEngine.WheelFrictionCurve' )
  var i535 = data
  i534.m_ExtremumSlip = i535[0]
  i534.m_ExtremumValue = i535[1]
  i534.m_AsymptoteSlip = i535[2]
  i534.m_AsymptoteValue = i535[3]
  i534.m_Stiffness = i535[4]
  return i534
}

Deserializers["UnityEngine.JointAngleLimits2D"] = function (request, data, root) {
  var i536 = root || request.c( 'UnityEngine.JointAngleLimits2D' )
  var i537 = data
  i536.m_LowerAngle = i537[0]
  i536.m_UpperAngle = i537[1]
  return i536
}

Deserializers["UnityEngine.JointMotor2D"] = function (request, data, root) {
  var i538 = root || request.c( 'UnityEngine.JointMotor2D' )
  var i539 = data
  i538.m_MotorSpeed = i539[0]
  i538.m_MaximumMotorTorque = i539[1]
  return i538
}

Deserializers["UnityEngine.JointSuspension2D"] = function (request, data, root) {
  var i540 = root || request.c( 'UnityEngine.JointSuspension2D' )
  var i541 = data
  i540.m_DampingRatio = i541[0]
  i540.m_Frequency = i541[1]
  i540.m_Angle = i541[2]
  return i540
}

Deserializers["UnityEngine.JointTranslationLimits2D"] = function (request, data, root) {
  var i542 = root || request.c( 'UnityEngine.JointTranslationLimits2D' )
  var i543 = data
  i542.m_LowerTranslation = i543[0]
  i542.m_UpperTranslation = i543[1]
  return i542
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh"] = function (request, data, root) {
  var i544 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh' )
  var i545 = data
  i544.name = i545[0]
  i544.halfPrecision = !!i545[1]
  i544.useSimplification = !!i545[2]
  i544.useUInt32IndexFormat = !!i545[3]
  i544.vertexCount = i545[4]
  i544.aabb = i545[5]
  var i547 = i545[6]
  var i546 = []
  for(var i = 0; i < i547.length; i += 1) {
    i546.push( !!i547[i + 0] );
  }
  i544.streams = i546
  i544.vertices = i545[7]
  var i549 = i545[8]
  var i548 = []
  for(var i = 0; i < i549.length; i += 1) {
    i548.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh', i549[i + 0]) );
  }
  i544.subMeshes = i548
  var i551 = i545[9]
  var i550 = []
  for(var i = 0; i < i551.length; i += 16) {
    i550.push( new pc.Mat4().setData(i551[i + 0], i551[i + 1], i551[i + 2], i551[i + 3],  i551[i + 4], i551[i + 5], i551[i + 6], i551[i + 7],  i551[i + 8], i551[i + 9], i551[i + 10], i551[i + 11],  i551[i + 12], i551[i + 13], i551[i + 14], i551[i + 15]) );
  }
  i544.bindposes = i550
  var i553 = i545[10]
  var i552 = []
  for(var i = 0; i < i553.length; i += 1) {
    i552.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape', i553[i + 0]) );
  }
  i544.blendShapes = i552
  return i544
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh"] = function (request, data, root) {
  var i558 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh' )
  var i559 = data
  i558.triangles = i559[0]
  return i558
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape"] = function (request, data, root) {
  var i564 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape' )
  var i565 = data
  i564.name = i565[0]
  var i567 = i565[1]
  var i566 = []
  for(var i = 0; i < i567.length; i += 1) {
    i566.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame', i567[i + 0]) );
  }
  i564.frames = i566
  return i564
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material"] = function (request, data, root) {
  var i568 = root || new pc.UnityMaterial()
  var i569 = data
  i568.name = i569[0]
  request.r(i569[1], i569[2], 0, i568, 'shader')
  i568.renderQueue = i569[3]
  i568.enableInstancing = !!i569[4]
  var i571 = i569[5]
  var i570 = []
  for(var i = 0; i < i571.length; i += 1) {
    i570.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter', i571[i + 0]) );
  }
  i568.floatParameters = i570
  var i573 = i569[6]
  var i572 = []
  for(var i = 0; i < i573.length; i += 1) {
    i572.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter', i573[i + 0]) );
  }
  i568.colorParameters = i572
  var i575 = i569[7]
  var i574 = []
  for(var i = 0; i < i575.length; i += 1) {
    i574.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter', i575[i + 0]) );
  }
  i568.vectorParameters = i574
  var i577 = i569[8]
  var i576 = []
  for(var i = 0; i < i577.length; i += 1) {
    i576.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter', i577[i + 0]) );
  }
  i568.textureParameters = i576
  var i579 = i569[9]
  var i578 = []
  for(var i = 0; i < i579.length; i += 1) {
    i578.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag', i579[i + 0]) );
  }
  i568.materialFlags = i578
  return i568
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter"] = function (request, data, root) {
  var i582 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter' )
  var i583 = data
  i582.name = i583[0]
  i582.value = i583[1]
  return i582
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter"] = function (request, data, root) {
  var i586 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter' )
  var i587 = data
  i586.name = i587[0]
  i586.value = new pc.Color(i587[1], i587[2], i587[3], i587[4])
  return i586
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter"] = function (request, data, root) {
  var i590 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter' )
  var i591 = data
  i590.name = i591[0]
  i590.value = new pc.Vec4( i591[1], i591[2], i591[3], i591[4] )
  return i590
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter"] = function (request, data, root) {
  var i594 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter' )
  var i595 = data
  i594.name = i595[0]
  request.r(i595[1], i595[2], 0, i594, 'value')
  return i594
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag"] = function (request, data, root) {
  var i598 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag' )
  var i599 = data
  i598.name = i599[0]
  i598.enabled = !!i599[1]
  return i598
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Texture2D"] = function (request, data, root) {
  var i600 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Texture2D' )
  var i601 = data
  i600.name = i601[0]
  i600.width = i601[1]
  i600.height = i601[2]
  i600.mipmapCount = i601[3]
  i600.anisoLevel = i601[4]
  i600.filterMode = i601[5]
  i600.hdr = !!i601[6]
  i600.format = i601[7]
  i600.wrapMode = i601[8]
  i600.alphaIsTransparency = !!i601[9]
  i600.alphaSource = i601[10]
  i600.graphicsFormat = i601[11]
  i600.sRGBTexture = !!i601[12]
  i600.desiredColorSpace = i601[13]
  i600.wrapU = i601[14]
  i600.wrapV = i601[15]
  return i600
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Cubemap"] = function (request, data, root) {
  var i602 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Cubemap' )
  var i603 = data
  i602.name = i603[0]
  i602.atlasId = i603[1]
  i602.mipmapCount = i603[2]
  i602.hdr = !!i603[3]
  i602.size = i603[4]
  i602.anisoLevel = i603[5]
  i602.filterMode = i603[6]
  var i605 = i603[7]
  var i604 = []
  for(var i = 0; i < i605.length; i += 4) {
    i604.push( UnityEngine.Rect.MinMaxRect(i605[i + 0], i605[i + 1], i605[i + 2], i605[i + 3]) );
  }
  i602.rects = i604
  i602.wrapU = i603[8]
  i602.wrapV = i603[9]
  return i602
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.Scene"] = function (request, data, root) {
  var i608 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.Scene' )
  var i609 = data
  i608.name = i609[0]
  i608.index = i609[1]
  i608.startup = !!i609[2]
  return i608
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Camera"] = function (request, data, root) {
  var i610 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Camera' )
  var i611 = data
  i610.aspect = i611[0]
  i610.orthographic = !!i611[1]
  i610.orthographicSize = i611[2]
  i610.backgroundColor = new pc.Color(i611[3], i611[4], i611[5], i611[6])
  i610.nearClipPlane = i611[7]
  i610.farClipPlane = i611[8]
  i610.fieldOfView = i611[9]
  i610.depth = i611[10]
  i610.clearFlags = i611[11]
  i610.cullingMask = i611[12]
  i610.rect = i611[13]
  request.r(i611[14], i611[15], 0, i610, 'targetTexture')
  i610.usePhysicalProperties = !!i611[16]
  i610.focalLength = i611[17]
  i610.sensorSize = new pc.Vec2( i611[18], i611[19] )
  i610.lensShift = new pc.Vec2( i611[20], i611[21] )
  i610.gateFit = i611[22]
  i610.commandBufferCount = i611[23]
  i610.cameraType = i611[24]
  i610.enabled = !!i611[25]
  return i610
}

Deserializers["UnityEngine.Rendering.Universal.UniversalAdditionalCameraData"] = function (request, data, root) {
  var i612 = root || request.c( 'UnityEngine.Rendering.Universal.UniversalAdditionalCameraData' )
  var i613 = data
  i612.m_RenderShadows = !!i613[0]
  i612.m_RequiresDepthTextureOption = i613[1]
  i612.m_RequiresOpaqueTextureOption = i613[2]
  i612.m_CameraType = i613[3]
  var i615 = i613[4]
  var i614 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Camera')))
  for(var i = 0; i < i615.length; i += 2) {
  request.r(i615[i + 0], i615[i + 1], 1, i614, '')
  }
  i612.m_Cameras = i614
  i612.m_RendererIndex = i613[5]
  i612.m_VolumeLayerMask = UnityEngine.LayerMask.FromIntegerValue( i613[6] )
  request.r(i613[7], i613[8], 0, i612, 'm_VolumeTrigger')
  i612.m_VolumeFrameworkUpdateModeOption = i613[9]
  i612.m_RenderPostProcessing = !!i613[10]
  i612.m_Antialiasing = i613[11]
  i612.m_AntialiasingQuality = i613[12]
  i612.m_StopNaN = !!i613[13]
  i612.m_Dithering = !!i613[14]
  i612.m_ClearDepth = !!i613[15]
  i612.m_AllowXRRendering = !!i613[16]
  i612.m_AllowHDROutput = !!i613[17]
  i612.m_UseScreenCoordOverride = !!i613[18]
  i612.m_ScreenSizeOverride = new pc.Vec4( i613[19], i613[20], i613[21], i613[22] )
  i612.m_ScreenCoordScaleBias = new pc.Vec4( i613[23], i613[24], i613[25], i613[26] )
  i612.m_RequiresDepthTexture = !!i613[27]
  i612.m_RequiresColorTexture = !!i613[28]
  i612.m_Version = i613[29]
  i612.m_TaaSettings = request.d('UnityEngine.Rendering.Universal.TemporalAA+Settings', i613[30], i612.m_TaaSettings)
  return i612
}

Deserializers["UnityEngine.Rendering.Universal.TemporalAA+Settings"] = function (request, data, root) {
  var i618 = root || request.c( 'UnityEngine.Rendering.Universal.TemporalAA+Settings' )
  var i619 = data
  i618.m_Quality = i619[0]
  i618.m_FrameInfluence = i619[1]
  i618.m_JitterScale = i619[2]
  i618.m_MipBias = i619[3]
  i618.m_VarianceClampScale = i619[4]
  i618.m_ContrastAdaptiveSharpening = i619[5]
  return i618
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.GameObject"] = function (request, data, root) {
  var i620 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.GameObject' )
  var i621 = data
  i620.name = i621[0]
  i620.tagId = i621[1]
  i620.enabled = !!i621[2]
  i620.isStatic = !!i621[3]
  i620.layer = i621[4]
  return i620
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Light"] = function (request, data, root) {
  var i622 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Light' )
  var i623 = data
  i622.type = i623[0]
  i622.color = new pc.Color(i623[1], i623[2], i623[3], i623[4])
  i622.cullingMask = i623[5]
  i622.intensity = i623[6]
  i622.range = i623[7]
  i622.spotAngle = i623[8]
  i622.shadows = i623[9]
  i622.shadowNormalBias = i623[10]
  i622.shadowBias = i623[11]
  i622.shadowStrength = i623[12]
  i622.shadowResolution = i623[13]
  i622.lightmapBakeType = i623[14]
  i622.renderMode = i623[15]
  request.r(i623[16], i623[17], 0, i622, 'cookie')
  i622.cookieSize = i623[18]
  i622.shadowNearPlane = i623[19]
  i622.occlusionMaskChannel = i623[20]
  i622.isBaked = !!i623[21]
  i622.mixedLightingMode = i623[22]
  i622.enabled = !!i623[23]
  return i622
}

Deserializers["UnityEngine.Rendering.Universal.UniversalAdditionalLightData"] = function (request, data, root) {
  var i624 = root || request.c( 'UnityEngine.Rendering.Universal.UniversalAdditionalLightData' )
  var i625 = data
  i624.m_Version = i625[0]
  i624.m_UsePipelineSettings = !!i625[1]
  i624.m_AdditionalLightsShadowResolutionTier = i625[2]
  i624.m_LightLayerMask = i625[3]
  i624.m_RenderingLayers = i625[4]
  i624.m_CustomShadowLayers = !!i625[5]
  i624.m_ShadowLayerMask = i625[6]
  i624.m_ShadowRenderingLayers = i625[7]
  i624.m_LightCookieSize = new pc.Vec2( i625[8], i625[9] )
  i624.m_LightCookieOffset = new pc.Vec2( i625[10], i625[11] )
  i624.m_SoftShadowQuality = i625[12]
  return i624
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.MeshFilter"] = function (request, data, root) {
  var i626 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.MeshFilter' )
  var i627 = data
  request.r(i627[0], i627[1], 0, i626, 'sharedMesh')
  return i626
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.MeshRenderer"] = function (request, data, root) {
  var i628 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.MeshRenderer' )
  var i629 = data
  request.r(i629[0], i629[1], 0, i628, 'additionalVertexStreams')
  i628.enabled = !!i629[2]
  request.r(i629[3], i629[4], 0, i628, 'sharedMaterial')
  var i631 = i629[5]
  var i630 = []
  for(var i = 0; i < i631.length; i += 2) {
  request.r(i631[i + 0], i631[i + 1], 2, i630, '')
  }
  i628.sharedMaterials = i630
  i628.receiveShadows = !!i629[6]
  i628.shadowCastingMode = i629[7]
  i628.sortingLayerID = i629[8]
  i628.sortingOrder = i629[9]
  i628.lightmapIndex = i629[10]
  i628.lightmapSceneIndex = i629[11]
  i628.lightmapScaleOffset = new pc.Vec4( i629[12], i629[13], i629[14], i629[15] )
  i628.lightProbeUsage = i629[16]
  i628.reflectionProbeUsage = i629[17]
  return i628
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.BoxCollider"] = function (request, data, root) {
  var i634 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.BoxCollider' )
  var i635 = data
  i634.center = new pc.Vec3( i635[0], i635[1], i635[2] )
  i634.size = new pc.Vec3( i635[3], i635[4], i635[5] )
  i634.enabled = !!i635[6]
  i634.isTrigger = !!i635[7]
  request.r(i635[8], i635[9], 0, i634, 'material')
  return i634
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.RectTransform"] = function (request, data, root) {
  var i636 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.RectTransform' )
  var i637 = data
  i636.pivot = new pc.Vec2( i637[0], i637[1] )
  i636.anchorMin = new pc.Vec2( i637[2], i637[3] )
  i636.anchorMax = new pc.Vec2( i637[4], i637[5] )
  i636.sizeDelta = new pc.Vec2( i637[6], i637[7] )
  i636.anchoredPosition3D = new pc.Vec3( i637[8], i637[9], i637[10] )
  i636.rotation = new pc.Quat(i637[11], i637[12], i637[13], i637[14])
  i636.scale = new pc.Vec3( i637[15], i637[16], i637[17] )
  return i636
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Canvas"] = function (request, data, root) {
  var i638 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Canvas' )
  var i639 = data
  i638.planeDistance = i639[0]
  i638.referencePixelsPerUnit = i639[1]
  i638.isFallbackOverlay = !!i639[2]
  i638.renderMode = i639[3]
  i638.renderOrder = i639[4]
  i638.sortingLayerName = i639[5]
  i638.sortingOrder = i639[6]
  i638.scaleFactor = i639[7]
  request.r(i639[8], i639[9], 0, i638, 'worldCamera')
  i638.overrideSorting = !!i639[10]
  i638.pixelPerfect = !!i639[11]
  i638.targetDisplay = i639[12]
  i638.overridePixelPerfect = !!i639[13]
  i638.enabled = !!i639[14]
  return i638
}

Deserializers["UnityEngine.UI.CanvasScaler"] = function (request, data, root) {
  var i640 = root || request.c( 'UnityEngine.UI.CanvasScaler' )
  var i641 = data
  i640.m_UiScaleMode = i641[0]
  i640.m_ReferencePixelsPerUnit = i641[1]
  i640.m_ScaleFactor = i641[2]
  i640.m_ReferenceResolution = new pc.Vec2( i641[3], i641[4] )
  i640.m_ScreenMatchMode = i641[5]
  i640.m_MatchWidthOrHeight = i641[6]
  i640.m_PhysicalUnit = i641[7]
  i640.m_FallbackScreenDPI = i641[8]
  i640.m_DefaultSpriteDPI = i641[9]
  i640.m_DynamicPixelsPerUnit = i641[10]
  i640.m_PresetInfoIsWorld = !!i641[11]
  return i640
}

Deserializers["UnityEngine.UI.GraphicRaycaster"] = function (request, data, root) {
  var i642 = root || request.c( 'UnityEngine.UI.GraphicRaycaster' )
  var i643 = data
  i642.m_IgnoreReversedGraphics = !!i643[0]
  i642.m_BlockingObjects = i643[1]
  i642.m_BlockingMask = UnityEngine.LayerMask.FromIntegerValue( i643[2] )
  return i642
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer"] = function (request, data, root) {
  var i644 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer' )
  var i645 = data
  i644.cullTransparentMesh = !!i645[0]
  return i644
}

Deserializers["UnityEngine.UI.Image"] = function (request, data, root) {
  var i646 = root || request.c( 'UnityEngine.UI.Image' )
  var i647 = data
  request.r(i647[0], i647[1], 0, i646, 'm_Sprite')
  i646.m_Type = i647[2]
  i646.m_PreserveAspect = !!i647[3]
  i646.m_FillCenter = !!i647[4]
  i646.m_FillMethod = i647[5]
  i646.m_FillAmount = i647[6]
  i646.m_FillClockwise = !!i647[7]
  i646.m_FillOrigin = i647[8]
  i646.m_UseSpriteMesh = !!i647[9]
  i646.m_PixelsPerUnitMultiplier = i647[10]
  request.r(i647[11], i647[12], 0, i646, 'm_Material')
  i646.m_Maskable = !!i647[13]
  i646.m_Color = new pc.Color(i647[14], i647[15], i647[16], i647[17])
  i646.m_RaycastTarget = !!i647[18]
  i646.m_RaycastPadding = new pc.Vec4( i647[19], i647[20], i647[21], i647[22] )
  return i646
}

Deserializers["UnityEngine.UI.Button"] = function (request, data, root) {
  var i648 = root || request.c( 'UnityEngine.UI.Button' )
  var i649 = data
  i648.m_OnClick = request.d('UnityEngine.UI.Button+ButtonClickedEvent', i649[0], i648.m_OnClick)
  i648.m_Navigation = request.d('UnityEngine.UI.Navigation', i649[1], i648.m_Navigation)
  i648.m_Transition = i649[2]
  i648.m_Colors = request.d('UnityEngine.UI.ColorBlock', i649[3], i648.m_Colors)
  i648.m_SpriteState = request.d('UnityEngine.UI.SpriteState', i649[4], i648.m_SpriteState)
  i648.m_AnimationTriggers = request.d('UnityEngine.UI.AnimationTriggers', i649[5], i648.m_AnimationTriggers)
  i648.m_Interactable = !!i649[6]
  request.r(i649[7], i649[8], 0, i648, 'm_TargetGraphic')
  return i648
}

Deserializers["UnityEngine.UI.Button+ButtonClickedEvent"] = function (request, data, root) {
  var i650 = root || request.c( 'UnityEngine.UI.Button+ButtonClickedEvent' )
  var i651 = data
  i650.m_PersistentCalls = request.d('UnityEngine.Events.PersistentCallGroup', i651[0], i650.m_PersistentCalls)
  return i650
}

Deserializers["UnityEngine.Events.PersistentCallGroup"] = function (request, data, root) {
  var i652 = root || request.c( 'UnityEngine.Events.PersistentCallGroup' )
  var i653 = data
  var i655 = i653[0]
  var i654 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Events.PersistentCall')))
  for(var i = 0; i < i655.length; i += 1) {
    i654.add(request.d('UnityEngine.Events.PersistentCall', i655[i + 0]));
  }
  i652.m_Calls = i654
  return i652
}

Deserializers["UnityEngine.Events.PersistentCall"] = function (request, data, root) {
  var i658 = root || request.c( 'UnityEngine.Events.PersistentCall' )
  var i659 = data
  request.r(i659[0], i659[1], 0, i658, 'm_Target')
  i658.m_TargetAssemblyTypeName = i659[2]
  i658.m_MethodName = i659[3]
  i658.m_Mode = i659[4]
  i658.m_Arguments = request.d('UnityEngine.Events.ArgumentCache', i659[5], i658.m_Arguments)
  i658.m_CallState = i659[6]
  return i658
}

Deserializers["UnityEngine.UI.Navigation"] = function (request, data, root) {
  var i660 = root || request.c( 'UnityEngine.UI.Navigation' )
  var i661 = data
  i660.m_Mode = i661[0]
  i660.m_WrapAround = !!i661[1]
  request.r(i661[2], i661[3], 0, i660, 'm_SelectOnUp')
  request.r(i661[4], i661[5], 0, i660, 'm_SelectOnDown')
  request.r(i661[6], i661[7], 0, i660, 'm_SelectOnLeft')
  request.r(i661[8], i661[9], 0, i660, 'm_SelectOnRight')
  return i660
}

Deserializers["UnityEngine.UI.ColorBlock"] = function (request, data, root) {
  var i662 = root || request.c( 'UnityEngine.UI.ColorBlock' )
  var i663 = data
  i662.m_NormalColor = new pc.Color(i663[0], i663[1], i663[2], i663[3])
  i662.m_HighlightedColor = new pc.Color(i663[4], i663[5], i663[6], i663[7])
  i662.m_PressedColor = new pc.Color(i663[8], i663[9], i663[10], i663[11])
  i662.m_SelectedColor = new pc.Color(i663[12], i663[13], i663[14], i663[15])
  i662.m_DisabledColor = new pc.Color(i663[16], i663[17], i663[18], i663[19])
  i662.m_ColorMultiplier = i663[20]
  i662.m_FadeDuration = i663[21]
  return i662
}

Deserializers["UnityEngine.UI.SpriteState"] = function (request, data, root) {
  var i664 = root || request.c( 'UnityEngine.UI.SpriteState' )
  var i665 = data
  request.r(i665[0], i665[1], 0, i664, 'm_HighlightedSprite')
  request.r(i665[2], i665[3], 0, i664, 'm_PressedSprite')
  request.r(i665[4], i665[5], 0, i664, 'm_SelectedSprite')
  request.r(i665[6], i665[7], 0, i664, 'm_DisabledSprite')
  return i664
}

Deserializers["UnityEngine.UI.AnimationTriggers"] = function (request, data, root) {
  var i666 = root || request.c( 'UnityEngine.UI.AnimationTriggers' )
  var i667 = data
  i666.m_NormalTrigger = i667[0]
  i666.m_HighlightedTrigger = i667[1]
  i666.m_PressedTrigger = i667[2]
  i666.m_SelectedTrigger = i667[3]
  i666.m_DisabledTrigger = i667[4]
  return i666
}

Deserializers["TMPro.TextMeshProUGUI"] = function (request, data, root) {
  var i668 = root || request.c( 'TMPro.TextMeshProUGUI' )
  var i669 = data
  i668.m_hasFontAssetChanged = !!i669[0]
  request.r(i669[1], i669[2], 0, i668, 'm_baseMaterial')
  i668.m_maskOffset = new pc.Vec4( i669[3], i669[4], i669[5], i669[6] )
  i668.m_text = i669[7]
  i668.m_isRightToLeft = !!i669[8]
  request.r(i669[9], i669[10], 0, i668, 'm_fontAsset')
  request.r(i669[11], i669[12], 0, i668, 'm_sharedMaterial')
  var i671 = i669[13]
  var i670 = []
  for(var i = 0; i < i671.length; i += 2) {
  request.r(i671[i + 0], i671[i + 1], 2, i670, '')
  }
  i668.m_fontSharedMaterials = i670
  request.r(i669[14], i669[15], 0, i668, 'm_fontMaterial')
  var i673 = i669[16]
  var i672 = []
  for(var i = 0; i < i673.length; i += 2) {
  request.r(i673[i + 0], i673[i + 1], 2, i672, '')
  }
  i668.m_fontMaterials = i672
  i668.m_fontColor32 = UnityEngine.Color32.ConstructColor(i669[17], i669[18], i669[19], i669[20])
  i668.m_fontColor = new pc.Color(i669[21], i669[22], i669[23], i669[24])
  i668.m_enableVertexGradient = !!i669[25]
  i668.m_colorMode = i669[26]
  i668.m_fontColorGradient = request.d('TMPro.VertexGradient', i669[27], i668.m_fontColorGradient)
  request.r(i669[28], i669[29], 0, i668, 'm_fontColorGradientPreset')
  request.r(i669[30], i669[31], 0, i668, 'm_spriteAsset')
  i668.m_tintAllSprites = !!i669[32]
  request.r(i669[33], i669[34], 0, i668, 'm_StyleSheet')
  i668.m_TextStyleHashCode = i669[35]
  i668.m_overrideHtmlColors = !!i669[36]
  i668.m_faceColor = UnityEngine.Color32.ConstructColor(i669[37], i669[38], i669[39], i669[40])
  i668.m_fontSize = i669[41]
  i668.m_fontSizeBase = i669[42]
  i668.m_fontWeight = i669[43]
  i668.m_enableAutoSizing = !!i669[44]
  i668.m_fontSizeMin = i669[45]
  i668.m_fontSizeMax = i669[46]
  i668.m_fontStyle = i669[47]
  i668.m_HorizontalAlignment = i669[48]
  i668.m_VerticalAlignment = i669[49]
  i668.m_textAlignment = i669[50]
  i668.m_characterSpacing = i669[51]
  i668.m_characterHorizontalScale = i669[52]
  i668.m_wordSpacing = i669[53]
  i668.m_lineSpacing = i669[54]
  i668.m_lineSpacingMax = i669[55]
  i668.m_paragraphSpacing = i669[56]
  i668.m_charWidthMaxAdj = i669[57]
  i668.m_TextWrappingMode = i669[58]
  i668.m_wordWrappingRatios = i669[59]
  i668.m_overflowMode = i669[60]
  request.r(i669[61], i669[62], 0, i668, 'm_linkedTextComponent')
  request.r(i669[63], i669[64], 0, i668, 'parentLinkedComponent')
  i668.m_enableKerning = !!i669[65]
  var i675 = i669[66]
  var i674 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.TextCore.OTL_FeatureTag')))
  for(var i = 0; i < i675.length; i += 1) {
    i674.add(i675[i + 0]);
  }
  i668.m_ActiveFontFeatures = i674
  i668.m_enableExtraPadding = !!i669[67]
  i668.checkPaddingRequired = !!i669[68]
  i668.m_isRichText = !!i669[69]
  i668.m_parseCtrlCharacters = !!i669[70]
  i668.m_isOrthographic = !!i669[71]
  i668.m_isCullingEnabled = !!i669[72]
  i668.m_horizontalMapping = i669[73]
  i668.m_verticalMapping = i669[74]
  i668.m_uvLineOffset = i669[75]
  i668.m_geometrySortingOrder = i669[76]
  i668.m_IsTextObjectScaleStatic = !!i669[77]
  i668.m_VertexBufferAutoSizeReduction = !!i669[78]
  i668.m_useMaxVisibleDescender = !!i669[79]
  i668.m_pageToDisplay = i669[80]
  i668.m_margin = new pc.Vec4( i669[81], i669[82], i669[83], i669[84] )
  i668.m_isUsingLegacyAnimationComponent = !!i669[85]
  i668.m_isVolumetricText = !!i669[86]
  request.r(i669[87], i669[88], 0, i668, 'm_Material')
  i668.m_EmojiFallbackSupport = !!i669[89]
  i668.m_Maskable = !!i669[90]
  i668.m_Color = new pc.Color(i669[91], i669[92], i669[93], i669[94])
  i668.m_RaycastTarget = !!i669[95]
  i668.m_RaycastPadding = new pc.Vec4( i669[96], i669[97], i669[98], i669[99] )
  return i668
}

Deserializers["TMPro.VertexGradient"] = function (request, data, root) {
  var i676 = root || request.c( 'TMPro.VertexGradient' )
  var i677 = data
  i676.topLeft = new pc.Color(i677[0], i677[1], i677[2], i677[3])
  i676.topRight = new pc.Color(i677[4], i677[5], i677[6], i677[7])
  i676.bottomLeft = new pc.Color(i677[8], i677[9], i677[10], i677[11])
  i676.bottomRight = new pc.Color(i677[12], i677[13], i677[14], i677[15])
  return i676
}

Deserializers["UnityEngine.EventSystems.EventSystem"] = function (request, data, root) {
  var i680 = root || request.c( 'UnityEngine.EventSystems.EventSystem' )
  var i681 = data
  request.r(i681[0], i681[1], 0, i680, 'm_FirstSelected')
  i680.m_sendNavigationEvents = !!i681[2]
  i680.m_DragThreshold = i681[3]
  return i680
}

Deserializers["UnityEngine.InputSystem.UI.InputSystemUIInputModule"] = function (request, data, root) {
  var i682 = root || request.c( 'UnityEngine.InputSystem.UI.InputSystemUIInputModule' )
  var i683 = data
  i682.m_MoveRepeatDelay = i683[0]
  i682.m_MoveRepeatRate = i683[1]
  request.r(i683[2], i683[3], 0, i682, 'm_XRTrackingOrigin')
  request.r(i683[4], i683[5], 0, i682, 'm_ActionsAsset')
  request.r(i683[6], i683[7], 0, i682, 'm_PointAction')
  request.r(i683[8], i683[9], 0, i682, 'm_MoveAction')
  request.r(i683[10], i683[11], 0, i682, 'm_SubmitAction')
  request.r(i683[12], i683[13], 0, i682, 'm_CancelAction')
  request.r(i683[14], i683[15], 0, i682, 'm_LeftClickAction')
  request.r(i683[16], i683[17], 0, i682, 'm_MiddleClickAction')
  request.r(i683[18], i683[19], 0, i682, 'm_RightClickAction')
  request.r(i683[20], i683[21], 0, i682, 'm_ScrollWheelAction')
  request.r(i683[22], i683[23], 0, i682, 'm_TrackedDevicePositionAction')
  request.r(i683[24], i683[25], 0, i682, 'm_TrackedDeviceOrientationAction')
  i682.m_DeselectOnBackgroundClick = !!i683[26]
  i682.m_PointerBehavior = i683[27]
  i682.m_CursorLockBehavior = i683[28]
  i682.m_ScrollDeltaPerTick = i683[29]
  i682.m_SendPointerHoverToParent = !!i683[30]
  return i682
}

Deserializers["PlayableSmokeController"] = function (request, data, root) {
  var i684 = root || request.c( 'PlayableSmokeController' )
  var i685 = data
  request.r(i685[0], i685[1], 0, i684, 'mainCamera')
  request.r(i685[2], i685[3], 0, i684, 'block')
  request.r(i685[4], i685[5], 0, i684, 'exitPoint')
  request.r(i685[6], i685[7], 0, i684, 'endCard')
  request.r(i685[8], i685[9], 0, i684, 'ctaButton')
  i684.moveDuration = i685[10]
  return i684
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings"] = function (request, data, root) {
  var i686 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings' )
  var i687 = data
  i686.ambientIntensity = i687[0]
  i686.reflectionIntensity = i687[1]
  i686.ambientMode = i687[2]
  i686.ambientLight = new pc.Color(i687[3], i687[4], i687[5], i687[6])
  i686.ambientSkyColor = new pc.Color(i687[7], i687[8], i687[9], i687[10])
  i686.ambientGroundColor = new pc.Color(i687[11], i687[12], i687[13], i687[14])
  i686.ambientEquatorColor = new pc.Color(i687[15], i687[16], i687[17], i687[18])
  i686.fogColor = new pc.Color(i687[19], i687[20], i687[21], i687[22])
  i686.fogEndDistance = i687[23]
  i686.fogStartDistance = i687[24]
  i686.fogDensity = i687[25]
  i686.fog = !!i687[26]
  request.r(i687[27], i687[28], 0, i686, 'skybox')
  i686.fogMode = i687[29]
  var i689 = i687[30]
  var i688 = []
  for(var i = 0; i < i689.length; i += 1) {
    i688.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap', i689[i + 0]) );
  }
  i686.lightmaps = i688
  i686.lightProbes = request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes', i687[31], i686.lightProbes)
  i686.lightmapsMode = i687[32]
  i686.mixedBakeMode = i687[33]
  i686.environmentLightingMode = i687[34]
  i686.ambientProbe = new pc.SphericalHarmonicsL2(i687[35])
  request.r(i687[36], i687[37], 0, i686, 'customReflection')
  request.r(i687[38], i687[39], 0, i686, 'defaultReflection')
  i686.defaultReflectionMode = i687[40]
  i686.defaultReflectionResolution = i687[41]
  i686.sunLightObjectId = i687[42]
  i686.pixelLightCount = i687[43]
  i686.defaultReflectionHDR = !!i687[44]
  i686.hasLightDataAsset = !!i687[45]
  i686.hasManualGenerate = !!i687[46]
  return i686
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap"] = function (request, data, root) {
  var i692 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap' )
  var i693 = data
  request.r(i693[0], i693[1], 0, i692, 'lightmapColor')
  request.r(i693[2], i693[3], 0, i692, 'lightmapDirection')
  request.r(i693[4], i693[5], 0, i692, 'shadowMask')
  return i692
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes"] = function (request, data, root) {
  var i694 = root || new UnityEngine.LightProbes()
  var i695 = data
  return i694
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.UniversalRenderPipelineAsset"] = function (request, data, root) {
  var i702 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.UniversalRenderPipelineAsset' )
  var i703 = data
  i702.AdditionalLightsRenderingMode = i703[0]
  i702.LightRenderingMode = request.d('Luna.Unity.DTO.UnityEngine.Assets.LightRenderingMode', i703[1], i702.LightRenderingMode)
  i702.MainLightRenderingModeValue = i703[2]
  i702.SupportsMainLightShadows = !!i703[3]
  i702.MixedLightingSupported = !!i703[4]
  i702.MainLightShadowmapResolutionValue = i703[5]
  i702.SupportsSoftShadows = !!i703[6]
  i702.SoftShadowQualityValue = i703[7]
  i702.ShadowDistance = i703[8]
  i702.ShadowCascadeCount = i703[9]
  i702.Cascade2Split = i703[10]
  i702.Cascade3Split = new pc.Vec2( i703[11], i703[12] )
  i702.Cascade4Split = new pc.Vec3( i703[13], i703[14], i703[15] )
  i702.CascadeBorder = i703[16]
  i702.ShadowDepthBias = i703[17]
  i702.ShadowNormalBias = i703[18]
  i702.RequireDepthTexture = !!i703[19]
  i702.RequireOpaqueTexture = !!i703[20]
  i702.scriptableRendererData = request.d('Luna.Unity.DTO.UnityEngine.Assets.ScriptableRendererData', i703[21], i702.scriptableRendererData)
  return i702
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.LightRenderingMode"] = function (request, data, root) {
  var i704 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.LightRenderingMode' )
  var i705 = data
  i704.Disabled = i705[0]
  i704.PerVertex = i705[1]
  i704.PerPixel = i705[2]
  return i704
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ScriptableRendererData"] = function (request, data, root) {
  var i706 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ScriptableRendererData' )
  var i707 = data
  i706.opaqueLayerMask = i707[0]
  i706.transparentLayerMask = i707[1]
  var i709 = i707[2]
  var i708 = []
  for(var i = 0; i < i709.length; i += 1) {
    i708.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderObjects', i709[i + 0]) );
  }
  i706.RenderObjectsFeatures = i708
  i706.name = i707[3]
  return i706
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderObjects"] = function (request, data, root) {
  var i712 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderObjects' )
  var i713 = data
  i712.settings = request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+RenderObjectsSettings', i713[0], i712.settings)
  i712.name = i713[1]
  i712.typeName = i713[2]
  return i712
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader"] = function (request, data, root) {
  var i714 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader' )
  var i715 = data
  var i717 = i715[0]
  var i716 = new (System.Collections.Generic.List$1(Bridge.ns('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError')))
  for(var i = 0; i < i717.length; i += 1) {
    i716.add(request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError', i717[i + 0]));
  }
  i714.ShaderCompilationErrors = i716
  i714.name = i715[1]
  i714.guid = i715[2]
  var i719 = i715[3]
  var i718 = []
  for(var i = 0; i < i719.length; i += 1) {
    i718.push( i719[i + 0] );
  }
  i714.shaderDefinedKeywords = i718
  var i721 = i715[4]
  var i720 = []
  for(var i = 0; i < i721.length; i += 1) {
    i720.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass', i721[i + 0]) );
  }
  i714.passes = i720
  var i723 = i715[5]
  var i722 = []
  for(var i = 0; i < i723.length; i += 1) {
    i722.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass', i723[i + 0]) );
  }
  i714.usePasses = i722
  var i725 = i715[6]
  var i724 = []
  for(var i = 0; i < i725.length; i += 1) {
    i724.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue', i725[i + 0]) );
  }
  i714.defaultParameterValues = i724
  request.r(i715[7], i715[8], 0, i714, 'unityFallbackShader')
  i714.readDepth = !!i715[9]
  i714.hasDepthOnlyPass = !!i715[10]
  i714.isCreatedByShaderGraph = !!i715[11]
  i714.disableBatching = !!i715[12]
  i714.compiled = !!i715[13]
  return i714
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError"] = function (request, data, root) {
  var i728 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError' )
  var i729 = data
  i728.shaderName = i729[0]
  i728.errorMessage = i729[1]
  return i728
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass"] = function (request, data, root) {
  var i734 = root || new pc.UnityShaderPass()
  var i735 = data
  i734.id = i735[0]
  i734.subShaderIndex = i735[1]
  i734.name = i735[2]
  i734.passType = i735[3]
  i734.grabPassTextureName = i735[4]
  i734.usePass = !!i735[5]
  i734.zTest = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i735[6], i734.zTest)
  i734.zWrite = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i735[7], i734.zWrite)
  i734.culling = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i735[8], i734.culling)
  i734.blending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i735[9], i734.blending)
  i734.alphaBlending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i735[10], i734.alphaBlending)
  i734.colorWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i735[11], i734.colorWriteMask)
  i734.offsetUnits = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i735[12], i734.offsetUnits)
  i734.offsetFactor = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i735[13], i734.offsetFactor)
  i734.stencilRef = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i735[14], i734.stencilRef)
  i734.stencilReadMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i735[15], i734.stencilReadMask)
  i734.stencilWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i735[16], i734.stencilWriteMask)
  i734.stencilOp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i735[17], i734.stencilOp)
  i734.stencilOpFront = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i735[18], i734.stencilOpFront)
  i734.stencilOpBack = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i735[19], i734.stencilOpBack)
  var i737 = i735[20]
  var i736 = []
  for(var i = 0; i < i737.length; i += 1) {
    i736.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag', i737[i + 0]) );
  }
  i734.tags = i736
  var i739 = i735[21]
  var i738 = []
  for(var i = 0; i < i739.length; i += 1) {
    i738.push( i739[i + 0] );
  }
  i734.passDefinedKeywords = i738
  var i741 = i735[22]
  var i740 = []
  for(var i = 0; i < i741.length; i += 1) {
    i740.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup', i741[i + 0]) );
  }
  i734.passDefinedKeywordGroups = i740
  var i743 = i735[23]
  var i742 = []
  for(var i = 0; i < i743.length; i += 1) {
    i742.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i743[i + 0]) );
  }
  i734.variants = i742
  var i745 = i735[24]
  var i744 = []
  for(var i = 0; i < i745.length; i += 1) {
    i744.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i745[i + 0]) );
  }
  i734.excludedVariants = i744
  i734.hasDepthReader = !!i735[25]
  return i734
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value"] = function (request, data, root) {
  var i746 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value' )
  var i747 = data
  i746.val = i747[0]
  i746.name = i747[1]
  return i746
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending"] = function (request, data, root) {
  var i748 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending' )
  var i749 = data
  i748.src = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i749[0], i748.src)
  i748.dst = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i749[1], i748.dst)
  i748.op = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i749[2], i748.op)
  return i748
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp"] = function (request, data, root) {
  var i750 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp' )
  var i751 = data
  i750.pass = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i751[0], i750.pass)
  i750.fail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i751[1], i750.fail)
  i750.zFail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i751[2], i750.zFail)
  i750.comp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i751[3], i750.comp)
  return i750
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag"] = function (request, data, root) {
  var i754 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag' )
  var i755 = data
  i754.name = i755[0]
  i754.value = i755[1]
  return i754
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup"] = function (request, data, root) {
  var i758 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup' )
  var i759 = data
  var i761 = i759[0]
  var i760 = []
  for(var i = 0; i < i761.length; i += 1) {
    i760.push( i761[i + 0] );
  }
  i758.keywords = i760
  i758.hasDiscard = !!i759[1]
  return i758
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant"] = function (request, data, root) {
  var i764 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant' )
  var i765 = data
  i764.passId = i765[0]
  i764.subShaderIndex = i765[1]
  var i767 = i765[2]
  var i766 = []
  for(var i = 0; i < i767.length; i += 1) {
    i766.push( i767[i + 0] );
  }
  i764.keywords = i766
  i764.vertexProgram = i765[3]
  i764.fragmentProgram = i765[4]
  i764.exportedForWebGl2 = !!i765[5]
  i764.readDepth = !!i765[6]
  return i764
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass"] = function (request, data, root) {
  var i770 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass' )
  var i771 = data
  request.r(i771[0], i771[1], 0, i770, 'shader')
  i770.pass = i771[2]
  return i770
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue"] = function (request, data, root) {
  var i774 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue' )
  var i775 = data
  i774.name = i775[0]
  i774.type = i775[1]
  i774.value = new pc.Vec4( i775[2], i775[3], i775[4], i775[5] )
  i774.textureValue = i775[6]
  i774.shaderPropertyFlag = i775[7]
  return i774
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Sprite"] = function (request, data, root) {
  var i776 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Sprite' )
  var i777 = data
  i776.name = i777[0]
  request.r(i777[1], i777[2], 0, i776, 'texture')
  i776.aabb = i777[3]
  i776.vertices = i777[4]
  i776.triangles = i777[5]
  i776.textureRect = UnityEngine.Rect.MinMaxRect(i777[6], i777[7], i777[8], i777[9])
  i776.packedRect = UnityEngine.Rect.MinMaxRect(i777[10], i777[11], i777[12], i777[13])
  i776.border = new pc.Vec4( i777[14], i777[15], i777[16], i777[17] )
  i776.transparency = i777[18]
  i776.bounds = i777[19]
  i776.pixelsPerUnit = i777[20]
  i776.textureWidth = i777[21]
  i776.textureHeight = i777[22]
  i776.nativeSize = new pc.Vec2( i777[23], i777[24] )
  i776.pivot = new pc.Vec2( i777[25], i777[26] )
  i776.textureRectOffset = new pc.Vec2( i777[27], i777[28] )
  return i776
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Font"] = function (request, data, root) {
  var i778 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Font' )
  var i779 = data
  i778.name = i779[0]
  i778.ascent = i779[1]
  i778.originalLineHeight = i779[2]
  i778.fontSize = i779[3]
  var i781 = i779[4]
  var i780 = []
  for(var i = 0; i < i781.length; i += 1) {
    i780.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo', i781[i + 0]) );
  }
  i778.characterInfo = i780
  request.r(i779[5], i779[6], 0, i778, 'texture')
  i778.originalFontSize = i779[7]
  return i778
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo"] = function (request, data, root) {
  var i784 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo' )
  var i785 = data
  i784.index = i785[0]
  i784.advance = i785[1]
  i784.bearing = i785[2]
  i784.glyphWidth = i785[3]
  i784.glyphHeight = i785[4]
  i784.minX = i785[5]
  i784.maxX = i785[6]
  i784.minY = i785[7]
  i784.maxY = i785[8]
  i784.uvBottomLeftX = i785[9]
  i784.uvBottomLeftY = i785[10]
  i784.uvBottomRightX = i785[11]
  i784.uvBottomRightY = i785[12]
  i784.uvTopLeftX = i785[13]
  i784.uvTopLeftY = i785[14]
  i784.uvTopRightX = i785[15]
  i784.uvTopRightY = i785[16]
  return i784
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.TextAsset"] = function (request, data, root) {
  var i786 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.TextAsset' )
  var i787 = data
  i786.name = i787[0]
  i786.bytes64 = i787[1]
  i786.data = i787[2]
  return i786
}

Deserializers["TMPro.TMP_FontAsset"] = function (request, data, root) {
  var i788 = root || request.c( 'TMPro.TMP_FontAsset' )
  var i789 = data
  i788.normalStyle = i789[0]
  i788.normalSpacingOffset = i789[1]
  i788.boldStyle = i789[2]
  i788.boldSpacing = i789[3]
  i788.italicStyle = i789[4]
  i788.tabSize = i789[5]
  request.r(i789[6], i789[7], 0, i788, 'atlas')
  i788.m_SourceFontFileGUID = i789[8]
  i788.m_CreationSettings = request.d('TMPro.FontAssetCreationSettings', i789[9], i788.m_CreationSettings)
  request.r(i789[10], i789[11], 0, i788, 'm_SourceFontFile')
  i788.m_SourceFontFilePath = i789[12]
  i788.m_AtlasPopulationMode = i789[13]
  i788.InternalDynamicOS = !!i789[14]
  var i791 = i789[15]
  var i790 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.TextCore.Glyph')))
  for(var i = 0; i < i791.length; i += 1) {
    i790.add(request.d('UnityEngine.TextCore.Glyph', i791[i + 0]));
  }
  i788.m_GlyphTable = i790
  var i793 = i789[16]
  var i792 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_Character')))
  for(var i = 0; i < i793.length; i += 1) {
    i792.add(request.d('TMPro.TMP_Character', i793[i + 0]));
  }
  i788.m_CharacterTable = i792
  var i795 = i789[17]
  var i794 = []
  for(var i = 0; i < i795.length; i += 2) {
  request.r(i795[i + 0], i795[i + 1], 2, i794, '')
  }
  i788.m_AtlasTextures = i794
  i788.m_AtlasTextureIndex = i789[18]
  i788.m_IsMultiAtlasTexturesEnabled = !!i789[19]
  i788.m_GetFontFeatures = !!i789[20]
  i788.m_ClearDynamicDataOnBuild = !!i789[21]
  i788.m_AtlasWidth = i789[22]
  i788.m_AtlasHeight = i789[23]
  i788.m_AtlasPadding = i789[24]
  i788.m_AtlasRenderMode = i789[25]
  var i797 = i789[26]
  var i796 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.TextCore.GlyphRect')))
  for(var i = 0; i < i797.length; i += 1) {
    i796.add(request.d('UnityEngine.TextCore.GlyphRect', i797[i + 0]));
  }
  i788.m_UsedGlyphRects = i796
  var i799 = i789[27]
  var i798 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.TextCore.GlyphRect')))
  for(var i = 0; i < i799.length; i += 1) {
    i798.add(request.d('UnityEngine.TextCore.GlyphRect', i799[i + 0]));
  }
  i788.m_FreeGlyphRects = i798
  i788.m_FontFeatureTable = request.d('TMPro.TMP_FontFeatureTable', i789[28], i788.m_FontFeatureTable)
  i788.m_ShouldReimportFontFeatures = !!i789[29]
  var i801 = i789[30]
  var i800 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_FontAsset')))
  for(var i = 0; i < i801.length; i += 2) {
  request.r(i801[i + 0], i801[i + 1], 1, i800, '')
  }
  i788.m_FallbackFontAssetTable = i800
  var i803 = i789[31]
  var i802 = []
  for(var i = 0; i < i803.length; i += 1) {
    i802.push( request.d('TMPro.TMP_FontWeightPair', i803[i + 0]) );
  }
  i788.m_FontWeightTable = i802
  var i805 = i789[32]
  var i804 = []
  for(var i = 0; i < i805.length; i += 1) {
    i804.push( request.d('TMPro.TMP_FontWeightPair', i805[i + 0]) );
  }
  i788.fontWeights = i804
  i788.m_fontInfo = request.d('TMPro.FaceInfo_Legacy', i789[33], i788.m_fontInfo)
  var i807 = i789[34]
  var i806 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_Glyph')))
  for(var i = 0; i < i807.length; i += 1) {
    i806.add(request.d('TMPro.TMP_Glyph', i807[i + 0]));
  }
  i788.m_glyphInfoList = i806
  i788.m_KerningTable = request.d('TMPro.KerningTable', i789[35], i788.m_KerningTable)
  var i809 = i789[36]
  var i808 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_FontAsset')))
  for(var i = 0; i < i809.length; i += 2) {
  request.r(i809[i + 0], i809[i + 1], 1, i808, '')
  }
  i788.fallbackFontAssets = i808
  i788.m_Version = i789[37]
  i788.m_FaceInfo = request.d('UnityEngine.TextCore.FaceInfo', i789[38], i788.m_FaceInfo)
  request.r(i789[39], i789[40], 0, i788, 'm_Material')
  return i788
}

Deserializers["TMPro.FontAssetCreationSettings"] = function (request, data, root) {
  var i810 = root || request.c( 'TMPro.FontAssetCreationSettings' )
  var i811 = data
  i810.sourceFontFileName = i811[0]
  i810.sourceFontFileGUID = i811[1]
  i810.faceIndex = i811[2]
  i810.pointSizeSamplingMode = i811[3]
  i810.pointSize = i811[4]
  i810.padding = i811[5]
  i810.paddingMode = i811[6]
  i810.packingMode = i811[7]
  i810.atlasWidth = i811[8]
  i810.atlasHeight = i811[9]
  i810.characterSetSelectionMode = i811[10]
  i810.characterSequence = i811[11]
  i810.referencedFontAssetGUID = i811[12]
  i810.referencedTextAssetGUID = i811[13]
  i810.fontStyle = i811[14]
  i810.fontStyleModifier = i811[15]
  i810.renderMode = i811[16]
  i810.includeFontFeatures = !!i811[17]
  return i810
}

Deserializers["UnityEngine.TextCore.Glyph"] = function (request, data, root) {
  var i814 = root || request.c( 'UnityEngine.TextCore.Glyph' )
  var i815 = data
  i814.m_Index = i815[0]
  i814.m_Metrics = request.d('UnityEngine.TextCore.GlyphMetrics', i815[1], i814.m_Metrics)
  i814.m_GlyphRect = request.d('UnityEngine.TextCore.GlyphRect', i815[2], i814.m_GlyphRect)
  i814.m_Scale = i815[3]
  i814.m_AtlasIndex = i815[4]
  i814.m_ClassDefinitionType = i815[5]
  return i814
}

Deserializers["UnityEngine.TextCore.GlyphMetrics"] = function (request, data, root) {
  var i816 = root || request.c( 'UnityEngine.TextCore.GlyphMetrics' )
  var i817 = data
  i816.m_Width = i817[0]
  i816.m_Height = i817[1]
  i816.m_HorizontalBearingX = i817[2]
  i816.m_HorizontalBearingY = i817[3]
  i816.m_HorizontalAdvance = i817[4]
  return i816
}

Deserializers["UnityEngine.TextCore.GlyphRect"] = function (request, data, root) {
  var i818 = root || request.c( 'UnityEngine.TextCore.GlyphRect' )
  var i819 = data
  i818.m_X = i819[0]
  i818.m_Y = i819[1]
  i818.m_Width = i819[2]
  i818.m_Height = i819[3]
  return i818
}

Deserializers["TMPro.TMP_Character"] = function (request, data, root) {
  var i822 = root || request.c( 'TMPro.TMP_Character' )
  var i823 = data
  i822.m_ElementType = i823[0]
  i822.m_Unicode = i823[1]
  i822.m_GlyphIndex = i823[2]
  i822.m_Scale = i823[3]
  return i822
}

Deserializers["TMPro.TMP_FontFeatureTable"] = function (request, data, root) {
  var i828 = root || request.c( 'TMPro.TMP_FontFeatureTable' )
  var i829 = data
  var i831 = i829[0]
  var i830 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.MultipleSubstitutionRecord')))
  for(var i = 0; i < i831.length; i += 1) {
    i830.add(request.d('TMPro.MultipleSubstitutionRecord', i831[i + 0]));
  }
  i828.m_MultipleSubstitutionRecords = i830
  var i833 = i829[1]
  var i832 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.LigatureSubstitutionRecord')))
  for(var i = 0; i < i833.length; i += 1) {
    i832.add(request.d('TMPro.LigatureSubstitutionRecord', i833[i + 0]));
  }
  i828.m_LigatureSubstitutionRecords = i832
  var i835 = i829[2]
  var i834 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord')))
  for(var i = 0; i < i835.length; i += 1) {
    i834.add(request.d('UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord', i835[i + 0]));
  }
  i828.m_GlyphPairAdjustmentRecords = i834
  var i837 = i829[3]
  var i836 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.MarkToBaseAdjustmentRecord')))
  for(var i = 0; i < i837.length; i += 1) {
    i836.add(request.d('TMPro.MarkToBaseAdjustmentRecord', i837[i + 0]));
  }
  i828.m_MarkToBaseAdjustmentRecords = i836
  var i839 = i829[4]
  var i838 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.MarkToMarkAdjustmentRecord')))
  for(var i = 0; i < i839.length; i += 1) {
    i838.add(request.d('TMPro.MarkToMarkAdjustmentRecord', i839[i + 0]));
  }
  i828.m_MarkToMarkAdjustmentRecords = i838
  return i828
}

Deserializers["TMPro.MultipleSubstitutionRecord"] = function (request, data, root) {
  var i842 = root || request.c( 'TMPro.MultipleSubstitutionRecord' )
  var i843 = data
  i842.m_TargetGlyphID = i843[0]
  i842.m_SubstituteGlyphIDs = i843[1]
  return i842
}

Deserializers["TMPro.LigatureSubstitutionRecord"] = function (request, data, root) {
  var i846 = root || request.c( 'TMPro.LigatureSubstitutionRecord' )
  var i847 = data
  i846.m_ComponentGlyphIDs = i847[0]
  i846.m_LigatureGlyphID = i847[1]
  return i846
}

Deserializers["UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord"] = function (request, data, root) {
  var i850 = root || request.c( 'UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord' )
  var i851 = data
  i850.m_FirstAdjustmentRecord = request.d('UnityEngine.TextCore.LowLevel.GlyphAdjustmentRecord', i851[0], i850.m_FirstAdjustmentRecord)
  i850.m_SecondAdjustmentRecord = request.d('UnityEngine.TextCore.LowLevel.GlyphAdjustmentRecord', i851[1], i850.m_SecondAdjustmentRecord)
  i850.m_FeatureLookupFlags = i851[2]
  return i850
}

Deserializers["UnityEngine.TextCore.LowLevel.GlyphAdjustmentRecord"] = function (request, data, root) {
  var i852 = root || request.c( 'UnityEngine.TextCore.LowLevel.GlyphAdjustmentRecord' )
  var i853 = data
  i852.m_GlyphIndex = i853[0]
  i852.m_GlyphValueRecord = request.d('UnityEngine.TextCore.LowLevel.GlyphValueRecord', i853[1], i852.m_GlyphValueRecord)
  return i852
}

Deserializers["UnityEngine.TextCore.LowLevel.GlyphValueRecord"] = function (request, data, root) {
  var i854 = root || request.c( 'UnityEngine.TextCore.LowLevel.GlyphValueRecord' )
  var i855 = data
  i854.m_XPlacement = i855[0]
  i854.m_YPlacement = i855[1]
  i854.m_XAdvance = i855[2]
  i854.m_YAdvance = i855[3]
  return i854
}

Deserializers["TMPro.MarkToBaseAdjustmentRecord"] = function (request, data, root) {
  var i858 = root || request.c( 'TMPro.MarkToBaseAdjustmentRecord' )
  var i859 = data
  i858.m_BaseGlyphID = i859[0]
  i858.m_BaseGlyphAnchorPoint = request.d('TMPro.GlyphAnchorPoint', i859[1], i858.m_BaseGlyphAnchorPoint)
  i858.m_MarkGlyphID = i859[2]
  i858.m_MarkPositionAdjustment = request.d('TMPro.MarkPositionAdjustment', i859[3], i858.m_MarkPositionAdjustment)
  return i858
}

Deserializers["TMPro.MarkToMarkAdjustmentRecord"] = function (request, data, root) {
  var i862 = root || request.c( 'TMPro.MarkToMarkAdjustmentRecord' )
  var i863 = data
  i862.m_BaseMarkGlyphID = i863[0]
  i862.m_BaseMarkGlyphAnchorPoint = request.d('TMPro.GlyphAnchorPoint', i863[1], i862.m_BaseMarkGlyphAnchorPoint)
  i862.m_CombiningMarkGlyphID = i863[2]
  i862.m_CombiningMarkPositionAdjustment = request.d('TMPro.MarkPositionAdjustment', i863[3], i862.m_CombiningMarkPositionAdjustment)
  return i862
}

Deserializers["TMPro.TMP_FontWeightPair"] = function (request, data, root) {
  var i868 = root || request.c( 'TMPro.TMP_FontWeightPair' )
  var i869 = data
  request.r(i869[0], i869[1], 0, i868, 'regularTypeface')
  request.r(i869[2], i869[3], 0, i868, 'italicTypeface')
  return i868
}

Deserializers["TMPro.FaceInfo_Legacy"] = function (request, data, root) {
  var i870 = root || request.c( 'TMPro.FaceInfo_Legacy' )
  var i871 = data
  i870.Name = i871[0]
  i870.PointSize = i871[1]
  i870.Scale = i871[2]
  i870.CharacterCount = i871[3]
  i870.LineHeight = i871[4]
  i870.Baseline = i871[5]
  i870.Ascender = i871[6]
  i870.CapHeight = i871[7]
  i870.Descender = i871[8]
  i870.CenterLine = i871[9]
  i870.SuperscriptOffset = i871[10]
  i870.SubscriptOffset = i871[11]
  i870.SubSize = i871[12]
  i870.Underline = i871[13]
  i870.UnderlineThickness = i871[14]
  i870.strikethrough = i871[15]
  i870.strikethroughThickness = i871[16]
  i870.TabWidth = i871[17]
  i870.Padding = i871[18]
  i870.AtlasWidth = i871[19]
  i870.AtlasHeight = i871[20]
  return i870
}

Deserializers["TMPro.TMP_Glyph"] = function (request, data, root) {
  var i874 = root || request.c( 'TMPro.TMP_Glyph' )
  var i875 = data
  i874.id = i875[0]
  i874.x = i875[1]
  i874.y = i875[2]
  i874.width = i875[3]
  i874.height = i875[4]
  i874.xOffset = i875[5]
  i874.yOffset = i875[6]
  i874.xAdvance = i875[7]
  i874.scale = i875[8]
  return i874
}

Deserializers["TMPro.KerningTable"] = function (request, data, root) {
  var i876 = root || request.c( 'TMPro.KerningTable' )
  var i877 = data
  var i879 = i877[0]
  var i878 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.KerningPair')))
  for(var i = 0; i < i879.length; i += 1) {
    i878.add(request.d('TMPro.KerningPair', i879[i + 0]));
  }
  i876.kerningPairs = i878
  return i876
}

Deserializers["TMPro.KerningPair"] = function (request, data, root) {
  var i882 = root || request.c( 'TMPro.KerningPair' )
  var i883 = data
  i882.xOffset = i883[0]
  i882.m_FirstGlyph = i883[1]
  i882.m_FirstGlyphAdjustments = request.d('TMPro.GlyphValueRecord_Legacy', i883[2], i882.m_FirstGlyphAdjustments)
  i882.m_SecondGlyph = i883[3]
  i882.m_SecondGlyphAdjustments = request.d('TMPro.GlyphValueRecord_Legacy', i883[4], i882.m_SecondGlyphAdjustments)
  i882.m_IgnoreSpacingAdjustments = !!i883[5]
  return i882
}

Deserializers["UnityEngine.TextCore.FaceInfo"] = function (request, data, root) {
  var i884 = root || request.c( 'UnityEngine.TextCore.FaceInfo' )
  var i885 = data
  i884.m_FaceIndex = i885[0]
  i884.m_FamilyName = i885[1]
  i884.m_StyleName = i885[2]
  i884.m_PointSize = i885[3]
  i884.m_Scale = i885[4]
  i884.m_UnitsPerEM = i885[5]
  i884.m_LineHeight = i885[6]
  i884.m_AscentLine = i885[7]
  i884.m_CapLine = i885[8]
  i884.m_MeanLine = i885[9]
  i884.m_Baseline = i885[10]
  i884.m_DescentLine = i885[11]
  i884.m_SuperscriptOffset = i885[12]
  i884.m_SuperscriptSize = i885[13]
  i884.m_SubscriptOffset = i885[14]
  i884.m_SubscriptSize = i885[15]
  i884.m_UnderlineOffset = i885[16]
  i884.m_UnderlineThickness = i885[17]
  i884.m_StrikethroughOffset = i885[18]
  i884.m_StrikethroughThickness = i885[19]
  i884.m_TabWidth = i885[20]
  return i884
}

Deserializers["UnityEngine.InputSystem.InputActionAsset"] = function (request, data, root) {
  var i886 = root || request.c( 'UnityEngine.InputSystem.InputActionAsset' )
  var i887 = data
  var i889 = i887[0]
  var i888 = []
  for(var i = 0; i < i889.length; i += 1) {
    i888.push( request.d('UnityEngine.InputSystem.InputActionMap', i889[i + 0]) );
  }
  i886.m_ActionMaps = i888
  var i891 = i887[1]
  var i890 = []
  for(var i = 0; i < i891.length; i += 1) {
    i890.push( request.d('UnityEngine.InputSystem.InputControlScheme', i891[i + 0]) );
  }
  i886.m_ControlSchemes = i890
  i886.m_IsProjectWide = !!i887[2]
  return i886
}

Deserializers["UnityEngine.InputSystem.InputActionMap"] = function (request, data, root) {
  var i894 = root || request.c( 'UnityEngine.InputSystem.InputActionMap' )
  var i895 = data
  i894.m_Name = i895[0]
  i894.m_Id = i895[1]
  request.r(i895[2], i895[3], 0, i894, 'm_Asset')
  var i897 = i895[4]
  var i896 = []
  for(var i = 0; i < i897.length; i += 1) {
    i896.push( request.d('UnityEngine.InputSystem.InputAction', i897[i + 0]) );
  }
  i894.m_Actions = i896
  var i899 = i895[5]
  var i898 = []
  for(var i = 0; i < i899.length; i += 1) {
    i898.push( request.d('UnityEngine.InputSystem.InputBinding', i899[i + 0]) );
  }
  i894.m_Bindings = i898
  return i894
}

Deserializers["UnityEngine.InputSystem.InputAction"] = function (request, data, root) {
  var i902 = root || request.c( 'UnityEngine.InputSystem.InputAction' )
  var i903 = data
  i902.m_Name = i903[0]
  i902.m_Type = i903[1]
  i902.m_ExpectedControlType = i903[2]
  i902.m_Id = i903[3]
  i902.m_Processors = i903[4]
  i902.m_Interactions = i903[5]
  var i905 = i903[6]
  var i904 = []
  for(var i = 0; i < i905.length; i += 1) {
    i904.push( request.d('UnityEngine.InputSystem.InputBinding', i905[i + 0]) );
  }
  i902.m_SingletonActionBindings = i904
  i902.m_Flags = i903[7]
  return i902
}

Deserializers["UnityEngine.InputSystem.InputBinding"] = function (request, data, root) {
  var i908 = root || request.c( 'UnityEngine.InputSystem.InputBinding' )
  var i909 = data
  i908.m_Name = i909[0]
  i908.m_Id = i909[1]
  i908.m_Path = i909[2]
  i908.m_Interactions = i909[3]
  i908.m_Processors = i909[4]
  i908.m_Groups = i909[5]
  i908.m_Action = i909[6]
  i908.m_Flags = i909[7]
  return i908
}

Deserializers["UnityEngine.InputSystem.InputControlScheme"] = function (request, data, root) {
  var i912 = root || request.c( 'UnityEngine.InputSystem.InputControlScheme' )
  var i913 = data
  i912.m_Name = i913[0]
  i912.m_BindingGroup = i913[1]
  var i915 = i913[2]
  var i914 = []
  for(var i = 0; i < i915.length; i += 1) {
    i914.push( request.d('UnityEngine.InputSystem.InputControlScheme+DeviceRequirement', i915[i + 0]) );
  }
  i912.m_DeviceRequirements = i914
  return i912
}

Deserializers["UnityEngine.InputSystem.InputControlScheme+DeviceRequirement"] = function (request, data, root) {
  var i918 = root || request.c( 'UnityEngine.InputSystem.InputControlScheme+DeviceRequirement' )
  var i919 = data
  i918.m_ControlPath = i919[0]
  i918.m_Flags = i919[1]
  return i918
}

Deserializers["UnityEngine.InputSystem.InputActionReference"] = function (request, data, root) {
  var i920 = root || request.c( 'UnityEngine.InputSystem.InputActionReference' )
  var i921 = data
  request.r(i921[0], i921[1], 0, i920, 'm_Asset')
  i920.m_ActionId = i921[2]
  return i920
}

Deserializers["TMPro.TMP_Settings"] = function (request, data, root) {
  var i922 = root || request.c( 'TMPro.TMP_Settings' )
  var i923 = data
  i922.assetVersion = i923[0]
  i922.m_TextWrappingMode = i923[1]
  i922.m_enableKerning = !!i923[2]
  var i925 = i923[3]
  var i924 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.TextCore.OTL_FeatureTag')))
  for(var i = 0; i < i925.length; i += 1) {
    i924.add(i925[i + 0]);
  }
  i922.m_ActiveFontFeatures = i924
  i922.m_enableExtraPadding = !!i923[4]
  i922.m_enableTintAllSprites = !!i923[5]
  i922.m_enableParseEscapeCharacters = !!i923[6]
  i922.m_EnableRaycastTarget = !!i923[7]
  i922.m_GetFontFeaturesAtRuntime = !!i923[8]
  i922.m_missingGlyphCharacter = i923[9]
  i922.m_ClearDynamicDataOnBuild = !!i923[10]
  i922.m_warningsDisabled = !!i923[11]
  request.r(i923[12], i923[13], 0, i922, 'm_defaultFontAsset')
  i922.m_defaultFontAssetPath = i923[14]
  i922.m_defaultFontSize = i923[15]
  i922.m_defaultAutoSizeMinRatio = i923[16]
  i922.m_defaultAutoSizeMaxRatio = i923[17]
  i922.m_defaultTextMeshProTextContainerSize = new pc.Vec2( i923[18], i923[19] )
  i922.m_defaultTextMeshProUITextContainerSize = new pc.Vec2( i923[20], i923[21] )
  i922.m_autoSizeTextContainer = !!i923[22]
  i922.m_IsTextObjectScaleStatic = !!i923[23]
  var i927 = i923[24]
  var i926 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_FontAsset')))
  for(var i = 0; i < i927.length; i += 2) {
  request.r(i927[i + 0], i927[i + 1], 1, i926, '')
  }
  i922.m_fallbackFontAssets = i926
  i922.m_matchMaterialPreset = !!i923[25]
  i922.m_HideSubTextObjects = !!i923[26]
  request.r(i923[27], i923[28], 0, i922, 'm_defaultSpriteAsset')
  i922.m_defaultSpriteAssetPath = i923[29]
  i922.m_enableEmojiSupport = !!i923[30]
  i922.m_MissingCharacterSpriteUnicode = i923[31]
  var i929 = i923[32]
  var i928 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_Asset')))
  for(var i = 0; i < i929.length; i += 2) {
  request.r(i929[i + 0], i929[i + 1], 1, i928, '')
  }
  i922.m_EmojiFallbackTextAssets = i928
  i922.m_defaultColorGradientPresetsPath = i923[33]
  request.r(i923[34], i923[35], 0, i922, 'm_defaultStyleSheet')
  i922.m_StyleSheetsResourcePath = i923[36]
  request.r(i923[37], i923[38], 0, i922, 'm_leadingCharacters')
  request.r(i923[39], i923[40], 0, i922, 'm_followingCharacters')
  i922.m_UseModernHangulLineBreakingRules = !!i923[41]
  return i922
}

Deserializers["TMPro.TMP_SpriteAsset"] = function (request, data, root) {
  var i932 = root || request.c( 'TMPro.TMP_SpriteAsset' )
  var i933 = data
  request.r(i933[0], i933[1], 0, i932, 'spriteSheet')
  var i935 = i933[2]
  var i934 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_Sprite')))
  for(var i = 0; i < i935.length; i += 1) {
    i934.add(request.d('TMPro.TMP_Sprite', i935[i + 0]));
  }
  i932.spriteInfoList = i934
  var i937 = i933[3]
  var i936 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_SpriteAsset')))
  for(var i = 0; i < i937.length; i += 2) {
  request.r(i937[i + 0], i937[i + 1], 1, i936, '')
  }
  i932.fallbackSpriteAssets = i936
  var i939 = i933[4]
  var i938 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_SpriteCharacter')))
  for(var i = 0; i < i939.length; i += 1) {
    i938.add(request.d('TMPro.TMP_SpriteCharacter', i939[i + 0]));
  }
  i932.m_SpriteCharacterTable = i938
  var i941 = i933[5]
  var i940 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_SpriteGlyph')))
  for(var i = 0; i < i941.length; i += 1) {
    i940.add(request.d('TMPro.TMP_SpriteGlyph', i941[i + 0]));
  }
  i932.m_GlyphTable = i940
  i932.m_Version = i933[6]
  i932.m_FaceInfo = request.d('UnityEngine.TextCore.FaceInfo', i933[7], i932.m_FaceInfo)
  request.r(i933[8], i933[9], 0, i932, 'm_Material')
  return i932
}

Deserializers["TMPro.TMP_Sprite"] = function (request, data, root) {
  var i944 = root || request.c( 'TMPro.TMP_Sprite' )
  var i945 = data
  i944.name = i945[0]
  i944.hashCode = i945[1]
  i944.unicode = i945[2]
  i944.pivot = new pc.Vec2( i945[3], i945[4] )
  request.r(i945[5], i945[6], 0, i944, 'sprite')
  i944.id = i945[7]
  i944.x = i945[8]
  i944.y = i945[9]
  i944.width = i945[10]
  i944.height = i945[11]
  i944.xOffset = i945[12]
  i944.yOffset = i945[13]
  i944.xAdvance = i945[14]
  i944.scale = i945[15]
  return i944
}

Deserializers["TMPro.TMP_SpriteCharacter"] = function (request, data, root) {
  var i950 = root || request.c( 'TMPro.TMP_SpriteCharacter' )
  var i951 = data
  i950.m_Name = i951[0]
  i950.m_ElementType = i951[1]
  i950.m_Unicode = i951[2]
  i950.m_GlyphIndex = i951[3]
  i950.m_Scale = i951[4]
  return i950
}

Deserializers["TMPro.TMP_SpriteGlyph"] = function (request, data, root) {
  var i954 = root || request.c( 'TMPro.TMP_SpriteGlyph' )
  var i955 = data
  request.r(i955[0], i955[1], 0, i954, 'sprite')
  i954.m_Index = i955[2]
  i954.m_Metrics = request.d('UnityEngine.TextCore.GlyphMetrics', i955[3], i954.m_Metrics)
  i954.m_GlyphRect = request.d('UnityEngine.TextCore.GlyphRect', i955[4], i954.m_GlyphRect)
  i954.m_Scale = i955[5]
  i954.m_AtlasIndex = i955[6]
  i954.m_ClassDefinitionType = i955[7]
  return i954
}

Deserializers["TMPro.TMP_StyleSheet"] = function (request, data, root) {
  var i956 = root || request.c( 'TMPro.TMP_StyleSheet' )
  var i957 = data
  var i959 = i957[0]
  var i958 = new (System.Collections.Generic.List$1(Bridge.ns('TMPro.TMP_Style')))
  for(var i = 0; i < i959.length; i += 1) {
    i958.add(request.d('TMPro.TMP_Style', i959[i + 0]));
  }
  i956.m_StyleList = i958
  return i956
}

Deserializers["TMPro.TMP_Style"] = function (request, data, root) {
  var i962 = root || request.c( 'TMPro.TMP_Style' )
  var i963 = data
  i962.m_Name = i963[0]
  i962.m_HashCode = i963[1]
  i962.m_OpeningDefinition = i963[2]
  i962.m_ClosingDefinition = i963[3]
  i962.m_OpeningTagArray = i963[4]
  i962.m_ClosingTagArray = i963[5]
  return i962
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources"] = function (request, data, root) {
  var i964 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources' )
  var i965 = data
  var i967 = i965[0]
  var i966 = []
  for(var i = 0; i < i967.length; i += 1) {
    i966.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Resources+File', i967[i + 0]) );
  }
  i964.files = i966
  i964.componentToPrefabIds = i965[1]
  return i964
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources+File"] = function (request, data, root) {
  var i970 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources+File' )
  var i971 = data
  i970.path = i971[0]
  request.r(i971[1], i971[2], 0, i970, 'unityObject')
  return i970
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings"] = function (request, data, root) {
  var i972 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings' )
  var i973 = data
  var i975 = i973[0]
  var i974 = []
  for(var i = 0; i < i975.length; i += 1) {
    i974.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder', i975[i + 0]) );
  }
  i972.scriptsExecutionOrder = i974
  var i977 = i973[1]
  var i976 = []
  for(var i = 0; i < i977.length; i += 1) {
    i976.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer', i977[i + 0]) );
  }
  i972.sortingLayers = i976
  var i979 = i973[2]
  var i978 = []
  for(var i = 0; i < i979.length; i += 1) {
    i978.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer', i979[i + 0]) );
  }
  i972.cullingLayers = i978
  i972.timeSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings', i973[3], i972.timeSettings)
  i972.physicsSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings', i973[4], i972.physicsSettings)
  i972.physics2DSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings', i973[5], i972.physics2DSettings)
  i972.qualitySettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i973[6], i972.qualitySettings)
  i972.enableRealtimeShadows = !!i973[7]
  i972.enableAutoInstancing = !!i973[8]
  i972.enableStaticBatching = !!i973[9]
  i972.enableDynamicBatching = !!i973[10]
  i972.usePreservativeDynamicBatching = !!i973[11]
  i972.lightmapEncodingQuality = i973[12]
  i972.desiredColorSpace = i973[13]
  var i981 = i973[14]
  var i980 = []
  for(var i = 0; i < i981.length; i += 1) {
    i980.push( i981[i + 0] );
  }
  i972.allTags = i980
  return i972
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder"] = function (request, data, root) {
  var i984 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder' )
  var i985 = data
  i984.name = i985[0]
  i984.value = i985[1]
  return i984
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer"] = function (request, data, root) {
  var i988 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer' )
  var i989 = data
  i988.id = i989[0]
  i988.name = i989[1]
  i988.value = i989[2]
  return i988
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer"] = function (request, data, root) {
  var i992 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer' )
  var i993 = data
  i992.id = i993[0]
  i992.name = i993[1]
  return i992
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings"] = function (request, data, root) {
  var i994 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings' )
  var i995 = data
  i994.fixedDeltaTime = i995[0]
  i994.maximumDeltaTime = i995[1]
  i994.timeScale = i995[2]
  i994.maximumParticleTimestep = i995[3]
  return i994
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings"] = function (request, data, root) {
  var i996 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings' )
  var i997 = data
  i996.gravity = new pc.Vec3( i997[0], i997[1], i997[2] )
  i996.defaultSolverIterations = i997[3]
  i996.bounceThreshold = i997[4]
  i996.autoSyncTransforms = !!i997[5]
  i996.autoSimulation = !!i997[6]
  var i999 = i997[7]
  var i998 = []
  for(var i = 0; i < i999.length; i += 1) {
    i998.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask', i999[i + 0]) );
  }
  i996.collisionMatrix = i998
  return i996
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask"] = function (request, data, root) {
  var i1002 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask' )
  var i1003 = data
  i1002.enabled = !!i1003[0]
  i1002.layerId = i1003[1]
  i1002.otherLayerId = i1003[2]
  return i1002
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings"] = function (request, data, root) {
  var i1004 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings' )
  var i1005 = data
  request.r(i1005[0], i1005[1], 0, i1004, 'material')
  i1004.gravity = new pc.Vec2( i1005[2], i1005[3] )
  i1004.positionIterations = i1005[4]
  i1004.velocityIterations = i1005[5]
  i1004.velocityThreshold = i1005[6]
  i1004.maxLinearCorrection = i1005[7]
  i1004.maxAngularCorrection = i1005[8]
  i1004.maxTranslationSpeed = i1005[9]
  i1004.maxRotationSpeed = i1005[10]
  i1004.baumgarteScale = i1005[11]
  i1004.baumgarteTOIScale = i1005[12]
  i1004.timeToSleep = i1005[13]
  i1004.linearSleepTolerance = i1005[14]
  i1004.angularSleepTolerance = i1005[15]
  i1004.defaultContactOffset = i1005[16]
  i1004.autoSimulation = !!i1005[17]
  i1004.queriesHitTriggers = !!i1005[18]
  i1004.queriesStartInColliders = !!i1005[19]
  i1004.callbacksOnDisable = !!i1005[20]
  i1004.reuseCollisionCallbacks = !!i1005[21]
  i1004.autoSyncTransforms = !!i1005[22]
  var i1007 = i1005[23]
  var i1006 = []
  for(var i = 0; i < i1007.length; i += 1) {
    i1006.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask', i1007[i + 0]) );
  }
  i1004.collisionMatrix = i1006
  return i1004
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask"] = function (request, data, root) {
  var i1010 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask' )
  var i1011 = data
  i1010.enabled = !!i1011[0]
  i1010.layerId = i1011[1]
  i1010.otherLayerId = i1011[2]
  return i1010
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.QualitySettings"] = function (request, data, root) {
  var i1012 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.QualitySettings' )
  var i1013 = data
  var i1015 = i1013[0]
  var i1014 = []
  for(var i = 0; i < i1015.length; i += 1) {
    i1014.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i1015[i + 0]) );
  }
  i1012.qualityLevels = i1014
  var i1017 = i1013[1]
  var i1016 = []
  for(var i = 0; i < i1017.length; i += 1) {
    i1016.push( i1017[i + 0] );
  }
  i1012.names = i1016
  i1012.shadows = i1013[2]
  i1012.anisotropicFiltering = i1013[3]
  i1012.antiAliasing = i1013[4]
  i1012.lodBias = i1013[5]
  i1012.shadowCascades = i1013[6]
  i1012.shadowDistance = i1013[7]
  i1012.shadowmaskMode = i1013[8]
  i1012.shadowProjection = i1013[9]
  i1012.shadowResolution = i1013[10]
  i1012.softParticles = !!i1013[11]
  i1012.softVegetation = !!i1013[12]
  i1012.activeColorSpace = i1013[13]
  i1012.desiredColorSpace = i1013[14]
  i1012.masterTextureLimit = i1013[15]
  i1012.maxQueuedFrames = i1013[16]
  i1012.particleRaycastBudget = i1013[17]
  i1012.pixelLightCount = i1013[18]
  i1012.realtimeReflectionProbes = !!i1013[19]
  i1012.shadowCascade2Split = i1013[20]
  i1012.shadowCascade4Split = new pc.Vec3( i1013[21], i1013[22], i1013[23] )
  i1012.streamingMipmapsActive = !!i1013[24]
  i1012.vSyncCount = i1013[25]
  i1012.asyncUploadBufferSize = i1013[26]
  i1012.asyncUploadTimeSlice = i1013[27]
  i1012.billboardsFaceCameraPosition = !!i1013[28]
  i1012.shadowNearPlaneOffset = i1013[29]
  i1012.streamingMipmapsMemoryBudget = i1013[30]
  i1012.maximumLODLevel = i1013[31]
  i1012.streamingMipmapsAddAllCameras = !!i1013[32]
  i1012.streamingMipmapsMaxLevelReduction = i1013[33]
  i1012.streamingMipmapsRenderersPerFrame = i1013[34]
  i1012.resolutionScalingFixedDPIFactor = i1013[35]
  i1012.streamingMipmapsMaxFileIORequests = i1013[36]
  i1012.currentQualityLevel = i1013[37]
  return i1012
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame"] = function (request, data, root) {
  var i1022 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame' )
  var i1023 = data
  i1022.weight = i1023[0]
  i1022.vertices = i1023[1]
  i1022.normals = i1023[2]
  i1022.tangents = i1023[3]
  return i1022
}

Deserializers["UnityEngine.Events.ArgumentCache"] = function (request, data, root) {
  var i1024 = root || request.c( 'UnityEngine.Events.ArgumentCache' )
  var i1025 = data
  request.r(i1025[0], i1025[1], 0, i1024, 'm_ObjectArgument')
  i1024.m_ObjectArgumentAssemblyTypeName = i1025[2]
  i1024.m_IntArgument = i1025[3]
  i1024.m_FloatArgument = i1025[4]
  i1024.m_StringArgument = i1025[5]
  i1024.m_BoolArgument = !!i1025[6]
  return i1024
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+RenderObjectsSettings"] = function (request, data, root) {
  var i1026 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+RenderObjectsSettings' )
  var i1027 = data
  i1026.Event = request.d('Luna.Unity.DTO.UnityEngine.Assets.EnumDescription', i1027[0], i1026.Event)
  i1026.filterSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+FilterSettings', i1027[1], i1026.filterSettings)
  i1026.overrideMaterialId = i1027[2]
  i1026.overrideMaterialPassIndex = i1027[3]
  i1026.overrideShaderId = i1027[4]
  i1026.overrideShaderPassIndex = i1027[5]
  i1026.overrideMode = request.d('Luna.Unity.DTO.UnityEngine.Assets.EnumDescription', i1027[6], i1026.overrideMode)
  i1026.overrideDepthState = !!i1027[7]
  i1026.depthCompareFunction = request.d('Luna.Unity.DTO.UnityEngine.Assets.EnumDescription', i1027[8], i1026.depthCompareFunction)
  i1026.enableWrite = !!i1027[9]
  i1026.stencilSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.StencilStateData', i1027[10], i1026.stencilSettings)
  i1026.cameraSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+CustomCameraSettings', i1027[11], i1026.cameraSettings)
  return i1026
}

Deserializers["TMPro.GlyphAnchorPoint"] = function (request, data, root) {
  var i1028 = root || request.c( 'TMPro.GlyphAnchorPoint' )
  var i1029 = data
  i1028.m_XCoordinate = i1029[0]
  i1028.m_YCoordinate = i1029[1]
  return i1028
}

Deserializers["TMPro.MarkPositionAdjustment"] = function (request, data, root) {
  var i1030 = root || request.c( 'TMPro.MarkPositionAdjustment' )
  var i1031 = data
  i1030.m_XPositionAdjustment = i1031[0]
  i1030.m_YPositionAdjustment = i1031[1]
  return i1030
}

Deserializers["TMPro.GlyphValueRecord_Legacy"] = function (request, data, root) {
  var i1032 = root || request.c( 'TMPro.GlyphValueRecord_Legacy' )
  var i1033 = data
  i1032.xPlacement = i1033[0]
  i1032.yPlacement = i1033[1]
  i1032.xAdvance = i1033[2]
  i1032.yAdvance = i1033[3]
  return i1032
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.EnumDescription"] = function (request, data, root) {
  var i1034 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.EnumDescription' )
  var i1035 = data
  i1034.Value = i1035[0]
  return i1034
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+FilterSettings"] = function (request, data, root) {
  var i1036 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+FilterSettings' )
  var i1037 = data
  i1036.RenderQueueType = request.d('Luna.Unity.DTO.UnityEngine.Assets.EnumDescription', i1037[0], i1036.RenderQueueType)
  i1036.LayerMask = i1037[1]
  var i1039 = i1037[2]
  var i1038 = []
  for(var i = 0; i < i1039.length; i += 1) {
    i1038.push( i1039[i + 0] );
  }
  i1036.PassNames = i1038
  return i1036
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.StencilStateData"] = function (request, data, root) {
  var i1040 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.StencilStateData' )
  var i1041 = data
  i1040.overrideStencilState = !!i1041[0]
  i1040.stencilReference = i1041[1]
  i1040.stencilCompareFunctionValue = request.d('Luna.Unity.DTO.UnityEngine.Assets.EnumDescription', i1041[2], i1040.stencilCompareFunctionValue)
  i1040.passOperationValue = request.d('Luna.Unity.DTO.UnityEngine.Assets.EnumDescription', i1041[3], i1040.passOperationValue)
  i1040.failOperationValue = request.d('Luna.Unity.DTO.UnityEngine.Assets.EnumDescription', i1041[4], i1040.failOperationValue)
  i1040.zFailOperationValue = request.d('Luna.Unity.DTO.UnityEngine.Assets.EnumDescription', i1041[5], i1040.zFailOperationValue)
  return i1040
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+CustomCameraSettings"] = function (request, data, root) {
  var i1042 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+CustomCameraSettings' )
  var i1043 = data
  i1042.overrideCamera = !!i1043[0]
  i1042.restoreCamera = !!i1043[1]
  i1042.offset = new pc.Vec4( i1043[2], i1043[3], i1043[4], i1043[5] )
  i1042.cameraFieldOfView = i1043[6]
  return i1042
}

Deserializers.fields = {"Luna.Unity.DTO.UnityEngine.Assets.Mesh":{"name":0,"halfPrecision":1,"useSimplification":2,"useUInt32IndexFormat":3,"vertexCount":4,"aabb":5,"streams":6,"vertices":7,"subMeshes":8,"bindposes":9,"blendShapes":10},"Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh":{"triangles":0},"Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape":{"name":0,"frames":1},"Luna.Unity.DTO.UnityEngine.Assets.Material":{"name":0,"shader":1,"renderQueue":3,"enableInstancing":4,"floatParameters":5,"colorParameters":6,"vectorParameters":7,"textureParameters":8,"materialFlags":9},"Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag":{"name":0,"enabled":1},"Luna.Unity.DTO.UnityEngine.Textures.Texture2D":{"name":0,"width":1,"height":2,"mipmapCount":3,"anisoLevel":4,"filterMode":5,"hdr":6,"format":7,"wrapMode":8,"alphaIsTransparency":9,"alphaSource":10,"graphicsFormat":11,"sRGBTexture":12,"desiredColorSpace":13,"wrapU":14,"wrapV":15},"Luna.Unity.DTO.UnityEngine.Textures.Cubemap":{"name":0,"atlasId":1,"mipmapCount":2,"hdr":3,"size":4,"anisoLevel":5,"filterMode":6,"rects":7,"wrapU":8,"wrapV":9},"Luna.Unity.DTO.UnityEngine.Scene.Scene":{"name":0,"index":1,"startup":2},"Luna.Unity.DTO.UnityEngine.Components.Camera":{"aspect":0,"orthographic":1,"orthographicSize":2,"backgroundColor":3,"nearClipPlane":7,"farClipPlane":8,"fieldOfView":9,"depth":10,"clearFlags":11,"cullingMask":12,"rect":13,"targetTexture":14,"usePhysicalProperties":16,"focalLength":17,"sensorSize":18,"lensShift":20,"gateFit":22,"commandBufferCount":23,"cameraType":24,"enabled":25},"Luna.Unity.DTO.UnityEngine.Scene.GameObject":{"name":0,"tagId":1,"enabled":2,"isStatic":3,"layer":4},"Luna.Unity.DTO.UnityEngine.Components.Light":{"type":0,"color":1,"cullingMask":5,"intensity":6,"range":7,"spotAngle":8,"shadows":9,"shadowNormalBias":10,"shadowBias":11,"shadowStrength":12,"shadowResolution":13,"lightmapBakeType":14,"renderMode":15,"cookie":16,"cookieSize":18,"shadowNearPlane":19,"occlusionMaskChannel":20,"isBaked":21,"mixedLightingMode":22,"enabled":23},"Luna.Unity.DTO.UnityEngine.Components.MeshFilter":{"sharedMesh":0},"Luna.Unity.DTO.UnityEngine.Components.MeshRenderer":{"additionalVertexStreams":0,"enabled":2,"sharedMaterial":3,"sharedMaterials":5,"receiveShadows":6,"shadowCastingMode":7,"sortingLayerID":8,"sortingOrder":9,"lightmapIndex":10,"lightmapSceneIndex":11,"lightmapScaleOffset":12,"lightProbeUsage":16,"reflectionProbeUsage":17},"Luna.Unity.DTO.UnityEngine.Components.BoxCollider":{"center":0,"size":3,"enabled":6,"isTrigger":7,"material":8},"Luna.Unity.DTO.UnityEngine.Components.RectTransform":{"pivot":0,"anchorMin":2,"anchorMax":4,"sizeDelta":6,"anchoredPosition3D":8,"rotation":11,"scale":15},"Luna.Unity.DTO.UnityEngine.Components.Canvas":{"planeDistance":0,"referencePixelsPerUnit":1,"isFallbackOverlay":2,"renderMode":3,"renderOrder":4,"sortingLayerName":5,"sortingOrder":6,"scaleFactor":7,"worldCamera":8,"overrideSorting":10,"pixelPerfect":11,"targetDisplay":12,"overridePixelPerfect":13,"enabled":14},"Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer":{"cullTransparentMesh":0},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings":{"ambientIntensity":0,"reflectionIntensity":1,"ambientMode":2,"ambientLight":3,"ambientSkyColor":7,"ambientGroundColor":11,"ambientEquatorColor":15,"fogColor":19,"fogEndDistance":23,"fogStartDistance":24,"fogDensity":25,"fog":26,"skybox":27,"fogMode":29,"lightmaps":30,"lightProbes":31,"lightmapsMode":32,"mixedBakeMode":33,"environmentLightingMode":34,"ambientProbe":35,"customReflection":36,"defaultReflection":38,"defaultReflectionMode":40,"defaultReflectionResolution":41,"sunLightObjectId":42,"pixelLightCount":43,"defaultReflectionHDR":44,"hasLightDataAsset":45,"hasManualGenerate":46},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap":{"lightmapColor":0,"lightmapDirection":2,"shadowMask":4},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes":{"bakedProbes":0,"positions":1,"hullRays":2,"tetrahedra":3,"neighbours":4,"matrices":5},"Luna.Unity.DTO.UnityEngine.Assets.UniversalRenderPipelineAsset":{"AdditionalLightsRenderingMode":0,"LightRenderingMode":1,"MainLightRenderingModeValue":2,"SupportsMainLightShadows":3,"MixedLightingSupported":4,"MainLightShadowmapResolutionValue":5,"SupportsSoftShadows":6,"SoftShadowQualityValue":7,"ShadowDistance":8,"ShadowCascadeCount":9,"Cascade2Split":10,"Cascade3Split":11,"Cascade4Split":13,"CascadeBorder":16,"ShadowDepthBias":17,"ShadowNormalBias":18,"RequireDepthTexture":19,"RequireOpaqueTexture":20,"scriptableRendererData":21},"Luna.Unity.DTO.UnityEngine.Assets.LightRenderingMode":{"Disabled":0,"PerVertex":1,"PerPixel":2},"Luna.Unity.DTO.UnityEngine.Assets.ScriptableRendererData":{"opaqueLayerMask":0,"transparentLayerMask":1,"RenderObjectsFeatures":2,"name":3},"Luna.Unity.DTO.UnityEngine.Assets.RenderObjects":{"settings":0,"name":1,"typeName":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader":{"ShaderCompilationErrors":0,"name":1,"guid":2,"shaderDefinedKeywords":3,"passes":4,"usePasses":5,"defaultParameterValues":6,"unityFallbackShader":7,"readDepth":9,"hasDepthOnlyPass":10,"isCreatedByShaderGraph":11,"disableBatching":12,"compiled":13},"Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError":{"shaderName":0,"errorMessage":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass":{"id":0,"subShaderIndex":1,"name":2,"passType":3,"grabPassTextureName":4,"usePass":5,"zTest":6,"zWrite":7,"culling":8,"blending":9,"alphaBlending":10,"colorWriteMask":11,"offsetUnits":12,"offsetFactor":13,"stencilRef":14,"stencilReadMask":15,"stencilWriteMask":16,"stencilOp":17,"stencilOpFront":18,"stencilOpBack":19,"tags":20,"passDefinedKeywords":21,"passDefinedKeywordGroups":22,"variants":23,"excludedVariants":24,"hasDepthReader":25},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value":{"val":0,"name":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending":{"src":0,"dst":1,"op":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp":{"pass":0,"fail":1,"zFail":2,"comp":3},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup":{"keywords":0,"hasDiscard":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant":{"passId":0,"subShaderIndex":1,"keywords":2,"vertexProgram":3,"fragmentProgram":4,"exportedForWebGl2":5,"readDepth":6},"Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass":{"shader":0,"pass":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue":{"name":0,"type":1,"value":2,"textureValue":6,"shaderPropertyFlag":7},"Luna.Unity.DTO.UnityEngine.Textures.Sprite":{"name":0,"texture":1,"aabb":3,"vertices":4,"triangles":5,"textureRect":6,"packedRect":10,"border":14,"transparency":18,"bounds":19,"pixelsPerUnit":20,"textureWidth":21,"textureHeight":22,"nativeSize":23,"pivot":25,"textureRectOffset":27},"Luna.Unity.DTO.UnityEngine.Assets.Font":{"name":0,"ascent":1,"originalLineHeight":2,"fontSize":3,"characterInfo":4,"texture":5,"originalFontSize":7},"Luna.Unity.DTO.UnityEngine.Assets.Font+CharacterInfo":{"index":0,"advance":1,"bearing":2,"glyphWidth":3,"glyphHeight":4,"minX":5,"maxX":6,"minY":7,"maxY":8,"uvBottomLeftX":9,"uvBottomLeftY":10,"uvBottomRightX":11,"uvBottomRightY":12,"uvTopLeftX":13,"uvTopLeftY":14,"uvTopRightX":15,"uvTopRightY":16},"Luna.Unity.DTO.UnityEngine.Assets.TextAsset":{"name":0,"bytes64":1,"data":2},"Luna.Unity.DTO.UnityEngine.Assets.Resources":{"files":0,"componentToPrefabIds":1},"Luna.Unity.DTO.UnityEngine.Assets.Resources+File":{"path":0,"unityObject":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings":{"scriptsExecutionOrder":0,"sortingLayers":1,"cullingLayers":2,"timeSettings":3,"physicsSettings":4,"physics2DSettings":5,"qualitySettings":6,"enableRealtimeShadows":7,"enableAutoInstancing":8,"enableStaticBatching":9,"enableDynamicBatching":10,"usePreservativeDynamicBatching":11,"lightmapEncodingQuality":12,"desiredColorSpace":13,"allTags":14},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer":{"id":0,"name":1,"value":2},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer":{"id":0,"name":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings":{"fixedDeltaTime":0,"maximumDeltaTime":1,"timeScale":2,"maximumParticleTimestep":3},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings":{"gravity":0,"defaultSolverIterations":3,"bounceThreshold":4,"autoSyncTransforms":5,"autoSimulation":6,"collisionMatrix":7},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask":{"enabled":0,"layerId":1,"otherLayerId":2},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings":{"material":0,"gravity":2,"positionIterations":4,"velocityIterations":5,"velocityThreshold":6,"maxLinearCorrection":7,"maxAngularCorrection":8,"maxTranslationSpeed":9,"maxRotationSpeed":10,"baumgarteScale":11,"baumgarteTOIScale":12,"timeToSleep":13,"linearSleepTolerance":14,"angularSleepTolerance":15,"defaultContactOffset":16,"autoSimulation":17,"queriesHitTriggers":18,"queriesStartInColliders":19,"callbacksOnDisable":20,"reuseCollisionCallbacks":21,"autoSyncTransforms":22,"collisionMatrix":23},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask":{"enabled":0,"layerId":1,"otherLayerId":2},"Luna.Unity.DTO.UnityEngine.Assets.QualitySettings":{"qualityLevels":0,"names":1,"shadows":2,"anisotropicFiltering":3,"antiAliasing":4,"lodBias":5,"shadowCascades":6,"shadowDistance":7,"shadowmaskMode":8,"shadowProjection":9,"shadowResolution":10,"softParticles":11,"softVegetation":12,"activeColorSpace":13,"desiredColorSpace":14,"masterTextureLimit":15,"maxQueuedFrames":16,"particleRaycastBudget":17,"pixelLightCount":18,"realtimeReflectionProbes":19,"shadowCascade2Split":20,"shadowCascade4Split":21,"streamingMipmapsActive":24,"vSyncCount":25,"asyncUploadBufferSize":26,"asyncUploadTimeSlice":27,"billboardsFaceCameraPosition":28,"shadowNearPlaneOffset":29,"streamingMipmapsMemoryBudget":30,"maximumLODLevel":31,"streamingMipmapsAddAllCameras":32,"streamingMipmapsMaxLevelReduction":33,"streamingMipmapsRenderersPerFrame":34,"resolutionScalingFixedDPIFactor":35,"streamingMipmapsMaxFileIORequests":36,"currentQualityLevel":37},"Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame":{"weight":0,"vertices":1,"normals":2,"tangents":3},"Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+RenderObjectsSettings":{"Event":0,"filterSettings":1,"overrideMaterialId":2,"overrideMaterialPassIndex":3,"overrideShaderId":4,"overrideShaderPassIndex":5,"overrideMode":6,"overrideDepthState":7,"depthCompareFunction":8,"enableWrite":9,"stencilSettings":10,"cameraSettings":11},"Luna.Unity.DTO.UnityEngine.Assets.EnumDescription":{"Value":0},"Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+FilterSettings":{"RenderQueueType":0,"LayerMask":1,"PassNames":2},"Luna.Unity.DTO.UnityEngine.Assets.StencilStateData":{"overrideStencilState":0,"stencilReference":1,"stencilCompareFunctionValue":2,"passOperationValue":3,"failOperationValue":4,"zFailOperationValue":5},"Luna.Unity.DTO.UnityEngine.Assets.RenderObjects+CustomCameraSettings":{"overrideCamera":0,"restoreCamera":1,"offset":2,"cameraFieldOfView":6}}

Deserializers.requiredComponents = {"37":[38],"39":[38],"40":[38],"41":[38],"42":[38],"43":[38],"44":[45],"46":[2],"47":[48],"49":[48],"50":[48],"51":[48],"52":[48],"53":[48],"54":[55],"56":[55],"57":[55],"58":[55],"59":[55],"60":[55],"61":[55],"62":[55],"63":[55],"64":[55],"65":[55],"66":[55],"67":[55],"68":[2],"69":[10],"70":[71],"72":[71],"14":[13],"25":[24],"73":[14],"74":[13],"75":[2],"5":[2],"7":[6],"76":[13],"77":[18,13],"78":[10],"79":[18,13],"80":[13],"81":[13],"82":[10,13],"22":[13,18],"83":[84],"85":[84],"86":[84],"87":[13],"88":[13],"17":[14],"19":[18,13],"89":[13],"16":[14],"90":[13],"91":[13],"92":[13],"93":[13],"94":[13],"95":[13],"96":[13],"97":[13],"98":[13],"99":[18,13],"100":[13],"101":[13],"102":[13],"103":[13],"104":[18,13],"105":[13],"106":[24],"107":[24],"108":[24],"109":[24],"110":[2],"111":[2]}

Deserializers.types = ["UnityEngine.Shader","UnityEngine.Texture2D","UnityEngine.Camera","UnityEngine.AudioListener","UnityEngine.MonoBehaviour","UnityEngine.Rendering.Universal.UniversalAdditionalCameraData","UnityEngine.Light","UnityEngine.Rendering.Universal.UniversalAdditionalLightData","UnityEngine.MeshFilter","UnityEngine.Mesh","UnityEngine.MeshRenderer","UnityEngine.Material","UnityEngine.BoxCollider","UnityEngine.RectTransform","UnityEngine.Canvas","UnityEngine.EventSystems.UIBehaviour","UnityEngine.UI.CanvasScaler","UnityEngine.UI.GraphicRaycaster","UnityEngine.CanvasRenderer","UnityEngine.UI.Image","UnityEngine.Sprite","UnityEngine.UI.Button","TMPro.TextMeshProUGUI","TMPro.TMP_FontAsset","UnityEngine.EventSystems.EventSystem","UnityEngine.InputSystem.UI.InputSystemUIInputModule","UnityEngine.InputSystem.InputActionAsset","UnityEngine.InputSystem.InputActionReference","PlayableSmokeController","UnityEngine.Transform","UnityEngine.GameObject","UnityEngine.Cubemap","UnityEngine.Font","TMPro.TMP_Settings","TMPro.TMP_SpriteAsset","TMPro.TMP_StyleSheet","UnityEngine.TextAsset","UnityEngine.AudioLowPassFilter","UnityEngine.AudioBehaviour","UnityEngine.AudioHighPassFilter","UnityEngine.AudioReverbFilter","UnityEngine.AudioDistortionFilter","UnityEngine.AudioEchoFilter","UnityEngine.AudioChorusFilter","UnityEngine.Cloth","UnityEngine.SkinnedMeshRenderer","UnityEngine.FlareLayer","UnityEngine.CharacterJoint","UnityEngine.Rigidbody","UnityEngine.ConfigurableJoint","UnityEngine.ConstantForce","UnityEngine.FixedJoint","UnityEngine.HingeJoint","UnityEngine.SpringJoint","UnityEngine.CompositeCollider2D","UnityEngine.Rigidbody2D","UnityEngine.Joint2D","UnityEngine.AnchoredJoint2D","UnityEngine.SpringJoint2D","UnityEngine.DistanceJoint2D","UnityEngine.FrictionJoint2D","UnityEngine.HingeJoint2D","UnityEngine.RelativeJoint2D","UnityEngine.SliderJoint2D","UnityEngine.TargetJoint2D","UnityEngine.FixedJoint2D","UnityEngine.WheelJoint2D","UnityEngine.ConstantForce2D","UnityEngine.StreamingController","UnityEngine.TextMesh","UnityEngine.Tilemaps.TilemapRenderer","UnityEngine.Tilemaps.Tilemap","UnityEngine.Tilemaps.TilemapCollider2D","UnityEngine.InputSystem.UI.TrackedDeviceRaycaster","UnityEngine.Rendering.UI.UIFoldout","UnityEngine.Rendering.Universal.PixelPerfectCamera","TMPro.TMP_Dropdown","TMPro.TMP_SelectionCaret","TMPro.TMP_SubMesh","TMPro.TMP_SubMeshUI","TMPro.TMP_Text","TMPro.TextContainer","TMPro.TextMeshPro","Unity.VisualScripting.SceneVariables","Unity.VisualScripting.Variables","Unity.VisualScripting.ScriptMachine","Unity.VisualScripting.StateMachine","UnityEngine.UI.Dropdown","UnityEngine.UI.Graphic","UnityEngine.UI.AspectRatioFitter","UnityEngine.UI.ContentSizeFitter","UnityEngine.UI.GridLayoutGroup","UnityEngine.UI.HorizontalLayoutGroup","UnityEngine.UI.HorizontalOrVerticalLayoutGroup","UnityEngine.UI.LayoutElement","UnityEngine.UI.LayoutGroup","UnityEngine.UI.VerticalLayoutGroup","UnityEngine.UI.Mask","UnityEngine.UI.MaskableGraphic","UnityEngine.UI.RawImage","UnityEngine.UI.RectMask2D","UnityEngine.UI.ScrollRect","UnityEngine.UI.Scrollbar","UnityEngine.UI.Slider","UnityEngine.UI.Text","UnityEngine.UI.Toggle","UnityEngine.EventSystems.BaseInputModule","UnityEngine.EventSystems.PointerInputModule","UnityEngine.EventSystems.StandaloneInputModule","UnityEngine.EventSystems.TouchInputModule","UnityEngine.EventSystems.Physics2DRaycaster","UnityEngine.EventSystems.PhysicsRaycaster"]

Deserializers.unityVersion = "6000.0.72f1";

Deserializers.productName = "ColorBlock_PlayworksPlayable";

Deserializers.lunaInitializationTime = "07/15/2026 16:26:55";

Deserializers.lunaDaysRunning = "0.0";

Deserializers.lunaVersion = "7.2.0";

Deserializers.lunaSHA = "ea08d29afe2968efcb8d91d5624f033c6485cc68";

Deserializers.creativeName = "";

Deserializers.lunaAppID = "0";

Deserializers.projectId = "c0a6f19ace5e34f5bac9a4e93465ce13";

Deserializers.packagesInfo = "com.unity.inputsystem: 1.19.0\ncom.unity.render-pipelines.universal: 17.0.4\ncom.unity.timeline: 1.8.10\ncom.unity.ugui: 2.0.0";

Deserializers.externalJsLibraries = "";

Deserializers.androidLink = ( typeof window !== "undefined")&&window.$environment.packageConfig.androidLink?window.$environment.packageConfig.androidLink:'Empty';

Deserializers.iosLink = ( typeof window !== "undefined")&&window.$environment.packageConfig.iosLink?window.$environment.packageConfig.iosLink:'Empty';

Deserializers.base64Enabled = "False";

Deserializers.minifyEnabled = "True";

Deserializers.isForceUncompressed = "False";

Deserializers.isAntiAliasingEnabled = "False";

Deserializers.isRuntimeAnalysisEnabledForCode = "False";

Deserializers.runtimeAnalysisExcludedClassesCount = "0";

Deserializers.runtimeAnalysisExcludedMethodsCount = "0";

Deserializers.runtimeAnalysisExcludedModules = "";

Deserializers.isRuntimeAnalysisEnabledForShaders = "True";

Deserializers.isRealtimeShadowsEnabled = "False";

Deserializers.isLunaCompilerV2Used = "False";

Deserializers.companyName = "DefaultCompany";

Deserializers.buildPlatform = "StandaloneOSX";

Deserializers.applicationIdentifier = "com.Unity-Technologies.com.unity.template.urp-blank";

Deserializers.disableAntiAliasing = true;

Deserializers.graphicsConstraint = 24;

Deserializers.linearColorSpace = true;

Deserializers.buildID = "5ce91e39-90f4-434d-a1cd-9ec010e16188";

Deserializers.runtimeInitializeOnLoadInfos = [[["UnityEngine","Rendering","DebugUpdater","RuntimeInit"],["UnityEngine","Experimental","Rendering","ScriptableRuntimeReflectionSystemSettings","ScriptingDirtyReflectionSystemInstance"]],[["Unity","VisualScripting","RuntimeVSUsageUtility","RuntimeInitializeOnLoadBeforeSceneLoad"],["UnityEngine","InputSystem","InputSystem","RunInitialUpdate"],["Unity","AI","Navigation","NavMeshModifierVolume","ClearNavMeshModifiers"],["Unity","AI","Navigation","NavMeshLink","ClearTrackedList"],["Unity","AI","Navigation","NavMeshSurface","ClearNavMeshSurfaces"],["Unity","AI","Navigation","NavMeshModifier","ClearNavMeshModifiers"],["UnityEngine","AI","NavMesh","ClearPreUpdateListeners"]],[["$BurstDirectCallInitializer","Initialize"],["$BurstDirectCallInitializer","Initialize"],["$BurstDirectCallInitializer","Initialize"],["$BurstDirectCallInitializer","Initialize"]],[["UnityEngine","Experimental","Rendering","XRSystem","XRSystemInit"]],[["UnityEngine","InputSystem","Plugins","InputForUI","InputSystemProvider","Bootstrap"],["UnityEngine","InputSystem","UI","InputSystemUIInputModule","ResetDefaultActions"],["UnityEngine","InputSystem","InputSystem","RunInitializeInPlayer"]]];

Deserializers.typeNameToIdMap = function(){ var i = 0; return Deserializers.types.reduce( function( res, item ) { res[ item ] = i++; return res; }, {} ) }()

