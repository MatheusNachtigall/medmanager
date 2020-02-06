function PageLoad_Live() {
    $(document).delegate('input.deletar', 'click', function () {
        return confirm('Deseja excluir esse item?');
    });

    $(document).delegate('input.executar', 'click', function () {
        if ($('.checkbox input[type="checkbox"]:checked', '#ctl00_ctl00_content_contentInterna_upConteudo').length) {
            return confirm('Deseja excluir os itens selecionados?');
        } else {
            alert('Nenhum item selecionado.');
            return false;
        }
    });

    $(document).delegate('.tbLista td:not(.irgnoreClick)', 'click', function () {
        location.href = 'Editar.aspx?ID=' + $(this).closest('tr').find('input[type=hidden]').eq(0).val();
    });

    $(".busca-cep").blur(function () {
        var cep = $(this).val().replace(/\D/g, '');
        if (cep != "") {
            var validacep = /^[0-9]{8}$/;
            if (validacep.test(cep)) {
                $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {
                    if (!("erro" in dados)) {
                        $(".cep-uf").val(dados.uf);
                        $(".cep-cidade").val(dados.localidade);
                        $(".cep-logradouro").val(dados.logradouro);
                        $(".cep-bairro").val(dados.bairro);
                    }
                });
            }
        }
    });
}

function PageLoad() {
    $('.selecionarTodos').click(function () {
        if ($(this).is(':checked')) {
            $('input[type=checkbox]:not(.selecionarTodos)').prop('checked', true);
        } else {
            $('input[type=checkbox]:not(.selecionarTodos)').prop('checked', false);
        }
    });
    var SPMaskBehavior = function (val) {
        return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
    };
    var spOptions = { onKeyPress: function (val, e, field, options) { field.mask(SPMaskBehavior.apply({}, arguments), options); }};
    $('.mask-fone').mask(SPMaskBehavior, spOptions);
    $('.mask-numero').mask('000000000000000', { reverse: true });
    $('.mask-money').mask('#.##0,00', { reverse: true });
    $('.mask-percent').mask('##0,00%', { reverse: true });
    $('.mask-cep').mask('00000-000');
    $('.mask-cpf').mask('000.000.000-00', { reverse: true });
    $('.mask-cnpj').mask('00.000.000/0000-00', { reverse: true });
	$('.mask-date').mask('00/00/0000', { placeholder: "__/__/____" });
    $('.tb-registros tr').each(function () {
        if ($(this).find('input[type="hidden"]').length > 0) {
            var hdnID = $(this).find('input[type="hidden"]:eq(0)').val();
            $(this).find('td.td-detalhe').click(function () {
                location.href = 'Editar.aspx?ID=' + hdnID;
            });
        }
    });

    $('.input-check input').change(function(){
		$(this).closest('.input-check').toggleClass('input-checked', ($(this).prop('checked')));
    }).each(function(){
		$(this).closest('.input-check').toggleClass('input-checked', ($(this).prop('checked'))).css('visibility', 'visible');
	});
	window.setTimeout(function () {
		$('.input-check').addClass('trans');
	}, 200);
	

	$('.wallet').click(function () {
		location.href = '/modulos/Financeiro_Lancamentos/Listar.aspx?carteira=' + $(this).find('.ipt-carteira').val();
		return false;
    });

    $('.menu-lateral a').each(function (idx, anchor) {
        $(anchor).attr('href').indexOf('..') != 0 && $(anchor).addClass('active');
    });

    $('table .td-valor').each(function () {
        if ($(this).text() == "Sim") {
            $(this).css("background-color", "#b7e1cd");
        } else {
            $(this).css("background-color", "#f4c7c3");
        }
	});

	$('.typeahead').each(function () {
		$(this).val($(this).closest('.input-txt').find('select option:selected').text());
		$(this).typeahead({hint: true, highlight: true, minLength: 1}, {
			source: function findMatches(q, cb) {
				var substrRegex = new RegExp(q, 'i'), matches = [];
				$(this.$el).closest('.input-txt').find('select').find('option').each(function () {
					var txt = $(this).text();
					if (substrRegex.test(txt)) matches.push(txt);
				});
				cb(matches);
			},
			limit: 10,
			templates: {empty: '<div class="not-found">Nenhum registro encontrado.</div>'}
		}).bind('typeahead:select typeahead:autocomplete', function (ev, suggestion) {
			var $sel = $(this).closest('.input-txt').find('select');
			$sel.find('option').each(function () {
				if ($(this).text() == suggestion) {
					$sel.val($(this).val());
					$('input[value="Filtrar"]').click();
					return false;
				}
			});
		});
	});

	$('.atalho-plantao').each(function () {
		$(this).attr('href', $(this).attr('href') + '?plantao=' + getParameterByName('ID'))
	});

	$('.atalho-pessoa').each(function () {
		$(this).attr('href', $(this).attr('href') + '?pessoa=' + getParameterByName('ID'))
    });

    $('.atalho-chamada').each(function () {
        $(this).attr('href', $(this).attr('href') + '?chamada=' + getParameterByName('ID'))
    });
    
    //tira borda vermelha ao preencher (login)
	$('#content_txtEmail, #content_txtSenha').on('change', function (ev) {
		$(this).val() != "" ? $(this).css('border-bottom-color', '#424241') : $(this).css('border-bottom-color', 'red');
	});

	var chart;
	if ($('#relatorioChart').length)
	{
		if (chart) chart.destroy();
        console.log(JSON.parse($('#hdDados').val()))
		chart = generateChart($('#hdNome').val(), JSON.parse($('#hdDados').val()));
	}
}

$(function () {
    PageLoad_Live();
    PageLoad();
});

function generateChart(name, data) {

	var labels = data.map(function (e) {
		return e.mes_ano;
	});
	var values = data.map(function (e) {
		return e.valor_mes;
    });;
    var values_inss = data.map(function (e) {
        return e.valor_mes_inss;
    });;

	var ctx = document.getElementById('relatorioChart').getContext('2d');
	return new Chart(ctx, {
		// The type of chart we want to create
		type: 'line',

		// The data for our dataset
		data: {
			labels: labels,
            datasets: [
                {
				    label: name,
				    borderColor: 'rgb(90, 120, 240)',
                    data: values,
                    pointRadius: 4,
                    pointHitRadius: 10
                },
                {
                    label: 'INSS',
                    borderColor: 'rgb(241, 102, 96)',
                    data: values_inss,
                    pointRadius: 4,
                    pointHitRadius: 10
                }
            ]
		},

		// Configuration options go here
		options: {
            aspectRatio: 3,
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    },
                }]
            }
		}
	});


}