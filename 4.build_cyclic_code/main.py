# -----------INPUT DATA----------------
g = [1, 1, 1, 0, 1, 0, 0, 0, 1]
n = 15
# -----------INPUT DATA END------------


import sys

import numpy as np

sys.path.append('../2.build_kode_words')
from code_words_from_G import *

k = len(g)
r = n - k

G = []
for i in range(r):
    vec = [0]*n
    vec[i:i + 9] = g
    G.append(vec)

G = np.array(G)
print("порождающая матрица G = ")
print(G)
print("\nкодовые слова:")
code_words_from_G(G)
