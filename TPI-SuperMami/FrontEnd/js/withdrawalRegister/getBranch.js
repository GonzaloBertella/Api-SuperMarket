// const getCompany = () => {
const url = "https://localhost:5001/Branch/GetAllBranches";

fetch(url)
  .then((response) => response.json())
  .then((data) => {
    let branch = document.getElementById("branch");
    let html = document.createElement("option");
    html.value = "";
    html.text = "Seleccione una sucursal";
    branch.appendChild(html);
    for (let i = 0; i < data.return.$values.length; i++) {
      let option = document.createElement("option");
      option.value = data.return.$values[i].idBranch;
      option.text = data.return.$values[i].name;
      branch.add(option);
    }
  })
  .catch((error) => console.log(error));
// };


// $(document).ready(function () {
//   $.ajax({
//       url: "https://localhost:5001/Branch/GetAllBranches",
//       type: "GET",

//       success: function (result) {
//           if (result.ok) {
              
//               cargarCombo(result.return);
              
              
//           } else {
//               Swal.fire(result.error);
//           }
//       },
//       error: function (error) {
//           console.log(error);
//       },
//   });
// });

// function cargarCombo(datos) {
//   select = document.getElementById("branch");
//   for (let i = 0; i < datos.length; i++) {
//     var option = document.createElement("option");
//     option.text = datos[i].Name;
//     option.value=i+1;
//     select.add(option);
//   }
//   console.log(select.value);
// } 