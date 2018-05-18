//import { triggerAsyncId } from "async_hooks";

// Write your JavaScript code.
window.addEventListener('load', function (event){

//let btn = document.getElementById('mysteryButton');
//let div = document.getElementById('mysteryDiv');
    if (document.getElementById('likeBtn') !== null) {
        let likeBtn = document.getElementById('likeBtn');
    }
    if (document.getElementById('tinderButton') !== null) {
        let tinderButton = document.getElementById('tinderButton');
    }
let profileTemplate = document.getElementById('profileTemplate');
let descriptionTemplate = document.getElementById('descriptionTemplate');
let imageTemplate = document.getElementById("imageTemplate");
let secretId = document.getElementById('secretId');
let secretProfileId = document.getElementById('secretProfileId');
let counter = 0;

    if (document.getElementById('tinderButton') !== null) {
        tinderButton.addEventListener('click', function (event) {
            fetch('/Api/Finder/GetProfiles?id=' + secretId.textContent)
                .then(response => {
                    return response.json();
                })
                .then(data => {
                    try {
                        var profile = JSON.parse(data[counter]);
                        counter++;

                        nameTemplate.innerHTML = profile.FirstName + ' ' + profile.LastName;
                        descriptionTemplate.innerHTML = profile.Description;
                        imageTemplate.src = "data:image/png;base64," + profile.AvatarImage;

                        secretProfileId.innerHTML = profile.UserId;
                        likeBtn.className = 'btn btn-success pull-right';
                        tinderButton.textContent = 'Keep looking';
                        tinderButton.className = 'btn pull-left';
                        console.log("API run successfully");

                    }
                    catch (e) {
                        nameTemplate.innerHTML = 'No more profiles!';
                        descriptionTemplate.innerHTML = '';
                        imageTemplate.src = '';
                        likeBtn.className = 'btn btn-success hidden';
                        tinderButton.className = 'btn hidden';
                        console.log("No more profiles (or failed to parse profile object)");
                        counter = 0;
                    }
                })
                .catch(error => {
                    console.log(error);
                });
        });
    }

    if (document.getElementById('likeBtn') !== null) {
likeBtn.addEventListener('click', function (event) {
    let url = '/Api/Finder/Like?id=' + secretId.textContent + '&likeId=' + secretProfileId.textContent;
    console.log(url);
    fetch(url)
        .then(response => {
            return response.json();
        })
        .then(data => {
            try {
                if (counter > 0) {
                    counter--;
                }
                var profile = JSON.parse(data[counter]);
                
                nameTemplate.innerHTML = profile.FirstName + ' ' + profile.LastName;
                descriptionTemplate.innerHTML = profile.Description;
                imageTemplate.src = "data:image/png;base64," + profile.AvatarImage;

                secretProfileId.innerHTML = profile.UserId;
                likeBtn.className = 'btn btn-success pull-right';
                tinderButton.textContent = 'Keep looking';
                tinderButton.className = 'btn pull-left';
                console.log("API run successfully");
                

            }
            catch (e) {
                var arrayLength = data.length
                nameTemplate.innerHTML = 'No more profiles!';
                descriptionTemplate.innerHTML = '';
                imageTemplate.src = '';
                likeBtn.className = 'btn btn-success hidden';
                tinderButton.className = 'btn hidden';
                console.log("No more profiles (or failed to parse profile object)");
                counter = 0;
                console.log(arrayLength);
                console.log(counter);
            }
        })
        .catch(error => {
            console.log(error);
        });
        });
    }
    });


//btn.addEventListener('click', function (event) {
//    fetch('/Api/MysteryAnimal')
//        .then(response => {
//            return response.json();
//        })
//        .then(data => {
//            div.innerHTML += '<br/>' + data;
//        })
//        .catch(error => {
//            console.log(error);
//        });
//});