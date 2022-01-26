const form = document.getElementById('form');
const inputs = document.querySelectorAll('#form input');
const branch = document.getElementById('branch');
getEmail()
// getBranch();

const expresions = {
    // user: /^[a-zA-Z0-9\_\-]{4,16}$/, // Letras, numeros, guion y guion_bajo
    order: /^\d{1,10000}$/,
    weight: /^\d{1,10000}$/,
    volume: /^\d{1,10000}$/,
    bags: /^\d{1,10000}$/
}

const attributes = {

    order: false,
    weight: false,
    volume: false,
    bags: false,
    branch: false
}

const validateform = (e) => {
    switch (e.target.name) {
        case "order":
            validate(expresions.order, e.target, 'order');
            break;
        // case "weight":
        //     validate(expresions.weight, e.target, 'weight');
        //     break;
        // case "volume":
        //     validate(expresions.volume, e.target, 'volume');
        //     break;
        // case "bags":
        //     validate(expresions.bags, e.target, 'bags');
        //     break;
        case "date":
            validateDate();
            break;
        case "branch":
            validateBranch();
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

const validateBranch = () => {
    if (branch.value == "" || branch.value == 0) {
        document.getElementById(`group__branch`).classList.add('form__group-incorrect');
        document.getElementById(`group__branch`).classList.remove('form__group-correct');
        document.querySelector(`#group__branch i`).classList.add('fa-times-circle');
        document.querySelector(`#group__branch i`).classList.remove('fa-check-circle');
        attributes.branch = false;
    } else {
        document.getElementById(`group__branch`).classList.remove('form__group-incorrect');
        document.getElementById(`group__branch`).classList.add('form__group-correct');
        document.querySelector(`#group__branch i`).classList.remove('fa-times-circle');
        document.querySelector(`#group__branch i`).classList.add('fa-check-circle');
        attributes.branch = true;
    }
}

const validateFormat = () => {
	let vol = document.getElementById('volume')
	let wei = document.getElementById('weight')
	let bag = document.getElementById('bags')

	if(vol.value != 0 || wei.value != 0) bag.disabled = false
}
validateFormat();

inputs.forEach((input) => {
    input.addEventListener('keyup', validateform);
    input.addEventListener('blur', validateform);
});
branch.addEventListener('keyup', validateform);
branch.addEventListener('blur', validateform);

form.addEventListener('submit', (e) => {
    e.preventDefault();
    if (attributes.order && attributes.branch) {
        form.reset();
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