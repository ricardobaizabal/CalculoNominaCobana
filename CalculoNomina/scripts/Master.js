$(function () {
    setNavigation();
    MuestraEmpresas();

    //var empresaId = $('.IdEmpresa').val();

    //if (empresaId == 0) {
    //    MuestraMensaje();

    //} else {
      
    //}
});
function MuestraEmpresas() {

    $.ajax({
        type: "POST",
        url: "../Empresa.aspx/MuestraMenu",
        //url: "../humantop/Empresa.aspx/MuestraMenu",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {

            response = jQuery.parseJSON(result.d);
            if (response.Error == 0) {
                $(response.Empresa).each(function () {
                    if (this.Activo == 0) {
                        $('.metismenu li').removeClass('disabled');
                    } else {
                        $('.metismenu li').removeClass('disabled');
                    }
                });

            }
            else {
                OpenWindowMessage(response.Descripcion);
            }
        },
        complete: function () {
            //HideLock();
        },
        error: function (xhr, status, error) {
            // Boil the ASP.NET AJAX error down to JSON.
            var err = eval("(" + xhr.responseText + ")");

            // Display the specific error raised by the server (e.g. not a
            //   valid value for Int32, or attempted to divide by zero).
            OpenWindowMessage(err.Message);
        }
    });
}
function valida(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 13) {
        busqueda();
    }
}
//function MuestraMensaje() {
//    setTimeout(function () {
//        toastr.options = {
//            closeButton: true,
//            progressBar: true,
//            showMethod: 'slideDown',
//            //positionClass: toast-top-full-width,
//            timeOut: 4000
//        };
//        toastr.success('NomiLink', 'Bienvenido a ');
//        //toastr.success('Favor de Seleccionar una empresa', 'NomiLink Notificacion');

//    }, 1300);
//    var data1 = [
//        [0, 4], [1, 8], [2, 5], [3, 10], [4, 4], [5, 16], [6, 5], [7, 11], [8, 6], [9, 11], [10, 30], [11, 10], [12, 13], [13, 4], [14, 3], [15, 3], [16, 6]
//    ];
//    var data2 = [
//        [0, 1], [1, 0], [2, 2], [3, 0], [4, 1], [5, 3], [6, 1], [7, 5], [8, 2], [9, 3], [10, 2], [11, 1], [12, 0], [13, 2], [14, 8], [15, 0], [16, 0]
//    ];
//    $("#flot-dashboard-chart").length && $.plot($("#flot-dashboard-chart"), [
//        data1, data2
//    ],
//            {
//                series: {
//                    lines: {
//                        show: false,
//                        fill: true
//                    },
//                    splines: {
//                        show: true,
//                        tension: 0.4,
//                        lineWidth: 1,
//                        fill: 0.4
//                    },
//                    points: {
//                        radius: 0,
//                        show: true
//                    },
//                    shadowSize: 2
//                },
//                grid: {
//                    hoverable: true,
//                    clickable: true,
//                    tickColor: "#d5d5d5",
//                    borderWidth: 1,
//                    color: '#d5d5d5'
//                },
//                colors: ["#1ab394", "#1C84C6"],
//                xaxis: {
//                },
//                yaxis: {
//                    ticks: 4
//                },
//                tooltip: false
//            }
//    );
//    //var doughnutData = [
//    //    {
//    //        value: 300,
//    //        color: "#a3e1d4",
//    //        highlight: "#1ab394",
//    //        label: "App"
//    //    },
//    //    {
//    //        value: 50,
//    //        color: "#dedede",
//    //        highlight: "#1ab394",
//    //        label: "Software"
//    //    },
//    //    {
//    //        value: 100,
//    //        color: "#A4CEE8",
//    //        highlight: "#1ab394",
//    //        label: "Laptop"
//    //    }
//    //];
//    var doughnutOptions = {
//        segmentShowStroke: true,
//        segmentStrokeColor: "#fff",
//        segmentStrokeWidth: 2,
//        percentageInnerCutout: 45, // This is 0 for Pie charts
//        animationSteps: 100,
//        animationEasing: "easeOutBounce",
//        animateRotate: true,
//        animateScale: false
//    };
//    //var ctx = document.getElementById("doughnutChart").getContext("2d");
//    //var DoughnutChart = new Chart(ctx).Doughnut(doughnutData, doughnutOptions);
//    //var polarData = [
//    //    {
//    //        value: 300,
//    //        color: "#a3e1d4",
//    //        highlight: "#1ab394",
//    //        label: "App"
//    //    },
//    //    {
//    //        value: 140,
//    //        color: "#dedede",
//    //        highlight: "#1ab394",
//    //        label: "Software"
//    //    },
//    //    {
//    //        value: 200,
//    //        color: "#A4CEE8",
//    //        highlight: "#1ab394",
//    //        label: "Laptop"
//    //    }
//    //];
//    var polarOptions = {
//        scaleShowLabelBackdrop: true,
//        scaleBackdropColor: "rgba(255,255,255,0.75)",
//        scaleBeginAtZero: true,
//        scaleBackdropPaddingY: 1,
//        scaleBackdropPaddingX: 1,
//        scaleShowLine: true,
//        segmentShowStroke: true,
//        segmentStrokeColor: "#fff",
//        segmentStrokeWidth: 2,
//        animationSteps: 100,
//        animationEasing: "easeOutBounce",
//        animateRotate: true,
//        animateScale: false
//    };
//    //var ctx = document.getElementById("polarChart").getContext("2d");
//    //var Polarchart = new Chart(ctx).PolarArea(polarData, polarOptions);
//}
function getUrlParameters() {
    var vars = {};
    var parts = window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi,
    function (m, key, value) {
        vars[key] = value;
    });
    return vars;
}
function setNavigation() {

    var url_parts = location.href.split('/');

    var last_segment = url_parts[url_parts.length - 1];

    $('.metismenu a[href="' + last_segment + '"]').parents('li').addClass('active');

    //if (last_segment == 'EditorSubsidioDiaria.aspx?id=1')
    //{
    //    //var array = last_segment.split('Editor');
    //    //var page= array.slice(0,-5)
    //    //var page = array[array.length - 1];
    //    var Editor_Page = page.substr(-3);

    //    //$('.metismenu a[href="' + Editor_Page + '"]').parents('li').addClass('active');
    //}
}