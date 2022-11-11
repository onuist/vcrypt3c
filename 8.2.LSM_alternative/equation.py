from Polynopial import *
q=2

def not_expressed_in_basis(vec, basis):
    n = len(basis)
    for i in range(2 ** n):
        res = 0
        for j in range(n):
            if i & (1 << j):
                res += basis[j]
        if np.array_equal(res % q, vec):
            return False
    return True


def get_linear_independent_lines_of(M, indices=None):
    res = []
    zero_line = np.zeros(M.shape[1])
    for i in range(M.shape[0]):
        if not np.array_equal(M[i], zero_line) and not_expressed_in_basis(M[i], res):
            res.append(M[i])
            if indices is not None:
                indices.append(i)
    return np.array(res)


def gauss(X):
    X = np.array(X)
    print(X)
    X_ind = get_linear_independent_lines_of(X)
    if X_ind.shape[0] != X.shape[0]:
        X = X_ind
        print("------\n" + str(X % 2))
    m = len(X)
    n = len(X[0])
    for i in range(m):
        print("------\n" + str(X % 2))
        no_zero = -1
        for j in range(i, m):
            if X[j][i] != 0:
                no_zero = j
                break

        if no_zero == -1:
            continue
        if no_zero > 0:
            tmp = X[i]
            X[i] = X[no_zero]
            X[no_zero] = tmp
            print("------\n" + str(X % 2))
        for j in range(i, m):
            if X[j][i] != 0:
                X[j] = X[j] / X[j][i] * X[i][i] - X[i]

    return X


def solve_XY_eq_0(A):
    res = []
    print("Solving with Gauss:")
    A = gauss(A)
    m = len(A)
    n = len(A[0])
    for i in range(2 ** n):
        x = np.array([[int(bool(i & 2 << k)) for k in range(n)]])
        m = A * x
        if np.array_equal(m % q, np.zeros((m.shape[0], m.shape[1]))):
            res.append(x)
    return res


if __name__ == "__main__":
    q = 2
    X = [[0, 0, 0, 1],
         [0, 0, 1, 0],
         [0, 0, 0, 1],
         [0, 0, 1, 0]]

    print(solve_XY_eq_0(X))
