// Write your JavaScript code.


let btn = document.getElementById('mysteryButton');
let div = document.getElementById('mysteryDiv');

let tinderButton = document.getElementById('tinderButton');
let profileResult = document.getElementById('profileResult');
let secretId = document.getElementById('secretId');
let counter = 0;


tinderButton.addEventListener('click', function (event) {
    fetch('/Api/GetProfiles/' + secretId.textContent)
        .then(response => {
            return response.json();
        })
        .then(data => {
            //var profile = JSON.parse(data[counter]);
            //console.log(profile);
            //profileResult.innerHTML += '<br/>' + profile.Id + '<br/>' + profile.FirstName + '<br/>' + profile.LastName + '<br/>';
            profileResult.innerHTML = data[counter];
            counter++;
            //console.log(data[counter]);
            console.log("ok");
        })
        .catch(error => {
            console.log(error);
        });
});

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
        });
});

