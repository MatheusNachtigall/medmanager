$(document).ready(function(){
    $('select').formSelect();
    $('.tabs').tabs();
    $('.sidenav').sidenav({
        draggable: true
    });
    
    // .on('click tap', 'li a', () => {
    //     $('.sidenav').sidenav('close');
    // });
    $(".dropdown-trigger").dropdown({
        hover: false
    });
    $('.collapsible').collapsible();
    $('.date').mask('00/00/0000', { placeholder: "__/__/____" });
    $('.money').mask('#.##0,00', { reverse: true });
    var ctx = document.getElementById('chart-area').getContext('2d');
    window.myPie = new Chart(ctx, config);







    WS.InserirRequisicao('LOAD_PLANTOES', {}, function(ret) {
        if (ret.sucesso) {
            console.log('RET: ', ret.lstPlantao);
        }
    });






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

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
  }