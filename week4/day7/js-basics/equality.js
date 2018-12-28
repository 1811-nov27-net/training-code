function compare(a, b) {
    console.log("a: " + a + ", b: " + b);
    // "double equals" and "triple equals"
    // double equals coerces the type of the values
    // to try to "compare value without caring about type"
    console.log("a == b: " + (a == b));
    console.log("a === b: " + (a === b));
    console.log();
}

compare(5, "5");
compare({}, "");
compare(null, "");
compare(null, undefined);

// always use triple equals aka. "strict equality"
// not "loose equality"
