$(document).ready(function () {

    $.get("CBookTickets/Count",
        function (result, status, xhr) {
            $("#noti").html(result);
        });
});

function cal(event) {
    event.preventDefault();
    var ReSeat = document.getElementById('requirdSeat').value;
    var TiPrice = document.getElementById('TicketsPrice').value;

    var total = ReSeat * TiPrice;
    document.getElementById('dfgh').innerHTML = total;
};

function ChechFormTi(event) {

    var tickets = document.getElementById('tickets').value;
    var seat = document.getElementById('seat').value;
    var gate = document.getElementById('gate').value;

    if (tickets == '') {
        document.getElementById('ticket_error').classList.remove('hidden');
        event.preventDefault();
    }

    if (seat == '') {
        document.getElementById('seat_error').classList.remove('hidden');
        event.preventDefault();
    }

    if (gate == '') {
        document.getElementById('gate_error').classList.remove('hidden');
        event.preventDefault();
    }
};