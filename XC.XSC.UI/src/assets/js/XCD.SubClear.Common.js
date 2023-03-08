//Datepicker
// var Datepicker = function () {
//     var arrows;
//     arrows = {
//         leftArrow: '<i class="fas fa-chevron-left"></i>',
//         rightArrow: '<i class="fas fa-chevron-right"></i>',
//     }

//     // input group layout 
//     $('.datepickers').datepicker({
//         todayHighlight: true,
//         orientation: "auto",
//         templates: arrows,
//         format: "dd/mm/yyyy"
//     });

// }();

(function ($) {
    "use strict";
    $(".preloader").fadeOut();

    $('#to-recover').on("click", function () {
        $("#login-form").hide();
        $("#recover-form").fadeIn();
    });

    $('#to-login').on("click", function () {
        $("#login-form").fadeIn();
        $("#recover-form").hide();
    });
    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }

                form.classList.add('was-validated')
            }, false)
        })

    //Password visibilty
    $('#pwd-eye, #pwd-eye1, #pwd-eye2, #pwd-eye3').click(function () {

        if ($(this).hasClass('fa-eye-slash')) {

            $(this).removeClass('fa-eye-slash');

            $(this).addClass('fa-eye');

            $('#login-pwd, #oldpassword, #newpassword, #newconfirmpassword').attr('type', 'text');

        } else {

            $(this).removeClass('fa-eye');

            $(this).addClass('fa-eye-slash');

            $('#login-pwd, #oldpassword, #newpassword, #newconfirmpassword').attr('type', 'password');
        }
    });

    //Mobile Menu
    $('#mobilemenu').click(function (e) {
        e.stopPropagation();
        $("body").toggleClass("menu-active", 200);
    });

    //Date range picker
    // $('.daterangepickers').daterangepicker({
    //     locale: {
    //         format: 'DD/MM/YYYY'
    //       }
    // });

    
    
    // $('.dashboardtab').DataTable({
    //     "responsive": true,
    //     "info": false,
    //     "lengthChange": false,
    //     "paging": false
    // });

    
    
    // $('.dashboardtable').DataTable({
    //     "responsive": true,
    //     "info": true,
    //     "lengthChange": false,
    //     "paging": true,
    //     "pageLength" : 5,
    //     "pagingType": "full_numbers"
    // });


    // //Alert dismiss after 5 sec
    // $(".alert").fadeTo(4000, 400).slideUp(400, function () {
    //     $(".alert").slideUp(400);
    // });

    // //Multiselect dropdown
    // $('.multiselect-dropdown').multiselect({
    //     enableFiltering: false,
    // });

    // //btn action
    // $('.btn-action').click(function(){
    //     $('.divshow').removeClass('d-none')
    // })

    // $(function () {
    //     $("[data-bs-toggle='tooltip']").tooltip();
        
    //     //Datatable bootstrap
    //     $('#custom-table').DataTable({
    //         "responsive": true,
    //         "lengthMenu":[5,10, 25, 50, "All"],
    //         "pagingType": "full_numbers"
    //     });
    // })
    
})(jQuery);




function addrole() {
    document.getElementById("table-role").insertRow(-1).innerHTML = '<td><select class="form-select"><option value="">Select</option><option value="">Admin</option><option value="">Processor</option><option value="">Manager</option></select></td><td><select class="form-select"><option value="">Select</option><option value="">Property</option><option value="">Casualty</option></select></td><td><select class="form-select"><option value="">Select</option><option value="">Harry</option><option value="">Lisa</option></select></td><td><input type="text" class="form-control datepickers" placeholder="Eg: 12/09/2021"></td><td><a class="btn btn-secondary btn-sm btnicon"><i class="fa fa-plus"></i></a><a class="btn btn-secondary btn-sm btnicon" onclick="deleterole();"><i class="fa fa-trash"></i></a></td>';
    
}


//toggle sidebar

var mini = true;

function toggleSidebar() {
  if (mini) {
    document.getElementById("mySidebar").style.width = "300px";
    
    document.getElementById("wrapper").style.paddingLeft = "315px";
    this.mini = false;
  } else {
    document.getElementById("mySidebar").style.width = "75px";
    
    document.getElementById("wrapper").style.paddingLeft = "100px";
    this.mini = true; 
  }
};

function showSidebar(){   
    var x = document.getElementById("mySidebar"); 
    if (x.style.width === "75px") {
        x.style.width = "300px";
        
    document.getElementById("wrapper").style.marginLeft = "215px";
      } else {
        x.style.width = "75px";
        
    document.getElementById("wrapper").style.marginLeft = "0px";
      }
};



