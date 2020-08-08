let REQ_LOAD_PLANTOES = function () {
    WS.InserirRequisicao('LOAD_PLANTOES', {}, function(ret) {
        if (ret.sucesso) {
            LP = ret.lstPlantao;
            arrangePlantoes(ret.lstPlantao);
            buildHospitalSelect(ret.lstPlantao);
            buildMonthSelect();
            let month = new Date().toLocaleString('default', { month: 'long', year: 'numeric' }).toString().replace(' de ','/');
            let lstPlantoes = PLANTOES[month.split('/')[1]][month.split('/')[0]];
            buildChartAndGraph(lstPlantoes);
            $('.select-mes').val(month);
            $( 'select' ).formSelect();
            buildDetailedPlantaoList();
            
            clearCalendarEvents();
            loadCalendarEvents();
        }
    });
}

let REQ_INSERIR_PLANTAO = function () {

    arrDatas = [];
    arrHorarios = [];
    arrPeriodos = [];
    arrValores = [];
    
    $('.field-multiple .field').each(function(){
        arrDatas[arrDatas.length] = $('input[type=date]', $(this)).val();
        arrHorarios[arrHorarios.length] = $('input[type=time]', $(this)).val();
        arrPeriodos[arrPeriodos.length] = $('select', $(this)).val();
        arrValores[arrValores.length] = $('input[type=text].money', $(this)).val();
    });

    var metodo = ($('#id').val() == "" ? 'INSERIR_PLANTAO' : 'EDITAR_PLANTAO')

    WS.InserirRequisicao(metodo, {
        HOSPITAL_ID:    $('#select-hospital').val(),
        DATA:   arrDatas,
        HORARIO: arrHorarios,
        PERIODO: arrPeriodos,
        VALOR: arrValores,
        INSS: $("#INSS").prop('checked'),
        CNPJ: $("#CNPJ").prop('checked'),
        PLANTAO_ID: ($('#id').val() == "" ? 0 : $('#id').val())
    }, function(ret) {
        if (ret.sucesso){
            // $('input').val('');
            // $('input[type="checkbox"]').prop("checked",false)
            clearInserirPlantaoFields();
            if ($('#id').val() == ""){
                M.toast({html: 'Plantão inserido!', classes: 'rounded green fw700'});            
            } else {
                M.toast({html: 'Plantão atualizado!', classes: 'rounded yellow black-text fw700'});
            }
            $('#id').val('');
            $('.tabs').tabs('select', 'calendar');
            REQ_LOAD_PLANTOES();
        } else {
            M.toast({html: 'Ocorreu um erro!', classes: 'rounded red fw700'});
            //toast com falha e motivo da falha
        }
    })
}

let REQ_MARCAR_PLANTAO_RECEBIDO = function (plantao_id) {
    WS.InserirRequisicao('MARCAR_PLANTAO_RECEBIDO', {
        PLANTAO_ID:    plantao_id,
    }, function(ret) {
        if (ret.sucesso){
            M.toast({html: 'Pagamento inserido!', classes: 'rounded green fw700'});
            REQ_LOAD_PLANTOES();
        } else {
            M.toast({html: 'Ocorreu um erro!', classes: 'rounded red fw700'});
            //toast com falha e motivo da falha
        }
    })
}

let REQ_EXCLUIR_PLANTAO = function (plantao_id) {
    $('.modal').modal('close');
    WS.InserirRequisicao('EXCLUIR_PLANTAO', {
        PLANTAO_ID:    plantao_id,
    }, function(ret) {
        if (ret.sucesso){
            M.toast({html: 'Plantão Excluido!', classes: 'rounded green fw700'});
            REQ_LOAD_PLANTOES();
        } else {
            M.toast({html: 'Ocorreu um erro!', classes: 'rounded red fw700'});
            //toast com falha e motivo da falha
        }
    })
}


let PREP_INSERIR_PLANTAO = function () {
    var media = [];
    var done = 0;
    
    var processaUpload = function(file, order) {
        console.log('0:', file);
        console.log('0:', order);
        
        done++;
        var formData = new FormData();
        formData.append('ACAO', 'UPLOAD_MEDIA');
        // formData.append('WS_USUARIO_ID', WS_USUARIO_ID);
        // formData.append('WS_TOKEN', WS_TOKEN);
        formData.append('MEDIA', file);
        formData.append('ORDEM', order); 
        console.log('1: ',formData);
        var uploadSucesso = false;
        $.ajax({
            url: WS_HTTP_URL,
            data: formData,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function(data) {
                var ret = JSON.parse(data);
                if (ret.sucesso) {
                    done--;
                    media.push(ret.fileName);
                    if (done == 0) REQ_INSERIR_PLANTAO(media);
                } else {
                    if (ret.motivo == "SESSAO_INVALIDA") {
                        WS.Logout();
                    } 
                    else if (ret.motivo == "FORMATO_INCORRETO") {
                        $('#nova-postagem-form .submit').removeClass('submit--sending');
                        toggleAlert('Formato do arquivo inválido.');
                        _BLOCK_AJAX = false;
                    }
                    else {
                        $('#nova-postagem-form .submit').removeClass('submit--sending');
                        toggleAlert('Desculpe, ocorreu um erro ao fazer Upload (#101).');
                        _BLOCK_AJAX = false;
                        console.log(ret.erro);
                    }
                }
                uploadSucesso = true;
            },
            complete: function(XMLHttpRequest, textStatus) {
                if (!uploadSucesso) {
                    toggleAlert('Desculpe, ocorreu um erro ao fazer Upload (#102).');
                    _BLOCK_AJAX = false;
                    console.log(XMLHttpRequest);
                    console.log(textStatus);
                }
            }
        });
    };

    Dropzone.forElement("#dragAndDropField").files.forEach(function (file,i) {
        processaUpload(file,i+1);
    })

    if (done == 0) REQ_INSERIR_PLANTAO(media);
}


