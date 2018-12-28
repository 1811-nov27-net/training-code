'use strict';

function alertMe() {
    alert("You clicked the element");
}

// we access most of the DOM API using the document object.

// let col1 = document.getElementById("col1");
// col1.onclick = alertMe(); // <-- wrong
// col1.onclick = alertMe; // <-- right

// scripts are run as soon as they're encountered in the HTML
// and elements are created in the browser's memory
// as soon as they are encountered on the page.
window.onload = function () {
    // this is the basic way of waiting until the document
    // is all loaded before trying to interact with it

    // let col1 = document.getElementById("col1");
    // col1.onclick = alertMe;
};

// better way that allows multiple functions to all
// listen on the same event.
window.addEventListener("load", function () {
    // let col1 = document.getElementById("col1");
    // col1.onclick = alertMe;
});

function printEventDetails(event) {
    console.log(event);
    console.log(event.target);
    console.log(this);
}

// don't wait all the way for every asset to be loaded,
// just all the elements created in the DOM
document.addEventListener("DOMContentLoaded", () => {
    let col1 = document.getElementById("col1");
    col1.addEventListener("click", alertMe);

    let header = document.getElementById("header");
    header.innerText += ", changed by JS";
    header.innerHTML = `<u>${header.innerHTML}</u>`;

    // jQuery, common JS library, makes a lot of these
    // basic DOM tasks faster to write, more readable
    let cell1 = document.getElementById("cell1");
    cell1.addEventListener("click", printEventDetails);

    let tbody = document.getElementById("tbody");
    // by default, event listeners/handlers are in bubbling mode
    tbody.addEventListener("click", printEventDetails);
    // "true" as third parameter will set capturing mode.
    tbody.addEventListener("click", printEventDetails, true);
});