// Highcharts.chart('chartpie', {
//     chart: {
//         type: 'pie',
//         height:300,
//         style: {
//             fontFamily: 'Ubuntu',
//             ffontWeight: '500',
//             fontSize: '13px'
//         }
//     },
//     title: {
//         text: null,
//         style: {
//             fontFamily: 'Ubuntu',
//             ffontWeight: '500',
//             fontSize: '12.5pt',
//             display:'none'
//         }
//     },
//     tooltip: {
//         pointFormat: '<b>{point.percentage:.0f}%</b>',
//         borderColor: null
//     },
//     accessibility: {
//         point: {
//             valueSuffix: '%'
//         }
//     },
//     plotOptions: {
//       pie: {
//         innerSize: 100,
//         depth: 45,
//         allowPointSelect: true,
//         slicedOffset: 0,
//         cursor: 'pointer',
//         dataLabels: {
//             enabled: true,
//             distance: -40,
//             format:"{point.percentage:.0f}%"
//         },
//         showInLegend: true
//       }
//     },
//     legend: {    
//       padding:0,
//       margin:20,
//       width:0,
//       align: 'center',
//       verticalAlign: 'top',
//       layout: 'horizontal',
//       x: 0,
//       y: 0,
//       itemStyle: {
//         fontWeight: '500'
//       }
//     },
//     series: [{
//         name: 'Brands',
//         colorByPoint: true,
//         data: [{
//             name: 'Outscope',
//             y: 6,
//             sliced: false,
//             color: '#2254c3'
//         }, {
//             name: 'Inscope',
//             y: 7,
//             sliced: false,
//             color: '#f58026'
//         }]
//     }],
//     credits:false
// });
  
  

// Highcharts.chart('chartbar', {
//     chart: {
//         type: 'column'
//     },
//     title: {
//         text: null,
//         style: {
//             fontFamily: 'Ubuntu',
//             ffontWeight: '500',
//             fontSize: '12.5pt',
//             display:'none'
//         }
//     },
//     subtitle: {
//         text: ' '
//     },
//     xAxis: {
//         categories: [
//             'STP',
//             'Data Validation',
//             'Review',
//             'Submitted to PAS'
//         ],
//         crosshair: true
//     },
//     yAxis: [{ // Primary yAxis
//         labels: {
//             format: '{value}'
//         },
//         title: {
//             text: 'Number of Submission',
//             style: {
//                 color: Highcharts.getOptions().colors[1]
//             }
//         }
//     }, { // Secondary yAxis
//         title: {
//             text: '',
//             style: {
//                 color: Highcharts.getOptions().colors[0]
//             }
//         },
//         labels: {
//             format: '{value}',
//             style: {
//                 color: Highcharts.getOptions().colors[0]
//             }
//         },
//         opposite: true
//     }],
//     tooltip: {
//         headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
//         pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
//             '<td style="padding:0"><b>{point.y:.0f}</b></td></tr>',
//         footerFormat: '</table>',
//         shared: true,
//         useHTML: true
//     },
//     plotOptions: {
//         column: {
//             pointPadding: 0.2,
//             borderWidth: 0
//         },
//         bar: {
//             dataLabels: {
//                 enabled: true
//             }
//         }
//     },
    
//     legend: {    
//         padding:0,
//         margin:20,
//         width:0,
//         align: 'center',
//         verticalAlign: 'top',
//         layout: 'horizontal',
//         x: 0,
//         y: 0,
//         itemStyle: {
//           fontWeight: '500'
//         }
//       },
//     series: [{
//         name: 'No of Cases',
//         type: 'column',        
//         color: '#2254c3',
//         yAxis: 1,
//         data: [3, 2, 1, 2],
//         tooltip: {
//             valueSuffix: ''
//         }

//     }, {
//         name: 'TAT',
//         type: 'spline',     
//         color: '#f58025',
//         data: [3, 2, 1, 2],
//         tooltip: {
//             valueSuffix: ''
//         }
//     }],
//     credits:false
// });

function functionalert(){
    $("#displayinfo").append('<div class="custom-alert"><div class="alert alert-danger" id="success-alert" role="alert"><strong>Rejected</strong><br>Rejection email sent to "External Party" for Submission No. xxxx<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button></div></div>');
    window.setTimeout(function () { 
        $("#success-alert").alert('close'); 
     }, 3000);
}

function functionreview(){
    $("#displayinfo").append('<div class="custom-alert"><div class="alert alert-success" id="success-alert" role="alert"><strong>Moved to PAS</strong><br>Updated Successfully.<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button></div></div>');
    window.setTimeout(function () { 
        $("#success-alert").alert('close'); 
     }, 3000);
}