
function VisualizeExternalPower(dataFileName, timeFileName, AlgType)

%load data
data = load(dataFileName);
size(data)
time = load(timeFileName);
time = (time - 5*3600) / 86400+datenum('01-Jan-1970') ;

%Build Figure

figure1 = figure;
set(figure1,'units','normalized','outerposition',[0 0 1 1]);

axes1 = axes('Parent',figure1);
box(axes1,'on');
hold(axes1,'all');
set(axes1,'FontSize',30,'FontWeight','bold');

%plot and set font, line type
p = plot(time(:,1),data(:,1));
set(p, 'Color', 'b', 'LineWidth', 3, 'linestyle','-');

    
    
    
  

%set x tick and y tick
%set(axes1,'YTick',[0.2,0.4,0.6,0.8,1.0],'YTickLabel',{'20%','40%','60%','80%','100%'},'XGrid','on','YGrid','on');
%set(axes1,'XTick',[200,400,600,800,1000],'XTickLabel',{200,400,600,800,1000},'XGrid','on','YGrid','on');

set(gca,'XTick', [time(1)-5*60/86400, time(12*24), time(2*12*24), time(3*12*24)]);
datetick('x', 'mm/dd HH:MM' ,'keepticks')


%set grid on 
set(axes1,'XGrid','on','YGrid','on');
    


%set legend
legend(axes1,'show','Location','NorthWest','FontSize',10,'FontWeight','bold');
legend(AlgType);

%set x, y Label
set(get(axes1,'XLabel'),'String','Time','FontSize',30,'FontWeight','bold');
set(get(axes1,'YLabel'),'String','Power (W)','FontSize',30,'FontWeight','bold')



%save to file
set(gcf, 'PaperPosition', [0 0 13 7]); %Position plot at left hand corner with width 5 and height 5.
set(gcf, 'PaperSize', [13 7]); %Set the paper to have width 5 and height 5.
%saveas(gcf, 'SolarTrace_High', 'pdf') %Save figure
saveas(gcf, strcat('.\figs\',AlgType), 'pdf') %Save figure  
saveas(gca, strcat('.\figs\',AlgType, '.eps'),'psc2') %Save figure 


end
 