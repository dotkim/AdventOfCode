const massArray = require('./mass');

function getFuel(mass) {
  return Math.floor(mass/3) - 2;
}

let sum = 0;
massArray.forEach((m) => {
  sum += getFuel(m);
});

console.log(sum);