// Write your JavaScript code.

function calculateTotal(unitPrice) {
    var seatsInput = document.getElementById('tripInput');
    var totalElement = document.getElementById('Totali');

    function updateTotal() {
        var inputValue = seatsInput.value.trim();
        

        var parsedValue = parseFloat(inputValue);
        if (isNaN(parsedValue)) {
            parsedValue = 0;
        }

        var total = parsedValue * unitPrice;

        totalElement.textContent = 'Total: ' + total.toFixed(2); 
    }


    updateTotal();


    seatsInput.addEventListener('input', updateTotal);


    seatsInput.addEventListener('change', updateTotal);
}

// kjo esh per dom load 
document.addEventListener('DOMContentLoaded', function() {
    calculateTotal(10); // 
});

function confirmTravel(qty, reservationDate, event) {
    event.preventDefault();

    var inputTrip = document.querySelector('#tripInput');

 
    var inputValue = parseInt(inputTrip.value);

    if (new Date(reservationDate) < new Date()) {
        Swal.fire({
            icon: 'error',
            title: 'Rezervimi nuk eshte i mundur!',
            text: 'Data e udhëtimit ka kaluar, nuk mund të bëhet rezervim për këtë udhëtim.',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'OK'
        });
        return; 
    }

    if (inputValue > qty) {
        Swal.fire({
            icon: 'error',
            title: 'Nuk ka mjaftueshem vende!',
            text: 'Na vjen keq, por numri i vendeve që ju kërkoni të rezervoni e kalon kapacitetin.',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'OK'
        });

        inputTrip.value = qty;
    } else {
        Swal.fire({
            title: 'A jane detajet ne rregull?',
            text: 'Ju lutem kontrolloni me vemendje te gjitha detajet e biletes',
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Po, e konfirmoj!'
        }).then((result) => {
            if (result.isConfirmed) {
                Swal.fire({
                    icon: 'success',
                    title: 'Sukses!',
                    text: 'Faleminderit per rezervimin.',
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'OK'
                }).then(() => {
                    var reservationForm = document.getElementById('reservationForm');
                    if (reservationForm) {
                        reservationForm.submit();
                    } else {
                        console.error('Form with ID "reservationForm" not found.');
                    }
                });
            }
        });
    }
}

function printPage() {
    var button = document.querySelector("#buttonToCall")
    button.classList.add('no-print');
    window.print();
    button.classList.remove('no-print');
}

let container = document.getElementById('container');

function toggle() {
    container.classList.toggle('sign-in');
    container.classList.toggle('sign-up');
}

setTimeout(() => {
    container.classList.add('sign-in');
}, 200);

$(document).ready(function () {
    const currency = localStorage.getItem('currency');
    if (currency === 'EUR') {
        $('#currencySwitch').prop('checked', true).next('label').text('EUR');
        getValueToMultiply('toEUR');
    } else {
        $('#currencySwitch').prop('checked', false).next('label').text('ALL');
    }
});

function toggleCurrency() {
    const isChecked = $('#currencySwitch').is(':checked');
    const label = $('#currencySwitch').next('label');

    if (isChecked) {
        label.text('EUR');
        localStorage.setItem('currency', 'EUR'); 
        getValueToMultiply('toEUR'); 
    } else {
        label.text('ALL');
        localStorage.setItem('currency', 'ALL'); 
        getValueToMultiply('toALL'); 
    }
}

function getValueToMultiply(direction) {
    $('#currencySwitch').prop('disabled', true);

    $.ajax({
        url: '/api/Scrapping/exchange-rate',
        type: 'GET',
        success: function (response) {
            var multiplier = response.exchangeRate;
            multiplyPrice(multiplier, direction);
            $('#currencySwitch').prop('disabled', false); 
        },
        error: function (xhr, status, error) {
            console.error('An error occurred while fetching the value to multiply with:', error);
            $('#currencySwitch').prop('disabled', false); 
        }
    });
}

function multiplyPrice(multiplier, direction) {
    $('.td-price').each(function () {
        var priceText = $(this).text().replace('ALL', '').replace('EUR', '').trim();
        var price = parseFloat(priceText); 
        if (!isNaN(price)) {
            var newPrice;
            if (direction === 'toEUR') {
                newPrice = (price / multiplier).toFixed(2); 
                $(this).text('EUR ' + newPrice); 
            } else {
                newPrice = (price * multiplier).toFixed(2); 
                $(this).text('ALL ' + newPrice); 
            }
        }
    });
}


$('.logoutButton').on('click', function () {
    localStorage.removeItem('currency');
});