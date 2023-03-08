
Highcharts.chart('chartpie1', {
    chart: {
        type: 'pie',
        height:260,
        
        options3d: {
            enabled: false,
            alpha: 45,
            beta: 0
        },
            style: {
            fontFamily: 'Ubuntu',
            ffontWeight: '500',
            fontSize: '13px'
        }
    },
    title: {
        text: null,
        style: {
            fontFamily: 'Ubuntu',
            ffontWeight: '500',
            fontSize: '12.5pt',
            display:'none'
        }
    },
    tooltip: {
        pointFormat: '<b>{point.percentage:.0f}%</b>',
        borderColor: null
    },
    accessibility: {
        point: {
            valueSuffix: '%'
        }
    },
    plotOptions: {
      pie: {
        innerSize: 100,
        depth: 45,
        allowPointSelect: true,
        slicedOffset: 0,
        cursor: 'pointer',
        dataLabels: {
            enabled: true,
            distance: -30,
            format:"{point.percentage:.0f}%"
        },
        showInLegend: true
      }
    },
    legend: {    
      padding:0,
      margin:20,
      width:0,
      align: 'center',
      verticalAlign: 'top',
      layout: 'horizontal',
      x: 0,
      y: 0,
      itemStyle: {
        fontWeight: '500'
      }
    },
    series: [{
        name: 'Brands',
        colorByPoint: true,
        data: [{
            name: 'Outscope',
            y: 6,
            sliced: false,
            color: '#2254c3'
        }, {
            name: 'Inscope',
            y: 7,
            sliced: false,
            color: '#f58026'
        }]
    }],
    credits:false
});
