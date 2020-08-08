
var buscarValorPlantao = function () {
    const periodo = $(this).val();
    if (periodo != ''){
        const hospital = $("#select-hospital option:selected").html();
        if(hospital != ''){
            let valor = LP.filter(e => ((e.HOSPITAL== hospital) && (e.PERIODO == periodo))).map(e => e.VALOR).filter((e,i,arr) => arr.indexOf(e) == i);
            let $valorField = $(this).parents('.field').find('input[type=text].money')
            
            
            if(valor.length == 1){
                $valorField.val(valor[0]);
                $valorField.siblings('label').addClass("active");
            } else {
                $valorField.val('');
                $valorField.siblings('label').removeClass("active");
            }
        }
    }
}


let clearInserirPlantaoFields = function () {
    $("#frm_adicionar_plantao .field:not(:first-child)").remove()
    $('#frm_adicionar_plantao input').val('');
    $('#frm_adicionar_plantao select').val('');
    $('input[type="checkbox"]').prop("checked",false)
    // $('#select-hospital').val("");
    // $('#select-periodo').val("");
    // $( 'select' ).formSelect();
    // $('#data_plantao').val('');
    // $('#hora_plantao').val('');
    // $('#valor').val('');
    // $('#data_pagamento').val('');
    // Dropzone.forElement("#dragAndDropField").removeAllFiles();
    // $('#trash-can').addClass('hidden');
    // $('.dz-default.dz-message').css('display','block');
}


const fillSelectHoras = function(position){
    let html = '';
    html += '<select id="select-periodo_' + position + '" name="select-periodo_' + position + '" class="periodo requireValidation">';
    html += "   <option value=''></option>";
    html += "   <option value=6>6 Horas</option>";
    html += "   <option value=12>12 Horas</option>";
    html += "   <option value=24>24 Horas</option>";
    html += "   <option disabled>──────────</option>";
    html += "   <option value=1>1 Hora</option>";
    html += "   <option value=2>2 Horas</option>";
    html += "   <option value=3>3 Horas</option>";
    html += "   <option value=4>4 Horas</option>";
    html += "   <option value=5>5 Horas</option>";
    html += "   <option value=7>7 Horas</option>";
    html += "   <option value=8>8 Horas</option>";
    html += "   <option value=9>9 Horas</option>";
    html += "   <option value=10>10 Horas</option>";
    html += "   <option value=11>11 Horas</option>";
    html += "   <option value=13>13 Horas</option>";
    html += "   <option value=14>14 Horas</option>";
    html += "   <option value=15>15 Horas</option>";
    html += "   <option value=16>16 Horas</option>";
    html += "   <option value=17>17 Horas</option>";
    html += "   <option value=18>18 Horas</option>";
    html += "   <option value=19>19 Horas</option>";
    html += "   <option value=20>20 Horas</option>";
    html += "   <option value=21>21 Horas</option>";
    html += "   <option value=22>22 Horas</option>";
    html += "   <option value=23>23 Horas</option>";
    html += "</select>";
    return html;
}



let validatePlantao = function () {
    var valid = true;
    $('.invalid').removeClass('invalid')
    
    if ($("#select-hospital").val() == '') {
        $("#select-hospital").siblings('input').addClass('invalid').change();
        valid = false;
    }
    
    $('.requireValidation').each(function () {
        if ($(this).val() == '') {
            if ($(this).is('select')) {
                $(this).siblings('input').addClass('invalid').change();
            } else {
                $(this).addClass('invalid').change();
            }
            valid = false;
        }
    })
    
    if (valid) {
        $('#modal-inserir-plantao .modal-hospital').html($("#select-hospital option:selected").html());
        $('#modal-inserir-plantao .modal-INSS').html(($("#INSS").prop('checked') ? "Sim" : "Não"));
        $('#modal-inserir-plantao .modal-CNPJ').html(($("#CNPJ").prop('checked') ? "Sim" : "Não"));

        var table_body = '';
        $('.field-multiple .field').each(function(){
            table_body += '<tr>';
            table_body += ' <td>' + $('input[type=date]', $(this)).val().replace(/(\d\d\d\d)-(\d\d)-(\d\d)/,'$3/$2/$1') + '</td>';
            table_body += ' <td>' + $('input[type=time]', $(this)).val() + '</td>';
            table_body += ' <td>' + $('select option:selected', $(this)).val() + '</td>';
            table_body += ' <td>' + $('input[type=text].money', $(this)).val() + '</td>';
            table_body += '</tr>';
        });
        //$('#modal-inserir-plantao .modal-data').html($("#data_plantao").val().replace(/(\d\d\d\d)-(\d\d)-(\d\d)/,'$3/$2/$1'));
        

        
        $('#modal-inserir-plantao tbody').html(table_body);
        // $('#modal-inserir-plantao .modal-data-pagamento').html($("#data_pagamento").val().replace(/(\d\d\d\d)-(\d\d)-(\d\d)/,'$3/$2/$1'));
        $('#modal-inserir-plantao').modal('open')
    } else {
        $('.error:first').focus();
    }
    return false;
}


$(function() {
    $('#plantao-add').on('click', function(e){
        var $multiple = $(this).closest('.field-multiple');
        var $fields = $('.field', $multiple);
        var $lastField = $fields.eq($fields.length - 1);

        var $clone = $lastField.clone();
        
        var $cloneInput = $('input', $clone);
        $cloneInput.val('');
        $cloneInput.removeAttr('filled');
        
        var $cloneDataInput = $('input[type=date]', $clone)
        $cloneDataInput.attr('name', 'data_plantao_' + $fields.length);
        $cloneDataInput.attr('id', 'data_plantao_' + $fields.length);
        $cloneDataInput.siblings('label').attr('for', 'data_plantao_' + $fields.length);
        
        var $cloneHoraInput = $('input[type=time]', $clone)
        $cloneHoraInput.attr('name', 'hora_plantao_' + $fields.length);
        $cloneHoraInput.attr('id', 'hora_plantao_' + $fields.length);
        $cloneHoraInput.siblings('label').attr('for', 'hora_plantao_' + $fields.length);
        
        var $cloneSelect = $('.select-wrapper', $clone)
        var $selectPosition = $cloneSelect.parent();
        $cloneSelect.remove()
        $('label',$selectPosition).before(fillSelectHoras($fields.length));
        
        var $cloneMoneyInput = $('input[type=text].money', $clone)
        $cloneMoneyInput.attr('name', 'valor_' + $fields.length);
        $cloneMoneyInput.attr('id', 'valor_' + $fields.length);
        $cloneMoneyInput.siblings('label').attr('for', 'valor_' + $fields.length);

        $(this).parent().before($clone);
        $('#select-periodo_' + $fields.length).formSelect();
        //$('#select-periodo_' + $fields.length).change(buscarValorPlantao)
        $('.money').mask('#.##0,00', { reverse: true });

        $('.field-multiple .field-remove').on('click', function(e){
            $(this).closest('.field').remove();
        });
        $('.periodo').change(buscarValorPlantao)
    })


    $("#btn-limpar").on("click", function () {
        clearInserirPlantaoFields();
    });
    
    $('.periodo').change(buscarValorPlantao)


});
