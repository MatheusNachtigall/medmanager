var TESTE;
var PLANTOES = null;
var PLANTOES_2 = null;
var LP = null;

$(function() {
    startMaterialize();
    
    $('.money').mask('#.##0,00', { reverse: true });
    
    REQ_LOAD_PLANTOES();
    
    const date = new Date(); 
    const month = date.toLocaleString('default', { month: 'long' }).toString().toUpperCase();
    $('.select-mes').val(month.toLowerCase());
    $( 'select' ).formSelect();
    startDropzone();
    
    $('#frm_adicionar_plantao').submit(function() {
        validatePlantao()
    });

    $('#modal-inserir-plantao .modal-btn-confirmar').click(function() {
        PREP_INSERIR_PLANTAO();
    });

    $("#btn-limpar").on("click", function () {
        clearInserirPlantaoFields();
    });
        
    $('#modal-confirmar-recebimento .modal-btn-confirmar').click(function() {
        let plantao_id =  parseInt($($(this).closest('.modal')).find('.modal-id').text());
        REQ_MARCAR_PLANTAO_RECEBIDO(plantao_id);
    });

    $('#modal-editar-plantao .modal-btn-confirmar').click(function() {
        let plantao_id =  parseInt($($(this).closest('.modal')).find('.modal-id').text());
        let plantao = LP.find( el => el.PLANTAO_ID == plantao_id )
        
        $('#id').val(plantao_id)
        $('#select-hospital').val(plantao.HOSPITAL_ID);
        $( 'select' ).formSelect();
        $('#data_plantao').val(plantao.DATA_PLANTAO.replace(/(\d\d)-(\d\d)-(\d\d\d\d)/,'$3-$1-$2'));
        $('#data_pagamento').val(plantao.DATA_PAGAMENTO.replace(/(\d\d)-(\d\d)-(\d\d\d\d)/,'$3-$1-$2'));
        $("#INSS").prop('checked', plantao.INSS);
        $("#CNPJ").prop('checked', plantao.CNPJ);
        $('#valor').val(plantao.VALOR.toFixed(2).toString().replace('.',','));
        $('.tabs').tabs('select', 'adicionar_plantao');
    });

    $('#modal-confirmar-exclusao .modal-btn-confirmar').click(function() {
        let plantao_id =  parseInt($($(this).closest('.modal')).find('.modal-id').text());
        REQ_EXCLUIR_PLANTAO(plantao_id);
    });

    $('#principal .select-mes').on('change', function(){
        let select_value = $(this).val();
        let lstPlantoes = PLANTOES[select_value.split('/')[1]][select_value.split('/')[0]];
        window.myPie.destroy();
        buildChartAndGraph(lstPlantoes)
    });

    $('#plantoes_detalhes .select-mes').on('change', function(){
        let month = $(this).val().split('/')[0];
        let plantao_list = $('.plantoes_list').children();
        
        for (var i = 0; i < plantao_list.length; i++) {
            if (month == ""){
                $(plantao_list[i]).show();
            } else {
                var plantao_mes = $(plantao_list[i]).find('.plantao-info');
                var m = new Date(plantao_mes.data('data')).toLocaleString('default', { month: 'long'}).toString();
                if (m == month){
                    $(plantao_list[i]).show();
                } else {
                    $(plantao_list[i]).hide();
                }
            }
        };
    });
});



let startMaterialize = function () {
    M.AutoInit();
    $('.sidenav').sidenav({
        draggable: true
    });
    $('.tabs').tabs();
};




let arrangePlantoes = function (lstPlantoes) {
    PLANTOES = lstPlantoes.reduce(function (r, o) {
        var m = o.DATA_PLANTAO.split(('-'))[2];
        if (r[m]){
            r[m].push({HOSPITAL: o.HOSPITAL, VALOR: o.VALOR, DATA_PLANTAO: o.DATA_PLANTAO, COR: o.COR})
        } else {
            r[m] = [{HOSPITAL: o.HOSPITAL, VALOR: o.VALOR, DATA_PLANTAO: o.DATA_PLANTAO, COR: o.COR}];
        }
        return r;
    }, {});
    var temp = Object.keys(PLANTOES).map(function(k){ return PLANTOES[k]; });
    
    temp.forEach(element => {
        var mes = element.reduce(function (r, o) {
            var m = new Date(o.DATA_PLANTAO).toLocaleString('default', { month: 'long'}).toString();
            if (r[m]){
                r[m].push({HOSPITAL: o.HOSPITAL, VALOR: o.VALOR, COR: o.COR});
            } else {
                r[m] =  [{HOSPITAL: o.HOSPITAL, VALOR: o.VALOR, COR: o.COR}]
            }
            return r;
        }, {});
        PLANTOES[element[0].DATA_PLANTAO.split(('-'))[2]] = mes
    });
}

