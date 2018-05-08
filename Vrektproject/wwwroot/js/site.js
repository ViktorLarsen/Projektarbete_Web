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
            try {
                var profile = JSON.parse(data[counter]);
                profileResult.innerHTML = '<br/>' + profile.Id + '<br/>' + profile.FirstName + '<br/>' + profile.LastName + '<br/>';
                counter++;
                console.log("API run successfully");

            }
            catch (e) {
                profileResult.innerHTML = 'No more profiles!';
                console.log("No more profiles (or failed to parse profile object)");
            }
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

