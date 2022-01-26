const getCompany = () => {
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
};
