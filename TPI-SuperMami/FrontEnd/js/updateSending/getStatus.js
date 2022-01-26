const getStatus = () => {
  const url = "https://localhost:5001/State/GetAllStates";

  fetch(url)
    .then((response) => response.json())
    .then((data) => {
      let status = document.getElementById("status");
      let html = document.createElement("option");
      html.value = "";
      html.text = "Seleccione un estado";
      status.appendChild(html);
      for (let i = 0; i < data.return.$values.length; i++) {
        let option = document.createElement("option");
        option.value = data.return.$values[i].idState;
        option.text = data.return.$values[i].state1;
        status.add(option);
      }
    })
    .catch((error) => console.log(error));
};
