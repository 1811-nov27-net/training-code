'use strict';

// so AJAX stands for Asynchronous JavaScript And XML
// "let's make requests and receive responses from
// XML-based services dynamically in the page."

// practical modern meaning:
// using the DOM API to send requests over the internet

// XMLHttpRequest is the traditional object used for this.

function ajaxGet(
    url,
    success,
    failure = res => console.log(res)) {

    let xhr = new XMLHttpRequest();

    xhr.addEventListener("readystatechange", () => {
        console.log(`ready-state is now: ${xhr.readyState}`);
        if (xhr.readyState === 4) {
            // we've recieved the response
            console.log(xhr.response);
            // if status code is success
            if (xhr.status >= 200 && xhr.status < 300) {
                // success
                success(xhr.response);
            } else {
                failure(xhr.response);
            }
        }
    });

    xhr.open("GET", url);
    xhr.send();
}

document.addEventListener("DOMContentLoaded", () => {
    let jokeHeader = document.getElementById("jokeHeader");
    let jokeBtn = document.getElementById("jokeBtn");
    let jokeFetchBtn = document.getElementById("jokeFetchBtn");

    jokeBtn.addEventListener("click", () => {
        ajaxGet("http://api.icndb.com/jokes/random", response => {
            // success
            // (DOM API provides JSON deserialize with
            // JSON.parse()
            // and JSON serialize with
            // JSON.serialize()
            let responseObj = JSON.parse(response);
            console.log(responseObj);
            let joke = responseObj.value.joke;
            jokeHeader.innerText = joke;
        });

        console.log("request about to be sent.");
    });

    jokeFetchBtn.addEventListener("click", () => {
        // fetch API, modern way to do AJAX
        fetch("http://api.icndb.com/jokes/random")
            .then(response => response.json())
            .then(obj => {
                jokeHeader.innerText = obj.value.joke;
            })
            .catch(err => console.log(err));

        // we can chain promises like this -
        // the .json() method returns a promise
        // to get the whole response body and deserialize it

        // catch will catch any error from along the
        // chain
        // or, we can handle an error from one of them
        // using 2nd function argument to .then.
    });


});

// task 1: make a webpage that allows you to
// type JSON in a <textarea> element, and then
// click a button to print it out to the console
// as a JS object. handle any errors gracefully
// with try/catch.

// task 2: make a webpage that somehow makes
// requests to a different API from the Chuck Norris
// Joke API, and somehow show the results
// on the page.
