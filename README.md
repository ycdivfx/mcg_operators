# MCG Operators
MCG Test operators

Simplex Noise adapted to MCG from https://github.com/WardBenjamin/SimplexNoise

## Install
Just `git clone https://github.com/ycdivfx/mcg_operators.git` and build it, or download one of the releases.
When you first start 3ds Max, you need add the **MCG Assembly Directories** to the 3dsmax.ini. For this, edit the **loader.ms** so that is points to where is the compiled assembly folder, and then run the **loader.ms**. This step is only required once. Alternative edit the **%localappdata%/Autodesk/3dsMax/20xx - 64bit/3dsmax.ini** file, by adding the `[MCG Assembly Directories]` section and entry like `MyOperators=PATH_TO_DLL`.

Happy MCGing
