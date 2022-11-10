import numpy as np


def bits_str(val: int, k: int):
    res = ""
    for i in range(k - 1, -1, -1):
        res += str(int(bool(val & (1 << i))))
    return res


def bit_array(val: int, k: int):
    res = []
    for i in range(k - 1, -1, -1):
        res.append(int(bool(val & (1 << i))))
    return np.array(res)


def code_words_from_G(G):
    k = G.shape[0]
    n = G.shape[1]
    min_weight = n
    for i in range(2 ** k):

        word = bit_array(i, k)
        code_word = np.matmul(word, G) % 2
        weight = np.sum(code_word)
        print(f"{word} -> c{i}={code_word} w(c{i})={weight}")
        if i != 0:
            min_weight = min(min_weight, weight)
    print(f"min_weight(весов Хемминга) = d = {min_weight}")
    t = int((min_weight - 1) / 2)
    print(f"Код исправляет t = (d-1)/2 = {t} ошибок")


if __name__ == "__main__":
    # -----------INPUT DATA----------------
    # only for q=2 (0,1)!!!!!!!!!!
    G = [[1, 0, 0, 1, 1, 0, 0],
         [0, 1, 0, 0, 1, 1, 0],
         [1, 1, 0, 0, 1, 0, 1]]
    # -----------INPUT DATA END------------

    code_words_from_G(np.array(G))
