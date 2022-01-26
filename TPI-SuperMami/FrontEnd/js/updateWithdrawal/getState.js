// const getStatus = () => {
const url2 = "https://localhost:5001/State/GetAllStates";

fetch(url2)
  .then((response) => response.json())
  .then((data) => {
    let state = document.getElementById("state");
    let html = document.createElement("option");
    html.value = "";
    html.text = "Seleccione un estado";
    state.appendChild(html);
    for (let i = 0; i < data.return.$values.length; i++) {
      let option = document.createElement("option");
      option.value = data.return.$values[i].idState;
      option.text = data.return.$values[i].state1;
      state.add(option);
    }
  })
  .catch((error) => console.log(error));
// };
