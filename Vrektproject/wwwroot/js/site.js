//import { triggerAsyncId } from "async_hooks";

// Write your JavaScript code.
window.addEventListener('load', function (event) {

    //let btn = document.getElementById('mysteryButton');
    //let div = document.getElementById('mysteryDiv');
    if (document.getElementById('likeBtn') !== null) {
        let likeBtn = document.getElementById('likeBtn');
    }
    if (document.getElementById('tinderButton') !== null) {
        let tinderButton = document.getElementById('tinderButton');
    }
    if (document.getElementById('skillsButton') !== null) {
        let skillsButton = document.getElementById('skillsButton');
    }
    if (document.getElementById('getMatchesButton') !== null) {
        let getMatchesButton = document.getElementById('getMatchesButton');
    }
    let profileTemplate = document.getElementById('profileTemplate');
    let descriptionTemplate = document.getElementById('descriptionTemplate');
    let imageTemplate = document.getElementById("imageTemplate");
    let secretId = document.getElementById('secretId');
    let secretProfileId = document.getElementById('secretProfileId');
    let counter = 0;
    let url = 'https://api.openweathermap.org/data/2.5/weather?id=';
    let apiKey = '9a95f113b67526c124822e4a52856d2c';
    let cityId = '2711537';
    let iconTemplate = document.getElementById('iconTemplate');
    let skillsTemplate = document.getElementById('skillsTemplate');
    let matchesTemplate = document.getElementById('matchesTemplate');

    if (document.getElementById('weatherTemplate') !== null) {
        let weatherUrl = url + cityId + '&APPID=' + apiKey;
        console.log(weatherUrl);
        fetch(weatherUrl)
            .then(function (response) {
                return response.json();
            })
            .then(function (weatherJson) {
                console.log(weatherJson);
                var temp = Math.round(parseInt(weatherJson.main.temp) - 273.15);
                document.getElementById('weatherTemplate').innerHTML =
                    'The weather in '
                    + weatherJson.name
                    + ' is '
                    + weatherJson.weather[0].main
                    + ' with '
                    + temp
                    + '&#8451;';
                var iconCode = weatherJson.weather[0].icon;
                console.log(iconCode);
                var iconSource = 'https://openweathermap.org/img/w/' + iconCode + '.png';
                console.log(iconSource);
                iconTemplate.src = iconSource;
            })
            .catch(function (error) {
                console.log(error);
            });
    }

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
                        if (profile.AvatarImage !== null) {
                            imageTemplate.src = "data:image/png;base64," + profile.AvatarImage;
                        }
                        else {
                            imageTemplate.src = defaultPic;
                        }

                        secretProfileId.innerHTML = profile.UserId;
                        likeBtn.className = 'btn btn-success pull-right';
                        tinderButton.textContent = 'Keep looking';
                        tinderButton.className = 'btn pull-left';
                        skillsButton.className = 'btn btn-default col-md-12 col-md-offset-0 row';
                        console.log("API run successfully");

                        skillsTemplate.innerHTML = '';
                       


                    }
                    catch (e) {
                        nameTemplate.innerHTML = 'No more profiles!';
                        descriptionTemplate.innerHTML = 'You have seen all profiles that are relevant for you.';
                        imageTemplate.className = 'hidden';
                        likeBtn.className = 'btn btn-success hidden';
                        tinderButton.className = 'btn hidden';
                        skillsButton.className = 'btn hidden';
                        
                        console.log("No more profiles (or failed to parse profile object)");
                        counter = 0;
                        skillsTemplate.innerHTML = '';
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
                        if (profile.AvatarImage !== null) {
                            imageTemplate.src = "data:image/png;base64," + profile.AvatarImage;
                        }
                        else {
                            imageTemplate.src = defaultPic;
                        }

                        secretProfileId.innerHTML = profile.UserId;
                        likeBtn.className = 'btn btn-success pull-right';
                        tinderButton.textContent = 'Keep looking';
                        tinderButton.className = 'btn pull-left';
                        console.log("API run successfully");

                        //
                        skillsTemplate.innerHTML = '';
                        skillsButton.className = 'btn btn-default col-md-12 col-md-offset-0 row';

                    }
                    catch (e) {
                        var arrayLength = data.length;
                        nameTemplate.innerHTML = 'No more profiles!';
                        descriptionTemplate.innerHTML = 'You have seen all profiles that are relevant for you.';
                        imageTemplate.className = 'hidden';
                        likeBtn.className = 'btn btn-success hidden';
                        tinderButton.className = 'btn hidden';
                        skillsButton.className = 'btn hidden';
                      
                        console.log("No more profiles (or failed to parse profile object)");
                        counter = 0;
                        console.log(arrayLength);
                        console.log(counter);
                        skillsTemplate.innerHTML = '';
                    }
                })
                .catch(error => {
                    console.log(error);
                });
        });
    }

    if (document.getElementById('skillsButton') !== null) {
        skillsButton.addEventListener('click', function (event) {
            fetch('/Api/Finder/Getskills?id=' + secretProfileId.textContent)
                .then(response => {
                    return response.json();
                })
                .then(data => {
                    try {
                        for (i = 0; i < data.length; i++) {
                            var skill = JSON.parse(data[i]);
                            skillsTemplate.innerHTML += '<br />' + skill.Name;
                            skillsButton.className = 'hidden';
                        }
                        console.log("API run successfully");
                    }
                    catch (e) {
                        console.log(e);
                    }
                })
                .catch(error => {
                    console.log(error);
                });
        });
    }

    if (document.getElementById('getMatchesButton') !== null) {
        getMatchesButton.addEventListener('click', function (event) {
            fetch('/Api/Finder/GetMatches')
                .then(response => {
                    return response.json();
                })
                .then(data => {
                    matchesTemplate.innerHTML = '';
                    try {
                        for (i = 0; i < data.length; i++) {
                            var match = JSON.parse(data[i]);
                            matchesTemplate.innerHTML += '<li class="list-group-item">' + match.Member.Profile.FirstName + " " + match.Member.Profile.LastName + ' <span class="Star">&#9733; </span>' + match.Recruiter.Profile.FirstName + " " + match.Recruiter.Profile.LastName + '</li>';
                        }
                        console.log("API run successfully");
                    }
                    catch (e) {
                        console.log(e);
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