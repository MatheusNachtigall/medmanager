var TESTE;

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
    
    
    

    
    
    
    WS.InserirRequisicao('LOAD_PLANTOES', {}, function(ret) {
        if (ret.sucesso) {
            console.log('RET: ', ret.lstPlantao);
            TESTE = ret.lstPlantao;
            var HospitaisDistintos = TESTE.map(e => e.HOSPITAL).filter( (v,i,s) => s.indexOf(v) === i);
            var dataSomas = []
            var cores = []
            for  (var i = 0; i < HospitaisDistintos.length; i++) {
                dataSomas.push(ret.lstPlantao.filter( (e => e.HOSPITAL == HospitaisDistintos[i])).map(e => e.VALOR).reduce(( acc, summ ) => acc + summ, 0));
                cores.push(random_rgba());
            }
            
            var config = {
                type: 'pie',
                data: {
                    datasets: [{
                        data: dataSomas,
                        backgroundColor: cores,
                        label: 'Dataset 1'
                    }],
                    labels: HospitaisDistintos
                },
                options: {
                    responsive: true
                }
            };
            
            
            window.myPie = new Chart(ctx, config);
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