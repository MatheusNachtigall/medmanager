let generateMonthlyPieChart = function (HospitaisDistintos, dataSomas, CoresDistintas) {
	var ctx = document.getElementById('chart-area').getContext('2d');
	var config = {
		type: 'pie',
		data: {
			datasets: [{
				data: dataSomas,
				backgroundColor: CoresDistintas,
				label: 'Dataset 1'
			}],
			labels: HospitaisDistintos
		},
		options: {
			responsive: true,
			legend: {
                onClick: (e) => e.stopPropagation(),
				position: 'right',
				labels: {
					boxWidth: 15,
					fontSize: 12,
					fontStyle: 'bold'
                },
			}
		}
	};
	window.myPie = new Chart(ctx, config);
}


let buildChartAndGraph = function (lstPlantoes) {
	// console.log(lstPlantoes);
	
    var HospitaisDistintos = lstPlantoes.map(e => e.HOSPITAL).filter( (v,i,s) => s.indexOf(v) === i);
    var CoresDistintas = lstPlantoes.map(e => e.COR).filter( (v,i,s) => s.indexOf(v) === i);
    
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
        valor_cidade = lstPlantoes.filter( (e => e.HOSPITAL == HospitaisDistintos[i])).map(e => e.VALOR).reduce(( acc, summ ) => acc + summ, 0);
        dataSomas.push(valor_cidade);
        mes_soma += valor_cidade;
        
        tableHTML += '  <tr>';
        tableHTML += '	    <td>' + HospitaisDistintos[i] + '</td>';
        tableHTML += '	    <td>' + valor_cidade.toFixed(2).toString().replace('.',',') + '</td>';
        tableHTML += '	</tr>';
    }
    tableHTML += '  <tr>';
    tableHTML += '	    <td class="fw700 pt5">Total:</td>';
    tableHTML += '	    <td class="fw700 pt5">' + mes_soma.toFixed(2).toString().replace('.',',') + '</td>';
    tableHTML += '	</tr>';
    tableHTML += '</tbody>';
    $('#table-valores').html(tableHTML);
    
    generateMonthlyPieChart(HospitaisDistintos, dataSomas, CoresDistintas);
}


