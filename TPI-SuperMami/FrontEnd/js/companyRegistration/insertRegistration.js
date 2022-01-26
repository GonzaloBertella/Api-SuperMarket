$(document).ready(function () {debugger
    $("#btnRegistrar").click(function () {
        debugger

        let reason = $("#reason").val();
        let cuit = $("#cuit").val();
        let address = $("#street").val();
        let salary = $("#salary").val();
        let name = $("#name").val();
        let email = $("#email").val();
        let phone = $("#phone").val();
        let location = $("#location").val();
        let packages = 3;

        CompanyRegister(reason, address, location, phone, email, cuit, packages, salary, name);
    });
});


function CompanyRegister(reason, address, location, phone, email, cuit, packages, salary, name) {
    comando = {
        "idShippingCompany": 0,
        "businessName": reason,
        "address": address,
        "location": location,
        "phone": phone,
        "email": email,
        "cuit": cuit,
        "shiftStartTime": "07:00",
        "shiftEndTime": "18:00",
        "idShippingType": parseInt(packages),
        "salary": parseFloat(salary),
        "contactName": name,
        "maxShippingsPerDay": parseInt(10)
    }


    $.ajax({
        url: "https://localhost:5001/ShippingCompany/RegisterShippingCompany",
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(comando),
        success: function (result) {
            if (result.ok) {
                swal("Excelente!", "Empresa registrada con éxito!", "success");

            }
            else {
                swal("Oops", "Something went wrong!", "error")
            }
        },
        error: function (error) {
            swal("Problemas en el servidor");
        }
    });
}