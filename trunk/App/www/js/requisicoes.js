let LOAD_PLANTOES = function () {
    WS.InserirRequisicao('LOAD_PLANTOES', {}, function(ret) {
        if (ret.sucesso) {
            LP = ret.lstPlantao;
            buildChartAndGraph(ret.lstPlantao);
            arrangePlantoes(ret.lstPlantao);
            buildMonthSelect();
        }
    });
}

