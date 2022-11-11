from equation import *

# -----------INPUT DATA----------------

A = [
    [0, 1, 1, 1],
    [1, 0, 1, 1],
    [0, 0, 0, 1],
    [0, 0, 1, 0]
]

B = [[1],
     [0],
     [0],
     [0]]

C = [
    [0, 0, 0, 1]
]

# -----------INPUT DATA END------------


A = np.array(A)
B = np.array(B)
C = np.array(C)


def optimize_lsm(A, B, C):
    global n
    n = A.shape[0]
    K = [C]
    for i in range(n - 1):
        K.append(np.matmul(K[-1], A))
    K = np.array(K).reshape(-1, n) % q
    print("\nK = ")
    print(K)
    print("\nT = ")
    T = get_linear_independent_lines_of(K)
    print(T)
    E = []
    for i in range(2 ** n):
        x = np.array([[int(bool(i & 1 << k)) for k in range(n)]])
        m = T * x
        if np.array_equal(m % q, np.zeros((m.shape[0], m.shape[1]))):
            E.append(x)
    print("\nE = ")
    print(E)
    if len(E) == 1:
        print("E содержит только нулевой вектор -> ЛПМ оптимальна")
        return A, B, C
    else:
        print("E содержит не только нулевой вектор -> ЛПМ не оптимальна")

    indices = []
    Q = get_linear_independent_lines_of(np.transpose(T), indices).transpose()
    print("\nQ = ")
    print(Q)
    Q_I = np.linalg.inv(Q)
    print("\nQ_I = ")
    print(Q_I)
    R = np.zeros(tuple(reversed(T.shape)))
    for i in range(len(indices)):
        R[indices[i]] = Q_I[i]
    print("\nR = ")
    print(R)
    A_h = np.matmul(np.matmul(T, A), R) % q
    print("\nA_h = ")
    print(A_h)
    B_h = np.matmul(T, B) % q
    print("\nB_h = ")
    print(B_h)
    C_h = np.matmul(C, R) % q
    print("\nC_h = ")
    print(C_h)

    return A_h, B_h, C_h


A, B, C = optimize_lsm(A, B, C)
optimize_lsm(A, B, C)
