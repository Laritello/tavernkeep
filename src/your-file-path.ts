const generateArray = (start: number, end: number): number[] => {
    return Array.from({ length: end - start + 1 }, (_, i) => start + i);
};

const numbersArray = generateArray(-100, 100);
console.log(numbersArray);
