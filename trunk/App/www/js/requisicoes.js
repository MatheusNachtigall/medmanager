let REQ_LOAD_PLANTOES = function () {
    WS.InserirRequisicao('LOAD_PLANTOES', {}, function(ret) {
        if (ret.sucesso) {
            LP = ret.lstPlantao;
            // buildChartAndGraph(ret.lstPlantao);
            arrangePlantoes(ret.lstPlantao);
            buildHospitalSelect(ret.lstPlantao);
            buildMonthSelect();
            let month = new Date().toLocaleString('default', { month: 'long', year: 'numeric' }).toString().replace(' de ','/');
            let lstPlantoes = PLANTOES[month.split('/')[1]][month.split('/')[0]];
            buildChartAndGraph(lstPlantoes);
            $('#select-mes').val(month);
            $( 'select' ).formSelect();
            buildDetailedPlantaoList();
        }
    });
}

let REQ_INSERIR_PLANTAO = function (media) {
    WS.InserirRequisicao('INSERIR_PLANTAO', {
        HOSPITAL_ID:    $('#select-hospital').val(),
        DATA_PLANTAO:   $('#data_plantao').val(),
        DATA_PAGAMENTO: $('#data_pagamento').val(),
        VALOR: $('#valor').val(),
        INSS: $("#INSS").prop('checked'),
        CNPJ: $("#CNPJ").prop('checked'),
        MEDIA: media
    }, function(ret) {
        if (ret.sucesso){
            $('#select-hospital').val("");
            $( 'select' ).formSelect();
            $('#data_plantao').val('');
            $('#data_pagamento').val('');
            $('#data_plantao').val('');
            $('input[type="checkbox"]').prop("checked",false)
            $('#valor').val('');
            Dropzone.forElement("#dragAndDropField").removeAllFiles();
            M.toast({html: 'Plantão inserido!', classes: 'rounded green'});
            
        } else {
            M.toast({html: 'Ocorreu um erro!', classes: 'rounded red'});
            //toast com falha e motivo da falha
        }
    })
}

let REQ_MARCAR_PLANTAO_RECEBIDO = function (Hosp_ID) {
    console.log('To aqui: ', Hosp_ID);
    
    WS.InserirRequisicao('MARCAR_PLANTAO_RECEBIDO', {
        HOSPITAL_ID:    Hosp_ID,
    }, function(ret) {
        console.log(ret);
        
        if (ret.sucesso){
            M.toast({html: 'Pagamento inserido!', classes: 'rounded green'});
            LOAD_PLANTOES();
        } else {
            M.toast({html: 'Ocorreu um erro!', classes: 'rounded red'});
            //toast com falha e motivo da falha
        }
    })
}

let REQ_EXCLUIR_PLANTAO = function (Hosp_ID) {
    WS.InserirRequisicao('EXCLUIR_PLANTAO', {
        HOSPITAL_ID:    Hosp_ID,
    }, function(ret) {
        if (ret.sucesso){
            M.toast({html: 'Plantão Excluido!', classes: 'rounded green'});
            LOAD_PLANTOES();
        } else {
            M.toast({html: 'Ocorreu um erro!', classes: 'rounded red'});
            //toast com falha e motivo da falha
        }
    })
}

let REQ_GET_ANEXOS = function (Plantao_ID) {
    WS.InserirRequisicao('GET_ANEXOS', {
        PLANTAO_ID:    Plantao_ID,
    }, function(ret) {
        if (ret.sucesso){
            var modalTextHTML = "";
            for (let i = 0; i < ret.anexos.length; i++) {
                 modalTextHTML += '   <a class="pdf-link" href="' + BACKEND + '/Uploads/' + Plantao_ID + '/' + element + '" target="_blank">' + ret.anexos[i] + '</a>';
            }
            $('#modal-download modal-text').html(modalTextHTML);      
            $('#modal-download').modal('open');
        } else {
            M.toast({html: 'Ocorreu um erro!', classes: 'rounded red'});
        }
    })
}




















let PREP_INSERIR_PLANTAO = function () {
    console.log('inserindo plantao');
    
    var media = [];
    var done = 0;
    
    var processaUpload = function(file, order) {
        done++;
        var formData = new FormData();
        formData.append('ACAO', 'UPLOAD_MEDIA');
        // formData.append('WS_USUARIO_ID', WS_USUARIO_ID);
        // formData.append('WS_TOKEN', WS_TOKEN);
        formData.append('MEDIA', file);
        formData.append('ORDEM', order); 
        
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
        console.log('inserindo arquivo ',i+1);
        processaUpload(file,i+1);
    })

    if (done == 0) REQ_INSERIR_PLANTAO(media);
}