let buildMonthSelect = function () {
    var selectHTML = '';
    selectHTML += '<select>';
    selectHTML += '<option value=""></option>';
    
    Object.keys(PLANTOES).forEach(element => {
        selectHTML += ' <optgroup Label="' + element + '">';
        Object.keys(PLANTOES[element]).forEach(el => {
            selectHTML += ' <option value="' + el + '/' + element + '">' + el.charAt(0).toUpperCase() + el.slice(1) + '</option>';
        });
        selectHTML += ' </optgroup>';
    });
    selectHTML += '</select>';
    $('.select-mes').html(selectHTML);
    $( 'select' ).formSelect();
}


let buildHospitalSelect = function (FullList) {
    var HospitalList = [];
    FullList.map(e => HospitalList.push({HOSPITAL_ID: e.HOSPITAL_ID, HOSPITAL: e.HOSPITAL}))

    let result = [];
    let map = new Map();
    for (const item of HospitalList) {
        if(!map.has(item.HOSPITAL_ID)){
            map.set(item.HOSPITAL_ID, true);    // set any value to Map
            result.push({
                HOSPITAL_ID: item.HOSPITAL_ID,
                HOSPITAL: item.HOSPITAL
            });
        }
    }
    
    var selectHTML = '';
    selectHTML += '<option value="" selected></option>';
    result.forEach(element => {
        selectHTML += ' <option value="' + element.HOSPITAL_ID + '">' + element.HOSPITAL + '</option>';
    });
    $('#select-hospital').html(selectHTML);
    $( 'select' ).formSelect();
    $("select[required]").css({display: "block", height: 0, padding: 0, width: 0, position: 'absolute'});
}


