// Write your JavaScript code.


let btn = document.getElementById('mysteryButton');
let div = document.getElementById('mysteryDiv');

let tinderButton = document.getElementById('tinderButton');
let profileResult = document.getElementById('profileResult');
let counter = 0;


tinderButton.addEventListener('click', function (event) {
    fetch('/Api/GetProfiles')
        .then(response => {
            return response.json();
        })
        .then(data => {
            //for (var i = 0; i < data.length; i++) {
            //    var profile = JSON.parse(data[i]);
            //    profileResult.innerHTML += '<br/>' + profile.Id;
            //}
            var profile = JSON.parse(data[counter]);
            profileResult.innerHTML += '<br/>' + profile.Id + '<br/>' + profile.FirstName + '<br/>' + profile.LastName + '<br/>';
            counter++;
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

