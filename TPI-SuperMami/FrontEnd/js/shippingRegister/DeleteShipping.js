
$(document).ready(function () {
    mostrarTabla();
    var idATratar;
    var idEstadoTratar;
  
    $("#Aceptado2").click(function () {
      DeleteShipping();
      
     
    });
    $("#Aceptado").click(function () {
      actualizarEstado();   
      
    });
  });
  
  function DeleteShipping() {
    comando = {
      "idShipping": parseInt(idATratar)
    };
  
    $.ajax({
      url: "https://localhost:5001/Shipping/DeleteShipping",
      type: "POST",
      dataType: "json",
      contentType: "application/json",
      data: JSON.stringify(comando),
      success: function (result) {
        if (result.ok) {
          swal("Envío Eliminado");
          console.log(comando);
  
          mostrarTabla();
        } else {
          alert(result.error);
        }
      },
      error: function (error) {
        console.log(error);
      }
    });
  }
  
  function mostrarModal(idModal, id) {
    $(idModal).modal("toggle");
    idATratar = id;  
  }
  
  function mostrarTabla() {
    $.ajax({
      url: "https://localhost:5001/Shipping/GetListJoin",
      type: "GET",
  
      success: function (result) {
        if (result.ok) {
          $("#cuerpoTabla").empty();
          for (var i = 0; i < result.return.$values.length; i++) {
            
            
            
            var html = "<tr>";
  
            html += "<td>" + result.return.$values[i].idShipping.idShipping + "</td>";
            html +=
              "<td>" +
              result.return.$values[i].idShipping.idDeliveryOrder +
              "</td>";
            html +=
              "<td>" + result.return.$values[i].idShipping.businessName + "</td>";
  
            html += "<td>" + result.return.$values[i].idShipping.deliveryDate + "</td>";            
            html += "<td>" + result.return.$values[i].idShipping.comment + "</td>";
            html += "<td>" + result.return.$values[i].idShipping.weight + "</td>";
            

            html +=
              "<td><button type='button' id='btnEstado" + result.return.$values[i].idShipping.idShipping +"' class='btn btn-info' disabled>" +
              result.return.$values[i].idShipping.state1 +
              "</button></td>";
           
            html +=
            "<td> <button type='button' onclick='estadoModal(\"#btnEstado" + result.return.$values[i].idShipping.idShipping +"\")' id='btnActualizar' class='btn btn-outline-primary' >Actualizar</button></td>";
            html +=
          "<td><button type='button' onclick='mostrarModal( \"#ventanaModal2\","+ result.return.$values[i].idShipping.idShipping +")' class='btn btn-outline-danger'>Eliminar</button></td>";
           
            html += "</tr>";
            $("#cuerpoTabla").append(html);
            
          }
        } else {
          Swal.fire(result.error);
        }
      },
      error: function (error) {
        console.log(error);
      }
    });
  }

  
function estadoModal(id) {
  $("#ventanaModal").modal("toggle");
  idEstadoTratar= id; 
   
}

function actualizarEstado(){
  
  if ($(idEstadoTratar).hasClass('btn-info')) {
      $(idEstadoTratar).removeClass('btn-info').addClass('btn-warning');
      document.getElementById("Pregunta1").innerHTML =
          "¿Esta seguro que quiere cambiar el Estado a 'Retirado'?";
      $(idEstadoTratar).text('Listo para retirar');      
  }
  else if ($(idEstadoTratar).hasClass('btn-warning')) {
      $(idEstadoTratar).removeClass('btn-warning').addClass('btn-success');
      $(idEstadoTratar).text('Retirado')
      $(idEstadoTratar).attr('disabled', 'disabled');
      
  }
}