let buildDetailedPlantaoList = function () {
    var HTML = '';
    const sortedPlantoes = LP.sort((a, b) => new Date(a.DATA_PLANTAO) - new Date(b.DATA_PLANTAO))

    sortedPlantoes.forEach(el => {
        HTML += '<div class="row plantao-id-' + el.PLANTAO_ID + '">';
        HTML += '   <div class="plantao-info col s12">';
        HTML += '       <div class="card ' + (el.RECEBIDO ? 'light-green' : 'red') + ' lighten-3">';
        HTML += '           <div class="card-content">';
        HTML += '               <span class="card-title plantao-data fw700">' + new Date(el.DATA_PLANTAO).toLocaleDateString() + '</span>';
        HTML += '               <div class="fixed-action-btn" style="position:relative; float:right; bottom:50px; right:-20px">';
        HTML += '                   <a class="btn-floating waves-effect waves-light green darken-4"><i class="material-icons">more_vert</i></a>';
        HTML += '                   <ul class="fab-options">';
        if (!el.RECEBIDO) {
            HTML += '                       <li><a class="btn-receber btn-small btn-floating green"><i class="material-icons">monetization_on</i></a></li>';
        }
        HTML += '                       <li><a class="btn-editar btn-small btn-floating yellow darken-1"><i class="material-icons">mode_edit</i></a></li>';
        HTML += '                       <li><a class="btn-excluir btn-small btn-floating red"><i class="material-icons">delete</i></a></li>';
        if (el.ANEXO.length > 0) {
            HTML += '                       <li><a class="btn-download btn-small btn-floating blue"><i class="material-icons">attach_file</i></a></li>';
        }
        HTML += '                   </ul>';
        HTML += '               </div>';
        HTML += '               <div class="row">';
        HTML += '                   <p class="col s6 fw700 plantao-hospital">' + el.HOSPITAL + '</p>';
        HTML += '                   <p class="col s6 fw700 plantao-valor">R$ ' + el.VALOR.toFixed(2).toString().replace('.',',') + '</p>';
        HTML += '               </div>';
        HTML += '           </div>';
        HTML += '       </div>';
        HTML += '   </div>';
        HTML += '</div>';
    });
    
    $('.plantoes_list').html(HTML);

    for (var i = 0; i < sortedPlantoes.length; i++) {
        var $item = $('.plantao-id-' + sortedPlantoes[i].PLANTAO_ID);
        $item.find('.plantao-info').data('id', sortedPlantoes[i].PLANTAO_ID);
        $item.find('.plantao-info').data('data', sortedPlantoes[i].DATA_PLANTAO);
        $item.find('.plantao-info').data('hospital', sortedPlantoes[i].HOSPITAL);
        $item.find('.plantao-info').data('valor', sortedPlantoes[i].VALOR.toFixed(2).toString().replace('.',','));
    }

    $('.fixed-action-btn').floatingActionButton({
        direction: 'left',
        hoverEnabled: false
    });
    
    $('.btn-receber').click(function() {
        let plantao_info =  $(this).closest(".plantao-info")
        $('#modal-confirmar-recebimento .modal-id').html($(plantao_info).data('id'));
        $('#modal-confirmar-recebimento .modal-hospital').html($(plantao_info).data('hospital'));
        $('#modal-confirmar-recebimento .modal-data-plantao').html($(plantao_info).data('data').replace(/(\d\d)-(\d\d)-(\d\d\d\d)/,'$2/$1/$3'));
        $('#modal-confirmar-recebimento .modal-valor').html($(plantao_info).data('valor'));
        $('#modal-confirmar-recebimento').modal('open')
    });
    
    $('.btn-editar').click(function() {
        let plantao_info =  $(this).closest(".plantao-info")
        $('#modal-editar-plantao .modal-id').html($(plantao_info).data('id'));
        $('#modal-editar-plantao .modal-hospital').html($(plantao_info).data('hospital'));
        $('#modal-editar-plantao .modal-data-plantao').html($(plantao_info).data('data').replace(/(\d\d)-(\d\d)-(\d\d\d\d)/,'$2/$1/$3'));
        $('#modal-editar-plantao .modal-valor').html($(plantao_info).data('valor'));
        $('#modal-editar-plantao').modal('open');
        //$('.adicionar_plantao_h5').html('Atualizar Plantão');
    });
    
    $('.btn-excluir').click(function() {
        let plantao_info =  $(this).closest(".plantao-info")
        $('#modal-confirmar-exclusao .modal-id').html($(plantao_info).data('id'));
        $('#modal-confirmar-exclusao .modal-hospital').html($(plantao_info).data('hospital'));
        $('#modal-confirmar-exclusao .modal-data-plantao').html($(plantao_info).data('data').replace(/(\d\d)-(\d\d)-(\d\d\d\d)/,'$2/$1/$3'));
        $('#modal-confirmar-exclusao .modal-valor').html($(plantao_info).data('valor'));
        $('#modal-confirmar-exclusao').modal('open')
    });

    $('.btn-download').click(function() {
        let plantao_info =  $(this).closest(".plantao-info")
        $('#modal-download .modal-id').html($(plantao_info).data('id'));
        $('#modal-download .modal-data-plantao').html($(plantao_info).data('data').replace(/(\d\d)-(\d\d)-(\d\d\d\d)/,'$2/$1/$3'));
        let plantao_anexos = LP.find( el => el.PLANTAO_ID == $(plantao_info).data('id')).ANEXO
        var modalTextHTML = "";
        for (let i = 0; i < plantao_anexos.length; i++) {
             modalTextHTML += '   <a class="pdf-link" href="' + BACKEND + 'Uploads/' + $(plantao_info).data('id') + '/' + plantao_anexos[i].ARQUIVO + '" target="_blank">' + plantao_anexos[i].ARQUIVO + '</a><br>';
        }
        $('#modal-download .modal-text').html(modalTextHTML);      
        $('#modal-download').modal('open');
    });
}

