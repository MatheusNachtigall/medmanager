var TESTE;

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
    
    
    const date = new Date(); 
    const month = date.toLocaleString('default', { month: 'long' }).toString().toUpperCase();
    $('#mes-atual').html(month);
    
    $('#select-mes').val(month.toLowerCase());
    $( 'select' ).formSelect();
    
    
    LOAD_PLANTOES();

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
