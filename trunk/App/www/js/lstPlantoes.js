$('#modal-confirmar-recebimento .modal-btn-confirmar').click(function() {
    let plantao_id =  parseInt($($(this).closest('.modal')).find('.modal-id').text());
    REQ_MARCAR_PLANTAO_RECEBIDO(plantao_id);
});