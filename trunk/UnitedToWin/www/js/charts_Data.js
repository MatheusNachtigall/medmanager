
		var randomScalingFactor = function() {
			return Math.round(Math.random() * 100);
		};

		var random_rgba =  function() {
			var o = Math.round, r = Math.random, s = 255;
			return 'rgba(' + o(r()*s) + ',' + o(r()*s) + ',' + o(r()*s) + ',' + r().toFixed(1) + ')';
		}
		


		var config = {
			type: 'pie',
			data: {
				datasets: [{
					data: [
						6400,
						5000,
						850,
					],
					backgroundColor: [
						random_rgba(),
						random_rgba(),
						random_rgba(),
					],
					label: 'Dataset 1'
				}],
				labels: [
					'UPA',
					'Candiota',
					'Cassino',
				]
			},
			options: {
				responsive: true
			}
		};

		window.onload = function() {
			
		};


		
	