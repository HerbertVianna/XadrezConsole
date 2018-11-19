using XadrezConsole.tabuleiro;
using System.Collections.Generic;
using System;

namespace XadrezConsole.xadrez {
    class PartidaDeXadrez {

        public Tabuleiro tabuleiro { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        HashSet<Peca> pecas;
        HashSet<Peca> capturadas;
        public bool xeque { get; private set; }
        public Peca vulneravelEnPassant { get; private set; }

        public PartidaDeXadrez() {
            tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            terminada = false;
            xeque = false;
            vulneravelEnPassant = null;
            colocarPecas();
        }


        public void colocarNovaPeca(char coluna, int linha, Peca peca) {
            tabuleiro.adicionarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        public Peca executarMovimento(Posicao origem, Posicao destino) {
            Peca p = tabuleiro.removerPeca(origem);
            p.incrementarQtdMovimentos();
            Peca pCapturada = tabuleiro.removerPeca(destino);
            tabuleiro.adicionarPeca(p, destino);
            if (pCapturada != null) {
                capturadas.Add(pCapturada);
            }

            //#jogadaEspecial roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2) {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna + 1);
                Peca torre = tabuleiro.removerPeca(origemTorre);
                torre.incrementarQtdMovimentos();
                tabuleiro.adicionarPeca(torre, destinoTorre);
            }

            //#jogadaEspecial roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2) {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna - 1);
                Peca torre = tabuleiro.removerPeca(origemTorre);
                torre.incrementarQtdMovimentos();
                tabuleiro.adicionarPeca(torre, destinoTorre);
            }

            //#jogadaEspecial en passant
            if(p is Peao) {
                if(origem.coluna != destino.coluna && pCapturada == null) {
                    Posicao posPeao;
                    if(p.cor == Cor.Branca) {
                        posPeao = new Posicao(destino.linha + 1, destino.coluna);
                    } else {
                        posPeao = new Posicao(destino.linha - 1, destino.coluna);
                    }
                    pCapturada = tabuleiro.removerPeca(posPeao);
                    capturadas.Add(pCapturada);
                }
            }
            return pCapturada;
        }

        public HashSet<Peca> pecasCapturadas(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas) {
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

        private Cor adversaria(Cor cor) {
            if (cor == Cor.Branca) {
                return Cor.Preta;
            } else {
                return Cor.Branca;
            }
        }

        private Peca Rei(Cor cor) {
            foreach (Peca x in pecasEmJogo(cor)) {
                if (x is Rei) {
                    return x;
                }
            }
            return null;
        }
        public bool estaEmXeque(Cor cor) {
            Peca R = Rei(cor);
            if (R == null) {
                throw new TabuleiroException("Não tem rei da cor" + cor + " no tabuleiro!");
            }
            foreach (Peca x in pecasEmJogo(adversaria(cor))) {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna]) {
                    return true;
                }
            }
            return false;
        }

