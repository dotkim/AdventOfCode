function parse(code, inputs) {
  code[1] = inputs.noun;
  code[2] = inputs.verb;
  for (let i = 0; i < code.length; i+=4) {
    if (code[i] === 99) break;

    let pos1 = code[i+1];
    let pos2 = code[i+2];
    let pos3 = code[i+3];
    
    let output =  0;
    if (code[i] == 1) output = add(code[pos1], code[pos2]);
    else if (code[i] == 2) output = multiply(code[pos1], code[pos2]);
    
    code[pos3] = output;
  }
  return code[0];
}

function add(num1, num2) {
  return num1 + num2;
}

function multiply(num1, num2) {
  return num1 * num2;
}

let out = 0;

for (let x = 0; x <= 99; x++) {
  if (out == 19690720) break;
  for (let z = 0; z <= 99; z++) {
    if (out == 19690720) break;
    out = parse([1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,1,10,19,1,19,5,23,2,23,6,27,1,27,5,31,2,6,31,35,1,5,35,39,2,39,9,43,1,43,5,47,1,10,47,51,1,51,6,55,1,55,10,59,1,59,6,63,2,13,63,67,1,9,67,71,2,6,71,75,1,5,75,79,1,9,79,83,2,6,83,87,1,5,87,91,2,6,91,95,2,95,9,99,1,99,6,103,1,103,13,107,2,13,107,111,2,111,10,115,1,115,6,119,1,6,119,123,2,6,123,127,1,127,5,131,2,131,6,135,1,135,2,139,1,139,9,0,99,2,14,0,0], { noun: x, verb: z });
    console.log('Inputs:', JSON.stringify({ noun: x, verb: z }), 'Output:', out);
  }
}

console.log(out);