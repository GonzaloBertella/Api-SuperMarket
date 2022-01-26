const getCompany = () => {
const url = 'https://localhost:5001/ShippingCompany/GetAll';

fetch(url)
  .then(response => response.json())
  .then(data => {
    let company = document.getElementById("company");
    let html = document.createElement("option")
    html.value = "";
    html.text = "Seleccione una empresa";
    company.appendChild(html);
    for (let i = 0; i < data.return.$values.length; i++) {
      let option = document.createElement("option");
      option.value = data.return.$values[i].idShippingCompany;
      option.text = data.return.$values[i].businessName;
      company.add(option);
    }
  })
  .catch(error =>console.log(error))
}