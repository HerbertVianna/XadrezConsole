using XadrezConsole.tabuleiro;
namespace XadrezConsole.xadrez {
    class Rei :Peca {

        private PartidaDeXadrez partida;

        public Rei(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida):base(tabuleiro, cor) {
            this.partida = partida;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tabuleiro.linhas, tabuleiro.colunas];

            Posicao pos = new Posicao(0, 0);

            //norte
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            if (tabuleiro.posicaoValida(pos)&&podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //nordeste
            pos.definirValores(posicao.linha - 1, posicao.coluna+1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //leste
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //sudeste
            pos.definirValores(posicao.linha+1, posicao.coluna + 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //sul
            pos.definirValores(posicao.linha+1, posicao.coluna);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //so
            pos.definirValores(posicao.linha + 1, posicao.coluna-1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //oeste
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //no
            pos.definirValores(posicao.linha-1, posicao.coluna - 1);
            if (tabuleiro.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //#jogadaEspecial roque
            if(qtdMovimentos==0 && !partida.xeque) {
                //#jogadaEspecial roque pequeno
                Posicao posicaoTorre1 = new Posicao(posicao.linha, posicao.coluna + 3);
                if (testeTorreParaRoque(posicaoTorre1)) {
                    //o espaço é vago?
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);
                    if(!tabuleiro.existePeca(p1) && !tabuleiro.existePeca(p2)) {
                        mat[posicao.linha, posicao.coluna + 2]=true;
                    }
                }
                //#jogadaEspecial roque grande
                Posicao posicaoTorre2 = new Posicao(posicao.linha, posicao.coluna - 4);
                if (testeTorreParaRoque(posicaoTorre2)) {
                    //o espaço é vago?
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);
                    if (!tabuleiro.existePeca(p1) && !tabuleiro.existePeca(p2) && !tabuleiro.existePeca(p3)) {
                        mat[posicao.linha, posicao.coluna -2] = true;
                    }
                }
            }
            return mat;
        }

        private bool testeTorreParaRoque(Posicao pos) {
            Peca p = tabuleiro.obterPeca(pos);
            return p != null && p is Torre && p.cor == cor && p.qtdMovimentos == 0;
        }

        private bool podeMover(Posicao posicao) {
            Peca p = tabuleiro.obterPeca(posicao);
            return p == null || p.cor != this.cor;
        }

        public override string ToString() {
            return "R";
        }
    }
}
