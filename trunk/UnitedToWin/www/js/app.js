

$(document).ready(function(){
    $('select').formSelect();
    $('.tabs').tabs();
});


$("#btn-limpar").on("click", function () {
    $('#codMaca').val('');
    $('#codAlmas').val('');
    $('#nivel').val('XX');
    $('input[name="mult"][value="1"]').prop("checked",true)
    $("#respPortal").text("")
    $("#respPortalVezes").text("")
    
    $('#nvAtual').val('');
    $('#nvDesejado').val('');
    $("#respTreinamento").text("")
    $("#respDiamantes").text("")

});