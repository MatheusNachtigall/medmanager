let LOAD_PLANTOES = function () {
    WS.InserirRequisicao('GRAFICO_MES', {}, function(ret) {
        if (ret.sucesso) {
            console.log('RET: ', ret.lstPlantao);
            TESTE = ret.lstPlantao;
            var HospitaisDistintos = ret.lstPlantao.map(e => e.HOSPITAL).filter( (v,i,s) => s.indexOf(v) === i);
            var CoresDistintas = ret.lstPlantao.map(e => e.COR).filter( (v,i,s) => s.indexOf(v) === i);
            
            var dataSomas = []
            var tableHTML = '';
            tableHTML += '<thead>';
            tableHTML += '	<tr>';
            tableHTML += '	    <th>Hospital</th>';
            tableHTML += '	    <th>Valor</th>';
            tableHTML += '	</tr>';
            tableHTML += '</thead>';
            tableHTML += '<tbody>';
            var mes_soma = 0;
            var valor_cidade;
            for  (var i = 0; i < HospitaisDistintos.length; i++) {
                valor_cidade = ret.lstPlantao.filter( (e => e.HOSPITAL == HospitaisDistintos[i])).map(e => e.VALOR).reduce(( acc, summ ) => acc + summ, 0);
                dataSomas.push(valor_cidade);
                mes_soma += valor_cidade;
                
                tableHTML += '  <tr>';
                tableHTML += '	    <td>' + HospitaisDistintos[i] + '</td>';
                tableHTML += '	    <td>' + valor_cidade + '</td>';
                tableHTML += '	</tr>';
            }
            tableHTML += '  <tr>';
            tableHTML += '	    <td class="fw700 pt5">Total:</td>';
            tableHTML += '	    <td class="fw700 pt5">' + mes_soma + '</td>';
            tableHTML += '	</tr>';
            tableHTML += '</tbody>';
            $('#table-valores').html(tableHTML);
            
            generateMonthlyPieChart(HospitaisDistintos, dataSomas, CoresDistintas);
        }
    });
}