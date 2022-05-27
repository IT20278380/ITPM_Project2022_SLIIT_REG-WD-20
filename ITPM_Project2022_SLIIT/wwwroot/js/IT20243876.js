function Food(event) {

    var price = document.getElementById('Price').value;

    if (!price(price)) {
        document.getElementById('price_error').classList.remove('hidden');
        event.preventDefault();
    } else {
        document.getElementById('price_error').classList.add('hidden');
    }
}

function price(input_str) {
    var re = /^[0-9]+$/;
    return re.test(input_str);
}

function PDFGeneration(event) {
    var data = $("#Order").html();
    $("#OrderInput").val(data);
}
