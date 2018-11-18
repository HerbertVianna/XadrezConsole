using XadrezConsole.tabuleiro;

namespace XadrezConsole.xadrez {
    class PartidaDeXadrez {

        public Tabuleiro tabuleiro { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
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

        public void realizarJogada(Posicao origem, Posicao destino) {
            executarMovimento(origem, destino);
            turno++;
            mudarJogador();
        }

        private void mudarJogador() {
            if (jogadorAtual == Cor.Branca) {
                jogadorAtual = Cor.Preta;
            } else {
                jogadorAtual = Cor.Branca;
            }
        }

        public void validarPosicaoOrigem(Posicao posicao) {
            if (tabuleiro.obterPeca(posicao)==null) {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if(jogadorAtual!= tabuleiro.obterPeca(posicao).cor) {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!tabuleiro.obterPeca(posicao).existeMovimentosPossiveis()) {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDestino(Posicao origem, Posicao destino) {
            if (!tabuleiro.obterPeca(origem).podeMoverPara(destino)) {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }
    }
}

