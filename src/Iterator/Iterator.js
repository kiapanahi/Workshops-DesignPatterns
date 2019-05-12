function range(start, end) {
    return {
        [Symbol.iterator]() {
            console.log("iterator got called!");
            return this;
        },
        next() {
            console.log("next got called!");
            if (start < end) {
                return { value: start++, done: false };
            }
            return { value: end, done: true };
        }
    }
}



//usage:
for (var item of range(1,10)) {
    console.log(item);
}