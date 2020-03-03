var TESTE;
var PLANTOES = null;
var PLANTOES_2 = null;
var LP = null;

$(document).ready(function(){
    $('.tabs').tabs();
    $('.sidenav').sidenav({
        draggable: true
    });
    $('.modal').modal();

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
    
    $('.select-mes').val(month.toLowerCase());
    $( 'select' ).formSelect();
    
    //GET_GRAFICO_MES(month);

    
    
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
    selectHTML += '<option value=""></option>';
    
    Object.keys(PLANTOES).forEach(element => {
        selectHTML += ' <optgroup Label="' + element + '">';
        Object.keys(PLANTOES[element]).forEach(el => {
            selectHTML += ' <option value="' + el + '/' + element + '">' + el.charAt(0).toUpperCase() + el.slice(1) + '</option>';
        });
        selectHTML += ' </optgroup>';
    });
    selectHTML += '</select>';
    $('.select-mes').html(selectHTML);
    $( 'select' ).formSelect();
}


let buildHospitalSelect = function (FullList) {
    var HospitalList = [];
    FullList.map(e => HospitalList.push({HOSPITAL_ID: e.HOSPITAL_ID, HOSPITAL: e.HOSPITAL}))

    let result = [];
    let map = new Map();
    for (const item of HospitalList) {
        if(!map.has(item.HOSPITAL_ID)){
            map.set(item.HOSPITAL_ID, true);    // set any value to Map
            result.push({
                HOSPITAL_ID: item.HOSPITAL_ID,
                HOSPITAL: item.HOSPITAL
            });
        }
    }
    
    var selectHTML = '';
    selectHTML += '<option value="" selected></option>';
    result.forEach(element => {
        selectHTML += ' <option value="' + element.HOSPITAL_ID + '">' + element.HOSPITAL + '</option>';
    });
    $('#select-hospital').html(selectHTML);
    $( 'select' ).formSelect();
    $("select[required]").css({display: "block", height: 0, padding: 0, width: 0, position: 'absolute'});

}


let buildDetailedPlantaoList = function () {
    var HTML = '';
    const sortedPlantoes = LP.sort((a, b) => new Date(a.DATA) - new Date(b.DATA))

    sortedPlantoes.forEach(el => {
        HTML += '<div class="row plantao-id-' + el.PLANTAO_ID + '">';
        HTML += '   <div class="plantao-info col s12 m6">';
        HTML += '       <div class="card ' + (el.RECEBIDO ? 'light-green' : 'red') + ' lighten-3">';
        HTML += '           <div class="card-content">';
        HTML += '               <span class="card-title plantao-data fw700">' + new Date(el.DATA).toLocaleDateString() + '</span>';
        HTML += '               <div class="fixed-action-btn horizontal" style="position:relative; float:right; bottom:50px; right:-20px">';
        HTML += '                   <a class="btn-floating waves-effect waves-light green darken-4"><i class="material-icons">more_vert</i></a>';
        HTML += '                   <ul>';
        HTML += '                       <li><a class="btn-small btn-floating yellow darken-1"><i class="material-icons">mode_edit</i></a></li>';
        HTML += '                       <li><a class="btn-small btn-floating green"><i class="material-icons">monetization_on</i></a></li>';
        HTML += '                       <li><a class="btn-small btn-floating red"><i class="material-icons">delete</i></a></li>';
        HTML += '                   </ul>';
        HTML += '               </div>';
        HTML += '               <div class="row">';
        HTML += '                   <p class="col s6 fw700 plantao-hospital">' + el.HOSPITAL + '</p>';
        HTML += '                   <p class="col s6 fw700 plantao-valor">R$ ' + el.VALOR + '</p>';
        HTML += '               </div>';
        HTML += '           </div>';
        HTML += '       </div>';
        HTML += '   </div>';
        HTML += '</div>';
    });
    
    
    
    
    
    $('.plantoes_list').html(HTML);

    for (var i = 0; i < sortedPlantoes.length; i++) {
        var $item = $('.plantao-id-' + sortedPlantoes[i].PLANTAO_ID);
        $item.find('.plantao-data').data('id', sortedPlantoes[i].PLANTAO_ID);
        $item.find('.plantao-data').data('data', sortedPlantoes[i].DATA);
    }

    $('.fixed-action-btn').floatingActionButton({
        direction: 'left',
        hoverEnabled: false
    });


}


$('#principal .select-mes').on('change', function(){
    let select_value = $(this).val();
    let lstPlantoes = PLANTOES[select_value.split('/')[1]][select_value.split('/')[0]];
    buildChartAndGraph(lstPlantoes)
});

$('#plantoes_detalhes .select-mes').on('change', function(){
    let month = $(this).val().split('/')[0];
    let plantao_list = $('.plantoes_list').children();
    
    for (var i = 0; i < plantao_list.length; i++) {
        if (month == ""){
            $(plantao_list[i]).show();
        } else {
            var plantao_mes = $(plantao_list[i]).find('.plantao-data');
            var m = new Date(plantao_mes.data('data')).toLocaleString('default', { month: 'long'}).toString();
            if (m == month){
                $(plantao_list[i]).show();
            } else {
                $(plantao_list[i]).hide();
            }
        }
    };
});

$('#frm_adicionar_plantao').submit(function() {
    var valid = true;
    $("#select-hospital").removeClass('invalid');
    $("#data_plantao").removeClass('invalid');
    $("#data_pagamento").removeClass('invalid');
    $("#valor").removeClass('invalid');
    $('.input-field .error-info').remove();
    
    if ($("#select-hospital").val() == '') {
        $("#select-hospital").addClass('invalid').change();
        $("#select-hospital").parent().parent('.input-field').append('<span class="error-info">* Campo obrigatório</span>');
        valid = false;
    }
    if ($("#data_plantao").val() == '') {
        $("#data_plantao").addClass('invalid').change();
        $("#data_plantao").parent('.input-field').append('<span class="error-info">* Campo obrigatório</span>');
        valid = false;
    }
    if ($("#data_pagamento").val() == '') {
        $("#data_pagamento").addClass('invalid').change();
        $("#data_pagamento").parent('.input-field').append('<span class="error-info">* Campo obrigatório</span>');
        valid = false;
    }
    if ($("#valor").val() == '') {
        $("#valor").addClass('invalid').change();
        $("#valor").parent('.input-field').append('<span class="error-info">* Campo obrigatório</span>');
        valid = false;
    }
    if (valid) {
        $('.modal-inserir-plantao')
        $('#modal-inserir-plantao #modal-hospital').html($("#select-hospital option:selected").html());
        $('#modal-inserir-plantao #modal-data-plantao').html($("#data_plantao").val().replace(/(\d\d\d\d)-(\d\d)-(\d\d)/,'$3/$2/$1'));
        $('#modal-inserir-plantao #modal-data-pagamento').html($("#data_pagamento").val().replace(/(\d\d\d\d)-(\d\d)-(\d\d)/,'$3/$2/$1'));
        $('#modal-inserir-plantao #modal-INSS').html(($("#INSS").prop('checked') ? "Sim" : "Não"));
        $('#modal-inserir-plantao #modal-CNPJ').html(($("#CNPJ").prop('checked') ? "Sim" : "Não"));
        $('#modal-inserir-plantao #modal-valor').html($("#valor").val());
        $('#modal-inserir-plantao').modal('open')
    } else {
        $('.error:first').focus();
    }
    return false;
});
        
        
$('#modal-btn-confirmar').click(function() {
    REQ_INSERIR_PLANTAO();
});