 const getTypeDocument = () => {
//   $(document).ready(function () {
//     $.ajax({
//       url: "https://localhost:5001/DocumentType/GetAllDocumentTypes",
//       type: "GET",
//       dataType: "json",
//       success: (result) => {       
//         if (result.ok) {
//           var html = "<option value=''>Seleccione un tipo de DNI</option>";
//           $("#typeDni").append(html);
        
//           select = document.getElementById("typeDni");
//           for (let i = 0; i < 2; i++) {
//             var option = document.createElement("option");
//             console.log(result);
//             option.value = result.return.$values[i].idDocumentType;
//             option.text = result.return.$values[i].documentType1;
//             select.add(option);
//           }
//         } else {
//           swal(result.error);
//         }
//       },
//       error: function (error) {
//         swal("Problemas al conseguir los tipos de DNI");
//       },
//     });
//   });
// }
const url = 'https://localhost:5001/DocumentType/GetAllDocumentTypes';

fetch(url)
  .then(response => response.json())
  .then(data => {
    let type = document.getElementById("typeDni");
    let html = document.createElement("option")
    html.value = "";
    html.text = "Seleccione un tipo de documento";
    type.appendChild(html);    
    for (let i = 0; i < data.return.$values.length; i++) {
      let option = document.createElement("option");
      option.value = data.return.$values[i].idDocumentType;
      option.text = data.return.$values[i].documentType1;
      type.add(option);
    }
  })
  .catch(error =>{
    swal("Error al traer los tipos de documentos")
    console.log(error)
  })
}