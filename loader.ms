(
	local iniFile = getMAXIniFile()
	local operatorsPath = (getfilenamepath(getsourcefilename())) + "/CurlNoise/bin/x64/Debug/netstandard2.0/"
	setINIsetting iniFile "MCG Assembly Directories" "ycdivfxOPS" operatorsPath
	ProceduralContentOps.SetUpCurrentUser()
)