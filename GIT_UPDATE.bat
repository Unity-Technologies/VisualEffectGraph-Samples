@echo off
ECHO Updating SRP/HDRP/VisualEffectGraph...

cd LocalPackages
git reset --hard
git pull origin release/2018.3

pause 