        public bool testeXequemate(Cor cor) {
            if (!estaEmXeque(cor)) {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor)) {
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < tabuleiro.linhas; i++) {
                    for (int j = 0; j < tabuleiro.colunas; j++) {
                        if (mat[i, j]) {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executarMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private void colocarPecas() {

            colocarNovaPeca('A', 1, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('B', 1, new Cavalo(tabuleiro, Cor.Branca));
            colocarNovaPeca('C', 1, new Bispo(tabuleiro, Cor.Branca));
            colocarNovaPeca('D', 1, new Dama(tabuleiro, Cor.Branca));
            colocarNovaPeca('E', 1, new Rei(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('F', 1, new Bispo(tabuleiro, Cor.Branca));
            colocarNovaPeca('G', 1, new Cavalo(tabuleiro, Cor.Branca));
            colocarNovaPeca('H', 1, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('A', 2, new Peao(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('B', 2, new Peao(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('C', 2, new Peao(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('D', 2, new Peao(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('E', 2, new Peao(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('F', 2, new Peao(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('G', 2, new Peao(tabuleiro, Cor.Branca, this));
            colocarNovaPeca('H', 2, new Peao(tabuleiro, Cor.Branca, this));

            colocarNovaPeca('A', 8, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('B', 8, new Cavalo(tabuleiro, Cor.Preta));
            colocarNovaPeca('C', 8, new Bispo(tabuleiro, Cor.Preta));
            colocarNovaPeca('D', 8, new Dama(tabuleiro, Cor.Preta));
            colocarNovaPeca('E', 8, new Rei(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('F', 8, new Bispo(tabuleiro, Cor.Preta));
            colocarNovaPeca('G', 8, new Cavalo(tabuleiro, Cor.Preta));
            colocarNovaPeca('H', 8, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('A', 7, new Peao(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('B', 7, new Peao(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('C', 7, new Peao(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('D', 7, new Peao(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('E', 7, new Peao(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('F', 7, new Peao(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('G', 7, new Peao(tabuleiro, Cor.Preta, this));
            colocarNovaPeca('H', 7, new Peao(tabuleiro, Cor.Preta, this));
        }

        public void realizarJogada(Posicao origem, Posicao destino) {
            Peca pecaCapturada = executarMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual)) {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca p = tabuleiro.obterPeca(destino);
            //#jogadaEspecial promocao
            if(p is Peao) {
                if (p.cor==Cor.Branca && destino.linha == 0 || p.cor == Cor.Preta && destino.linha == 7) {
                    p = tabuleiro.removerPeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Dama(tabuleiro, p.cor);
                    tabuleiro.adicionarPeca(dama, destino);
                    pecas.Add(dama);
                }
            }

            if (estaEmXeque(adversaria(jogadorAtual))) {
                xeque = true;
            } else {
                xeque = false;
            }
            if (testeXequemate(adversaria(jogadorAtual))) {
                terminada = true;
            } else {
                turno++;
                mudarJogador();
            }
            
            //#jogadaespecial en passant
            if(p is Peao && (destino.linha == origem.linha -2 || destino.linha == origem.linha + 2)) {
                vulneravelEnPassant = p;
            } else {
                vulneravelEnPassant = null;
            }
        }

        private void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) {
            Peca p = tabuleiro.removerPeca(destino);
            p.decrementarQtdMovimento();
            if (pecaCapturada != null) {
                tabuleiro.adicionarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tabuleiro.adicionarPeca(p, origem);

            //#jogadaEspecial roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2) {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna + 1);
                Peca torre = tabuleiro.removerPeca(destinoTorre);
                torre.decrementarQtdMovimento();
                tabuleiro.adicionarPeca(torre, origemTorre);
            }

            //#jogadaEspecial roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2) {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna - 1);
                Peca torre = tabuleiro.removerPeca(destinoTorre);
                torre.decrementarQtdMovimento();
                tabuleiro.adicionarPeca(torre, origemTorre);
            }

            //#jogadaEspecial en passant
            if(p is Peao) {
                if(origem.coluna != destino.coluna && pecaCapturada == vulneravelEnPassant) {
                    Peca peao = tabuleiro.removerPeca(destino);
                    Posicao posicaoPeao;
                    if(peao.cor == Cor.Branca) {
                        posicaoPeao = new Posicao(3, destino.coluna);
                    } else {
                        posicaoPeao = new Posicao(4, destino.coluna);
                    }
                    tabuleiro.adicionarPeca(peao, posicaoPeao);
                }
            }

        }

        private void mudarJogador() {
            if (jogadorAtual == Cor.Branca) {
                jogadorAtual = Cor.Preta;
            } else {
                jogadorAtual = Cor.Branca;
            }
        }

        public void validarPosicaoOrigem(Posicao posicao) {
            if (tabuleiro.obterPeca(posicao) == null) {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (jogadorAtual != tabuleiro.obterPeca(posicao).cor) {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!tabuleiro.obterPeca(posicao).existeMovimentosPossiveis()) {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDestino(Posicao origem, Posicao destino) {
            if (!tabuleiro.obterPeca(origem).movimentoPossivel(destino)) {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }
    }
}

