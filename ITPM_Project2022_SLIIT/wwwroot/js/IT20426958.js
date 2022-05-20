function Flists(event) {

    var time = document.getElementById('time').value;
    var price1 = document.getElementById('price1').value;
    var price2 = document.getElementById('price2').value;
    var price3 = document.getElementById('price3').value;
    var price4 = document.getElementById('price4').value;

    if (!time(time)) {
        document.getElementById('time_error').classList.remove('hidden');
        event.preventDefault();
    } else {
        document.getElementById('time_error').classList.add('hidden');
    }

    if (!Number(price1)) {
        document.getElementById('price1_error').classList.remove('hidden');
        event.preventDefault();
    } else {
        document.getElementById('price1_error').classList.add('hidden');
    }

    if (!Number(price2)) {
        document.getElementById('price2_error').classList.remove('hidden');
        event.preventDefault();
    } else {
        document.getElementById('price2_error').classList.add('hidden');
    }

    if (!Number(price3)) {
        document.getElementById('price3_error').classList.remove('hidden');
        event.preventDefault();
    } else {
        document.getElementById('price3_error').classList.add('hidden');
    }

    if (!Number(price4)) {
        document.getElementById('price4_error').classList.remove('hidden');
        event.preventDefault();
    } else {
        document.getElementById('price4_error').classList.add('hidden');
    }
}

function Number(input_str) {
    var re = /^[0-9]+$/;
    return re.test(input_str);
}

function time(input_str) {
    alert(price1);
    var re = /^\d{1,2}:\d{2}([ap]m)?$/;
    return re.test(input_str);
}