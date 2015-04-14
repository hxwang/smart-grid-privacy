
dataFileName= '..\data\3day_5min.txt';
timeFileName = '..\data\3day_5min_timestamp.txt';
AlgType = 'Origin'
VisualizeExternalPower(dataFileName, timeFileName, AlgType)


dataFileName= '..\data\simOutput\BE_extEnergy.txt';
AlgType = 'BE'
VisualizeExternalPower(dataFileName, timeFileName, AlgType)

dataFileName= '..\data\simOutput\BE_batteryPowerHist.txt';
AlgType = 'BE-Battery-Power'
VisualizeExternalPower(dataFileName, timeFileName, AlgType)

dataFileName= '..\data\simOutput\BE_batteryEnergyListHist.txt';
AlgType = 'BE-Battery-Energy'
VisualizeExternalPower(dataFileName, timeFileName, AlgType)

