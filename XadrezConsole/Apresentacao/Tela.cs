using System;
using XadrezConsole.tabuleiro;

namespace XadrezConsole.apresentacao {
    class Tela {
        public static void imprimirTabuleiro(Tabuleiro tab) {
            for (int i = 0; i < tab.linhas; i++) {
                for (int j = 0; j < tab.colunas; j++) {
                    if (tab.obterPeca(i, j) == null) {
                        Console.Write("- ");
                    } else {
                        Console.Write(tab.obterPeca(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
