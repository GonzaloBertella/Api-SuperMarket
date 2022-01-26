$(document).ready(function () {
  $("#btnRegistrar").click(function () {

      let order = $("#order").val();
      let company = $('select#company option:checked').val(); 
      let weight = $("#weight").val();
      let comment = $("#comment").val();

      ShippingRegister(order, company, weight, comment);
  });
});


function ShippingRegister(order, company, weight, comment) {
  comando = {
    "idShippingCompany": parseInt(order),
    "idDeliveryOrder": parseInt(company) ,
    "idUser": 1,
    "comment": comment,
    "weight":  parseFloat(weight)
  }

  if(comando.idShippingCompany == 2 && comando.weight)

  $.ajax({
      url: "https://localhost:5001/Shipping/RegisterShipping",
      type: "POST",
      dataType: "json",
      contentType: "application/json",
      data: JSON.stringify(comando),
      success: function (result) {        
          if (result.ok) {
              swal("Excelente!", "Se registró el envío.", "success");                          
          }
          else {
              swal("Error", "Algo salió mal", "error")
          }
      },
      error: function (error) {
          swal("Problemas en el servidor");
      }
  });
}