let validatePlantao = function () {
    var valid = true;
    $("#select-hospital").removeClass('invalid');
    $("#data_plantao").removeClass('invalid');
    $("#data_pagamento").removeClass('invalid');
    $("#valor").removeClass('invalid');
    $('.input-field .error-info').remove();
    
    if ($("#select-hospital").val() == '') {
        $("#select-hospital").addClass('invalid').change();
        $("#select-hospital").parent().parent('.input-field').append('<span class="error-info">* Campo obrigatório</span>');
        valid = false;
    }
    if ($("#data_plantao").val() == '') {
        $("#data_plantao").addClass('invalid').change();
        $("#data_plantao").parent('.input-field').append('<span class="error-info">* Campo obrigatório</span>');
        valid = false;
    }
    if ($("#data_pagamento").val() == '') {
        $("#data_pagamento").addClass('invalid').change();
        $("#data_pagamento").parent('.input-field').append('<span class="error-info">* Campo obrigatório</span>');
        valid = false;
    }
    if ($("#valor").val() == '') {
        $("#valor").addClass('invalid').change();
        $("#valor").parent('.input-field').append('<span class="error-info">* Campo obrigatório</span>');
        valid = false;
    }
    if (valid) {
        $('#modal-inserir-plantao .modal-hospital').html($("#select-hospital option:selected").html());
        $('#modal-inserir-plantao .modal-data-plantao').html($("#data_plantao").val().replace(/(\d\d\d\d)-(\d\d)-(\d\d)/,'$3/$2/$1'));
        $('#modal-inserir-plantao .modal-data-pagamento').html($("#data_pagamento").val().replace(/(\d\d\d\d)-(\d\d)-(\d\d)/,'$3/$2/$1'));
        $('#modal-inserir-plantao .modal-INSS').html(($("#INSS").prop('checked') ? "Sim" : "Não"));
        $('#modal-inserir-plantao .modal-CNPJ').html(($("#CNPJ").prop('checked') ? "Sim" : "Não"));
        $('#modal-inserir-plantao .modal-valor').html($("#valor").val());
        $('#modal-inserir-plantao').modal('open')
    } else {
        $('.error:first').focus();
    }
    return false;
}

let clearInserirPlantaoFields = function () {
    $('#select-hospital').val("");
    $( 'select' ).formSelect();
    $('#data_plantao').val('');
    $('#data_pagamento').val('');
    $('#data_plantao').val('');
    $('input[type="checkbox"]').prop("checked",false)
    $('#valor').val('');
    Dropzone.forElement("#dragAndDropField").removeAllFiles();
    $('#trash-can').addClass('hidden');
    $('.dz-default.dz-message').css('display','block');
}




var startDropzone = function(){     
    $("#dragAndDropField").dropzone({
        url: WS_HTTP_URL,
        autoProcessQueue: false,
        thumbnailWidth: 115,
        thumbnailHeight: 115,
        dictDefaultMessage: "Clique aqui " + (IsMobile ? "" : "ou arraste e solte ") + "para anexar arquivos.",
        parallelUploads: 100,
        maxFiles: 100,
        init : function() {
            var myDropzone = this;
            this.on("addedfiles", function(files) {
                $('#trash-can').removeClass('hidden');
                if (IsMobile) {
                    $('.dz-default.dz-message').css('display','none');
                } 
            });
        },
        accept: function(file, done) {
            if (!validExtension(file.name)) {
              done("Formato de arquivo inválido");
            }
            else { done(); }
        }
    });

    $("#trash-can").droppable({
        drop: function ( event, ui) {
            var p1 = new Promise(function(resolve, reject) {
                $(ui.draggable).remove();
                resolve();
            });
            p1.then(function () {
                if (Dropzone.forElement("#dragAndDropField").files.length == 0){
                    $('#trash-can').addClass('hidden');
                    $('.dz-default.dz-message').css('display','block');
                }
            })
        }
    });
    
    $("#dragAndDropField").sortable({
        items:'.dz-preview',
        cursor: 'move',
        opacity: 0.5,
        containment: '#dragAndDropField',
        distance: 20,
        tolerance: 'pointer',
        connectWith: "#trash-can",
        stop: function () {
            var newQueue = [];
            var queue = dragAndDropField.dropzone.files;
            $('#dragAndDropField .dz-preview .dz-filename [data-dz-name]').each(function (count, el) {           
                var name = el.innerHTML;
                queue.forEach(function(file) {
                   if (file.name === name) {
                        newQueue.push(file);
                   }
                });
            });
            dragAndDropField.dropzone.files = newQueue;
        }
    });

    function validExtension(fileName) {
        let exts = ['.JPG', '.JPEG', '.PNG', '.DIB', '.WEBP', '.JPEG', '.SVGZ', '.GIF', '.ICO', '.SVG', '.TIF', '.XBM', '.BMP', '.JFIF', '.PJPEG', '.PJP', '.TIFF',
                    '.MP4', '.M4V', '.OGV', '.MPEG', '.MPG', '.WMV', '.MOV', '.OGM', '.WEBM', '.ASX', '.AVI',
                    '.PDF']
        return (new RegExp('(' + exts.join('|').replace(/\./g, '\\.') + ')$')).test(fileName.toUpperCase());
    }
}