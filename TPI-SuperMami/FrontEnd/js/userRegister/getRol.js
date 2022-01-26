const getRol = () => {
  const url = "https://localhost:5001/Role/GetAllRoles";

  fetch(url)
    .then((response) => response.json())
    .then((data) => {
      console.log(data);
      let rol = document.getElementById("rolUser");
      let html = document.createElement("option");
      html.value = "";
      html.text = "Seleccione un rol";
      rol.appendChild(html);      
      for (let i = 0; i < data.return.$values.length; i++) {
        let option = document.createElement("option");
        option.value = data.return.$values[i].idRol;
        option.text = data.return.$values[i].rol;
        rol.add(option);
      }
    })
    .catch((error) => {
      swal("Error al traer los roles");
      console.log(error);
    });
};
