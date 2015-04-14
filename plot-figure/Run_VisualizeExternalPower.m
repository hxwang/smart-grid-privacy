
dataFileName= '..\data\3day_5min.txt';
timeFileName = '..\data\3day_5min_timestamp.txt';
AlgType = 'Origin'
VisualizeExternalPower(dataFileName, timeFileName, AlgType)



Alg = {'BE'; 'NILL'; 'LS2'}

for i = 1:1:size(Alg)
AlgType = char(Alg(i,:));
dataFileName= strcat('..\data\simOutput\',AlgType,'_extEnergy.txt');
VisualizeExternalPower(dataFileName, timeFileName, AlgType)

AlgTypeName =strcat( AlgType,'-Battery-Power')
dataFileName= strcat('..\data\simOutput\',AlgType,'_batteryPowerHist.txt');
VisualizeExternalPower(dataFileName, timeFileName, AlgTypeName)

dataFileName= strcat('..\data\simOutput\', AlgType, '_batteryEnergyListHist.txt');
AlgTypeName =strcat( AlgType,'-Battery-Energy')
VisualizeExternalPower(dataFileName, timeFileName, AlgTypeName)


end