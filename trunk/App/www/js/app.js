var TESTE;
var PLANTOES = null;
var PLANTOES_2 = null;
var LP = null;

$(document).ready(function(){
    $('.tabs').tabs();
    $('.sidenav').sidenav({
        draggable: true
    });
    
    $(".dropdown-trigger").dropdown({
        hover: false
    });
    $('.collapsible').collapsible();
    $('.date').mask('00/00/0000', { placeholder: "__/__/____" });
    $('.money').mask('#.##0,00', { reverse: true });
    
    LOAD_PLANTOES();
    
    const date = new Date(); 
    const month = date.toLocaleString('default', { month: 'long' }).toString().toUpperCase();
    //month = date.toLocaleString('default', { month: 'short', year: 'numeric' }).toString().replace(' de ','/').toUpperCase();
    
    $('#select-mes').val(month.toLowerCase());
    $( 'select' ).formSelect();
    
    //GET_GRAFICO_MES(month);

    
    
});

$('#frm_adicionar_plantao').submit(function() {
    console.log('Enviando Form');
    return false;
});

$("#btn-limpar").on("click", function () {
    $('#select-hospital').val("");
    $( 'select' ).formSelect();
    $('#data_plantao').val('');
    $('#data_pagamento').val('');
    $('#data_plantao').val('');
    $('input[type="checkbox"]').prop("checked",false)
    $('#valor').val('');

});

// $('a[href="#adicionar_plantao"').on('click', function(){
//     LOAD_HOSPITAIS();
// })



let arrangePlantoes = function (lstPlantoes) {
    
    PLANTOES = lstPlantoes.reduce(function (r, o) {
        var m = o.DATA.split(('-'))[2];
        if (r[m]){
            r[m].push({HOSPITAL: o.HOSPITAL, VALOR: o.VALOR, DATA: o.DATA, COR: o.COR})
        } else {
            r[m] = [{HOSPITAL: o.HOSPITAL, VALOR: o.VALOR, DATA: o.DATA, COR: o.COR}];
        }
        return r;
    }, {});
    var temp = Object.keys(PLANTOES).map(function(k){ return PLANTOES[k]; });
    
    
    temp.forEach(element => {
        var mes = element.reduce(function (r, o) {
            var m = new Date(o.DATA).toLocaleString('default', { month: 'long'}).toString();
            if (r[m]){
                r[m].push({HOSPITAL: o.HOSPITAL, VALOR: o.VALOR, COR: o.COR});
            } else {
                r[m] =  [{HOSPITAL: o.HOSPITAL, VALOR: o.VALOR, COR: o.COR}]
            }
            return r;
        }, {});
        PLANTOES[element[0].DATA.split(('-'))[2]] = mes
    });
}

let buildMonthSelect = function () {
    var selectHTML = '';
    selectHTML += '<select>';
    
    Object.keys(PLANTOES).forEach(element => {
        selectHTML += ' <optgroup Label="' + element + '">';
        Object.keys(PLANTOES[element]).forEach(el => {
            selectHTML += ' <option value="' + el + '/' + element + '">' + el.charAt(0).toUpperCase() + el.slice(1) + '</option>';
        });
        selectHTML += ' </optgroup>';
    });
    selectHTML += '</select>';
    $('#select-mes').html(selectHTML);
    $( 'select' ).formSelect();
}


let buildHospitalSelect = function (FullList) {
    FullList.map(e => {
        delete e.VALOR;
        delete e.DATA;
        delete e.COR;
        return e;
   })

    let result = [];
    let map = new Map();
    for (const item of FullList) {
        if(!map.has(item.HOSPITAL_ID)){
            map.set(item.HOSPITAL_ID, true);    // set any value to Map
            result.push({
                HOSPITAL_ID: item.HOSPITAL_ID,
                HOSPITAL: item.HOSPITAL
            });
        }
    }
    
    var selectHTML = '';
    selectHTML += '<select>';
    selectHTML += '<option value="" selected></option>';
    result.forEach(element => {
        selectHTML += ' <option value="' + element.HOSPITAL_ID + '">' + element.HOSPITAL + '</option>';
    });
    selectHTML += '</select>';
    $('#select-hospital').html(selectHTML);
    $( 'select' ).formSelect();
}


$('#select-mes').on('change', function(){
    let select_value = $(this).val();
    let lstPlantoes = PLANTOES[select_value.split('/')[1]][select_value.split('/')[0]];
    buildChartAndGraph(lstPlantoes)
});