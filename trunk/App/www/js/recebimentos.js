



    // // for (var i = 0; i < sortedHospitais.length; i++) {
    // //     var $item = $('.hospital-id-' + sortedHospitais[i].HOSPITAL_ID);
    // //     $item.find('.hospital-info').data('id', sortedHospitais[i].HOSPITAL_ID);
    // //     $item.find('.hospital-info').data('mes', sortedHospitais[i].DATA);
    // //     $item.find('.hospital-info').data('valor', sortedHospitais[i].VALOR.toFixed(2).toString().replace('.',','));
    // // }

    // $('.fixed-action-btn').floatingActionButton({
    //     direction: 'left',
    //     hoverEnabled: false
    // });
    
    // // $('.btn-receber').click(function() {
    // //     let plantao_info =  $(this).closest(".plantao-info")
    // //     $('#modal-confirmar-recebimento .modal-id').html($(plantao_info).data('id'));
    // //     $('#modal-confirmar-recebimento .modal-hospital').html($(plantao_info).data('hospital'));
    // //     $('#modal-confirmar-recebimento .modal-data').html($(plantao_info).data('data').replace(/(\d\d)-(\d\d)-(\d\d\d\d)/,'$2/$1/$3'));
    // //     $('#modal-confirmar-recebimento .modal-valor').html($(plantao_info).data('valor'));
    // //     $('#modal-confirmar-recebimento').modal('open')
    // // });



let arrangeHospitais = function () {
    
    HOSPITAIS = LP.reduce(function (r, o) {
        var m = o.DATA.split(('-'))[2];
        if (r[m]){
            
            r[m].push({HOSPITAL_ID: o.HOSPITAL_ID, HOSPITAL: o.HOSPITAL, VALOR: o.VALOR, DATA: o.DATA, RECEBIDO: o.RECEBIDO})
        } else {
            r[m] = [{HOSPITAL_ID: o.HOSPITAL_ID, HOSPITAL: o.HOSPITAL, VALOR: o.VALOR, DATA: o.DATA, RECEBIDO: o.RECEBIDO}];
        }
        return r;
    }, {});
    var temp2 = Object.keys(HOSPITAIS).map(function(k){ return HOSPITAIS[k]; });
    
    var index = -1;
    temp2.forEach(element => {
        var mes = element.reduce(function (r, o) {
            var m = new Date(o.DATA).toLocaleString('default', { month: 'long'}).toString();
            if (r[m]){
                index = r[m].findIndex(el => el.HOSPITAL == o.HOSPITAL)
                if (index == -1){
                    r[m].push({HOSPITAL_ID: o.HOSPITAL_ID, HOSPITAL: o.HOSPITAL, VALOR: o.VALOR, DATAS: [o.DATA]});
                } else {
                    r[m][index].VALOR += o.VALOR;
                    r[m][index].DATAS.push(o.DATA);
                }
            } else {
                r[m] =  [{HOSPITAL_ID: o.HOSPITAL_ID, HOSPITAL: o.HOSPITAL, VALOR: o.VALOR, DATAS: [o.DATA]}]
            }
            return r;
        }, {});
        HOSPITAIS[element[0].DATA.split(('-'))[2]] = mes
    });
}


$(function() {
    $('a[href="#recebimentos"]').click(function(){
        console.log('lalala');
        arrangeHospitais();
        $('#recebimentos .select-mes').change();
    });
    
    $('#recebimentos .select-mes').on('change', function(){
        let select_value = $(this).val();
        let lstHospitais = HOSPITAIS[select_value.split('/')[1]][select_value.split('/')[0]];
        
        var HTML = '';
        lstHospitais.forEach( el => {
            HTML += '<div class="row hospital-id-' + el.HOSPITAL_ID + '">';
            HTML += '   <div class="hospital-info col s12">';
            HTML += '       <div class="card ' + (el.RECEBIDO ? 'light-green' : 'red') + ' lighten-3">';
            HTML += '           <div class="card-content">';
            HTML += '               <span class="card-title hospital-nome fw700">' + el.HOSPITAL + '</span>';
            HTML += '               <div class="fixed-action-btn" style="position:relative; float:right; bottom:50px; right:-20px">';
            HTML += '                   <a class="btn-floating waves-effect waves-light green darken-4"><i class="material-icons">more_vert</i></a>';
            HTML += '                   <ul class="fab-options">';
            // if (!el.RECEBIDO) {
                //     HTML += '                       <li><a class="btn-receber btn-small btn-floating green"><i class="material-icons">monetization_on</i></a></li>';
                // }
                // if (el.ANEXO.length > 0) {
                    //     HTML += '                       <li><a class="btn-download btn-small btn-floating blue"><i class="material-icons">attach_file</i></a></li>';
            // }
            HTML += '                   </ul>';
            HTML += '               </div>';
            HTML += '               <div class="row">';
            // HTML += '                   <p class="col s6 fw700 plantao-data">' + new Date(el.DATA).toLocaleString('default', { month: 'long'}).toString(); + '</p>';
            HTML += '                   <p class="col s6 fw700 plantao-valor">R$ ' + el.VALOR.toFixed(2).toString().replace('.',',') + '</p>';
            HTML += '               </div>';
            HTML += '           </div>';
            HTML += '       </div>';
            HTML += '   </div>';
            HTML += '</div>';
        });
            
        $('.hospitais_list').html(HTML);
    });

    $( 'select' ).formSelect();
});
