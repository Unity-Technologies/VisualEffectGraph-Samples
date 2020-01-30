## Visual Effect Graph - Samples

In this project you will be able to access sample scenes and effects made with the Visual Effect Graph. You can download snapshots of these samples by using the [release](https://github.com/Unity-Technologies/VisualEffectGraph-Samples/releases) tab, or by cloning this repository.

There are also pre-built binaries for Windows and macOS available.

### Changelog

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

- #### The 2019.1 Release! ( 5.8.2-preview rev1 - 2019-03-19 )

  -  **New Samples**
    - Spaceship Holo Table : Holographic Pin-Screen effect isolated from the [Spaceship Demo](https://www.youtube.com/watch?v=rqMcPZoEc3U)

  - **General Improvements**
    - Upgraded all project, volumes, postprocesses and scenes to 2019.1 package track
      - HD/VFX Package versions to 5.8.2-preview
      - Editor version to 2019.1.0b6

  - **Fixes**
    - General Performance adjustments

- #### Third Release ( 4.9.0-preview rev1 - 2019-02-07 )

  - **New Samples**
    - Genie: Magic flow gushing out of the Magic lantern and taking the shape of a Genie
    - AR Radar : Lines and Dots that form a Radar Grid with multiple Targets
    - Voxelized Terrain : Interactive Terrain 
  - **Fixes and General Improvements:**
    - Simplified Sample Loader Script
    - GrassWind : Fixed the sample to reflect the fixes in ConnectTarget
    - GrassWind : Reworked the Camera Control (now works with cinemachine)
    - GrassWind : Added Ethan Body Diffuse texture

- #### Second Release ( 4.6.0-preview rev1 - 2019-01-07 )

  - **New Samples**
    - **GrassWind** : Grass quads on terrain reacting to player movement and wind
    - **Volumetric** : Lit Particles reacting to volumetric lighting and transmission
    - **Portal** : Magic portal reminiscing of a strange doctor....
  - **Fixes:**
    - Fixed Butterflies Bodies set to Opaque
    - Fixed Missing references to VectorField and Texture in UnityLogo template
    - Removed unused HDRP Resources file (now using default one)

- #### First Release ( 4.3.0-preview rev1 - 2018-11-27 )

  - **Base Project** and 3 Samples
    - **Unity Logo** : around 750k particles driven by vector fields
    - **Butterflies** : Procedurally animated and simulated butterflies
    - **Morphing Face** : Simulation-less face of cubes with masking and material animation

