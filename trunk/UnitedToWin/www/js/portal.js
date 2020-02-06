var opcoes_nivel = {
    "1":{codigo:"1",   custo: 8300, almas: 10},
    "2":{codigo:"2",   custo: 8500, almas: 11},
    "3":{codigo:"3",   custo: 8800, almas: 13},
    "4":{codigo:"4",   custo: 9100, almas: 14},
    "5":{codigo:"5",   custo: 9400, almas: 15},
    "6":{codigo:"6",   custo: 9700, almas: 17},
    "7":{codigo:"7",   custo: 10000, almas: 18},
    "8":{codigo:"8",   custo: 10200, almas: 19},
    "9":{codigo:"9",   custo: 10500, almas: 21},
    "10":{codigo:"10", custo: 10800, almas: 22},
    "11":{codigo:"11", custo: 11100, almas: 23},
    "12":{codigo:"12", custo: 11400, almas: 25},
    "13":{codigo:"13", custo: 11700, almas: 26},
    "14":{codigo:"14", custo: 11900, almas: 27},
    "15":{codigo:"15", custo: 12200, almas: 29},
    "16":{codigo:"16", custo: 12500, almas: 30},
    "17":{codigo:"17", custo: 12900, almas: 32},
    "18":{codigo:"18", custo: 13300, almas: 34},
    "19":{codigo:"19", custo: 13700, almas: 36},
    "20":{codigo:"20", custo: 14100, almas: 38},
    "21":{codigo:"21", custo: 14500, almas: 40},
    "22":{codigo:"22", custo: 14900, almas: 42},
    "23":{codigo:"23", custo: 15300, almas: 44},
    "24":{codigo:"24", custo: 15700, almas: 46},
    "25":{codigo:"25", custo: 16100, almas: 48},
    "26":{codigo:"26", custo: 16500, almas: 50},
    "27":{codigo:"27", custo: 16900, almas: 52},
    "28":{codigo:"28", custo: 17300, almas: 54},
    "29":{codigo:"29", custo: 17700, almas: 56},
    "30":{codigo:"30", custo: 18100, almas: 58},
    "31":{codigo:"31", custo: 18500, almas: 60},
    "32":{codigo:"32", custo: 18900, almas: 64},
    "33":{codigo:"33", custo: 19400, almas: 68},
    "34":{codigo:"34", custo: 19800, almas: 72},
    "35":{codigo:"35", custo: 20200, almas: 76},
    "36":{codigo:"36", custo: 20700, almas: 80},
    "37":{codigo:"37", custo: 21100, almas: 84},
    "38":{codigo:"38", custo: 21500, almas: 88},
    "39":{codigo:"39", custo: 22000, almas: 92},
    "40":{codigo:"40", custo: 22400, almas: 96},
    "41":{codigo:"41", custo: 22800, almas: 100},
    "42":{codigo:"42", custo: 23300, almas: 104},
    "43":{codigo:"43", custo: 23700, almas: 108},
    "44":{codigo:"44", custo: 24100, almas: 112},
    "45":{codigo:"45", custo: 25600, almas: 116},
    "46":{codigo:"46", custo: 25000, almas: 120},
    "47":{codigo:"47", custo: 25800, almas: 128},
    "48":{codigo:"48", custo: 26700, almas: 136},
    "49":{codigo:"49", custo: 27500, almas: 144},
    "50":{codigo:"50", custo: 28300, almas: 152},
    "51":{codigo:"51", custo: 29200, almas: 160},
    "52":{codigo:"52", custo: 30000, almas: 168},
    "53":{codigo:"53", custo: 30800, almas: 176},
    "54":{codigo:"54", custo: 31700, almas: 184},
    "55":{codigo:"55", custo: 32500, almas: 192},
    "56":{codigo:"56", custo: 33300, almas: 200},
    "57":{codigo:"57", custo: 34200, almas: 208},
    "58":{codigo:"58", custo: 35000, almas: 216},
    "59":{codigo:"59", custo: 35800, almas: 224},
    "60":{codigo:"60", custo: 36700, almas: 232},
    "61":{codigo:"61", custo: 37500, almas: 240},
    "62":{codigo:"62", custo: 38400, almas: 254},
    "63":{codigo:"63", custo: 39300, almas: 267},
    "64":{codigo:"64", custo: 40300, almas: 281},
    "65":{codigo:"65", custo: 41200, almas: 295},
    "66":{codigo:"66", custo: 42100, almas: 308},
    "67":{codigo:"67", custo: 43000, almas: 322},
    "68":{codigo:"68", custo: 43900, almas: 336},
    "69":{codigo:"69", custo: 44900, almas: 349},
    "70":{codigo:"70", custo: 45800, almas: 363},
    "71":{codigo:"71", custo: 46700, almas: 377},
    "72":{codigo:"72", custo: 30000, almas: 391},
    "73":{codigo:"73", custo: 30000, almas: 404},
    "74":{codigo:"74", custo: 30000, almas: 418},
    "75":{codigo:"75", custo: 30000, almas: 432},
    "76":{codigo:"76", custo: 30000, almas: 445},
    "77":{codigo:"77", custo: 30000, almas: 459},
    "78":{codigo:"78", custo: 30000, almas: 473},
    "79":{codigo:"79", custo: 30000, almas: 486},
    "80":{codigo:"80", custo: 30000, almas: 500}
};


$(document).ready(function(){
    
    $.each(opcoes_nivel, function(key, value) {
        $('#nivel').append('<option value="' + value.codigo + '">' + value.codigo + '</option>');
    })

    $('#codMaca').click(function(el) {
        $('#codAlmas').val('');
    });
    $('#codAlmas').click(function(el) {
        $('#codMaca').val('');
    });
});

function validaCampos( )
{
	var nivel = $('#nivel').val();		
	var maca = $('#codMaca').val();
    var colalmas = $('#codAlmas').val();
	
	if( !((nivel == "XX") || ((maca == "") && (colalmas == ""))) )
	{
        if (colalmas == '')
            calculaAlmas();
        else
            calculaMacas();
	}
}






function calculaAlmas()
{
    var txt = "Você ganhará ";
    var almas = opcoes_nivel[$('#nivel').val()].almas;
    var custo = opcoes_nivel[$('#nivel').val()].custo;
    var mult =  $('input[name="mult"]:checked').val();
    var maca = $('#codMaca').val();
    var cal = Math.floor(maca/custo) * almas * mult;
    txt += cal + " Almas"
    $("#respPortal").text(txt);
    txt = "Você precisará repetir o portal ";
    txt += Math.floor(maca/custo) + " vezes."
    $("#respPortalVezes").text(txt);
}

function calculaMacas()
{
    var txt = "Serão necessárias ";
    var colalmas = $('#codAlmas').val();
    var almas = opcoes_nivel[$('#nivel').val()].almas;
    var custo = opcoes_nivel[$('#nivel').val()].custo;
    var mult =  $('input[name="mult"]:checked').val();
    var cal = Math.ceil(colalmas/(almas*mult)) * custo;
    txt += cal;
    txt += " maçãs."
    $("#respPortal").text(txt);
    txt = "Você precisará repetir o portal ";
    txt += Math.floor(cal/custo) + " vezes."
    $("#respPortalVezes").text(txt);

}

$('#codMaca,#codAlmas').keyup(validaCampos);