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
            $('#modal-criar-novo-plantao .modal-data-plantao').html(info.dateStr.replace(/(\d\d\d\d)-(\d\d)-(\d\d)/,'$3/$2/$1'));
            $('#modal-criar-novo-plantao').modal('open')
            $('#modal-criar-novo-plantao .modal-btn-confirmar').click(function() {
                $('#data_plantao').val(info.dateStr);
                $('.tabs').tabs('select', 'adicionar_plantao');
            });
        },
    });
}


let loadCalendarEvents = function () {
    LP.forEach(element => {
        calendar.addEvent( {
            title: element.HOSPITAL,
            allDay: true,
            start: element.DATA_PLANTAO.replace(/(\d\d)-(\d\d)-(\d\d\d\d)/,'$3-$1-$2'),
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