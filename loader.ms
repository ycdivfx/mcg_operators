(
	local iniFile = getMAXIniFile()
	local operatorsPath = (getfilenamepath(getsourcefilename()))
	setINIsetting iniFile "MCG Assembly Directories" "ycdivfxOPS" operatorsPath
	ProceduralContentOps.SetUpCurrentUser()
)