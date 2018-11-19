using XadrezConsole.tabuleiro;
using System.Collections.Generic;

namespace XadrezConsole.xadrez {
    class PartidaDeXadrez {

        public Tabuleiro tabuleiro { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        HashSet<Peca> pecas;
        HashSet<Peca> capturadas;

        public PartidaDeXadrez() {
            tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            terminada = false;
            colocarPecas();
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca) {
            tabuleiro.adicionarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        public void executarMovimento(Posicao origem, Posicao destino) {
            Peca p = tabuleiro.removerPeca(origem);
            p.incrementarQtdMovimentos();
            Peca pCapturada = tabuleiro.removerPeca(destino);
            tabuleiro.adicionarPeca(p, destino);
            if(pCapturada != null) {
                capturadas.Add(pCapturada);
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas) {
                if (x.cor == cor) {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas) {
                if (x.cor == cor) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private void colocarPecas() {

            colocarNovaPeca('C', 1, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('C', 2, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('D', 2, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('E', 2, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('E', 1, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('D', 1, new Rei(tabuleiro, Cor.Branca));

            colocarNovaPeca('C', 7, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('C', 8, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('D', 7, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('E', 7, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('E', 8, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('D', 8, new Rei(tabuleiro, Cor.Preta));

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

