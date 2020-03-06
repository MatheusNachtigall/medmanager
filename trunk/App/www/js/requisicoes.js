let LOAD_PLANTOES = function () {
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

let REQ_INSERIR_PLANTAO = function () {
    WS.InserirRequisicao('INSERIR_PLANTAO', {
        HOSPITAL_ID:    $('#select-hospital').val(),
        DATA_PLANTAO:   $('#data_plantao').val(),
        DATA_PAGAMENTO: $('#data_pagamento').val(),
        VALOR: $('#valor').val(),
        INSS: $("#INSS").prop('checked'),
        CNPJ: $("#CNPJ").prop('checked')
    }, function(ret) {
        if (ret.sucesso){
            $('#select-hospital').val("");
            $( 'select' ).formSelect();
            $('#data_plantao').val('');
            $('#data_pagamento').val('');
            $('#data_plantao').val('');
            $('input[type="checkbox"]').prop("checked",false)
            $('#valor').val('');
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