using System;
using XadrezConsole.apresentacao;
using XadrezConsole.tabuleiro;
using XadrezConsole.xadrez;

namespace XadrezConsole {
    class Program {
        static void Main(string[] args) {

            try {
                Tabuleiro tabuleiro = new Tabuleiro(8, 8);
                tabuleiro.adicionarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(0, 0));
                tabuleiro.adicionarPeca(new Torre(tabuleiro, Cor.Branca), new Posicao(1, 3));
                tabuleiro.adicionarPeca(new Rei(tabuleiro, Cor.Preta), new Posicao(2, 4));

                Tela.imprimirTabuleiro(tabuleiro);
            }
            catch (TabuleiroException e) {
                Console.WriteLine(e.Message);
            }

            //PosicaoXadrez pos = new PosicaoXadrez('c', 7);
            //Console.WriteLine(pos);
            //Console.WriteLine(pos.toPosicao());
            Console.ReadLine();
        }
    }
}
