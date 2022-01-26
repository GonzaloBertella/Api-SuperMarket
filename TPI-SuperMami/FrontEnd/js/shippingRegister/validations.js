const form = document.getElementById('form');
const inputs = document.querySelectorAll('#form input');
const company = document.getElementById('company');
const closeSesion = document.getElementById('closeSesion');
getCompany();

const expresions = {
    // user: /^[a-zA-Z0-9\_\-]{4,16}$/, // Letras, numeros, guion y guion_bajo
    order: /^\d{1,10000}$/,
    weight: /^\d{1,10000}$/,
  
}

const attributes = {

    order: false,
    weight: false,
    company: false
}

const validateform = (e) => {
    switch (e.target.name) {
        case "order":
            validate(expresions.order, e.target, 'order');
            break;
        case "weight":
            validate(expresions.weight, e.target, 'weight');
            break;
        // case "volume":
        //     validate(expresions.volume, e.target, 'volume');
        //     break;
        // case "bags":
        //     validate(expresions.bags, e.target, 'bags');
        //     break;        
        case "company":
            validateCompany();
            break;
    }
}

const validate = (expresion, input, attribute) => {
    if (expresion.test(input.value)) {
        document.getElementById(`group__${attribute}`).classList.remove('form__group-incorrect');
        document.getElementById(`group__${attribute}`).classList.add('form__group-correct');
        document.querySelector(`#group__${attribute} i`).classList.add('fa-check-circle');
        document.querySelector(`#group__${attribute} i`).classList.remove('fa-times-circle');
        attributes[attribute] = true;
    } else {
        document.getElementById(`group__${attribute}`).classList.add('form__group-incorrect');
        document.getElementById(`group__${attribute}`).classList.remove('form__group-correct');
        document.querySelector(`#group__${attribute} i`).classList.add('fa-times-circle');
        document.querySelector(`#group__${attribute} i`).classList.remove('fa-check-circle');
        attributes[attribute] = false;
    }
}

const validateCompany = () => {
    if (company.value == "" || company.value == 0) {
        document.getElementById(`group__company`).classList.add('form__group-incorrect');
        document.getElementById(`group__company`).classList.remove('form__group-correct');
        document.querySelector(`#group__company i`).classList.add('fa-times-circle');
        document.querySelector(`#group__company i`).classList.remove('fa-check-circle');
        attributes.company = false;
    } else {
        document.getElementById(`group__company`).classList.remove('form__group-incorrect');
        document.getElementById(`group__company`).classList.add('form__group-correct');
        document.querySelector(`#group__company i`).classList.remove('fa-times-circle');
        document.querySelector(`#group__company i`).classList.add('fa-check-circle');
        attributes.company = true;
    }
}

// const validateFormat = () => {
// 	let vol = document.getElementById('volume')
// 	let wei = document.getElementById('weight')
// 	let bag = document.getElementById('bags')

// 	if(vol.value != 0 || wei.value != 0) bag.disabled = false
// }
// validateFormat();

// closeSesion.addEventListener('onclick', goLogOut);

inputs.forEach((input) => {
    input.addEventListener('keyup', validateform);
    input.addEventListener('blur', validateform);
});
company.addEventListener('keyup', validateform);
company.addEventListener('blur', validateform);

form.addEventListener('submit', (e) => {
    e.preventDefault();
    if (attributes.order && attributes.company && attributes.weight) {
        form.reset();
        insertShipping();
        document.getElementById('form__success-message').classList.add('form__success-message-active');
        setTimeout(() => {
            document.getElementById('form__success-message').classList.remove('form__success-message-active');
        }, 5000);
        document.getElementById('form__message').classList.remove('form__message-active');
        document.querySelectorAll('.form__group-correct').forEach((icono) => {
            icono.classList.remove('form__group-correct');
        });

    }
    else {
        document.getElementById('form__message').classList.add('form__message-active');
    }
});