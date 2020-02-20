let LOAD_PLANTOES = function () {
    WS.InserirRequisicao('LOAD_PLANTOES', {}, function(ret) {
        if (ret.sucesso) {
            LP = ret.lstPlantao;
            buildChartAndGraph(ret.lstPlantao);
            arrangePlantoes(ret.lstPlantao);
            buildMonthSelect();
            // let month = new Date().toLocaleString('default', { month: 'short', year: 'numeric' }).toString().replace(' de ','/').toUpperCase();
            // let lstPlantoes = PLANTOES[month.split('/')[1]][month.split('/')[0]];
            // buildChartAndGraph(lstPlantoes)
        }
    });
}

