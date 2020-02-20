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
    $('#hospital').val("");
    $( 'select' ).formSelect();
    $('#data_plantao').val('');
    $('#data_pagamento').val('');
    $('#data_plantao').val('');
    $('input[type="checkbox"]').prop("checked",false)
    $('#valor').val('');

});


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
            var m = new Date(o.DATA).toLocaleString('default', { month: 'short'}).toString().toUpperCase();
            if (r[m]){
                r[m].push({HOSPITAL: o.HOSPITAL, VALOR: o.VALOR, COR: o.COR});
            } else {
                r[m] =  [{HOSPITAL: o.HOSPITAL, VALOR: o.VALOR, COR: o.COR}]
            }
            return r;
        }, {});
        PLANTOES[element[0].DATA.split(('-'))[2]] = mes
    });

    var temp2 = LP.reduce(function (r, o) {
        var m = o.DATA.split(('-'))[2];
        if (r[m]){
            r[m].MESES.push({HOSPITAL: o.HOSPITAL, VALOR: o.VALOR, DATA: o.DATA, COR: o.COR})
        } else {
            r[m] = {ANO: m, MESES: [{HOSPITAL: o.HOSPITAL, VALOR: o.VALOR, DATA: o.DATA, COR: o.COR}]};
        }
        return r;
    }, {});
    PLANTOES_2 = Object.keys(temp2).map(function(k){ return temp2[k]; });





    PLANTOES_2.forEach(element => {
        var mes = element.MESES.reduce(function (r, o) {
            var m = new Date(o.DATA).toLocaleString('default', { month: 'long'}).toString();
            if (r[m]){
                r[m].DADOS.push({HOSPITAL: o.HOSPITAL, VALOR: o.VALOR, COR: o.COR});
            } else {
                r[m] = {MES: m, DADOS:[{HOSPITAL: o.HOSPITAL, VALOR: o.VALOR, COR: o.COR}]}
            }
            return r;
        }, {});
        var temp3 = Object.keys(mes).map(function(k){ return mes[k]; });

        temp2[element.MESES[0].DATA.split(('-'))[2]].MESES = temp3
    });
    PLANTOES_2 = Object.keys(temp2).map(function(k){ return temp2[k]; });

}

let buildMonthSelect = function () {
    var selectHTML = '';
    selectHTML += '<select>';
    
    PLANTOES_2.forEach(element => {
        selectHTML += ' <optgroup Label="' + element.ANO + '">';
        element.MESES.forEach(el => {
            selectHTML += ' <option>' + el.MES + '</option>';
        });
        selectHTML += ' </optgroup>';
    });
    selectHTML += '</select>';
    $('#select-mes').html(selectHTML);
    $( 'select' ).formSelect();

}