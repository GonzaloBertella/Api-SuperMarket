const form = document.getElementById('form');
const inputs = document.querySelectorAll('#form input');
const closeSesion = document.getElementById('closeSesion');
const heavy = document.getElementById('heavy')
const intermediate = document.getElementById('intermediate')
const light = document.getElementById('light')


const expresions = {
    // usuario: /^[a-zA-Z0-9\_\-]{4,16}$/, // Letras, numeros, guion y guion_bajo
    reason: /^[a-zA-ZÀ-ÿ\s]{1,40}$/, // Letras y espacios, pueden llevar acentos.
    name: /^[a-zA-ZÀ-ÿ\s]{1,40}$/, // Letras y espacios, pueden llevar acentos.
    location: /^[a-zA-ZÀ-ÿ\s]{1,40}$/, // Letras y espacios, pueden llevar acentos.
    street: /^[a-zA-ZÀ-ÿ\s]{1,40}$/, // Letras y espacios, pueden llevar acentos.
    email: /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/,
    phone: /^\d{9,14}$/, // 7 a 14 numeros.
    cuit: /^\d{9,20}$/, // 7 a 14 numeros.
    salary: /^\d{4,30}$/ // 7 a 14 numeros.
}

const attributes = {
    reason: false,
    street: false,
    email: false,
    phone: false,
    cuit: false,
    salary: false,
    name: false,
    location: false,
}

const validateform = (e) => {
    switch (e.target.name) {
        case "reason":
            validate(expresions.reason, e.target, 'reason');
            break;
        case "email":
            validate(expresions.email, e.target, 'email');
            break;
        case "phone":
            validate(expresions.phone, e.target, 'phone');
            break;
        case "street":
            validate(expresions.street, e.target, 'street');
            break;
        case "cuit":
            validate(expresions.cuit, e.target, 'cuit');
            break;
        case "salary":
            validate(expresions.salary, e.target, 'salary');
            break;
        case "name":
            validate(expresions.name, e.target, 'name');
            break;
        case "location":
            validate(expresions.location, e.target, 'location');
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




inputs.forEach((input) => {
    input.addEventListener('keyup', validateform);
    input.addEventListener('blur', validateform);
});

form.addEventListener('submit', (e) => {
    e.preventDefault();
    if (attributes.reason && attributes.name && attributes.email && attributes.salary && attributes.phone && attributes.street && attributes.cuit && attributes.location) {
        if(!heavy.checked && !light.checked && !intermediate.checked){
            document.getElementById('terms-message').classList.add('terms-message-active');
        }
        else{
            form.reset();
            document.getElementById('form__success-message').classList.add('form__success-message-active');
            setTimeout(() => {
                document.getElementById('form__success-message').classList.remove('form__success-message-active');
            }, 5000);
            document.getElementById('form__message').classList.remove('form__message-active');
            document.getElementById('terms-message').classList.remove('terms-message-active');
            document.querySelectorAll('.form__group-correct').forEach((icono) => {
                icono.classList.remove('form__group-correct');
            });
        }
    }
    else {
        document.getElementById('form__message').classList.add('form__message-active');
    }
});