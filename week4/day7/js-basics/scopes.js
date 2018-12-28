'use strict';

// this is a "function statement", declaring a function
function my_func(a) {
    console.log(a);
}

// we also have "function expression"
var my_func2 = (function(a) {
    console.log(a);
});

// with ES6 we ALSO have "arrow functions" / lambda
var my_func3 = a => console.log(a);
var no_params = () => console.log();
var two_params = (a, b) => console.log(a);

// these all do the same thing except that arrow functions
// do have a subtle difference with how "this" works

// in ES5, there were only two scopes.
// (remember in C# you have block scope -
//    we can access that variable within the nearest {}
//    (and only after its declaration))

// in ES5, we have document scope aka global scope
// and function scope.

var x = 5;
x = 5;

function f() {
    console.log(x); // undefined
    if (true) {
        var x = 7;
    }
    asdf = 'asdf'; // in a function, without declaring
                  // -- that's global scope


    // in a function, "var" uses function scope.
    // this "x" is visible throughout my function,
    // even at the top, before it's declared.
    //  sometimes called "hoisting"
}

// (global scope undeclared)
// asdf = '1234';

// ES5 has "strict mode"
'use strict';
// strict mode turns certain silent errors into thrown errors

// asdasdfasf = '1234';

// with strict mode, undeclared variables throw errors

// ES6 added block scope to javascript
// using two new ways to declare variables -
// "let" and "const"

// so when you use let and const, variable only in scope
// within the nearest pair of braces. const prevents
// changing the value after first assignment.

// use let and const always, never var or undeclared.

let obj = {
    name: 'Nick',
    skill: 10000,
    sayName: function() { console.log(this.name) },

    sayName2() {
        console.log(this.name)
    },

    sayNameArrow: () => console.log(this.name)
};

// obj.sayName();

// in javascript, outside arrow functions, "this"
// is "unbound"/"free"



function Person(name, age) {
    this.name = name;
    this.age = age;
    this.sayName = obj.sayName;
    this.sayNicksName = obj.sayNameArrow;
}

let person = new Person("Fred", 78);

person.sayName(); // "this" is resolved here, not at declaration
person.sayNicksName(); // "this" was resolved at declaration, because it's an arrow function

console.log(person);

function Graduate(name, age, gradYear) {
    this.__proto__ = new Person(name, age);
    this.gradYear = gradYear;
    // could have new methods too
}

let nick = new Graduate("Nick", 26, 2014);
console.log(nick);

// when javascript does property access (or assignment)
// on an object, it first scans the object
// if nothing is found, it then looks at that
// object's __proto__, and on and on.
// in ES6, we have proper classes with inheritance...

class Person2 {
    constructor(name, age) {
        this.name = name;
        this.age = age;
        this.sayName = obj.sayName;
        this.sayNicksName = obj.sayNameArrow;
    }
}

class Graduate2 extends Person { // "extends" instead of ":"
    constructor(name, age, gradYear) {
        super(name, age); // "super" instead of "base"
        this.gradYear = gradYear;
        // could have new methods too
    }
}

let nick2 = new Person2("asdf", 23);

// JavaScript (ES5) IS object-oriented, but without classes
// ES6 is object-oriented with classes
// "OOP" is one paradigm of programming
// "procedural" is another - like C
// "functional" is another, where functions (behavior) are just another kind of data

// new features in ES6:
/*
- let, const
- arrow functions
- class, interface
- method syntax for functions as properties
- string interpolation
- symbol type for GUIDs
- new useful built-in functions e.g. string search
- Promises for async stuff without callbacks
- native modules (like namespaces)
- built-in Set and Map
- "for of" loop which is like foreach
- getters and setters for properties like C#
- internationalization features
*/

// caniuse.com to see current browser support for things

// string interpolation
console.log("person's name: " + nick.name);
console.log(`person's name: ${nick.name}`);
//console.log($"person's name: {nick.name}"); // C# way, similar but no
