$(document).ready(function () {
    
    $("#btnOrdenEntrega").click(function () {

        let DeliveryOrderId = $("#order").val();
        $("#DeliveryOrderBody").empty();
        DeliveryOrderShow(DeliveryOrderId);
    });
});


function DeliveryOrderShow(DeliveryOrderId) {
    
    comando = {
        "idDeliveryOrder": parseInt(DeliveryOrderId)
    }

    $.ajax({
        url: "https://localhost:5001/DeliveryOrder/GetDeliveryOrderById",
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(comando),
        success: function (result) {
            if (result.ok) {
                $("#ventanaModal").modal("toggle");

                var html = "<tr>";
                html += "<td> Nombre </td>";
                html += "<td>" + result.return.name + "</td>";
                html += "</tr>";
                html += "<tr>";
                html += "<td> Apellido </td>";
                html += "<td>" + result.return.surname + "</td>";
                html += "</tr>";
                html += "<tr>";
                html += "<td> Teléfono </td>";
                html += "<td>" + result.return.phone + "</td>";
                html += "</tr>";
                html += "<tr>";
                html += "<td> Fecha </td>";
                html += "<td>" + result.return.deliveryDate + "</td>";
                html += "</tr>";
                html += "<tr>";
                html += "<td> Es propietario (Owner) </td>";
                if(result.return.isOwner)
                {html += "<td>  Sí </td>"; }
                else {
                    html += "<td> No </td>"; 
                }
                html += "</tr>";
                html += "<tr>";
                html += "<td> Precio del envío </td>";
                html += "<td>" + "$" + result.return.shippingPrice + "</td>";
                html += "</tr>";
                html += "<tr>";
                html += "<td> ¿Es gratis? </td>";
                if(result.return.isFree)
                {html += "<td>  Sí </td>"; }
                else {
                    html += "<td> No </td>"; 
                }

                html += "</tr>";
                html += "<tr>";
                $("#DeliveryOrderBody").append(html);
            }
            else {
                swal("Error", "Porfavor ingrese el N° de orden", "error")
            }
        },
        error: function (error) {
            swal("Error", "Porfavor ingrese el N° de orden.", "error")
        }
    });
}