import numpy as np

class Polynomial:
    def __init__(self, coefficients, q=2, char=''):
        self.c = np.array([int(i) for i in coefficients])
        self.q = q
        self.char = char

    def __add__(self, other):
        if self.n() > other.n():
            res = self.c.copy()
            res[self.n() - other.n():] += other.C
        else:
            res = other.C.copy()
            res[other.n() - self.n():] += self.c
        res %= self.q
        # remove leading zeros
        while res[0] == 0 and len(res) > 1:
            res = res[1:]

        res_char = self.char
        if self.char != '' and other.char != '':
            res_char += '+'
        res_char += other.char
        return Polynomial(res, self.q, res_char)

    def n(self):
        return len(self.c)

    def __mul__(self, other):
        return Polynomial(np.convolve(self.c, other.C) % self.q, self.q, self.char + other.char)

    def __str__(self):
        res = ''
        for i in range(self.n()):
            if self.c[i] != 0:
                if res != '':
                    res += " + "
                if i == self.n() - 1:
                    res += f"{self.c[i]}"
                elif i == self.n() - 2:
                    if self.c[i] == 1:
                        res += f"x"
                    else:
                        res += f"{self.c[i]}x"
                else:
                    if self.c[i] == 1:
                        res += f"x^{self.n() - 1 - i}"
                    else:
                        res += f"{self.c[i]}x^{self.n() - 1 - i}"
        if self.char != '':
            return f"{self.char}(x) = " + res
        else:
            return res
        return res

    def __getitem__(self, item):
        if item >= self.n():
            return 0
        return self.c[self.n() - 1 - item]

    def __setitem__(self, key, value):
        self.c[self.n() - 1 - key] = value
        while self.c[0] == 0 and len(self.c) > 1:
            self.c = self.c[1:]