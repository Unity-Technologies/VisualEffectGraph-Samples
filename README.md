## Visual Effect Graph - Samples

In this project you will be able to access sample scenes and effects made with the Visual Effect Graph. You can download snapshots of these samples by using the [release](https://github.com/Unity-Technologies/VisualEffectGraph-Samples/releases) tab, or by cloning this repository.

There are also pre-built binaries for Windows and/or macOS available.

### Changelog


- ####  2021.2 Release 2 : (12.1.1 rev1 2021-12-10)
  - **Updated HD Render Pipeline / Visual Effect Graph to 12.1.1**
  
  - **Notable Changes : **
    - Added **GooBall** Sample
    
    - Jacob's ladder effect excluded from TAA for better rendering (MagicBook Sample)
    
  - **Known Issues **
      - Prewarm delta time consideration (fixed in 2021.2.10f1)
      - Mac M1 build: scene flickering issue 
      - Linux build: "Vulkan - out of memory" issue
 
![gif](https://media4.giphy.com/media/8FH6dhA2saLF2fKEGd/giphy.gif?cid=790b7611660d4029418c8424f97970330e4e516f55aa960f&rid=giphy.gif&ct=g)
  
- ####  2021.2 Release 1 : (12.1.1 rev1 2021-11-29)

  - **Updated HD Render Pipeline / Visual Effect Graph to 12.1.1**
  - **Notable Changes : **
    - Added **Ellen Skinned Mesh** Sample

![gif](https://media2.giphy.com/media/Gtd18yGr8VDXTlkXrp/giphy.gif?cid=790b76115e1d6c810a6be3cbc2b008adf5b3b02722dc9115&rid=giphy.gif&ct=g) ![gif](https://media2.giphy.com/media/4b5yl3UUwezYfIxXcD/giphy.gif?cid=790b76111b77bd82988378d68a829ee3b50e5a71642435f9&rid=giphy.gif&ct=g)


- #### 2020.3 Release 1 : (10.5.0 rev1 2021-08-09)

  - **Updated HD Render Pipeline / Visual Effect Graph to 10.5.0**
  - **Notable Changes : **
    - Added **Meteorite** Sample
    - Rebuilt all Visual Effects

![gif](https://media.giphy.com/media/HWeVbv69k5t6UWZVmE/giphy-downsized.gif)

- #### 2020.2 Release 1 : (10.2.2 rev1 2020-12-15)

  - **Updated HD Render Pipeline / Visual Effect Graph to 10.2.2**
  - **Notable Changes : **
    - Rebuilt all Visual Effects
    - Fixed Portal Sample (Re-authored distortion as shader graph)

- #### 2019.4 Release 1 : (7.5.2 rev1 2020-12-15)

  - **Updated HD Render Pipeline / Visual Effect Graph to 7.5.2**
  - **Notable Changes : **
    - Rebuilt all Visual Effects

- #### 2019.3 Release 2 : (7.2.1 rev1 2020-03-10)

  - **Updated HD Render Pipeline / Visual Effect Graph to 7.2.1**
  - **Notable Changes : **
    - Rebuilt all Visual Effects
    - Upgraded all HDRP materials to 7.2.1
    - Added **Magic Book** Sample

![gif](https://media3.giphy.com/media/62fU5KhQEZSeAfNt42/giphy.gif?cid=790b76114fbe0d8ccad2925f5664924d6d2f2d539f736d33&rid=giphy.gif&ct=g)

- #### 2019.3 Update: (7.2.0 rev1 2020-02-12)

  - **Updated HD Render Pipeline / Visual Effect Graph to 7.2.0**
  - **Fixes and Improvements**
  - Rebuilt all Visual Effects
    - Upgraded all HDRP materials to 7.2.0
    - Fixed Ambient Lighting in GrassWind
    - Fixed SSS Profile for Chomper Character in EllenHologram
    - Fixed UI Labels in Voxelized Terrain that disappeared in 1440p
    - Fixed Bonfire Fire Shader : was using a texture that was removed from HD Render Pipeline Package
    - Removed unused Custom Render Texture + Shaders in SpaceshipHoloTable

- #### 2019.3.0 Release : (7.1.8 rev1 2020-01-30)

  - **New Samples**
    - **Bonfire** : Small Scene with Fire and smoke featuring custom VFX Shader Graph rendering (Featuring CC0 Assets from [Kenney](https://kenney.nl))
    - **Ribbon Pack** : Balls of Unraveling multi-colored trails featuring the new Particle Strips.
    - **Ellen Hologram** : Holographic reprojection of moving characters from [3D Game Kit](https://learn.unity.com/project/3d-game-kit)
  - **General Improvements**
    - Other Samples were updated and polished to match new features:
      - Added Motion Vectors / Motion blur in Butterflies, Portal, SpaceshipHoloTable, UnityLogo, Volumetric, VoxelizedTerrain
      - Reduced texture size of the SpaceshipHoloTable environment
      - Rebuilt all assets
      - Removed old ProceduralSky from scenes
      - Embedded Visual Effect Graph Sample Additions from package 7.1.6
    - Samples Navigation Improvements
      - Added Navigation Menu Bar (Accessible via Escape Key)
        - Options Window
        - Load Samples Window
        - Toggle Demo Mode
        - Toggle FPS Counter
        - Take Screenshot
  - **Known Issues**:
    - Ellen Hologram not working properly on macOS/metal
    - Linux Vulkan rendering is mostly broken

![gif](https://media.giphy.com/media/rQRZxpCNfht61L4wWx/giphy.gif) ![gif](https://media2.giphy.com/media/fZ6CFaINka2dcuVMrC/giphy.gif?cid=790b7611b9c32508fdc0c0ab50a337025226e4456e91d573&rid=giphy.gif&ct=g) ![gif](https://media.giphy.com/media/tq0UxWC90n7jty5mZ1/giphy.gif)

- #### The 2019.1 Release! ( 5.8.2-preview rev1 - 2019-03-19 )

  -  **New Samples**
     - **Spaceship Holo Table** : Holographic Pin-Screen effect isolated from the [Spaceship Demo](https://www.youtube.com/watch?v=rqMcPZoEc3U)

  - **General Improvements**
    - Upgraded all project, volumes, postprocesses and scenes to 2019.1 package track
      - HD/VFX Package versions to 5.8.2-preview
      - Editor version to 2019.1.0b6

  - **Fixes**
    - General Performance adjustments

![gif](https://media.giphy.com/media/4BMXmbhHMYi4wWLL61/giphy.gif)

- #### Third Release ( 4.9.0-preview rev1 - 2019-02-07 )

  - **New Samples**
    - **Genie**: Magic flow gushing out of the Magic lantern and taking the shape of a Genie
    - **AR Radar** : Lines and Dots that form a Radar Grid with multiple Targets
    - **Voxelized Terrain** : Interactive Terrain 
  - **Fixes and General Improvements:**
    - Simplified Sample Loader Script
    - GrassWind : Fixed the sample to reflect the fixes in ConnectTarget
    - GrassWind : Reworked the Camera Control (now works with cinemachine)
    - GrassWind : Added Ethan Body Diffuse texture

![gif](https://media.giphy.com/media/DeMUSeLVpgNmoxbOAM/giphy.gif) ![gif](https://media.giphy.com/media/FcLcjtljIcHMTBAbaH/giphy.gif) ![gif](https://media.giphy.com/media/3Bj5alkTJdJHA8uuzV/giphy.gif)

- #### Second Release ( 4.6.0-preview rev1 - 2019-01-07 )

  - **New Samples**
    - **GrassWind** : Grass quads on terrain reacting to player movement and wind
    - **Volumetric** : Lit Particles reacting to volumetric lighting and transmission
    - **Portal** : Magic portal reminiscing of a strange doctor....
  - **Fixes:**
    - Fixed Butterflies Bodies set to Opaque
    - Fixed Missing references to VectorField and Texture in UnityLogo template
    - Removed unused HDRP Resources file (now using default one)

![gif](https://media.giphy.com/media/GlqT7PZie6B6dGP7P7/giphy.gif) ![gif](https://media.giphy.com/media/ONg2Gcg0eiwlYT5XP9/giphy.gif) ![gif](https://media.giphy.com/media/XBbqp2r0VZMZhNozsJ/giphy.gif)

- #### First Release ( 4.3.0-preview rev1 - 2018-11-27 )

  - **Base Project** and 3 Samples
    - **Unity Logo** : around 750k particles driven by vector fields
    - **Butterflies** : Procedurally animated and simulated butterflies
    - **Morphing Face** : Simulation-less face of cubes with masking and material animation

![gif](https://media.giphy.com/media/A3Wrp9a25ZLaiWKoaD/giphy.gif) ![gif](https://media.giphy.com/media/vJ9woB5KF5AWHYpnDe/giphy.gif) ![gif](https://media.giphy.com/media/5KclpM4YSv76t1b1EU/giphy.gif)
