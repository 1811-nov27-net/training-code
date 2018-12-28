'use strict';

function addNumbers(a, b, callback) {
    let result = a + b;

    return callback(result);
}

addNumbers(3, 4, console.log); // prints 7

// callbacks are important because we
// do a lot of listening to/waiting for events
// in JS, and also asynchronous stuff

// often use arrow functions
addNumbers(3, 4, result => console.log("calculation done"));

// returns a function
function newCounter() {
    let c = 0;
    return () => {
        c++;
        return c;
    };
}

newCounter = function() {
    let inc = 0;
    return function () {
        return ++inc;
    };
};

let counter = newCounter();
// normally at this point, "c" would disappear from the stack
// because it has passed out of scope

console.log(counter()); // prints 1
console.log(counter()); // prints 2
console.log(counter()); // prints 3

let counter2 = newCounter();

console.log(counter2()); // prints 1
console.log(counter2()); // prints 2

// in JavaScript, variables that are referenced by functions
// that are still in scope, themselves remain in scope

// in JavaScript, functions "close over" any variables they reference

// this behavior is called "closure"
// sometimes we call the functions themselves "closures"

// before ES6, we wanted "namespaces", we wanted to
// encapsulate private details and expose only
// needed functionality
// closure allows us to do this

// IIFE (immediately-invoked function expression)
let library = (function () {
    let privateData = 0;
    function privateFunction() {
        return privateData;
    }
    return {
        libraryMethod() {
            return privateFunction() + 1;
        },
        // ...
    };
})();
// this library object doesn't contain privateData or
// privateFunction, so they can't be modified/accessed,
// and don't pollute the global namespace,
// but, the methods inside can still use them.

// some encapsulation and abstraction for JS.
