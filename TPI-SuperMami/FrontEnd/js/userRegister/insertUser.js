insertUser = () => {
    let name = document.getElementById("name").value;
    let lastName = document.getElementById("lastName").value;
    let dni = document.getElementById("dni").value;
    let typeDni = document.getElementById("typeDni").value;
    let email = document.getElementById("email").value;
    let phone = document.getElementById("phone").value;
    let rolUser = document.getElementById("rolUser").value;
    let password = document.getElementById("password").value;

    command = {
        name: name,
        surname: lastName,
        documentNumber: dni,
        idDocumentType: parseInt(typeDni),
        email: email,
        phone: phone,
        idRol: parseInt(rolUser),
        password: password,
    };

        axios({
            method: 'POST',
            url: 'https://localhost:5001/User/RegisterUser',
            data: command,
        }).then(res => {
            console.log(res.data.return.$values[res.data.return.$values.length - 1].idRol);
            if (res.data.return.$values[res.data.return.$values.length - 1].idRol == 1 || res.data.return.$values[res.data.return.$values.length - 1].idRol == 2) window.location.assign('Home.html');
            if (res.data.return.$values[res.data.return.$values.length - 1].idRol == 3) window.location.assign('http://127.0.0.1:5500/TPI-MSI-Grupo5/FrontEnd/views/ListShippings.html');
            if (res.data.return.$values[res.data.return.$values.length - 1].idRol == 4) window.location.assign('http://127.0.0.1:5500/TPI-MSI-Grupo5/FrontEnd/views/ListPickup.html');
            nameSurname = res.data.return.$values[res.data.return.$values.length - 1].name + ' ' + res.data.return.$values[res.data.return.$values.length - 1].surname
            boss = res.data.return.$values[res.data.return.$values.length - 1].idRol;
            localStorage.setItem("userPassword", nameSurname);
            localStorage.setItem("boss", boss);
        })
            .catch(err => {
                console.log(err)
                swal("Email o contraseña incorrecto")
            })


}
