function cal(event) {
    event.preventDefault();
    var ReSeat = document.getElementById('requirdSeat').value;
    var TiPrice = document.getElementById('TicketsPrice').value;

    var total = ReSeat * TiPrice;
    document.getElementById('dfgh').innerHTML = total;
}