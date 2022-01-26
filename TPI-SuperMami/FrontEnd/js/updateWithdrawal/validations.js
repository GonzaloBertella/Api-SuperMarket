const form = document.getElementById('form');
const inputs = document.querySelectorAll('#form input');
const selects = document.querySelectorAll('#form select');
// getStatus();

const expresions = {
    // user: /^[a-zA-Z0-9\_\-]{4,16}$/, // Letras, numeros, guion y guion_bajo
    order: /^\d{1,10000}$/,
    weight: /^\d{1,10000}$/,
    volume: /^\d{1,10000}$/,
    bags: /^\d{1,10000}$/
}

const attributes = {
    order: false,
    date: false,
    branch: false,
    status: false
}

const validateform = (e) => {
    switch (e.target.name) {
        case "order":
            validate(expresions.order, e.target, 'order');
            break;
        case "date":
            validateDate();
            break;
        case "branch":
            validateBranch();
            break;
        case "status":
            validateStatus();
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
const validateDate = () => {
    const date = document.getElementById('date');
    const dateNow = new Date();
    if (date.value < dateNow.toISOString()) {
        document.getElementById(`group__date`).classList.add('form__group-incorrect');
        document.getElementById(`group__date`).classList.remove('form__group-correct');
        document.querySelector(`#group__date i`).classList.add('fa-times-circle');
        document.querySelector(`#group__date i`).classList.remove('fa-check-circle');
        attributes.date = false;
    } else {
        document.getElementById(`group__date`).classList.remove('form__group-incorrect');
        document.getElementById(`group__date`).classList.add('form__group-correct');
        document.querySelector(`#group__date i`).classList.remove('fa-times-circle');
        document.querySelector(`#group__date i`).classList.add('fa-check-circle');
        attributes.date = true;
    }
}

const validateBranch = () => {
    const branch = document.getElementById('branch');
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
const validateStatus = () => {
    const status = document.getElementById('status');
    if (status.value == "" || status.value == 0) {
        document.getElementById(`group__status`).classList.add('form__group-incorrect');
        document.getElementById(`group__status`).classList.remove('form__group-correct');
        document.querySelector(`#group__status i`).classList.add('fa-times-circle');
        document.querySelector(`#group__status i`).classList.remove('fa-check-circle');
        attributes.status = false;
    } else {
        document.getElementById(`group__status`).classList.remove('form__group-incorrect');
        document.getElementById(`group__status`).classList.add('form__group-correct');
        document.querySelector(`#group__status i`).classList.remove('fa-times-circle');
        document.querySelector(`#group__status i`).classList.add('fa-check-circle');
        attributes.status = true;
    }
}
inputs.forEach((input) => {
    input.addEventListener('keyup', validateform);
    input.addEventListener('blur', validateform);
});
selects.forEach((select) => {
	select.addEventListener('keyup', validateform);
	select.addEventListener('blur', validateform);
});

form.addEventListener('submit', (e) => {
    e.preventDefault();
    if (attributes.order && attributes.date && attributes.branch && attributes.status) {
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