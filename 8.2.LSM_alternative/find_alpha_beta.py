from Polynopial import Polynomial

a4 = Polynomial("11", 2)
a1 = Polynomial("10", 2)
a = Polynomial("1", 2)
print("alpha:")
for k in range(16):
    print(f"{k} ~ {a}")
    a = a * a1
    if a[4]:
        a[4] = 0
        a = a + a4

b4 = Polynomial("1001", 2)
b1 = Polynomial("10", 2)
b = Polynomial("1", 2)
print("\n\nbeta:")
for k in range(16):
    print(f"{k} ~ {b}")
    b = b * b1
    if b[4]:
        b[4] = 0
        b = b + b4
