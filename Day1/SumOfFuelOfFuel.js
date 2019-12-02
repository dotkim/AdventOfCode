const massArray = require('./mass');

function getFuel(mass) {
  let sum = 0;
  for (let i = Math.floor(mass/3)-2; i >= 0; i = Math.floor(i/3)-2) {
    console.log(i);
    sum += i;
  }
  return sum;
}

let sum = 0;
massArray.forEach((m) => {
  sum += getFuel(m);
});

console.log(sum);