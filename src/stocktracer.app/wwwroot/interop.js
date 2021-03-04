(function() {
    window.apexChart = {
        renderChart: function(stonkSymbol, chartId, chartSelector, dataSeries) {
          var newDataSeries = dataSeries.map(function(d) {
            return {
              'x': new Date(d.x),
              'y': d.y
            };
          });

          console.log(newDataSeries);

          if (document.querySelector(`#${chartSelector}`).children.length > 0) {
            
            ApexCharts.exec(chartId, 'updateSeries', [{
              data: newDataSeries
            }], true);

          } else {

            var options = {
                series: [{
                data: newDataSeries
              }],
                chart: {
                id: chartId,
                type: 'candlestick',
                height: 400,
                width: '100%'
              },
              theme: {
                mode: 'dark',
                palete: 'palete9',
                monochrome: {
                  enabled: false,
                  color: '#5A2A27',
                  shadeTo: 'dark',
                  shadeIntesity: 0
                }
              },
              title: {
                text: `${stonkSymbol} Chart`,
                align: 'left'
              },
              tooltip: {
                x: {
                  format: 'dd MMM yyyy HH:mm'
                }
              },
              xaxis: {
                type: 'datetime',
                labels: {
                  datetimeUTC: false
                }
              },
              yaxis: {
                tooltip: {
                  enabled: true
                }
              }
            };
      
            var chart = new ApexCharts(document.querySelector(`#${chartSelector}`), options);
            chart.render();
          }
        }
    };
})();