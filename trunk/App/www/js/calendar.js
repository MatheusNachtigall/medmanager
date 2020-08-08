let loadCalendar = function () {
    var calendarEl = document.getElementById('calendar');

    calendar = new FullCalendar.Calendar(calendarEl, {
        plugins: [ 'interaction', 'dayGrid', 'moment'],
        locale: 'pt-br',
        showNonCurrentDates: false,
        views: {
            month: {
                titleFormat: 'MMMM YYYY'
            }
        },
        header: {
            left: 'prev,next',
            center: 'title',
            right: 'today'
        },
        selectable: true,
        dateClick: function(info) {
            // alert('Novo plantao? ' + info.dateStr);
            $('#calendar-criar-novo-plantao .modal-data').html(info.dateStr.replace(/(\d\d\d\d)-(\d\d)-(\d\d)/,'$3/$2/$1'));
            $('#calendar-criar-novo-plantao').modal('open')
            $('#calendar-criar-novo-plantao .modal-btn-confirmar').click(function() {
                $('#data_plantao').val(info.dateStr);
                $('.tabs').tabs('select', 'adicionar_plantao');
            });
        },
        eventClick: function(info) {
            $('#calendar-editar-plantao .modal-id').data('id',info.event.extendedProps.id);
            $('#calendar-editar-plantao .modal-hospital').html(info.event.title);
            $('#calendar-editar-plantao .modal-data').html(info.event.extendedProps.data.replace(/(\d\d)-(\d\d)-(\d\d\d\d)/,'$2/$1/$3'));
            $('#calendar-editar-plantao .modal-horario').html(info.event.extendedProps.horario);
            $('#calendar-editar-plantao .modal-periodo').html((info.event.extendedProps.periodo+' Horas'));
            $('#calendar-editar-plantao .modal-valor').html(info.event.extendedProps.valor);
            $('#calendar-editar-plantao').modal('open')
          }
    });
}


let loadCalendarEvents = function () {
    LP.forEach(element => {
        calendar.addEvent( {
            title: element.HOSPITAL,
            extendedProps: {
                id: element.PLANTAO_ID,
                valor: element.VALOR,
                data: element.DATA,
                horario: element.HORARIO,
                periodo: element.PERIODO
            },
            allDay: true,
            start: element.DATA.replace(/(\d\d)-(\d\d)-(\d\d\d\d)/,'$3-$1-$2'),
            color: element.COR,
        })
    });
}

let clearCalendarEvents = function () {
    var events = calendar.getEvents(); 
    var len = events.length;
    for (var i = 0; i < len; i++) { 
        events[i].remove(); 
    } 
}


