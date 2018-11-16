@echo off
ECHO Installing SRP/HDRP/VFX Editor...

md LocalPackages

git clone -b release/2018.3 https://github.com/Unity-Technologies/ScriptableRenderPipeline.git ./LocalPackages

pause