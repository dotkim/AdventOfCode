import functools

with open('06\\input') as file:
  groups = [x.split('\n') for x in file.read().split('\n\n')]

p1 = 0
p2 = 0
for group in groups:
  p1 += len(set(''.join(group)))
  p2 += len(functools.reduce(lambda l1, l2: l1.intersection(l2),
    [set(ans) for ans in group]))

print(p1)
print(p2)