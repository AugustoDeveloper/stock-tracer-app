(function() {
    window.bootstrapToast = {
      init: function() {
        console.log('init...');
            var toastElList = [].slice.call(document.querySelectorAll('.toast'));
            console.log(toastElList);
            var toastList = toastElList.map(function (toastEl) {
            return new bootstrap.Toast(toastEl, option)
            });
      }
    };
    window.apexChart = {
        renderChart: function(stonkSymbol, chartId, chartSelector, dataSeries) {
          var newDataSeries = dataSeries.map(function(d) {
            return {
              'x': new Date(d.x),
              'y': d.y
            };
          });

          let charDiv = document.querySelector(`#${chartSelector}`);
          if ( charDiv && 
               charDiv.children &&
               charDiv.children.length > 0) {
            
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

            let chartSelectorDiv = document.querySelector(`#${chartSelector}`);
            console.log(chartSelectorDiv);
            console.log(chartSelector);
      
            var chart = new ApexCharts(chartSelectorDiv, options);
            chart.render();
          }
        }
    };
})();