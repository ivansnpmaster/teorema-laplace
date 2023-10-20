using System;
					
public class Program
{
	public static void Main()
	{
		double[,] a = new double[,]
		{
			{1, 2, 0, 1},
			{-1, -3, 0, 1},
			{3, 6, 1, 3},
			{2, 4, 0, 3}
		};
		
		Matriz A = new Matriz(a);
		
		Console.WriteLine(A.Det());
	}
}

public class Matriz
{
	int linhas;
	int colunas;
	double[,] dados;
	
	public Matriz(double[,] m)
	{
		dados = m;
		linhas = dados.GetLength(0);
		colunas = dados.GetLength(1);
	}
	
	public Matriz(int l, int c)
	{
		linhas = l;
		colunas = c;
		dados = new double[linhas, colunas];
	}
	
	public double Det()
	{
		double determinante = 0;
		
		for (int i = 0; i < colunas; i++)
			determinante += dados[0, i] * Cofator(0, i);
		
		return determinante;
	}
	
	public double Cofator(int l, int c)
	{
		return Math.Pow(-1, l + c) * MenorComplementar(l, c);
	}
	
	public double MenorComplementar(int l, int c)
	{
		Matriz r = new Matriz(linhas - 1, colunas - 1);
		
		for (int i = 0; i < r.linhas; i++)
			for (int j = 0; j < r.colunas; j++)
			{
				int x = j < c ? j : j + 1;
				int y = i < l ? i : i + 1;
				r.dados[i, j] = dados[y, x];
			}
		
		if (r.linhas == 1)
			return r.dados[0, 0];
		
		return r.Det();
	}

	public Matriz MatrizCofatores()
	{
		var m = new Matriz(linhas, colunas);
		
		for (int i = 0; i < linhas; i++)
			for (int j = 0; j < colunas; j++)
				m.dados[i, j] = Cofator(i, j);
		
		return m;
	}
	
	public Matriz Inv()
	{
		var det = Det();
		var m = MatrizCofatores();
		var t = new Matriz(m.linhas, m.colunas);
		
		for (int i = 0; i < linhas; i++)
			for (int j = 0; j < colunas; j++)
				t.dados[i, j] = m.dados[j, i] / det;
		
		return t;
	}
}
