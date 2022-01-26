$(document).ready(function () {

  $("#btnAgregar").click(function() {

      let idDeliveryOrder = $('#order').val();
      let idBranch = $('select#branch option:checked').val();

      let volume = $('#observaciones').val();

      InsertPickup(idDeliveryOrder, volume, idBranch);
  });

});


function InsertPickup(IdDeliveryOrder, Volume, IdBranch ) {
  comando = {
      idDeliveryOrder: parseInt(IdDeliveryOrder) ,
      idUser: parseInt(1),
      volume: Volume,
      idBranch: parseInt(IdBranch)

  }; 

  $.ajax({
    url: "https://localhost:5001/Pickup/RegisterPickup",
    type: "POST",
    dataType: "json",
    contentType: "application/json",
    data: JSON.stringify(comando),
    success: function (result) {
      if (result.ok) {
        swal("Excelente!", "Se registró el retiro.", "success");
        console.log(comando);
      } else {
        alert(result.error);
      }
    },
    error: function (error) {
      console.log(error);
    },
  });
}