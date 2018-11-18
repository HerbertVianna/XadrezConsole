using XadrezConsole.tabuleiro;

namespace XadrezConsole.xadrez {
    class PartidaDeXadrez {

        public Tabuleiro tabuleiro { get; private set; }
        private int turno;
        private Cor jogadorAtual;
        public bool terminada { get; private set; }

        public PartidaDeXadrez() {
            tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            colocarPecas();
            terminada = false;
        }

        public void executarMovimento(Posicao origem, Posicao destino) {
            Peca p = tabuleiro.removerPeca(origem);
            p.incrementarQtdMovimentos();
            Peca pCapturada = tabuleiro.removerPeca(destino);
            tabuleiro.adicionarPeca(p, destino);
            
        }

        private void colocarPecas() {
            tabuleiro.adicionarPeca(new Torre(tabuleiro, Cor.Branca), new PosicaoXadrez('C', 1).toPosicao());
            tabuleiro.adicionarPeca(new Torre(tabuleiro, Cor.Branca), new PosicaoXadrez('C', 2).toPosicao());
            tabuleiro.adicionarPeca(new Torre(tabuleiro, Cor.Branca), new PosicaoXadrez('D', 2).toPosicao());
            tabuleiro.adicionarPeca(new Torre(tabuleiro, Cor.Branca), new PosicaoXadrez('E', 2).toPosicao());
            tabuleiro.adicionarPeca(new Torre(tabuleiro, Cor.Branca), new PosicaoXadrez('E', 1).toPosicao());
            tabuleiro.adicionarPeca(new Rei(tabuleiro, Cor.Branca), new PosicaoXadrez('D', 1).toPosicao());

            tabuleiro.adicionarPeca(new Torre(tabuleiro, Cor.Preta), new PosicaoXadrez('C', 7).toPosicao());
            tabuleiro.adicionarPeca(new Torre(tabuleiro, Cor.Preta), new PosicaoXadrez('C', 8).toPosicao());
            tabuleiro.adicionarPeca(new Torre(tabuleiro, Cor.Preta), new PosicaoXadrez('D', 7).toPosicao());
            tabuleiro.adicionarPeca(new Torre(tabuleiro, Cor.Preta), new PosicaoXadrez('E', 7).toPosicao());
            tabuleiro.adicionarPeca(new Torre(tabuleiro, Cor.Preta), new PosicaoXadrez('E', 8).toPosicao());
            tabuleiro.adicionarPeca(new Rei(tabuleiro, Cor.Preta), new PosicaoXadrez('D', 8).toPosicao());

        }
    }
}

