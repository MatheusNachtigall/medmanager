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
				position: 'right',
				labels: {
					boxWidth: 15,
					fontSize: 12,
					fontStyle: 'bold'
				}
			}
		}
	};
	window.myPie = new Chart(ctx, config);
}
	