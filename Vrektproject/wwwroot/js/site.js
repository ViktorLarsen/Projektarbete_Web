// Write your JavaScript code.


let btn = document.getElementById('mysteryButton');
let div = document.getElementById('mysteryDiv');


btn.addEventListener('click', function (event) {
    fetch('/Api/MysteryAnimal')
        .then(response => {
            return response.json();
        })
        .then(data => {
            div.innerHTML += '<br/>' + data;
        })
        .catch(error => {
            console.log(error);
        })
})