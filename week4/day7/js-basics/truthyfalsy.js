'use strict';

let x = "1234";

// in JS, we try to avoid the "automatic type coercion"
// usually
// except in if-conditions

if (x) {
    console.log("x is truthy");
}

// truthy: converts to true as a boolean
// falsy: converts to false as a boolean

// all values are truthy except a few exceptions
/*
0 (and -0)
NaN
""
null
undefined
false
*/
// everything else, including empty array, empty object,
// any other object, any function, etc. is truthy
// even "false" is truthy.
