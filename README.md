# MCG Operators

### Operators included
* Simplex Noise (1D, 2D, 3D and 3D with Vector3 output) adapted to MCG from https://github.com/WardBenjamin/SimplexNoise
* Mesh from points, good for caping, doesn't produce a pretty mesh :).

## Install

Download one of the latest release builds.
When you first start 3ds Max, you need add the `[MCG Assembly Directories]` to the 3dsmax.ini. For this, edit the **loader.ms** so that is points to where is the compiled assembly folder, and then run the **loader.ms**. This step is only required once. Alternative edit the `%localappdata%/Autodesk/3dsMax/20xx - 64bit/ENU/3dsmax.ini` file, by adding the `[MCG Assembly Directories]` section and entry like `MyOperators=PATH_TO_DLL`.

## Build

Start by cloneing the repository with `git clone https://github.com/ycdivfx/mcg_operators.git`. To build you need Visual Studio 2017 (Community edition works just fine). Open the solution in Visual Studio, change the paths to the 3ds Max assemblies if required (in the dependencies), by default it points to the default location of 3ds Max 2018.

Happy MCGing
